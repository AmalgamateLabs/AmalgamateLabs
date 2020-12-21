using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmalgamateLabs.Models
{
    public class Blog
    {
        [Key]
        [Display(Name = "Blog ID")]
        public int BlogId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; URLSafeTitle = value.ToLower().Replace(' ', '_'); }
        }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Subtitle { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Keywords { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public byte[] Thumbnail { get; set; }

        [Required]
        [Display(Name = "Header picture")]
        [DataType(DataType.Upload)]
        public byte[] HeaderPicture { get; set; }

        [Required]
        [Display(Name = "Open graph picture")]
        [DataType(DataType.Upload)]
        public byte[] OpenGraphPicture { get; set; }

        [Display(Name = "Article pictures")]
        [DataType(DataType.Upload)]
        public byte[] ArticlePictures { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Posted date")]
        //TODO: Add validation that no other blog exists with this date.
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation
        // https://blogs.msdn.microsoft.com/mvpawardprogram/2017/01/03/asp-net-core-mvc/
        public DateTime PostedDate { get; set; }

        [Required]
        [DataType(DataType.Html)]
        //[Column(TypeName = "varchar(MAX)")] SQLite can't do this.
        [Column(TypeName = "varchar(21844)")] // Something to go off of: https://stackoverflow.com/questions/332798/equivalent-of-varcharmax-in-mysql
        public string Article { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "URL safe title")]
        public string URLSafeTitle { get; set; }
    }
}
