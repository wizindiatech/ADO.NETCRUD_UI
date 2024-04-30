using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADO.NET_Crud.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Remarks { get; set; }
    }
}