using Model.Registrations;
using System.Threading.Tasks;

namespace Repository
{
  public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
  {
    public ProviderRepository(ContextBase dbContext) : base(dbContext)
    {
    }
    //public async Task<bool> ExistsCpf(string cpf)
    //{
    //   var exists = await _dbContext.Set<Client>().Where(x => x.Cpf == cpf).FirstOrDefaultAsync();
    //   if (exists == null)
    //      return false;
    //   else
    //      return true;
    //}
    public async Task<PagedResult<Provider>> GetAllPaged()
    {
      var paged = await base._dbContext.Set<Provider>()
         //.Where(x => (String.IsNullOrEmpty(clientFilter.cellPhoneOption) || (x.CellPhone == clientFilter.cellPhoneOption)) &&


         // ((clientFilter.selectOption == ClientFilterType.Cpf && (string.IsNullOrEmpty(clientFilter.nameCpfOption) || x.Cpf == clientFilter.nameCpfOption)) ||
         //(clientFilter.selectOption == ClientFilterType.Name && (string.IsNullOrEmpty(clientFilter.nameCpfOption) || x.Name.Contains(clientFilter.nameCpfOption))))

         //)
         .GetPagedAsync<Provider>(1, 10);
      return paged;
    }
  }
  public interface IProviderRepository : IGenericRepository<Provider>
  {
    //Task<bool> ExistsCpf(string cpf);
    Task<PagedResult<Provider>> GetAllPaged();
  }
}
