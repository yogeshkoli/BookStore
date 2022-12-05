using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}