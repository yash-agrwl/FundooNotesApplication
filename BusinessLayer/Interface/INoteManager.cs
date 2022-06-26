using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Interface
{
    public interface INoteManager
    {
        IConfiguration Configuration { get; }

        ResponseModel<NotesModel> CreateNote(NotesModel noteData);
    }
}