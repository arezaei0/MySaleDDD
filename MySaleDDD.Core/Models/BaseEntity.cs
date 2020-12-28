using System;
using System.Collections.Generic;
using System.Text;

namespace MySaleDDD.Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime AddedDate { get; set; }
        public string SystemUserId { get; set; }
        public string Titel { get; set; }

    }
}
