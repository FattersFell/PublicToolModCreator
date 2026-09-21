using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Editor.ToolModCreator.Common.Interfaces;
using Editor.ToolModCreator.Models;

namespace Editor.ToolModCreator.Core
{
    /// <summary>Scans folders and detects their mod types.</summary>
    public class ModScanner : IModScanner
    {
        private readonly IModTypeRegistry _registry;

        public ModScanner(IModTypeRegistry registry)
        {
            _registry = registry;
        }

        public List<ModModel> Scan(string unityFolderPath)
        {
            if (string.IsNullOrEmpty(unityFolderPath) || !Directory.Exists(unityFolderPath))
            {
                throw new DirectoryNotFoundException("Unity folder path is not set or does not exist!");
            }

            var result = new List<ModModel>();

            foreach (string directory in Directory.GetDirectories(unityFolderPath))
            {
                List<string> fileNames = Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
                    .Select(f => Path.GetFileNameWithoutExtension(f))
                    .ToList();

                result.Add(new ModModel(Path.GetFileName(directory), _registry.Detect(fileNames)));
            }

            return result;
        }
    }
}