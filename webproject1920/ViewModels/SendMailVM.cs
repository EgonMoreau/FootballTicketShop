﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webproject1920.ViewModels
{
    public class SendMailVM
    {
        [Required, Display(Name = "Jouw naam")]
        public string FromName { get; set; }
        [Required, Display(Name = "Jouw email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
