namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;

        public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService, Auth0Provider auth)
        {
            _profilesService = profilesService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
            _auth = auth;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> GetProfile(string id)
        {
            try 
            {
            Profile prof = _profilesService.GetProfile(id);
            return Ok(prof);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfile(string id)
        {
            try 
            {
            List<Keep> keeps = _keepsService.GetKeepsByProfile(id);
            return Ok(keeps);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public ActionResult<List<Vault>> GetVaultsByProfile(string id)
        {
            try 
            {
            List<Vault> vaults = _vaultsService.GetVaultsByProfile(id);
            return Ok(vaults);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
    }
}