using System;
using System.Collections.Generic;
using System.Linq;
using Editor.ToolModCreator.Common.Interfaces;
using Editor.ToolModCreator.Common.Types;

namespace Editor.ToolModCreator.Core
{
    /// <summary>Maps detected file markers to mod type creators.</summary>
    public class ModTypeRegistry : IModTypeRegistry
    {
        private const string ItemMarker = "Item";
        private const string BarricadeTypeName = "Barricade";
        private const string PantsTypeName = "Pants";
        private const string ShirtTypeName = "Shirt";
        private const string VestTypeName = "Vest";
        private const string HatTypeName = "Hat";
        private const string WaterTypeName = "Water";
        private const string FoodTypeName = "Food";
        private const string MedicalTypeName = "Medical";
        private const string CloudTypeName = "Cloud";
        private const string BackpackTypeName = "Backpack";
        private const string MaskTypeName = "Mask";
        private const string SupplyTypeName = "Supply";
        private const string LargeTypeName = "Large";
        private const string MediumTypeName = "Medium";
        private const string SmallTypeName = "Small";
        private const string WaterMarker = "Use";
        private const string LargeMarker = "Skybox";
        private const string MediumMarker = "Object";

        private static readonly Dictionary<ModeType, (string Marker, bool NeedsItem, ICanBeCreated Creator)> Types = new Dictionary<ModeType, (string, bool, ICanBeCreated)>
        {
            { ModeType.Barricade, (BarricadeTypeName, true, new TemplateModCreator(BarricadeTypeName)) },
            { ModeType.Pants, (PantsTypeName, true, new TemplateModCreator(PantsTypeName)) },
            { ModeType.Shirt, (ShirtTypeName, true, new TemplateModCreator(ShirtTypeName)) },
            { ModeType.Vest, (VestTypeName, true, new TemplateModCreator(VestTypeName)) },
            { ModeType.Hat, (HatTypeName, true, new TemplateModCreator(HatTypeName)) },
            { ModeType.Water, (WaterMarker, true, new TemplateModCreator(WaterTypeName)) },
            { ModeType.Food, (FoodTypeName, true, new TemplateModCreator(FoodTypeName)) },
            { ModeType.Medical, (MedicalTypeName, true, new TemplateModCreator(MedicalTypeName)) },
            { ModeType.Cloud, (CloudTypeName, true, new TemplateModCreator(CloudTypeName)) },
            { ModeType.Backpack, (BackpackTypeName, true, new TemplateModCreator(BackpackTypeName)) },
            { ModeType.Mask, (MaskTypeName, true, new TemplateModCreator(MaskTypeName)) },
            { ModeType.Supply, (null, true, new TemplateModCreator(SupplyTypeName)) },
            { ModeType.Large, (LargeMarker, false, new TemplateModCreator(LargeTypeName)) },
            { ModeType.Medium, (MediumMarker, false, new TemplateModCreator(MediumTypeName)) },
            { ModeType.Small, (SmallTypeName, false, new TemplateModCreator(SmallTypeName)) },
        };

        public ModeType Detect(IEnumerable<string> fileNames)
        {
            bool Has(string name) => fileNames.Contains(name, StringComparer.OrdinalIgnoreCase);
            bool hasItem = Has(ItemMarker);

            foreach (var pair in Types)
            {
                var entry = pair.Value;

                if (entry.Marker == null)
                {
                    continue;
                }

                if (entry.NeedsItem == hasItem && Has(entry.Marker))
                {
                    return pair.Key;
                }
            }

            return hasItem ? ModeType.Supply : ModeType.Unknown;
        }

        public ICanBeCreated CreateCreator(ModeType type)
        {
            return Types.TryGetValue(type, out var entry) ? entry.Creator : null;
        }
    }
}