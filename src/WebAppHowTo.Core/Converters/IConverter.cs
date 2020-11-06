using System.IO;

namespace WebAppHowTo.Core.Converters
{
    public interface IConverter
    {
        string Convert(FileInfo fileInfo, string destinationFolder);
    }
}