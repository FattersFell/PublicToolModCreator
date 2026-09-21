using System.Collections.Generic;

namespace Editor.ToolModCreator.Models
{
    /// <summary>Stores the editor state for the mod creator workflow.</summary>
    public class ModCreatorModel
    {
        public string UnityFolderPath = "";
        public string TargetFolderPath = "";
        public string FolderPrefix = "";
        public string NamePrefix = "";
 
        public readonly List<ModModel> FoundMods = new List<ModModel>();
        public readonly List<ModModel> ExportMods = new List<ModModel>();
    }
}