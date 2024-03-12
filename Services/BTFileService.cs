using TroubleTrails.Services.Interfaces;

namespace TroubleTrails.Services
{
    // helper service no constructor needed
    public class BTFileService : IBTFileService
    {
        private readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };


        public string ConvertByteArrayToFile(byte[] fileData, string extension)
        {
            try
            {
                string imageBase64Data = Convert.ToBase64String(fileData);  // bring the file in
                return string.Format($"data:{extension};base64, {imageBase64Data}"); // convert it to a string using $ string interpolation
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
        {
            try
            {
                MemoryStream memoryStream = new();  //instantiate MemoryStream
                await file.CopyToAsync(memoryStream); // copy file to memorystream  
                byte[] byteFile = memoryStream.ToArray();  //convert to array
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string FormatFileSize(long bytes)
        {
            throw new NotImplementedException();
        }

        public string GetFileIcon(string file)
        {
            throw new NotImplementedException();
        }
    }
}
