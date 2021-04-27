using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using Amazon.Lambda.Core;

namespace PlantListing.Extensions
{
    public static class FormFileExtensions
    {
        public static bool IsValidImage(this IFormFile file, int minBytes = 512, int maxBytes = 3145728)
        {
            ////-------------------------------------------
            ////  Check the image mime types
            ////-------------------------------------------
            //if (file.ContentType.ToLower() != "image/jpg" 
            //    && file.ContentType.ToLower() != "image/jpeg"
            //    && file.ContentType.ToLower() != "image/png")
            //{
            //    return false;
            //}

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(file.FileName).ToLower() != ".jpg"
                && Path.GetExtension(file.FileName).ToLower() != ".png"
                && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!file.OpenReadStream().CanRead)
                {
                    LambdaLogger.Log("!file.OpenReadStream().CanRead");
                    return false;
                }
                //------------------------------------------
                //check whether the image size exceeding the limit or not
                //------------------------------------------ 
                if (file.Length < minBytes)
                {
                    LambdaLogger.Log("file.Length < minBytes");
                    return false;
                }

                if(file.Length > maxBytes)
                {
                    LambdaLogger.Log("file.Length > maxBytes");
                    return false;
                }

                byte[] buffer = new byte[minBytes];
                file.OpenReadStream().Read(buffer, 0, minBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    LambdaLogger.Log("Regex.IsMatch");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("OpenReadStream exception");
                LambdaLogger.Log(ex.ToString());
                return false;
            }
            finally
            {
                file.OpenReadStream().Position = 0;
            }

            //-------------------------------------------
            //  Try to instantiate new image, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------
            //try
            //{
            //    using (var imageReadStream = new MemoryStream())
            //    {
            //        file.CopyTo(imageReadStream);
            //        using (var possibleImage = Image.FromStream(imageReadStream))
            //        {
            //        }
            //
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LambdaLogger.Log("imageReadStream exception");
            //    LambdaLogger.Log(ex.ToString());
            //    return false;
            //}
            //finally
            //{
            //    file.OpenReadStream().Position = 0;
            //}
        }
    }
}
