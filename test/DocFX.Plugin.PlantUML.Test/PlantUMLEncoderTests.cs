using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using docfx_plantuml;
using System.Text;

namespace DocFX.Plugin.PlantUML.Test
{
    [TestClass]
    public class PlantUMLEncoderTests
    {
        [TestMethod]
        public void EncodePlantUML_UMLString_ReturnsValidEncodedString()
        {
            var plantUML = "Bob -> Alice : hello";
            var encodedUML = "SyfFKj2rKt3CoKnELR1Io4ZDoSa70000";

            Assert.AreEqual<string>(encodedUML, plantUML.ToString().EncodePlantUML());
        }

        [TestMethod]
        public void EncodePlantUML_UMLMultiLineString_ReturnsValidEncodedString()
        {
            var plantUML = new StringBuilder();
            plantUML.AppendLine("Bob -> Alice : hello");
            plantUML.AppendLine("Alice -> Bob : Hi");

            var encodedUML = "SyfFKj2rKt3CoKnELR1Io4ZDoSddWl20mav0MIi5Zqm0";

            Assert.AreEqual<string>(encodedUML, plantUML.ToString().EncodePlantUML());
        }

    }
}
