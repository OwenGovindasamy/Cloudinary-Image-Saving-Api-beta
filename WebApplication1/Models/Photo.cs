using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Photo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(50)]
        public string Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}