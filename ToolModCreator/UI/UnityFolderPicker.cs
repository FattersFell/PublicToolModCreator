using Editor.ToolModCreator.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Editor.ToolModCreator.UI
{
    /// <summary>Provides a Unity editor folder selection dialog.</summary>
    public class UnityFolderPicker : IFolderPicker
    {
        public string PickFolder()
        {
            string folderPath = EditorUtility.OpenFolderPanel("Choose folder", "Assets", "");

            if (!string.IsNullOrEmpty(folderPath) && folderPath.StartsWith(Application.dataPath))
            {
                folderPath = "Assets" + folderPath.Substring(Application.dataPath.Length);
            }

            return folderPath;
        }
    }
}