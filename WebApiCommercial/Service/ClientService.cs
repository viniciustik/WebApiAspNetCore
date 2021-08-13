using Model;
using Repository;
using System.Threading.Tasks;

namespace Service
{
   public class ClientService: BaseService<Client>, IClientService
   {
      public ClientService(IGenericRepository<Client> repository) : base(repository)
      {
      }
    
      public Task<PagedResult<Client>> GetAllPaged(Filters clientFilter)
      {
         return  (repository as IClientRepository).GetAllPaged(clientFilter);
      }

   }
   public interface IClientService : IBaseService<Client>
   {
      Task<PagedResult<Client>> GetAllPaged(Filters clientFilter);
   
   }
}
