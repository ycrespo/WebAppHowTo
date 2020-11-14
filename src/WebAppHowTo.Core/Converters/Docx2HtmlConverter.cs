using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;

namespace WebAppHowTo.Core.Converters
{
    public static class Docx2HtmlConverter 
    {
        public static string Convert(FileInfo fileInfo, string outputDir)
        {
            var byteArray = File.ReadAllBytes(fileInfo.FullName);

            using var memoryStream = new MemoryStream();
            memoryStream.Write(byteArray, offset: 0, byteArray.Length);
            using var wordDoc = WordprocessingDocument.Open(memoryStream, isEditable: true);
            var htmlFileName = new FileInfo(fileInfo.Name.Replace(".docx", ".html"));
            if (!string.IsNullOrEmpty(outputDir))
            {
                var dirInfo = new DirectoryInfo(outputDir);
                if (!dirInfo.Exists)
                {
                    throw new Exception("Output directory does not exist");
                }

                htmlFileName = new FileInfo(Path.Combine(dirInfo.FullName, htmlFileName.Name));
            }

            var imageDir = htmlFileName.FullName.Substring(startIndex: 0, htmlFileName.FullName.Length - 5) + "_images";
            var imageCounter = 0;

            var pageTitle = fileInfo.FullName;
            var part = wordDoc.CoreFilePropertiesPart;
            if (part != null)
            {
                pageTitle = (string) part.GetXDocument().Descendants(DC.title).FirstOrDefault() ?? fileInfo.FullName;
            }

            var settings = new HtmlConverterSettings
            {
                AdditionalCss = "body { margin: 1cm auto; max-width: 20cm; padding: 0; }",
                PageTitle = pageTitle,
                FabricateCssClasses = true,
                CssClassPrefix = "pt-",
                RestrictToSupportedLanguages = false,
                RestrictToSupportedNumberingFormats = false,
                ImageHandler = imageInfo =>
                {
                    var localDirInfo = new DirectoryInfo(imageDir);
                    if (!localDirInfo.Exists)
                    {
                        localDirInfo.Create();
                    }

                    ++imageCounter;
                    var extension = imageInfo.ContentType.Split(separator: '/')[1].ToLower();
                    ImageFormat imageFormat = null;
                    switch (extension)
                    {
                        case "png":
                            imageFormat = ImageFormat.Png;
                            break;
                        case "gif":
                            imageFormat = ImageFormat.Gif;
                            break;
                        case "bmp":
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case "jpeg":
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        case "tiff":
                            // Convert tiff to gif.
                            extension = "gif";
                            imageFormat = ImageFormat.Gif;
                            break;
                        case "x-wmf":
                            extension = "wmf";
                            imageFormat = ImageFormat.Wmf;
                            break;
                    }

                    // If the image format isn't one that we expect, ignore it,
                    // and don't return markup for the link.
                    if (imageFormat == null)
                    {
                        return null;
                    }

                    var imageFileName = imageDir + "/image" +
                                        imageCounter + "." + extension;
                    try
                    {
                        imageInfo.Bitmap.Save(imageFileName, imageFormat);
                    }
                    catch (ExternalException)
                    {
                        return null;
                    }

                    var imageSource = localDirInfo.Name + "/image" +
                                      imageCounter + "." + extension;

                    var img = new XElement(Xhtml.img,
                        new XAttribute(NoNamespace.src, imageSource),
                        imageInfo.ImgStyleAttribute,
                        imageInfo.AltText != null ? new XAttribute(NoNamespace.alt, imageInfo.AltText) : null);
                    return img;
                }
            };
            var htmlElement = HtmlConverter.ConvertToHtml(wordDoc, settings);

            // Produce HTML document with <!DOCTYPE html > declaration to tell the browser
            // we are using HTML5.
            var html = new XDocument(
                new XDocumentType("html", publicId: null, systemId: null, internalSubset: null),
                htmlElement);

            var htmlString = html.ToString(SaveOptions.DisableFormatting);
            File.WriteAllText(htmlFileName.FullName, htmlString, Encoding.UTF8);
            
            return htmlFileName.FullName;
        }
    }
}