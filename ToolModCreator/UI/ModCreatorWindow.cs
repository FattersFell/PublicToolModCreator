using System;
using Editor.ToolModCreator.Bootstrapper;
using Editor.ToolModCreator.Common.Interfaces;
using Editor.ToolModCreator.Common.Types;
using Editor.ToolModCreator.Models;
using Editor.ToolModCreator.Presenter;
using UnityEditor;
using UnityEngine;

namespace Editor.ToolModCreator.UI
{
    /// <summary>Displays the mod creator workflow in a Unity editor window.</summary>
    public partial class ModCreatorWindow : EditorWindow, IModCreatorView
    {
        public event Action ChooseUnityFolderRequested;
        public event Action ChooseTargetFolderRequested;
        public event Action<string> FolderPrefixChanged;
        public event Action<string> NamePrefixChanged;
        public event Action ScanRequested;
        public event Action<int, string, ModeType> ModEdited;
        public event Action SaveToExportRequested;
        public event Action<int> RemoveFromExportRequested;
        public event Action ClearExportRequested;
        public event Action CreateFoldersRequested;

        private ModCreatorModel _model;
        private ModCreatorPresenter _presenter;
        private Vector2 _foundScroll;
        private Vector2 _exportScroll;

        private void OnEnable()
        {
            _presenter = ModCreatorBootstrapper.Bootstrap(this);
        }

        private void OnDisable()
        {
            _presenter?.Dispose();
            _presenter = null;
        }

        public void Bind(ModCreatorModel model) => _model = model;
        public void ShowInfo(string message) => Debug.Log(message);
        public void ShowError(string message) => Debug.LogError(message);

        private void OnGUI()
        {
            if (_model == null)
            {
                return;
            }

            DrawFolderStep("Step 1. Choose Unity folder", _model.UnityFolderPath, ChooseUnityFolderRequested);
            DrawFolderStep("Step 2. Choose Target folder", _model.TargetFolderPath, ChooseTargetFolderRequested);
            DrawModsStep();
            DrawPrefixStep("Step 4. Folder prefix (optional)", "Prefix", "Preview: ", _model.FolderPrefix, FolderPrefixChanged);
            DrawPrefixStep("Step 5. Name prefix for English.dat (optional)", "Name Prefix", "Preview Name: ", _model.NamePrefix, NamePrefixChanged);
            DrawCreateStep();
        }



    }
}