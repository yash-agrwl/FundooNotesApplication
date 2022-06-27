using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpGet]
        [Route("GetNotes")]
        public IActionResult GetNotes(int userId)
        {
            try
            {
                var result = this._manager.GetNotes(userId);

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

        [HttpPatch]
        [Route("Edit")]
        public async Task<IActionResult> EditNotes(NotesEditModel noteData)
        {
            try
            {
                var result = await this._manager.EditNotes(noteData);
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

        [HttpPatch]
        [Route("ToggleArchive/{noteId}")]
        public IActionResult ToggleArchive(int noteId, int userId)
        {
            try
            {
                var result = this._manager.ToggleArchive(noteId, userId);

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

        [HttpGet]
        [Route("GetArchive")]
        public IActionResult GetArchive(int userId)
        {
            try
            {
                var result = this._manager.GetArchive(userId);

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

        [HttpPatch]
        [Route("TogglePin")]
        public IActionResult TogglePin(int noteId, int userId)
        {
            try
            {
                var result = this._manager.TogglePin(noteId, userId);

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

        [HttpPatch]
        [Route("SetColor")]
        public IActionResult SetColor(int noteId, int userId, string noteColor)
        {
            try
            {
                var result = this._manager.SetColor(noteId, userId, noteColor);

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

        [HttpPatch]
        [Route("MoveToTrash")]
        public IActionResult MoveToTrash(int noteId, int userId)
        {
            try
            {
                var result = this._manager.MoveToTrash(noteId, userId);

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

        [HttpGet]
        [Route("GetTrash")]
        public IActionResult GetTrash(int userId)
        {
            try
            {
                var result = this._manager.GetTrash(userId);

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
