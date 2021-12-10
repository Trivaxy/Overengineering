using System;
using System.IO;

namespace Overengineering.Resources.Loaders
{
	/// <summary>
	/// Loader that is designed to load important content types in FNA.
	/// </summary>
	public sealed class FNALoader<T> : ILoader
	{
		private string fileQuery;

		public string FileQuery => fileQuery;

		public FNALoader(string fileQuery)
		{
			this.fileQuery = fileQuery;
		}

		public void Load(string path)
		{
			string fileName = Path.ChangeExtension(path.Substring(AssetServer.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar).Replace('\\', '/'), null);
			T asset = LoadAsset(fileName);

			// this check is needed in order to dispose assets that are being replaced, to avoid a memory leak
			if (Assets<T>.Has(fileName) && Assets<T>.Get(fileName) is IDisposable disposable)
			{
				Assets<T>.Register(fileName, asset);
				disposable.Dispose();
			}
			else
				Assets<T>.Register(fileName, asset);
		}

		private T LoadAsset(string contentPath) => AssetServer.ContentManager.Load<T>(contentPath);
	}
}