using System.Collections.Generic;
using Editor.ToolModCreator.Models;

namespace Editor.ToolModCreator.Common.Interfaces
{
    public interface IModScanner
    {
        List<ModModel> Scan(string unityFolderPath);
    }
}