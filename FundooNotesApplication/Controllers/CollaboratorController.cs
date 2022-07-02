using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
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
        public IActionResult AddCollaborator(CollaboratorModel collab)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
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
        public IActionResult GetCollaborator(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
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
        public IActionResult DeleteCollaborator(int noteId, string collabMail)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
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
