using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Model;
using Repository;

namespace WebAppCommercial.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService userService;
      private readonly ICompanyService companyService;

      public UserController(IUserService userService, ICompanyService companyService)
    {
      this.userService = userService;
         this.companyService = companyService;
    }

    [HttpGet]
    public async Task<ActionResult<User>> Get()
    {
      var result = await userService.GetAll();
      return Ok(result);
    }

    [HttpGet("getteste")]
    [Authorize]
    public async Task<ActionResult<User>> GetTeste()
    {
      return Ok("Logado");
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
      var user = new User { Name = "ProfControl user" };
      return user;
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

    // POST api/values
    [HttpPost]
    public IActionResult Register([FromBody] User value)
    {

      return Ok("Usuário Cadastrado com Sucesso.");
    }

      [HttpPost("{id}")]
      public async Task<ActionResult<dynamic>> Create([FromBody] User data)
      {
         try
         {
            var company=await companyService.GetByIdAsync(data.IdCompany);
            if(company!=null)
            {
               await userService.Create(data);
               return true;
            }
            else
            {
               return false;
            }
         }
         catch (Exception ex)
         {

            return false;
         }
      }

      private async Task CreateUserAsync(User user)
    {
      await userService.Create(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody]AuthenticateModel model)
    {
         try
         {
            var user = await userService.GetUser(model);

            if (user!=null)
            {
               var token = TokenService.GenerateToken(user);

               //List<Clinic> clinic = await userClinicService.GetAllByIdUser(user);
               return new
               {
                  //Clinic = clinic,
                  idEmp = 1,
                  name = user.Name,
                  role = user.Role,
                  password=user.Password,
                  token = token

               };
            }
            else
               return Ok(false);
         }
         catch (Exception ex)
         {

            return Ok(false);
         }
      }

    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

  }
}
