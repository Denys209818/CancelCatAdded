using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFEntities
{
    [Table("tblAppCatsCancelating")]
    public class AppCat
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(4000)]
        public string Details { get; set; }
        public DateTime Birthday { get; set; }
        public string ImgUrl { get; set; }
        public virtual ICollection<AppCatPrice> CatPrices { get; set; }
    }
}
