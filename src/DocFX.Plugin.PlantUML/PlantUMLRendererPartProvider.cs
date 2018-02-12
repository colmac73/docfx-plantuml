using Microsoft.DocAsCode.Dfm;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.IO.Compression;

namespace DocFX.Plugin.PlantUML
{
    [Export(typeof(IDfmCustomizedRendererPartProvider))]
    public class PlantUMLRendererPartProvider : IDfmCustomizedRendererPartProvider
    {        
        public IEnumerable<IDfmCustomizedRendererPart> CreateParts(IReadOnlyDictionary<string, object> parameters)
        {
            yield return new PlantUMLRendererPart();
        }
    }
}