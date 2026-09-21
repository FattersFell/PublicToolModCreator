using System;
using Editor.ToolModCreator.Core;
using UnityEditor;
using UnityEngine;

namespace Editor.ToolModCreator.UI
{
    public partial class ModCreatorWindow
    {
        private void DrawPrefixStep(string title, string fieldLabel, string previewLabel, string current, Action<string> onChanged)
        {
            GUILayout.Label(title, EditorStyles.largeLabel);

            string edited = EditorGUILayout.TextField(fieldLabel, current);
            if (edited != current)
            {
                onChanged?.Invoke(edited);
            }

            GUILayout.Label(previewLabel + PrefixUtility.Apply(current, "Apple"), EditorStyles.miniLabel);
        }
    }
}