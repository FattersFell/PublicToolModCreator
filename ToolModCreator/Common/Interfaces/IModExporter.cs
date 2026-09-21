using System.Collections.Generic;
using Editor.ToolModCreator.Models;

namespace Editor.ToolModCreator.Common.Interfaces
{
    public interface IModExporter
    {
        int Export(IEnumerable<ModModel> mods, ExportOptionsModel optionsModel);
    }
}