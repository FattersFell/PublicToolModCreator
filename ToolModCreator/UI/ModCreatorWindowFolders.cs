using System;
using UnityEditor;
using UnityEngine;

namespace Editor.ToolModCreator.UI
{
    public partial class ModCreatorWindow
    {
        private void DrawFolderStep(string title, string path, Action onChoose)
        {
            GUILayout.Label(title, EditorStyles.largeLabel);
            GUILayout.Label("Now path: " + path, EditorStyles.wordWrappedLabel);

            if (GUILayout.Button("Choose folder"))
            {
                onChoose?.Invoke();
            }
        }
    }
}