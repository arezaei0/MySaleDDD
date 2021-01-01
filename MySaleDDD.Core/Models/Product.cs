using MySaleDDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Core.Models
{
    public class Product:BaseEntity
    {
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public string ProductCode { get; set; }
        public string Size { get; set; }
        public int Qty { get; set; }
        public byte[] ProductImage { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
    }
}
