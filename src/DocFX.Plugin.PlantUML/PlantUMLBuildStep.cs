using Microsoft.DocAsCode.Plugins;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;
using System.Collections.Immutable;
using System.IO;

namespace DocFX.Plugin.PlantUML
{
    [Export(nameof(PlantUMLDocumentProcessor), typeof(IDocumentBuildStep))]
    public class PlantUMLBuildStep : IDocumentBuildStep
    {
        private readonly TaskFactory _taskFactory = new TaskFactory(new StaTaskScheduler(1));

        private PlantUMLConverter _converter = new PlantUMLConverter();

        public string Name => nameof(PlantUMLBuildStep);

        public int BuildOrder => 0;

        public void Build(FileModel model, IHostService host)
        {
            string content = (string)((Dictionary<string, object>)model.Content)["conceptual"];
            content = _taskFactory.StartNew(() =>
                    _converter.ConvertToString(content, RenderFormat.PNG)
                ).Result;
            ((Dictionary<string, object>)model.Content)["conceptual"] = content;
        }

        public void Postbuild(ImmutableList<FileModel> models, IHostService host)
        {
            // NOP
        }

        public IEnumerable<FileModel> Prebuild(ImmutableList<FileModel> models, IHostService host)
        {
            return models;
        }
    }
}
