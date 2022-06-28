using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CommonLayer
{
    public class CollaboratorModel
    {
        [Key]
        public int CollaborationId { get; set; }

        [ForeignKey("Notes")]
        public int NoteId { get; set; }

        [JsonIgnore]
        public NotesModel Notes { get; set; }

        public string SharedEmail { get; set; }
    }
}
