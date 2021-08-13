using Model;
using Repository;
using System.Threading.Tasks;

namespace Service
{
   public class UserService : BaseService<User>, IUserService
  {
    public UserService(IGenericRepository<User> repository) : base(repository)
    {

    }
      public Task<User> GetUser(AuthenticateModel model)
      {
         return (repository as IUserRepository).GetUser(model);
      }

  }

  public interface IUserService : IBaseService<User>
  {
      Task <User>GetUser(AuthenticateModel model);
  }

}
