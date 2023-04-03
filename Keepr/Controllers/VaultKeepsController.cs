namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/vaultkeeps")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly Auth0Provider _auth;
        private readonly VaultsService _vaultsService;

        public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth, VaultsService vaultsService)
        {
            _vaultKeepsService = vaultKeepsService;
            _auth = auth;
            _vaultsService = vaultsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vaultkeep>> CreateVK([FromBody] Vaultkeep vkData)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            if(userInfo == null)throw new Exception("You must be logged in to create a vault keep.");
            vkData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.GetOneVault(vkData.Id ,vkData.CreatorId);
            Vaultkeep vaultkeep = _vaultKeepsService.CreateVK(vkData, vault);
            vaultkeep.CreatorId = userInfo.Id;
            return Ok(vaultkeep);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Vaultkeep>> DeleteVK(int id)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            string message = _vaultKeepsService.DeleteVK(id, userId);
            return Ok(message);
            
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
    }
}