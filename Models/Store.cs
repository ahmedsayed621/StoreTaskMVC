using System.ComponentModel.DataAnnotations;

namespace StoreTaskMVC.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public bool IsMain { get; set; }=true;
        public bool IsInvoiceDirect { get; set; }=true;

    }
}
