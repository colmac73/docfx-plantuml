using Microsoft.DocAsCode.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using System.Composition;
using System.IO;

namespace docfx_plantuml
{
    [Export(typeof(IDocumentProcessor))]
    public class PlantUMLDocumentProcessor : IDocumentProcessor
    {
        public string Name => throw new NotImplementedException();

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

            throw new NotImplementedException();
        }

        public SaveResult Save(FileModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateHref(FileModel model, IDocumentBuildContext context)
        {
            throw new NotImplementedException();
        }
    }
}
