using System.ComponentModel.DataAnnotations;

namespace AmalgamateLabs.Models
{
    public class StoreApp
    {
        [Key]
        [Display(Name = "Store app ID")]
        public int StoreAppId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "URL safe title")]
        public string URLSafeTitle { get; set; }

        [Required]
        [Display(Name = "Logo image")]
        [DataType(DataType.Upload)]
        public byte[] LogoImage { get; set; }

        [Required]
        [Display(Name = "Main image")]
        [DataType(DataType.Upload)]
        public byte[] MainImage { get; set; }

        [Required]
        [Display(Name = "Feature 1 image")]
        [DataType(DataType.Upload)]
        public byte[] Feature1Image { get; set; }

        [Required]
        [Display(Name = "Feature 2 image")]
        [DataType(DataType.Upload)]
        public byte[] Feature2Image { get; set; }

        [Required]
        [Display(Name = "Feature 3 image")]
        [DataType(DataType.Upload)]
        public byte[] Feature3Image { get; set; }

        [Required]
        [Display(Name = "Open graph picture")]
        [DataType(DataType.Upload)]
        public byte[] OpenGraphPicture { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Feature 1")]
        public string FeatureOne { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Feature 2")]
        public string FeatureTwo { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Feature 3")]
        public string FeatureThree { get; set; }
    }
}
