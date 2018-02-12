using Microsoft.DocAsCode.Plugins;
using Microsoft.DocAsCode.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DocFX.Plugin.PlantUML
{
    [Export(typeof(IDocumentProcessor))]
    public class PlantUMLDocumentProcessor : IDocumentProcessor
    {
        public string Name => nameof(PlantUMLDocumentProcessor);

        public IEnumerable<IDocumentBuildStep> BuildSteps => throw new NotImplementedException();

        public ProcessingPriority GetProcessingPriority(FileAndType file)
        {
            var extensionList = new string[] { ".plantuml", ".pu", ".puml", ".iuml", ".wsd" };

            if (file.Type == DocumentType.Article && 
                extensionList.Contains(Path.GetExtension(file.File), StringComparer.OrdinalIgnoreCase))
            {
                return ProcessingPriority.Normal;
            }

            return ProcessingPriority.NotSupported;
        }

        public FileModel Load(FileAndType file, ImmutableDictionary<string, object> metadata)
        {
            var content = new Dictionary<string, object>
            {
                ["conceptual"] = File.ReadAllText(Path.Combine(file.BaseDir, file.File)),
                ["type"] = "Conceptual",
                ["path"] = file.File
            };

            return new FileModel(file, content);
        }

        public SaveResult Save(FileModel model)
        {
            return new SaveResult
            {
                DocumentType = "Conceptual",
                ResourceFile = model.File
            };
        }

        public void UpdateHref(FileModel model, IDocumentBuildContext context)
        {
            throw new NotImplementedException();
        }
    }
}
