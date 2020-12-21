using AmalgamateLabs.Models;
using System;

namespace AmalgamateLabs.Base
{
    // www.sluniverse.com/ffn/index.php/2013/04/using-extension-methods-in-a-asp-net-mvc-razor-view/
    public static class Extensions
    {
        public static string LongPrintedDate(this DateTime dateTime)
        {
            return dateTime.ToString("MMMM d, yyyy");
        }

        public static string ShortPrintedDate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-M-d");
        }

        public static string ImagePath(this byte[] imageArray)
        {
            string imageBase64 = Convert.ToBase64String(imageArray);
            return $"data:image/png;base64,{imageBase64}";
        }

        public static string UniquePageIdentifier(this Blog blog)
        {
            return $"{blog.PostedDate.ShortPrintedDate()}-BlogPost-{blog.BlogId}";
        }

        public static string CanonicalUrl(this Blog blog)
        {
            return $"https://amalgamatelabs.com/Blog/{blog.BlogId}/{blog.URLSafeTitle}";
        }

        public static string CanonicalUrl(this WebGLGame webGLGame)
        {
            return $"https://amalgamatelabs.com/WebGLGame/Details/{webGLGame.WebGLGameId}";
        }
    }
}
