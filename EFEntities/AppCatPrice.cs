using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFEntities
{
    [Table("tblAppCatPricesCanceling")]
    public class AppCatPrice
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }
        [ForeignKey("Cat.Id")]
        public int CatId { get; set; }
        public virtual AppCat Cat { get; set; }
    }
}
