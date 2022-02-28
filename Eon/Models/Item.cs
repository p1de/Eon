using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eon.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string ItemName { get; set; }

        [Column(TypeName = "int")]
        public int ItemPrice { get; set; }
        public int? CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Items")]
        public Customer Owner { get; set; }
    }
}
