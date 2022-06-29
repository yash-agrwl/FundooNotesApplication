using CommonLayer;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRepository
    {
        IConfiguration Configuration { get; }

        ResponseModel<CollaboratorModel> AddCollaborator(CollaboratorModel collab, int userId);

        ResponseModel<List<string>> GetCollaborator(int noteId, int userId);
    }
}