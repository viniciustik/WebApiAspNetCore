using Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
  public class ClientRepository : GenericRepository<Client>, IClientRepository
  {
    public ClientRepository(ContextBase dbContext) : base(dbContext)
    {
    }

    public async Task<PagedResult<Client>> GetAllPaged(Filters clientFilter)
    {
      var paged = await base._dbContext.Set<Client>()
         .Where(x => (String.IsNullOrEmpty(clientFilter.cellPhoneOption) || (x.CellPhone == clientFilter.cellPhoneOption)) &&

         (clientFilter.selectOption == FilterType.Name && (string.IsNullOrEmpty(clientFilter.textOption) || x.Name.Contains(clientFilter.textOption)))

         )
         .GetPagedAsync<Client>(clientFilter.pageNumber, clientFilter.pageSize);
      return paged;
    }
  }
  public interface IClientRepository : IGenericRepository<Client>
  {

    Task<PagedResult<Client>> GetAllPaged(Filters clientFilter);
  }
}
