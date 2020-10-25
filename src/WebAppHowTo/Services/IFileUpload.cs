using System.Threading.Tasks;
using BlazorInputFile;

namespace WebAppHowTo.Services
{
    public interface IFileUpload
    {
        Task UploadAsync(IFileListEntry file);
    }
}