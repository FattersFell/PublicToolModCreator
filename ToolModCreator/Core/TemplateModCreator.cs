using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Editor.ToolModCreator.Common.Interfaces;
using Editor.ToolModCreator.Data;
using UnityEditor;
using UnityEngine;

namespace Editor.ToolModCreator.Core
{
    /// <summary>Creates mod data files from external templates and JSON.</summary>
    public sealed class TemplateModCreator : ICanBeCreated
    {
        private const string AssetRoot = "Assets/Editor/ToolModCreator";
        private readonly string _typeName;

        public TemplateModCreator(string typeName)
        {
            _typeName = typeName;
        }

        public void CreateDataFile(string fileName)
        {
            TextAsset template = LoadTextAsset("Templates/" + _typeName + ".txt");
            TextAsset dataAsset = LoadTextAsset("Data/Parameters/" + _typeName + ".json");
            
            ModTypeData data = JsonUtility.FromJson<ModTypeData>(dataAsset.text);

            if (data == null || data.Variations == null || data.Variations.Length == 0)
            {
                throw new InvalidDataException("No variations configured for mod type " + _typeName + ".");
            }

            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["GUID"] = Guid.NewGuid().ToString("N"),
                ["Type"] = _typeName,
                ["ID"] = string.Empty
            };

            ModTypeVariation variation = data.Variations[0];
            if (variation.Fields != null)
            {
                foreach (ModTypeField field in variation.Fields)
                {
                    if (field != null && !string.IsNullOrEmpty(field.Key))
                    {
                        values[field.Key] = field.Value ?? string.Empty;
                    }
                }
            }

            string content = template.text;
            foreach (KeyValuePair<string, string> value in values)
            {
                content = content.Replace("{{" + value.Key + "}}", value.Value);
            }

            File.WriteAllText(fileName, content, new UTF8Encoding(false));
        }

        private static TextAsset LoadTextAsset(string relativePath)
        {
            string assetPath = AssetRoot + "/" + relativePath;
            TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath);
            
            if (asset == null)
            {
                throw new FileNotFoundException("Could not load tool mod asset.", assetPath);
            }

            return asset;
        }
    }
}