using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;
using System.IO;
using System.Threading.Tasks;

namespace DocFX.Plugin.PlantUML
{
    public class PlantUMLRendererPart : DfmCustomizedRendererPartBase<IMarkdownRenderer, MarkdownCodeBlockToken, MarkdownBlockContext>
    {
        static PlantUMLConverter _converter = new PlantUMLConverter("https://www.plantuml.com/");

        public override string Name => "PlantUMLRendererPart";

        public override bool Match(IMarkdownRenderer renderer, MarkdownCodeBlockToken token, MarkdownBlockContext context)
        {
            return token.Lang == "plantuml";
        }

        public override StringBuffer Render(IMarkdownRenderer renderer, MarkdownCodeBlockToken token, MarkdownBlockContext context)
        {
            string svgOutput = _converter.ConvertToString(token.Code, RenderFormat.SVG);

            StringBuffer buffer = string.Format("<div class=\"{0}\">{1}\n</div>", renderer.Options.LangPrefix, svgOutput);
                        
            return buffer;
        }
    }
}