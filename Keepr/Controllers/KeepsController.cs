namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/keeps")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        private readonly Auth0Provider _auth;

        public KeepsController(KeepsService keepsService, Auth0Provider auth)
        {
            _keepsService = keepsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        async public Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = _keepsService.CreateKeep(keepData);
            keep.Creator = userInfo;
            return Ok(keep);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpGet]
        async public Task<ActionResult<Keep>> GetAllKeeps()
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Keep> keeps = _keepsService.GetAllKeeps(userInfo?.Id);
            return Ok(keeps);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Keep>> GetOneKeep(int id)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.GetOneKeep(id, userInfo.Id);
            return Ok(keep);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep updateData, int id)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            updateData.CreatorId = userInfo.Id;
            updateData.Id = id;
            Keep keep = _keepsService.UpdateKeep(updateData);
            return Ok(keep);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> DeleteKeep(int id)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            String message = _keepsService.DeleteKeep(id);
            return Ok(message);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
    }
}