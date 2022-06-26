﻿using CommonLayer;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteManager
    {
        IConfiguration Configuration { get; }

        ResponseModel<NotesModel> CreateNote(NotesModel noteData);

        Task<ResponseModel<NotesModel>> EditNotes(NotesEditModel noteData);
    }
}