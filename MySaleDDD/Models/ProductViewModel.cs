using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Models
{
    public class ProductViewModel:BaseViewModel
    {
        [Display(Name =nameof(UnitId),ResourceType =typeof(Resources.Labels))]
        public int UnitId { get; set; }
        [Display(Name =nameof(UnitTitle),ResourceType =typeof(Resources.Labels))]
        public string UnitTitle { get; set; }
        [Display(Name = nameof(BrandId), ResourceType = typeof(Resources.Labels))]
        public int BrandId { get; set; }
        [Display(Name = nameof(BrandTitel), ResourceType = typeof(Resources.Labels))]
        public string BrandTitel { get; set; }
        [Display(Name = nameof(ProductCode), ResourceType = typeof(Resources.Labels))]
        public string ProductCode { get; set; }
        [Display(Name = nameof(Size), ResourceType = typeof(Resources.Labels))]
        public string Size { get; set; }
        [Display(Name = nameof(Qty), ResourceType = typeof(Resources.Labels))]
        public int Qty { get; set; }
        public IFormFile MyFile { get; set; }
        public byte[] ProductImage { get; set; }
        [Display(Name = nameof(ByePrice), ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString="{0:c}" )]  //برای نمایش کلمه ریال در کنار واحد پولی
        public double ByePrice { get; set; }
        [Display(Name = nameof(SellPrice), ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public double SellPrice { get; set; }
    }
}
