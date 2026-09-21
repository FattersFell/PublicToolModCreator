using Editor.ToolModCreator.Common.Types;
using Editor.ToolModCreator.Models;
using UnityEditor;
using UnityEngine;

namespace Editor.ToolModCreator.UI
{
    public partial class ModCreatorWindow
    {
        private void DrawModsStep()
        {
            GUILayout.Label("Step 3. Type mods", EditorStyles.largeLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Take mods"))
            {
                ScanRequested?.Invoke();
            }

            if (_model.FoundMods.Count > 0 && GUILayout.Button("Save Changes for Export"))
            {
                SaveToExportRequested?.Invoke();
            }

            GUILayout.EndHorizontal();

            if (_model.FoundMods.Count > 0)
            {
                DrawFoundMods();
            }

            if (_model.ExportMods.Count > 0)
            {
                DrawExportList();
            }
        }

        private void DrawFoundMods()
        {
            GUILayout.Space(10);
            GUILayout.Label($"Found mods ({_model.FoundMods.Count}):", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Folder Name", EditorStyles.boldLabel, GUILayout.ExpandWidth(true));
            GUILayout.Label("Type", EditorStyles.boldLabel, GUILayout.Width(120));
            GUILayout.EndHorizontal();

            _foundScroll = GUILayout.BeginScrollView(_foundScroll, GUILayout.Height(150));

            for (int i = 0; i < _model.FoundMods.Count; i++)
            {
                ModModel mod = _model.FoundMods[i];

                GUILayout.BeginHorizontal();
                string newName = GUILayout.TextField(mod.FolderName, GUILayout.ExpandWidth(true));
                ModeType newType = (ModeType)EditorGUILayout.EnumPopup(mod.Type, GUILayout.Width(120));
                GUILayout.EndHorizontal();

                if (newName != mod.FolderName || newType != mod.Type)
                {
                    ModEdited?.Invoke(i, newName, newType);
                }
            }

            GUILayout.EndScrollView();
        }

        private void DrawExportList()
        {
            GUILayout.Space(20);
            GUILayout.Label($"Export List ({_model.ExportMods.Count} mods):", EditorStyles.largeLabel);

            int indexToRemove = -1;

            _exportScroll = GUILayout.BeginScrollView(_exportScroll, GUILayout.Height(100));

            for (int i = 0; i < _model.ExportMods.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"{i + 1}.", GUILayout.Width(25));
                GUILayout.Label(_model.ExportMods[i].FolderName, GUILayout.ExpandWidth(true));
                GUILayout.Label(_model.ExportMods[i].Type.ToString(), GUILayout.Width(120));

                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    indexToRemove = i;
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            if (indexToRemove >= 0)
            {
                RemoveFromExportRequested?.Invoke(indexToRemove);
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear Export List"))
            {
                ClearExportRequested?.Invoke();
            }

            GUILayout.FlexibleSpace();
            GUILayout.Label($"Ready for export: {_model.ExportMods.Count} mods", EditorStyles.miniLabel);
            GUILayout.EndHorizontal();
        }
    }
}