using UnityEditor;
using UnityEngine;

namespace Editor.ToolModCreator.UI
{
    public partial class ModCreatorWindow
    {
        private void DrawCreateStep()
        {
            GUILayout.Label("Step 6. Create mod folders", EditorStyles.largeLabel);

            if (GUILayout.Button("Create folders"))
            {
                CreateFoldersRequested?.Invoke();
            }
        }
    }
}