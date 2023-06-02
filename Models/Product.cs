using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalog.Models
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        [StringLength(255)]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName ="decimal(9,2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(255)]
        public string? ImageUrl { get; set; }
        public float Stock { get;set; }
        public DateTime RecordDate { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        //The jsonIgnore, it ignores the serialization and deserialization of this property
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
