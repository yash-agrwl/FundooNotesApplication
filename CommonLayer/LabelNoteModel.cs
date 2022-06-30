using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CommonLayer
{
    public class LabelNoteModel
    {
        [Key]
        public int LabelNoteId { get; set; }

        [Required]
        [ForeignKey("LabelNames")]
        public int LabelNameId { get; set; }
        [JsonIgnore]
        public virtual LabelNameModel LabelNames { get; set; }

        [Required]
        [ForeignKey("Notes")]
        public int NoteId { get; set; }
        [JsonIgnore]
        public virtual NotesModel Notes { get; set; }
    }
}
