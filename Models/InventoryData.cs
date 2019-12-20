using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hometest.Models
{
    public class InventoryData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Count")]
        public int Count { get; set; }

        [Required]
        [DisplayName("Unit Price")]
        public double Unit_Price { get; set; }

        [Required]
        [DisplayName("Group")]
        public Guid GroupDataId { get; set; }
        public GroupData GroupData { get; set; }
    }
}
