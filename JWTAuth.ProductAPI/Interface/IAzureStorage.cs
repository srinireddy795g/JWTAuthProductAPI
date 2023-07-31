using JWTAuth.ProductAPI.Models;

namespace JWTAuth.ProductAPI.Interface
{
    public interface IAzureStorage
    {
        /// <summary>
        /// Uploads a file submitted with request
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Blob with status</returns>
        Task<BlobResponseDto> UploadAsync(IFormFile file);

        /// <summary>
        /// Downloads a file with specified file name
        /// </summary>
        /// <param name="blobFileName"></param>
        /// <returns>Blob</returns>
        Task<BlobDto?> DownloadAsync(string blobFileName);

        /// <summary>
        // Delete a file with specified file name
        /// </summary>
        /// <param name="blobFileName"></param>
        /// <returns>Blob with status</returns>
        Task<BlobResponseDto> DeleteAsync(string blobFileName);

        /// <summary>
        /// Returns list of files
        /// </summary>
        /// <returns>blobs in a list</returns>
        Task<List<BlobDto>> GetAllAsync();
    }
}
