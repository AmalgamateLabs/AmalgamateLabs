using AmalgamateLabs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AmalgamateLabs.Base
{
    public class Seed
    {
        private const string SEED_DATA_PATH = @"..\SeedData\";

        public static async Task<int> InitiateSeed(SQLiteDBContext context)
        {
            int recordsUpdated = 0;

            if (context.SystemConfigs == null || !context.SystemConfigs.Any())
            {
                recordsUpdated += await SeedSystemConfig(context);
                recordsUpdated += await SeedWebGLData(context);
                recordsUpdated += await SeedStoreAppData(context);
                recordsUpdated += await SeedBlogData(context);
            }

            return recordsUpdated;
        }

        private static async Task<int> SeedSystemConfig(DbContext context)
        {
            context.Add(new SystemConfig()
            {
                EmailAddress = "amalgamatelabs@gmail.com",
                CryptKey = null, // SECRET
                AuthKey = null, // SECRET
                HTTPStatusCodes = File.ReadAllText("Base/HTTPStatusCodes.xml"),
                SendGridAPIKey = "SECRET",
                SendGridEmailSendCount = 0,
                SendGridLastActiveDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),

                // Amazon Affiliate Data
                AmazonAssociateID = "SECRET",
                AWSAccessKey = "SECRET",
                EncryptedAWSSecretKey = "SECRET",
            });

            return await context.SaveChangesAsync();
        }

        private static async Task<int> SeedWebGLData(DbContext context)
        {
            context.Add(new WebGLGame()
            {
                Title = "Monty Hall",
                URLSafeTitle = "Monty_Hall",
                PostedDate = new DateTime(2016, 2, 14),
                SmallImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\MontyHall\SmallImage.jpg")),
                LargeImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\MontyHall\LargeImage.png")),
                OpenGraphPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\MontyHall\OpenGraphPicture.jpg")),
                Screenshot = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\MontyHall\Screenshot.jpg")),
                Designer = "Aaron Salisbury",
                Genre = "Simulation",
                Engine = "Unity",
                Categories = "Simulation, Probability, Single-Player",
                Description = "Suppose you're on a game show, and you're given the choice of three doors: Behind one door is a car; behind the others, goats. You pick a door, say number 1, and the host, who knows what's behind the doors, opens another door, say number 3, which has a goat. He then says to you, \"Do you want to pick door number 2?\" Is it to your advantage to switch your choice?",
                Concept = "Practical demonstration of Bayes' theorem and conditional probability. Based on a letter by Steve Selvin to the American Statistician in 1975.",
                GameType = WebGLGame.GameTypes.Activities,
                Rating = WebGLGame.Ratings.Everyone
            });

            context.Add(new WebGLGame()
            {
                Title = "Night of the Clicking Dead",
                URLSafeTitle = "Night_of_the_Clicking_Dead",
                PostedDate = new DateTime(2016, 3, 26),
                SmallImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\NightOfTheClickingDead\SmallImage.jpg")),
                LargeImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\NightOfTheClickingDead\LargeImage.png")),
                OpenGraphPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\NightOfTheClickingDead\OpenGraphPicture.jpg")),
                Screenshot = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\NightOfTheClickingDead\Screenshot.jpg")),
                Designer = "Aaron Salisbury",
                Genre = "Clicking",
                Engine = "Unity",
                Categories = "Clicking, Single-Player",
                Description = "Can you survive the Night of the Clicking Dead? Blast (click) your way to the highest zombie slayer level while gaining upgrades to increase XP and automatic fire.",
                Concept = "So a few of us at work got caught up in Canada Clicker and I was challenged to make a nonsensical and trite clicking game. I've found aspects of it interesting however, such as learning to maintain save states in WebGL and leveraging client-side JavaScript through Unity. Please, enjoy Night of the Clicking Dead!",
                GameType = WebGLGame.GameTypes.Activities,
                Rating = WebGLGame.Ratings.Everyone
            });

            context.Add(new WebGLGame()
            {
                Title = "Reverent Saga: A Winter's Tale",
                URLSafeTitle = "Reverent_Saga_-_A_Winters_Tale",
                PostedDate = new DateTime(2015, 8, 22),
                SmallImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\ReverentSaga-AWintersTale\SmallImage.jpg")),
                LargeImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\ReverentSaga-AWintersTale\LargeImage.png")),
                OpenGraphPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\ReverentSaga-AWintersTale\OpenGraphPicture.jpg")),
                Screenshot = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\ReverentSaga-AWintersTale\Screenshot.jpg")),
                Designer = "Aaron Salisbury",
                Genre = "Text Adventure",
                Engine = "Unity",
                Categories = "Text, Adventure, Single-Player",
                Description = "The first game in the Reverent Saga series sets the framework for the universe. Take on the role of an adventurer seeking to exceed their lot in life in pursuit of the ideals set forth in the Knight’s Code. In doing so you’ll face the rigors of travel across a dangerous land, battle monsters, use magic, solve puzzles, and more.",
                Concept = "A Winter’s Tale is inspired by first generation, 70’s era, text-based games and interactive fiction. In that spirit, the game is designed as if played using a monochrome monitor; all text and graphics being displayed in one color of phosphor.",
                GameType = WebGLGame.GameTypes.Full,
                Rating = WebGLGame.Ratings.Everyone
            });

            context.Add(new WebGLGame()
            {
                Title = "Reverent Saga: Escape from the Swamp",
                URLSafeTitle = "Reverent_Saga_-_Escape_from_the_Swamp",
                PostedDate = new DateTime(2016, 2, 21),
                SmallImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\ReverentSaga-EscapeFromTheSwamp\SmallImage.jpg")),
                LargeImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\ReverentSaga-EscapeFromTheSwamp\LargeImage.png")),
                OpenGraphPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\ReverentSaga-EscapeFromTheSwamp\OpenGraphPicture.jpg")),
                Screenshot = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"WebGLGames\ReverentSaga-EscapeFromTheSwamp\Screenshot.jpg")),
                Designer = "Aaron Salisbury",
                Genre = "Platformer",
                Engine = "Unity",
                Categories = "Platformer, Adventure, Single-Player",
                Description = "Escape from the swamp with as much health and time as possible. In the second game in the Reverent Saga series you're a princess that is going to save herself, but along your path you'll have to negotiate numerous hazards and obstacles.",
                Concept = "Escape from the Swamp is inspired by, and a humble tribute to, Pitfall! which was originally released on the Atari 2600. Developed by David Crane, Pitfall! was elegantly engineered and ahead of its time.",
                GameType = WebGLGame.GameTypes.Full,
                Rating = WebGLGame.Ratings.Everyone
            });

            return await context.SaveChangesAsync();
        }

        private static async Task<int> SeedStoreAppData(DbContext context)
        {
            context.Add(new StoreApp()
            {
                Title = "Sophron Budget",
                URLSafeTitle = "Sophron_Budget",
                Description = "Architecto numquam perspiciatis commodi laboriosam quod debitis placeat maxime quaerat soluta quia porro dicta sunt nemo voluptates!",
                OpenGraphPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"StoreApps\SophronBudget\OpenGraphPicture.png")),
                LogoImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"StoreApps\SophronBudget\LogoImage.png")),
                MainImage = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"StoreApps\SophronBudget\MainImage.png")),
                Feature1Image = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"StoreApps\SophronBudget\MainImage.png")),
                Feature2Image = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"StoreApps\SophronBudget\MainImage.png")),
                Feature3Image = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"StoreApps\SophronBudget\Feature3Image.png")),
                Feature1Title = "Feature 1 Title",
                Feature1Description = "Omnis, esse quo natus soluta minima facilis ratione dignissimos necessitatibus quod dolorem labore molestiae maxime veritatis laudantium aut odio ullam laboriosam autem!",
                Feature2Title = "Feature 2 Title",
                Feature2Description = "Omnis, esse quo natus soluta minima facilis ratione dignissimos necessitatibus quod dolorem labore molestiae maxime veritatis laudantium aut odio ullam laboriosam autem!",
                Feature3Title = "Export to Excel",
                Feature3Description = "Omnis, esse quo natus soluta minima facilis ratione dignissimos necessitatibus quod dolorem labore molestiae maxime veritatis laudantium aut odio ullam laboriosam autem!",
            });

            return await context.SaveChangesAsync();
        }

        private static async Task<int> SeedBlogData(DbContext context)
        {
            Blog webGLBlog = new Blog()
            {
                Author = "Aaron Salisbury",
                Title = "The Death of Unity Web Player",
                Subtitle = "Getting Started With WebGL",
                Keywords = "blog, developer, dev, Unity, Web Player, WebGL",
                Description = "We had ought to embrace, as Unity has, WebGL. They are putting the resources behind it, even bringing in engineers from Mozilla, and the last I looked, they have it running at only ~1.5 times slower than native code. All the major browsers are expected to support WebGL and now end users will not have to be bothered with having to download a plugin. That said, WebGL builds are now available with Unity 5, but as an early-access and with some limitations.",
                Thumbnail = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\TheDeathOfUnityWebPlayer\Thumbnail.jpg")),
                HeaderPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\TheDeathOfUnityWebPlayer\HeaderPicture.jpg")),
                OpenGraphPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\TheDeathOfUnityWebPlayer\OpenGraphPicture.jpg")),
                PostedDate = new DateTime(2015, 8, 23),
                Article = File.ReadAllText(Path.Combine(SEED_DATA_PATH, @"Blogs\TheDeathOfUnityWebPlayer\Article.txt")),
                ArticlePictures = Images.ConvertListOfImagesToArrayOfImages(new List<byte[]>()
                {
                    Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\TheDeathOfUnityWebPlayer\Article_1.jpg")),
                    Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\TheDeathOfUnityWebPlayer\Article_2.jpg"))
                })
            };

            int webGLImageCounter = 1;
            foreach (byte[] articlePicture in Images.ConvertArrayOfImagesToListOfImages(webGLBlog.ArticlePictures))
            {
                webGLBlog.Article = webGLBlog.Article.Replace($"ARTICLE_IMAGE_SOURCE_{webGLImageCounter++}", articlePicture.ImagePath());
            }

            context.Add(webGLBlog);

            context.Add(new Blog()
            {
                Author = "Aaron Salisbury",
                Title = "Unity and XML",
                Subtitle = "Start Parsing XML Data for Your Games",
                Keywords = "blog, developer, dev, Unity, XML, parse, parsing",
                Description = "Create XML elements by name to house game data respresenting objects of similar characteristics to support your games.",
                Thumbnail = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\UnityAndXML\Thumbnail.jpg")),
                HeaderPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\UnityAndXML\HeaderPicture.jpg")),
                OpenGraphPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\UnityAndXML\OpenGraphPicture.jpg")),
                PostedDate = new DateTime(2015, 8, 30),
                Article = File.ReadAllText(Path.Combine(SEED_DATA_PATH, @"Blogs\UnityAndXML\Article.txt"))
            });

            Blog pixelArtBlog = new Blog()
            {
                Author = "Aaron Salisbury",
                Title = "Easy Way to Draw Pixel Art in Illustrator",
                Subtitle = "Exploring the Rectangular Grid Tool",
                Keywords = "blog, developer, dev, Adobe, Illustrator, pixel, art, rectangular, grid, tool, live, paint, bucket",
                Description = "Exploring the Rectangular Grid Tool and Live Paint Bucket to easily and quickly create pixel art in Illustrator.",
                Thumbnail = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\EasyWayToDrawPixelArtInIllustrator\Thumbnail.jpg")),
                HeaderPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\EasyWayToDrawPixelArtInIllustrator\HeaderPicture.jpg")),
                OpenGraphPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\EasyWayToDrawPixelArtInIllustrator\OpenGraphPicture.jpg")),
                PostedDate = new DateTime(2015, 9, 6),
                Article = File.ReadAllText(Path.Combine(SEED_DATA_PATH, @"Blogs\EasyWayToDrawPixelArtInIllustrator\Article.txt")),
                ArticlePictures = Images.ConvertListOfImagesToArrayOfImages(new List<byte[]>()
                    {
                        Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\EasyWayToDrawPixelArtInIllustrator\Article_1.jpg")),
                        Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\EasyWayToDrawPixelArtInIllustrator\Article_2.jpg")),
                        Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\EasyWayToDrawPixelArtInIllustrator\Article_3.jpg")),
                        Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\EasyWayToDrawPixelArtInIllustrator\Article_4.jpg")),
                        Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\EasyWayToDrawPixelArtInIllustrator\Article_5.jpg"))
                    })
            };

            int pixelArtImageCounter = 1;
            foreach (byte[] articlePicture in Images.ConvertArrayOfImagesToListOfImages(pixelArtBlog.ArticlePictures))
            {
                pixelArtBlog.Article = pixelArtBlog.Article.Replace($"ARTICLE_IMAGE_SOURCE_{pixelArtImageCounter++}", articlePicture.ImagePath());
            }

            context.Add(pixelArtBlog);


            context.Add(new Blog()
            {
                Author = "Aaron Salisbury",
                Title = "Data Persistence",
                Subtitle = "Saving Games Online in Unity WebGL",
                Keywords = "blog, developer, dev, Unity, data, persistence, save game",
                Description = "Create individual player save data for Unity and store it client side to be reloaded later.",
                Thumbnail = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\DataPersistence\Thumbnail.jpg")),
                HeaderPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\DataPersistence\HeaderPicture.jpg")),
                OpenGraphPicture = Images.ConvertImageToBytes(Path.Combine(SEED_DATA_PATH, @"Blogs\DataPersistence\OpenGraphPicture.jpg")),
                PostedDate = new DateTime(2016, 4, 2),
                Article = File.ReadAllText(Path.Combine(SEED_DATA_PATH, @"Blogs\DataPersistence\Article.txt"))
            });

            return await context.SaveChangesAsync();
        }
    }
}
