using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
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

        [HttpPatch]
        [Route("Restore")]
        public IActionResult RestoreNote(int noteId, int userId)
        {
            try
            {
                var result = this._manager.RestoreNote(noteId, userId);

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

        [HttpDelete]
        [Route("DeleteForever")]
        public IActionResult DeleteForever(int noteId, int userId)
        {
            try
            {
                var result = this._manager.DeleteForever(noteId, userId);

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
        [Route("AddReminder")]
        public IActionResult AddReminder(int noteId, int userId, string remind)
        {
            try
            {
                var result = this._manager.AddReminder(noteId, userId, remind);

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
        [Route("GetReminders")]
        public IActionResult GetReminders(int userId)
        {
            try
            {
                var result = this._manager.GetReminders(userId);

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
        [Route("DeleteReminder")]
        public IActionResult DeleteReminder(int noteId, int userId)
        {
            try
            {
                var result = this._manager.DeleteReminder(noteId, userId);

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
        [Route("AddImage")]
        public IActionResult AddImage(int noteId, int userId, IFormFile form)
        {
            try
            {
                var result = this._manager.AddImage(noteId, userId, form);

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
        [Route("RemoveImage")]
        public IActionResult RemoveImage(int noteId, int userId)
        {
            try
            {
                var result = this._manager.RemoveImage(noteId, userId);

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
