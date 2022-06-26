using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository _repository;

        public IConfiguration Configuration { get; }

        public NoteManager(INoteRepository repository, IConfiguration configuration)
        {
            this._repository = repository;
            this.Configuration = configuration;
        }

        public ResponseModel<NotesModel> CreateNote(NotesModel noteData)
        {
            try
            {
                return this._repository.CreateNote(noteData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel<NotesModel>> EditNotes(NotesEditModel noteData)
        {
            try
            {
                return await this._repository.EditNotes(noteData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
