namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;

        public VaultsController(VaultsService vaultsService, Auth0Provider auth)
        {
            _vaultsService = vaultsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        async public Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            vault.Creator = userInfo;
            return Ok(vault);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetOneVault(int id)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.GetOneVault(id, userInfo.Id);
            return Ok(vault);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditVault([FromBody] Vault updateData, int id)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            updateData.CreatorId = userInfo.Id;
            updateData.Id = id;
            Vault vault = _vaultsService.EditVault(updateData);
            return Ok(vault);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        async public Task<ActionResult<Vault>> DeleteVault(int id)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            String message = _vaultsService.DeleteVault(id, userInfo.Id);
            return Ok(message);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
    }
}