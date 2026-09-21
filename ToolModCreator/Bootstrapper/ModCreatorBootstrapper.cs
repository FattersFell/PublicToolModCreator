using Editor.ToolModCreator.Common.Interfaces;
using Editor.ToolModCreator.Core;
using Editor.ToolModCreator.Models;
using Editor.ToolModCreator.Presenter;
using Editor.ToolModCreator.UI;
using UnityEditor;

namespace Editor.ToolModCreator.Bootstrapper
{
    /// <summary>Creates and opens the mod creator editor window.</summary>
    public static class ModCreatorBootstrapper
    {
        [MenuItem("Tools/Mod Folder Creator")]
        public static void Open()
        {
            EditorWindow.GetWindow<ModCreatorWindow>("Mod Folder Creator");
        }

        public static ModCreatorPresenter Bootstrap(IModCreatorView view)
        {
            var registry = new ModTypeRegistry();

            return new ModCreatorPresenter(
                view,
                new ModCreatorModel(),
                new UnityFolderPicker(),
                new ModScanner(registry),
                new ModFolderExporter(registry)
                );
        }

    }
}