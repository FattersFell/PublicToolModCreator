using System;
using Editor.ToolModCreator.Common.Types;
using Editor.ToolModCreator.Models;

namespace Editor.ToolModCreator.Common.Interfaces
{
    public interface IModCreatorView
    {
        event Action ChooseUnityFolderRequested;
        event Action ChooseTargetFolderRequested;
        event Action<string> FolderPrefixChanged;
        event Action<string> NamePrefixChanged;
        event Action ScanRequested;
        event Action<int, string, ModeType> ModEdited;
        event Action SaveToExportRequested;
        event Action<int> RemoveFromExportRequested;
        event Action ClearExportRequested;
        event Action CreateFoldersRequested;
        
        void Bind(ModCreatorModel model);

        void ShowInfo(string message);
        void ShowError(string message);
    }
}