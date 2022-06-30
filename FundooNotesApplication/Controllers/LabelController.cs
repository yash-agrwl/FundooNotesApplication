using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotesApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : Controller
    {
        private readonly ILabelManager _manager;

        public LabelController(ILabelManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("CreateLabel")]
        public IActionResult CreateNewLabel(LabelNameModel labelData)
        {
            try
            {
                var result = this._manager.CreateNewLabel(labelData);

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
        [Route("GetLabels")]
        public IActionResult GetAllLabel(int userId)
        {
            try
            {
                var result = this._manager.GetAllLabel(userId);

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
        [Route("DeleteLabel")]
        public IActionResult DeleteLabel(int userId, string labelName)
        {
            try
            {
                var result = this._manager.DeleteLabel(userId, labelName);

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

        [HttpPost]
        [Route("AddNote")]
        public IActionResult AddNoteToLabel(string labelName, int noteId, int userId)
        {
            try
            {
                var result = this._manager.AddNoteToLabel(labelName, noteId, userId);

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
        public IActionResult GetAllNotesFromLabel(string labelName, int userId)
        {
            try
            {
                var result = this._manager.GetAllNotesFromLabel(labelName, userId);

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
        [Route("DeleteNote")]
        public IActionResult DeleteNoteFromLabel(string labelName, int noteId, int userId)
        {
            try
            {
                var result = this._manager.DeleteNoteFromLabel(labelName,noteId, userId);

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
