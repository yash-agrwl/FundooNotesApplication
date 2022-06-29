using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository _repository;

        public IConfiguration Configuration { get; }

        public CollaboratorManager(ICollaboratorRepository repository, IConfiguration configuration)
        {
            this._repository = repository;
            this.Configuration = configuration;
        }

        public ResponseModel<CollaboratorModel> AddCollaborator(CollaboratorModel collab, int userId)
        {
            try
            {
                return this._repository.AddCollaborator(collab, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<string>> GetCollaborator(int noteId, int userId)
        {
            try
            {
                return this._repository.GetCollaborator(noteId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
