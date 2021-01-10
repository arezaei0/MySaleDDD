using System;
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
        [Display(Name = nameof(Titel), ResourceType = typeof(Resources.Labels))]
        public string Titel { get; set; }
    }
    public class KeyValueViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
