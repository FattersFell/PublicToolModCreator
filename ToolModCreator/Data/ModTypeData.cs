using System;

namespace Editor.ToolModCreator.Data
{
    /// <summary>Represents JSON data containing mod type variations.</summary>
    [Serializable]
    public class ModTypeData
    {
        public ModTypeVariation[] Variations;
    }
    
    /// <summary>Represents one configurable variation of a mod type.</summary>
    [Serializable]
    public class ModTypeVariation
    {
        public ModTypeField[] Fields;
    }
    
    /// <summary>Represents one key-value field in a mod variation.</summary>
    [Serializable]
    public class ModTypeField
    {
        public string Key;
        public string Value;
    }
}