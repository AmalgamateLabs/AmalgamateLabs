using AmalgamateLabs.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmalgamateLabs.Models
{
    public class SystemConfig
    {
        private const byte SENDGRID_MAX_EMAIL_COUNT = 100;

        [Key]
        [Display(Name = "System configuration ID")]
        public int SystemConfigId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Cryptographic key")]
        public byte[] CryptKey { get; set; }

        [Required]
        [Display(Name = "Authentication key")]
        public byte[] AuthKey { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "HTTP status codes")]
        public string HTTPStatusCodes { get; set; }

        #region SendGrid
        [Required]
        [Display(Name = "SendGrid API key")]
        public string SendGridAPIKey { get; set; }

        [Required]
        [Display(Name = "SendGrid e-mail send count")]
        public int SendGridEmailSendCount { get; set; }

        [Display(Name = "SendGrid Last active date")]
        public DateTime SendGridLastActiveDateTime { get; set; }

        [NotMapped]
        [Editable(false)]
        public bool CanUseSendGrid { get { return SendGridEmailSendCount < SENDGRID_MAX_EMAIL_COUNT; } }
        #endregion

        #region Amazon Affiliate Data
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Amazon associate ID")]
        public string AmazonAssociateID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "AWS access key")]
        public string AWSAccessKey { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Encrypted AWS secret key")]
        public string EncryptedAWSSecretKey { get; set; }

        [NotMapped]
        [Editable(false)]
        internal string AWSSecretKey { get { return Security.SimpleDecrypt(EncryptedAWSSecretKey, CryptKey, AuthKey); } }
        #endregion
    }
}
