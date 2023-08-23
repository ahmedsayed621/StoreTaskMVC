using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreTaskMVC.Models
{
    public class Space
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int  StoreId { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
