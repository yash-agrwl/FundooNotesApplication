using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CommonLayer
{
    public class NotesModel
    {

        [Key]
        public int NoteId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual RegisterModel User { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        [DefaultValue(false)]
        public bool Pin { get; set; }

        [DefaultValue(false)]
        public bool Archive { get; set; }

        [DefaultValue(false)]
        public bool Trash { get; set; }

        [DefaultValue(null)]
        public string Reminder { get; set; }

        [DefaultValue("white")]
        public string Color { get; set; }

        public string AddedImage { get; set; }
    }
}
