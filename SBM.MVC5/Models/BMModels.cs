using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBM.MVC5.Models
{
    public class Mail 
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string From { get; set; }

        [Required]
        //[DataType(DataType.EmailAddress)]
        public string To { get; set; }

        //[DataType(DataType.EmailAddress)]
        public string CC { get; set; }

        //[DataType(DataType.EmailAddress)]
        public string BCC { get; set; }

        [Required]
        public string Subject { get; set; }

        [DataType(DataType.Upload)]
        public string Attachments { get; set; }

        public string files { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    
    }
}