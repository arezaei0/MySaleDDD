﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MySaleDDD.Resources;



namespace MySaleDDD.Models
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public DateTime? AddedDate { get; set; }
        public string SystemUserId { get; set; }
      
        public string Titel { get; set; }
    }
}
