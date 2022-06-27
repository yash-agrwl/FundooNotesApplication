﻿using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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

        public ResponseModel<List<NotesModel>> GetNotes(int userId)
        {
            try
            {
                return this._repository.GetNotes(userId);
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

        public ResponseModel<NotesModel> ToggleArchive(int noteId, int userId)
        {
            try
            {
                return this._repository.ToggleArchive(noteId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<NotesModel>> GetArchive(int userId)
        {
            try
            {
                return this._repository.GetArchive(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<NotesModel> TogglePin(int noteId, int userId)
        {
            try
            {
                return this._repository.TogglePin(noteId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<NotesModel> SetColor(int noteId, int userId, string noteColor)
        {
            try
            {
                return this._repository.SetColor(noteId, userId, noteColor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<NotesModel> MoveToTrash(int noteId, int userId)
        {
            try
            {
                return this._repository.MoveToTrash(noteId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<NotesModel>> GetTrash(int userId)
        {
            try
            {
                return this._repository.GetTrash(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<NotesModel> RestoreNote(int noteId, int userId)
        {
            try
            {
                return this._repository.RestoreNote(noteId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
