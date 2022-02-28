using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eon.Models
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string CustomerName { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string CustomerSurname { get; set; }

        [Column(TypeName = "int")]
        public int CustomerWealth { get; set; }

        [InverseProperty(nameof(Item.Owner))]
        public ICollection<Item> Items { get; set; }
    }
}
