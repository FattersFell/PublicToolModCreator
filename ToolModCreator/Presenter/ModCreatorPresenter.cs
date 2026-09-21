using System;
using Editor.ToolModCreator.Common.Interfaces;
using Editor.ToolModCreator.Common.Types;
using Editor.ToolModCreator.Models;

namespace Editor.ToolModCreator.Presenter
{
    /// <summary>Coordinates editor view actions with the mod creator model.</summary>
    public class ModCreatorPresenter : IDisposable
    {
        private readonly IModCreatorView _view;
        private readonly ModCreatorModel _model;
        private readonly IFolderPicker _folderPicker;
        private readonly IModScanner _scanner;
        private readonly IModExporter _exporter;

        public ModCreatorPresenter(
            IModCreatorView view,
            ModCreatorModel model,
            IFolderPicker folderPicker,
            IModScanner scanner,
            IModExporter exporter)
        {
            _view = view;
            _model = model;
            _folderPicker = folderPicker;
            _scanner = scanner;
            _exporter = exporter;

            _view.Bind(_model);

            Subscribe();
        }

        public void Dispose()
        {
            _view.ChooseUnityFolderRequested -= OnChooseUnityFolder;
            _view.ChooseTargetFolderRequested -= OnChooseTargetFolder;
            _view.FolderPrefixChanged -= OnFolderPrefixChanged;
            _view.NamePrefixChanged -= OnNamePrefixChanged;
            _view.ScanRequested -= OnScan;
            _view.ModEdited -= OnModEdited;
            _view.SaveToExportRequested -= OnSaveToExport;
            _view.RemoveFromExportRequested -= OnRemoveFromExport;
            _view.ClearExportRequested -= OnClearExport;
            _view.CreateFoldersRequested -= OnCreateFolders;
        }

        private void Subscribe()
        {
            _view.ChooseUnityFolderRequested += OnChooseUnityFolder;
            _view.ChooseTargetFolderRequested += OnChooseTargetFolder;
            _view.FolderPrefixChanged += OnFolderPrefixChanged;
            _view.NamePrefixChanged += OnNamePrefixChanged;
            _view.ScanRequested += OnScan;
            _view.ModEdited += OnModEdited;
            _view.SaveToExportRequested += OnSaveToExport;
            _view.RemoveFromExportRequested += OnRemoveFromExport;
            _view.ClearExportRequested += OnClearExport;
            _view.CreateFoldersRequested += OnCreateFolders;
        }

        private void OnChooseUnityFolder()
        {
            string path = _folderPicker.PickFolder();
            if (!string.IsNullOrEmpty(path))
            {
                _model.UnityFolderPath = path;
            }
        }

        private void OnChooseTargetFolder()
        {
            string path = _folderPicker.PickFolder();
            if (!string.IsNullOrEmpty(path))
            {
                _model.TargetFolderPath = path;
            }
        }

        private void OnFolderPrefixChanged(string value) => _model.FolderPrefix = value ?? "";
        private void OnNamePrefixChanged(string value) => _model.NamePrefix = value ?? "";

        private void OnScan()
        {
            try
            {
                var found = _scanner.Scan(_model.UnityFolderPath);
                _model.FoundMods.Clear();
                _model.FoundMods.AddRange(found);
            }
            catch (Exception e)
            {
                _view.ShowError(e.Message);
            }
        }

        private void OnModEdited(int index, string folderName, ModeType type)
        {
            if (index < 0 || index >= _model.FoundMods.Count)
            {
                return;
            }

            _model.FoundMods[index].FolderName = folderName;
            _model.FoundMods[index].Type = type;
        }

        private void OnSaveToExport()
        {
            _model.ExportMods.Clear();
            foreach (ModModel mod in _model.FoundMods)
            {
                _model.ExportMods.Add(mod.Clone());
            }

            _view.ShowInfo($"Saved {_model.ExportMods.Count} mods for export");
        }

        private void OnRemoveFromExport(int index)
        {
            if (index >= 0 && index < _model.ExportMods.Count)
            {
                _model.ExportMods.RemoveAt(index);
            }
        }

        private void OnClearExport() => _model.ExportMods.Clear();

        private void OnCreateFolders()
        {
            if (string.IsNullOrEmpty(_model.TargetFolderPath))
            {
                _view.ShowError("Target folder path is not set!");
                return;
            }

            if (_model.ExportMods.Count == 0 && _model.FoundMods.Count > 0)
            {
                CopyFoundModsToExport();
            }

            if (_model.ExportMods.Count == 0)
            {
                _view.ShowError("No mods selected for export!");
                return;
            }

            try
            {
                var options = new ExportOptionsModel
                {
                    TargetFolderPath = _model.TargetFolderPath,
                    FolderPrefix = _model.FolderPrefix,
                    NamePrefix = _model.NamePrefix
                };

                int created = _exporter.Export(_model.ExportMods, options);
                int skipped = _model.ExportMods.Count - created;

                _view.ShowInfo($"Created {created} mod folders" + (skipped > 0 ? $" ({skipped} skipped: Unknown type)" : ""));
            }
            catch (Exception e)
            {
                _view.ShowError("Export failed: " + e.Message);
            }
        }

        private void CopyFoundModsToExport()
        {
            _model.ExportMods.Clear();
            foreach (ModModel mod in _model.FoundMods)
            {
                _model.ExportMods.Add(mod.Clone());
            }
        }
    }
}