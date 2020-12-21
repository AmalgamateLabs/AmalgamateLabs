using System;
using System.ComponentModel.DataAnnotations;

namespace AmalgamateLabs.Models
{
    public class WebGLGame
    {
        [Key]
        [Display(Name = "WebGL game ID")]
        public int WebGLGameId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "URL safe title")]
        public string URLSafeTitle { get; set; } // Name this the same as the Unity build folder.

        [Required]
        [Display(Name = "Small image")]
        [DataType(DataType.Upload)]
        public byte[] SmallImage { get; set; }

        [Required]
        [Display(Name = "Large image")]
        [DataType(DataType.Upload)]
        public byte[] LargeImage { get; set; }

        [Required]
        [Display(Name = "Open graph picture")]
        [DataType(DataType.Upload)]
        public byte[] OpenGraphPicture { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public byte[] Screenshot { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Designer { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Engine { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Genre { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Posted date")]
        public DateTime PostedDate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Categories { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Concept { get; set; }

        public enum GameTypes { Full, Concepts, Activities }
        [Required]
        private int _gameType;
        public GameTypes GameType
        {
            get { return (GameTypes)Enum.ToObject(typeof(GameTypes), _gameType); }
            set { _gameType = (int)value; }
        }

        public enum Ratings { EarlyChildhood, Everyone, Teen, Mature, AdultsOnly }
        [Required]
        private int _rating;
        public Ratings Rating
        {
            get { return (Ratings)Enum.ToObject(typeof(Ratings), _rating); }
            set { _rating = (int)value; }
        }
    }
}
