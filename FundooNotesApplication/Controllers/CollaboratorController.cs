using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotesApplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorManager _manager;

        public CollaboratorController(ICollaboratorManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddCollaborator(CollaboratorModel collab, int userId)
        {
            try
            {
                var result = this._manager.AddCollaborator(collab, userId);

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
        [Route("Get")]
        public IActionResult GetCollaborator(int noteId, int userId)
        {
            try
            {
                var result = this._manager.GetCollaborator(noteId, userId);

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
        [Route("Delete")]
        public IActionResult DeleteCollaborator(int noteId, int userId, string collabMail)
        {
            try
            {
                var result = this._manager.DeleteCollaborator(noteId, userId, collabMail);

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
