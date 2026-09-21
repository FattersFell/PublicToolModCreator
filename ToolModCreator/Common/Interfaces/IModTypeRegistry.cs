using System.Collections.Generic;
using Editor.ToolModCreator.Common.Types;

namespace Editor.ToolModCreator.Common.Interfaces
{
    public interface IModTypeRegistry
    {
        ModeType Detect(IEnumerable<string> fileNamesWithoutExtension);
        ICanBeCreated CreateCreator(ModeType type);
    }
}