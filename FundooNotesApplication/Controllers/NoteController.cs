using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotesApplication.Controllers
{ 
    [Route("api/[Controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteManager _manager;

        public NoteController(INoteManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNote(NotesModel noteData)
        {
            try
            {
                var result = this._manager.CreateNote(noteData);
                if (result.Status == true)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
