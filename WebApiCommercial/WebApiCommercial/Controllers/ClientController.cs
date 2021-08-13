using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using Service;
using System;
using System.Threading.Tasks;

namespace WebAppCommercial.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class ClientController : ControllerBase
  {
    private readonly IClientService clientService;

    // GET: api/<ClientController>
    public ClientController(IClientService clientService)
    {
      this.clientService = clientService;

    }


    [HttpGet]
    public async Task<ActionResult<PagedResult<Client>>> Get([FromQuery] Filters filter)
    {

      var pagedData = await clientService.GetAllPaged(filter);

      return Ok(pagedData);
    }

    // GET api/<ClientController>/5
    [HttpGet("{filter}")]
    public string Get(string clinicFilter)
    {
      return "value";
    }

    // POST api/<ClientController>
    [HttpPost("{id}")]
    public void Post([FromBody] string value)
    {

    }
    [HttpPost]
    public async Task<ActionResult<dynamic>> Post([FromBody] Client model)
    {
      await clientService.Save(model);

      return true;
    }

    [HttpPatch]
    public async Task<ActionResult<PagedResult<Client>>> Patch([FromBody] Filters clientFilter)
    {
      if (!string.IsNullOrEmpty(clientFilter.cellPhoneOption))
        clientFilter.cellPhoneOption = clientFilter.cellPhoneOption.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("_", "").Replace("-", "").Replace("_", "");
      if (clientFilter.selectOption == FilterType.Cpf)
        clientFilter.textOption = clientFilter.textOption.Replace(".", "").Replace(".", "").Replace("-", "").Replace("_", "").Trim();
      var pagedData = await clientService.GetAllPaged(clientFilter);

      return Ok(pagedData);
    }

    // PUT api/<ClientController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<dynamic>> Put([FromBody] Client model)
    {
      try
      {
        if (model != null)
        {
          await clientService.Alter(model);

          return true;
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      return false;
    }

    // DELETE api/<ClientController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
