using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
