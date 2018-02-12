using Microsoft.VisualStudio.TestTools.UnitTesting;
using Svg;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace DocFX.Plugin.PlantUML.Test
{
    [TestClass]
    public class PlantUMLConverterTests
    {
        static PlantUMLConverter _converter = new PlantUMLConverter("https://www.plantuml.com/");

        [TestMethod]
        public void ConvertToImage_PlantUML_ReturnPNG()
        {
            var plantUML = "Bob -> Alice : hello";

            Task<Stream> response = _converter.ConvertToImage(plantUML, RenderFormat.PNG);

            var pngImg = Image.FromStream(response.Result);

            Assert.AreEqual<ImageFormat>(ImageFormat.Png, pngImg.RawFormat);
        }

        [TestMethod]
        public void ConvertToImage_PlantUML_ReturnSVG()
        {
            var plantUML = "Bob -> Alice : hello";

            Task<Stream> response = _converter.ConvertToImage(plantUML, RenderFormat.SVG);

            var svgImg = SvgDocument.Open<SvgDocument>(response.Result);

            Assert.IsNotNull(svgImg);
        }

        [TestMethod]
        public void ConvertToImage_PlantUML_ReturnASCIIArt()
        {
            var plantUML = "Bob -> Alice : hello";

            Task<Stream> response = _converter.ConvertToImage(plantUML, RenderFormat.TXT);

            using (var reader = new StreamReader(response.Result))
            {
                var output = reader.ReadToEnd();
                Assert.IsTrue(output.Length > 0);
            }
        }

        [TestMethod]
        public void ConvertToImage_PlantUML_ReturnImageMap()
        {
            var plantUML = "participant Bob [[http://www.plantuml.com]]\nBob->Alice : [[http://plantuml.sourceforge.net]] hello";

            Task<Stream> response = _converter.ConvertToImage(plantUML, RenderFormat.MAP);

            using (var reader = new StreamReader(response.Result))
            {
                var output = reader.ReadToEnd();
                Assert.IsTrue(output.StartsWith("<map"));
            }
        }
    }
}
