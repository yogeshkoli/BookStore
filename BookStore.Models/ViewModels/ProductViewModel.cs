using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product {get; set;}
        public IEnumerable<SelectListItem> CategoryList {get; set;}

        public IEnumerable<SelectListItem> CoverTypeList {get; set;}
    }
}