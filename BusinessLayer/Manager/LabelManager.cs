using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository _repository;

        public IConfiguration Configuration { get; }

        public LabelManager(ILabelRepository repository, IConfiguration configuration)
        {
            this._repository = repository;
            this.Configuration = configuration;
        }

        public ResponseModel<LabelNameModel> CreateNewLabel(LabelNameModel labelData)
        {
            try
            {
                return this._repository.CreateNewLabel(labelData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<string>> GetAllLabel(int userId)
        {
            try
            {
                return this._repository.GetAllLabel(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<LabelNameModel> EditLabel(int userId, string currentName, string newName)
        {
            try
            {
                return this._repository.EditLabel(userId, currentName, newName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<LabelNameModel> DeleteLabel(int userId, string labelName)
        {
            try
            {
                return this._repository.DeleteLabel(userId, labelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<LabelNoteModel> AddNoteToLabel(string labelName, int noteId, int userId)
        {
            try
            {
                return this._repository.AddNoteToLabel(labelName, noteId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<NotesModel>> GetAllNotesFromLabel(string labelName, int userId)
        {
            try
            {
                return this._repository.GetAllNotesFromLabel(labelName, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<LabelNoteModel> DeleteNoteFromLabel(string labelName, int noteId, int userId)
        {
            try
            {
                return this._repository.DeleteNoteFromLabel(labelName, noteId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
