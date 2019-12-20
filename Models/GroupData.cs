using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hometest.Models
{
    public class GroupData
    {
        public GroupData()
        {
            InventoryData = new HashSet<InventoryData>();
         
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DisplayName("Category Name")]
        public string Name { get; set; }


        public ICollection<InventoryData> InventoryData { get; set; }
    }
}
