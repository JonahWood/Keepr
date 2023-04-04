namespace Keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
    }

    [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
    }
    [HttpGet("{vaults}")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetUserVaults()
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _vaultsService.GetUserVaults(userInfo.Id);
      return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
  }
  [HttpPut]
  [Authorize]
  public async Task<ActionResult<List<Account>>> EditAccount([FromBody] Account updateData)
  {
    try 
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      updateData.Id = userInfo.Id;
      Account account = _accountService.Edit(updateData, userInfo.Email);
      return Ok(account);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
