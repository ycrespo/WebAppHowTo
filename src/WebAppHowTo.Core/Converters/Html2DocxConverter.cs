using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using OpenXmlPowerTools;

namespace WebAppHowTo.Core.Converters
{
    public static class Html2DocxConverter 
    {
        public static void Convert(string htmlFile, string outDir)
        {
            var s_ProduceAnnotatedHtml = true;

            var sourceHtmlFi = new FileInfo(htmlFile);

            var sourceImageDi = new DirectoryInfo(outDir);

            var destCssFi = new FileInfo(Path.Combine(outDir, sourceHtmlFi.Name.Replace(".html", "-2.css")));
            var destDocxFi = new FileInfo(Path.Combine(outDir, sourceHtmlFi.Name.Replace(".html", ".docx")));
            var annotatedHtmlFi = new FileInfo(Path.Combine(outDir, sourceHtmlFi.Name.Replace(".html", "-4-Annotated.txt")));

            var html = HtmlToWmlReadAsXElement.ReadAsXElement(sourceHtmlFi);

            var usedAuthorCss = HtmlToWmlConverter.CleanUpCss((string) html.Descendants().FirstOrDefault(d => d.Name.LocalName.ToLower() == "style"));
            File.WriteAllText(destCssFi.FullName, usedAuthorCss);

            var settings = HtmlToWmlConverter.GetDefaultSettings();
            // image references in HTML files contain the path to the subdir that contains the images, so base URI is the name of the directory
            // that contains the HTML files
            settings.BaseUriForImages = sourceHtmlFi.DirectoryName;

            var doc = HtmlToWmlConverter.ConvertHtmlToWml(defaultCss, usedAuthorCss, userCss, html, settings, null, s_ProduceAnnotatedHtml ? annotatedHtmlFi.FullName : null);
            doc.SaveAs(destDocxFi.FullName);
        }

        private class HtmlToWmlReadAsXElement
        {
            public static XElement ReadAsXElement(FileInfo sourceHtmlFi)
            {
                var htmlString = File.ReadAllText(sourceHtmlFi.FullName);
                XElement html = null;
                try
                {
                    html = XElement.Parse(htmlString);
                }
                catch (XmlException e)
                {
                    throw e;
                }
                // HtmlToWmlConverter expects the HTML elements to be in no namespace, so convert all elements to no namespace.
                html = (XElement) ConvertToNoNamespace(html);
                return html;
            }

            private static object ConvertToNoNamespace(XNode node)
            {
                if (node is XElement element)
                {
                    return new XElement(element.Name.LocalName,
                        element.Attributes().Where(a => !a.IsNamespaceDeclaration),
                        element.Nodes().Select(ConvertToNoNamespace));
                }

                return node;
            }
        }

        private const string defaultCss = @"html, address, blockquote, body, dd, div, dl, dt, fieldset, form, frame, frameset, h1, h2, h3, h4, h5, h6, noframes, ol, p, ul, center,
               dir, hr, menu, pre { display: block; unicode-bidi: embed } li { display: list-item } head { display: none } table { display: table }
               tr { display: table-row } thead { display: table-header-group } tbody { display: table-row-group } tfoot { display: table-footer-group }
               col { display: table-column } colgroup { display: table-column-group } td, th { display: table-cell } caption { display: table-caption }
               th { font-weight: bolder; text-align: center } caption { text-align: center } body { margin: auto; } h1 { font-size: 2em; margin: auto; }
               h2 { font-size: 1.5em; margin: auto; } h3 { font-size: 1.17em; margin: auto; } h4, p, blockquote, ul, fieldset, form, ol, dl, dir, 
               menu { margin: auto } a { color: blue; } h5 { font-size: .83em; margin: auto } h6 { font-size: .75em; margin: auto } h1, h2, h3, h4,
               h5, h6, b, strong { font-weight: bolder } blockquote { margin-left: 40px; margin-right: 40px } i, cite, em, var, address { font-style: italic }
               pre, tt, code, kbd, samp { font-family: monospace } pre { white-space: pre } button, textarea, input, select { display: inline-block }
               big { font-size: 1.17em } small, sub, sup { font-size: .83em } sub { vertical-align: sub } sup { vertical-align: super } table { border-spacing: 2px; }
               thead, tbody, tfoot { vertical-align: middle } td, th, tr { vertical-align: inherit } s, strike, del { text-decoration: line-through }
               hr { border: 1px inset } ol, ul, dir, menu, dd { margin-left: 40px } ol { list-style-type: decimal } ol ul, ul ol, ul ul, ol ol { margin-top: 0; margin-bottom: 0 }
               u, ins { text-decoration: underline } br:before { content: ""\A""; white-space: pre-line } center { text-align: center } :link, :visited { text-decoration: underline }
               :focus { outline: thin dotted invert } /* Begin bidirectionality settings (do not change) */ BDO[DIR=""ltr""] { direction: ltr; unicode-bidi: bidi-override }
               BDO[DIR=""rtl""] { direction: rtl; unicode-bidi: bidi-override } *[DIR=""ltr""] { direction: ltr; unicode-bidi: embed } *[DIR=""rtl""] { direction: rtl; unicode-bidi: embed }";

        private const string userCss = @"";
    }
}