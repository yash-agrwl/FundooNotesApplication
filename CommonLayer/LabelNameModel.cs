using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CommonLayer
{
    public class LabelNameModel
    {
        [Key]
        public int LabelNameId { get; set; }

        [Required]
        public string LabelName { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual RegisterModel Users { get; set; }
    }
}
