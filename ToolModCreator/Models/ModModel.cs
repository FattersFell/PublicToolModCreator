using Editor.ToolModCreator.Common.Types;

namespace Editor.ToolModCreator.Models
{
    /// <summary>Represents a mod folder and its selected type.</summary>
    public class ModModel
    {
        public string FolderName;
        public ModeType Type;
 
        public ModModel(string folderName, ModeType type)
        {
            FolderName = folderName;
            Type = type;
        }
 
        public ModModel Clone() => new ModModel(FolderName, Type);
    }

}