using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmalgamateLabs.Models
{
    public class Movie
    {
        [Key]
        [Display(Name = "Movie ID")]
        public int MovieId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Genre { get; set; }

        [Required]
        //[DataType(DataType.Url)]
        [Display(Name = "Detail page URL")]
        public string DetailPageURL { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Large image URL")]
        public string LargeImageURL { get; set; }

        [Required]
        [Display(Name = "Large image")]
        [DataType(DataType.Upload)]
        public byte[] LargeImage { get; set; }

        [NotMapped]
        [Editable(false)]
        [Display(Name = "Image source")]
        public string ImageSource { get { return $"data:image/png;base64,{Convert.ToBase64String(LargeImage)}"; } }

        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = 7)]
        [DataType(DataType.Text)]
        [Display(Name = "Image primary hex color")]
        public string ImagePrimaryHexColor { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        public static async Task<List<Movie>> GetMoviesFromXML(XDocument amazonItemXML)
        {
            List<Movie> movies = new List<Movie>();
            XNamespace ns = amazonItemXML.Root.FirstAttribute.Value;
            DateTime timestamp = DateTime.Now;

            foreach (XElement item in amazonItemXML.Root.Element(ns + "Items").Elements(ns + "Item")
                .Where(e => "Movie".Equals(e.Element(ns + "ItemAttributes").Element(ns + "ProductGroup").Value)))
            {
                Movie newMovie = new Movie()
                {
                    Title = item.Element(ns + "ItemAttributes").Element(ns + "Title").Value,
                    Genre = (item.Element(ns + "ItemAttributes").Element(ns + "Genre").Value).Replace('_', ' '),
                    DetailPageURL = item.Element(ns + "DetailPageURL").Value,
                    LargeImageURL = item.Element(ns + "LargeImage").Element(ns + "URL").Value,
                    Timestamp = timestamp
                };

                WebRequest webRequest = WebRequest.Create(newMovie.LargeImageURL);
                WebResponse webResponse = await webRequest.GetResponseAsync();
                using (Stream file = webResponse.GetResponseStream())
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    newMovie.LargeImage = memoryStream.ToArray();
                }

                newMovie.SetImagePrimaryHexColor();

                movies.Add(newMovie);
            }

            return movies;
        }

        private void SetImagePrimaryHexColor()
        {
            // Pseudocode in case ASP.NET Core decides to support System.Drawing
            //if (LargeImage != null)
            //{
            //    using (MemoryStream memoryStream = new MemoryStream(LargeImage))
            //    {
            //        Bitmap bitmap = new Bitmap(memoryStream);
            //        List<Color> colors = new List<Color>(bitmap.Size.Width * bitmap.Size.Height);

            //        for (int x = 0; x < bitmap.Size.Width; x++)
            //        {
            //            for (int y = 0; y < bitmap.Size.Height; y++)
            //            {
            //                try
            //                {
            //                    colors.Add(bitmap.GetPixel(x, y));
            //                }
            //                catch { }
            //            }
            //        }

            //        Color modeColor = colors.GroupBy(c => c).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();

            //        ImagePrimaryHexColor = ColorTranslator.ToHtml(modeColor);
            //    }
            //}

            if (LargeImage != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(LargeImage))
                using (Image<Rgba32> image = Image.Load<Rgba32>(memoryStream))
                {
                    List<Rgba32> pixels = new List<Rgba32>(2 * image.Height);

                    for (int y = 0; y < image.Height; y++)
                    {
                        // Gathering from the left & right edges.
                        pixels.Add(image[0, y]);
                        pixels.Add(image[image.Width, y]);
                    }

                    Rgba32 modePixel = pixels
                        .GroupBy(p => p)
                        .OrderByDescending(grp => grp.Count())
                        .Select(grp => grp.Key)
                        .First();

                    ImagePrimaryHexColor = $"#{modePixel.ToHex()}";
                }
            }
        }
    }
}
