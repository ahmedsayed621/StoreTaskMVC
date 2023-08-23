using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreTaskMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public int? SpaceId { get; set; }
        [ForeignKey("SpaceId")]
        public Space Space { get; set; }

        
    }
}
