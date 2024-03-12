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
                memoryStream.Close();
                memoryStream.Dispose(); // clean up

                return byteFile;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string FormatFileSize(long bytes)
        {
            int counter = 0;
            decimal fileSize = bytes;
            while (Math.Round(fileSize / 1024) >= 1)
            {
                fileSize /= bytes;
                counter++;
            }
            return string.Format("{0:n1}{1}", fileSize, suffixes[counter]);  //{0:n1} equal one decimal place
        }

        public string GetFileIcon(string file)
        {
           string fileImage = "default";
            if (!string.IsNullOrWhiteSpace(file))  //if the file is not null
            {
                fileImage = Path.GetExtension(file).Replace(".", "");// gets us the three letter extension such as PNG etc..
                return $"/img/png/{fileImage}.png";
            }
            return fileImage;
        }
    }
}
