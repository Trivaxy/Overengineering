using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Overengineering.Loaders
{
#nullable disable
    public class AssetLoader<T>
    {
        public AssetLoader(string Path, string[] FileFormats)
        {
            this.AssetPath = Path;
            this.FileFormats = FileFormats;
        }

        public string AssetPath { get; private set; }

        public string[] FileFormats { get; private set; }

        internal readonly static List<string> AssetPaths = new List<string>();

        internal static Dictionary<string, T> Assets = new Dictionary<string, T>();

        public void AddAssetsFromDirectories(string DirectoryPath)
        {
            string[] filePaths = Directory.GetFiles(DirectoryPath);
            for (int i = 0; i < filePaths.Length; i++)
            {
                string filePath = filePaths[i];

                bool hasFormat = false;
                foreach(string format in FileFormats)
                {
                    if (filePath.Contains(format)) hasFormat = true;
                }

                if (!hasFormat) continue;

                string charSeprator = @$"{AssetPath}\";
                int Index = filePath.IndexOf(charSeprator) + charSeprator.Length;
                string AlteredPath = filePath.Substring(Index);

                AssetPaths.Add(Path.GetFileNameWithoutExtension(AlteredPath));
            }
        }

        public void GetAllAssetPaths(string DirectoryPath)
        {
            AddAssetsFromDirectories(DirectoryPath);

            string[] remainingDirecotries = Directory.GetDirectories(DirectoryPath);

            for (int i = 0; i < remainingDirecotries.Length; i++)
            {
                var DirectorySubPath = remainingDirecotries[i];

                Debug.Write("Loading Assetes From: [" + DirectorySubPath + "]\n");
                GetAllAssetPaths(DirectorySubPath);
                Debug.Write("\n\n");
            }
        }

        public void LoadTexturesToAssetCache(ContentManager content)
        {
            foreach (string Asset in AssetPaths)
            {
                string LoadName = Asset.Replace(@"\", "/");
                Assets.Add(Asset, content.Load<T>(LoadName));
            }
        }

        public void Load() => GetAllAssetPaths(AssetPath);       
    }
}
