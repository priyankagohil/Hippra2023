using Hippra.Data;
using Hippra.Models.AzureStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Hippra.Code
{
    public class ImageHelper
    {
        private AzureStorage Storage;
        public ImageHelper(AzureStorage _Storage)
        {
            Storage = _Storage;
        }

        //https://damienbod.com/2018/05/13/uploading-and-sending-image-messages-with-asp-net-core-signalr/
        //https://github.com/Azure-Samples/storage-blob-upload-from-webapp/blob/master/ImageResizeWebApp/ImageResizeWebApp/Helpers/StorageHelper.cs

        public bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<string> UploadImageToStorage(Stream fileStream, string fileName)
        {
            fileName = "img" + fileName;// + Guid.NewGuid().ToString();
            await Storage.SetBlobFile(fileName, fileStream);
            return fileName;
        }
        public async Task<string> DeleteImageToStorage(string fileName)
        {
            return await Storage.DeleteBlob(fileName);
        }
        public static async Task<string> ProcessFormFile(IFormFile formFile,
            ModelStateDictionary modelState)
        {
            var fieldDisplayName = string.Empty;

            // Use reflection to obtain the display name for the model 
            // property associated with this IFormFile. If a display
            // name isn't found, error messages simply won't show
            // a display name.
            MemberInfo property =
                typeof(ImageMessage).GetProperty(
                    formFile.Name.Substring(formFile.Name.IndexOf(".") + 1));

            if (property != null)
            {
                var displayAttribute =
                    property.GetCustomAttribute(typeof(DisplayAttribute))
                        as DisplayAttribute;

                if (displayAttribute != null)
                {
                    fieldDisplayName = $"{displayAttribute.Name} ";
                }
            }

            // Use Path.GetFileName to obtain the file name, which will
            // strip any path information passed as part of the
            // FileName property. HtmlEncode the result in case it must 
            // be returned in an error message.
            var fileName = WebUtility.HtmlEncode(
                Path.GetFileName(formFile.FileName));

            if (!((formFile.ContentType.ToLower() != "image/png") ||
                (formFile.ContentType.ToLower() != "image/jpeg") ||
                (formFile.ContentType.ToLower() != "image/gif")))
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fieldDisplayName}file ({fileName}) must be a text file.");
            }

            // Check the file length and don't bother attempting to
            // read it if the file contains no content. This check
            // doesn't catch files that only have a BOM as their
            // content, so a content length check is made later after 
            // reading the file's content to catch a file that only
            // contains a BOM.
            if (formFile.Length == 0)
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fieldDisplayName}file ({fileName}) is empty.");
            }
            else if (formFile.Length > 1048576)
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fieldDisplayName}file ({fileName}) exceeds 1 MB.");
            }
            else
            {
                try
                {
                    return "";
                    //string fileContents;

                    //// The StreamReader is created to read files that are UTF-8 encoded. 
                    //// If uploads require some other encoding, provide the encoding in the 
                    //// using statement. To change to 32-bit encoding, change 
                    //// new UTF8Encoding(...) to new UTF32Encoding().
                    //using (
                    //    var reader =
                    //        new StreamReader(
                    //            formFile.OpenReadStream(),
                    //            new UTF8Encoding(encoderShouldEmitUTF8Identifier: false,
                    //                throwOnInvalidBytes: true),
                    //            detectEncodingFromByteOrderMarks: true))
                    //{
                    //    fileContents = await reader.ReadToEndAsync();

                    //    // Check the content length in case the file's only
                    //    // content was a BOM and the content is actually
                    //    // empty after removing the BOM.
                    //    if (fileContents.Length > 0)
                    //    {
                    //        return fileContents;
                    //    }
                    //    else
                    //    {
                    //        modelState.AddModelError(formFile.Name,
                    //            $"The {fieldDisplayName}file ({fileName}) is empty.");
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    modelState.AddModelError(formFile.Name,
                        $"The {fieldDisplayName}file ({fileName}) upload failed. " +
                        $"Please contact the Help Desk for support. Error: {ex.Message}");
                    // Log the exception
                }
            }

            return null;
        }
    }
}
