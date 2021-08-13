using System.Collections.Generic;

namespace Model.Registrations
{
  public class Company : BaseEntity
  {
    public string CorporateName { get; set; }
    public ICollection<DescriptionFiles> DescriptionFiles { get; set; }
    public ICollection<User> Users { get; set; }
  }
}
