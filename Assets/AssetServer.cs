using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Assets.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Overengineering.Assets
{
	/// <summary>
	/// Manages all registered loaders and provides realtime asset-replacing.
	/// </summary>
	public static class AssetServer
	{
		private static IDictionary<string, List<ILoader>> loaderCollections = new Dictionary<string, List<ILoader>>();
		private static FileSystemWatcher watcher;

		/// <summary>
		/// The root directory of the internal FNA ContentManager.
		/// </summary>
		public static string RootDirectory => ContentManager.RootDirectory;

		internal static ContentManager ContentManager { get; set; }

		internal static Dictionary<string, object> ContentManagerCache { get; set; }

		internal static IList<IDisposable> ContentManagerDisposeCache { get; set; }

		internal static GraphicsDevice GraphicsDevice { get; set; }

		/// <summary>
		/// Registers a loader.
		/// </summary>
		/// <param name="loader">The loader to register.</param>
		public static void RegisterLoader(ILoader loader)
		{
			if (!loaderCollections.ContainsKey(loader.FileQuery))
				loaderCollections[loader.FileQuery] = new List<ILoader>();

			loaderCollections[loader.FileQuery].Add(loader);
		}

		internal static void Start(ContentManager content)
		{
			ContentManager = content;
			RegisterInitialLoaders();
			LoadAssetsInitially();

			// FNA caches things that are loaded with the ContentManager
			// Reloads mandate that we uncache them with reflection, so we obtain references to the internal caches
			ContentManagerCache = typeof(ContentManager)
				.GetField("loadedAssets", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ContentManager)
				as Dictionary<string, object>;

			ContentManagerDisposeCache = typeof(ContentManager)
				.GetField("disposableAssets", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ContentManager)
				as List<IDisposable>;

			watcher = new FileSystemWatcher(ContentManager.RootDirectory);
			watcher.IncludeSubdirectories = true;

			watcher.Created += OnFileAffected;
			watcher.Changed += OnFileAffected;
			watcher.Renamed += OnFileAffected;

			watcher.EnableRaisingEvents = true;
		}

		private static void OnFileAffected(object sender, FileSystemEventArgs e)
		{
			string assetKey = e.FullPath.Substring(RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
			if (ContentManagerCache.TryGetValue(assetKey, out object value))
			{
				ContentManagerCache.Remove(assetKey);

				if (value is IDisposable disposable)
					ContentManagerDisposeCache.Remove(disposable); // We need to dispose it ourselves since FNA won't
			}

			ReloadFile(e.FullPath);
		}

		private static void RegisterInitialLoaders()
		{
			RegisterLoader(new FNALoader<Texture2D>("Textures/*.png"));
			RegisterLoader(new FNALoader<Model>("Models/*.xnb"));
		}

		private static void LoadAssetsInitially()
		{
			foreach (KeyValuePair<string, List<ILoader>> loaderCollection in loaderCollections)
				foreach (string file in Directory.EnumerateFiles(ContentManager.RootDirectory, loaderCollection.Key, SearchOption.AllDirectories))
					foreach (ILoader loader in loaderCollection.Value)
						loader.Load(file);
		}

		private static void ReloadFile(string path)
		{
			foreach (string fileQuery in loaderCollections.Keys)
			{
				if (FileMatchesPattern(path, fileQuery))
				{
					foreach (ILoader loader in loaderCollections[fileQuery])
						loader.Load(path);
				}
			}
		}

		private static bool FileMatchesPattern(string file, string pattern)
		{
			return new Regex(
				"^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$",
				RegexOptions.IgnoreCase | RegexOptions.Singleline
			).IsMatch(file);
		}
	}
}