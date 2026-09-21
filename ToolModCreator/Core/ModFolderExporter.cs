using System.Collections.Generic;
using System.IO;
using System.Text;
using Editor.ToolModCreator.Common.Interfaces;
using Editor.ToolModCreator.Models;

namespace Editor.ToolModCreator.Core
{
    /// <summary>Exports selected mods into generated mod folders.</summary>
    public class ModFolderExporter : IModExporter
    {
        private readonly IModTypeRegistry _registry;

        public ModFolderExporter(IModTypeRegistry registry)
        {
            _registry = registry;
        }

        public int Export(IEnumerable<ModModel> mods, ExportOptionsModel optionsModel)
        {
            int created = 0;

            foreach (ModModel mod in mods)
            {
                ICanBeCreated creator = _registry.CreateCreator(mod.Type);
                if (creator == null)
                {
                    continue;
                }

                string folderName = PrefixUtility.Apply(optionsModel.FolderPrefix, mod.FolderName);
                string folderPath = Path.Combine(optionsModel.TargetFolderPath, folderName);
                Directory.CreateDirectory(folderPath);

                creator.CreateDataFile(Path.Combine(folderPath, folderName + ".dat"));
                WriteEnglishFile(Path.Combine(folderPath, "English.dat"), PrefixUtility.Apply(optionsModel.NamePrefix, folderName));
                created++;
            }

            return created;
        }

        private static void WriteEnglishFile(string filePath, string displayName)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Name {0}", displayName);
                writer.WriteLine("Description");
            }
        }
    }
}