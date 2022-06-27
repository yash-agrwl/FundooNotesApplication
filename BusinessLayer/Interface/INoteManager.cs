using CommonLayer;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteManager
    {
        IConfiguration Configuration { get; }

        ResponseModel<NotesModel> CreateNote(NotesModel noteData);

        Task<ResponseModel<NotesModel>> EditNotes(NotesEditModel noteData);

        ResponseModel<NotesModel> ToggleArchive(int noteId, int userId);

        ResponseModel<NotesModel> TogglePin(int noteId, int userId);

        ResponseModel<NotesModel> SetColor(int noteId, int userId, string noteColor);

        ResponseModel<List<NotesModel>> GetNotes(int userId);

        ResponseModel<List<NotesModel>> GetArchive(int userId);

        ResponseModel<NotesModel> MoveToTrash(int noteId, int userId);
    }
}