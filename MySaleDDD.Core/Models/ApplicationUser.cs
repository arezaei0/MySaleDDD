using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaleDDD.Core.Models
{
    public class ApplicationUser:IdentityUser
    {
        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; }
        public DateTime LastModified  { get; set; }
        public DateTime AddDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
    }
}
