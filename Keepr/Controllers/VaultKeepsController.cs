namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/vaultkeeps")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly Auth0Provider _auth;

        public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
        {
            _vaultKeepsService = vaultKeepsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vaultkeep>> CreateVK([FromBody] Vaultkeep vkData)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vkData.CreatorId = userInfo.Id;
            Vaultkeep vaultkeep = _vaultKeepsService.CreateVK(vkData);
            return Ok(vaultkeep);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
    }
}