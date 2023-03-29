using KTechPassApp.Data.Entity;
using KTechPassApp.Services.Services;
using KTechPassApp.ViewModels.General;
using KTechPassApp.ViewModels.Record;
using KTechPassApp.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace KTechPassAppAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    [Produces("application/json")]
    public class RecordController : ControllerBase
    {


        IUserService userService;
        IConfiguration configuration;
        IRecordService recordService;
        private PassContext db;
        public RecordController(IUserService userService, IConfiguration configuration, PassContext db, IRecordService recordService)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.db = db;
            this.recordService = recordService;
        }
       /// <summary>
       /// Create New Password User
       /// </summary>
       /// <param name="recordModel"></param>
       /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateNewRecord(UserRecord recordModel)
        {
            try
            {
                var response = recordService.CreateUserRecord(recordModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetRecordList()
        {
            List<RecordVM> response = recordService.GetRecordList();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRecordDetail(int id)
        {
            RecordDetailVM response = recordService.GetRecordById(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateRecord(RecordDetailVM model)
        {
            ResponseModel response = recordService.UpdateUserRecord(model);
            return Ok(response);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public IActionResult DeleteRecord(int id)
        {
            ResponseModel response = recordService.DeleteUserRecord(id);
            return Ok(response);
        }


    }
}