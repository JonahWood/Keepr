namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Profile> GetProfile(int id)
        {
            try 
            {
            
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
    }
}