using System.ComponentModel;

namespace CommonLayer
{
    public class NotesEditModel
    {
        public int NoteId { get; set; }

        public int UserId { get; set; }

        [DefaultValue(null)]
        public string Title { get; set; }

        [DefaultValue(null)]
        public string Body { get; set; }
    }
}
