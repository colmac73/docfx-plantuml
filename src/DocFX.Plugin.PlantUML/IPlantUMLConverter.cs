using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocFX.Plugin.PlantUML
{
    interface IPlantUMLConverter
    {
        Task<Stream> ConvertToImage(string plantUML, RenderFormat type);
    }
}
