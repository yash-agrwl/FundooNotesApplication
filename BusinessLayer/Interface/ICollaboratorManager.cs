using CommonLayer;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorManager
    {
        IConfiguration Configuration { get; }

        ResponseModel<CollaboratorModel> AddCollaborator(CollaboratorModel collab, int userId);

        ResponseModel<List<string>> GetCollaborator(int noteId, int userId);
    }
}