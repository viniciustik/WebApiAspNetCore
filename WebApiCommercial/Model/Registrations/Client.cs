using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
  public class Client : BaseEntity
  {
    
    public string Document { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CellPhone { get; set; }
    public string ZipCode { get; set; }
    public string Address { get; set; }
    public string Bairro { get; set; }
    public string Complement { get; set; }
    public string NameState { get; set; }
    public string NameCity { get; set; }
    public DateTime BirthDate { get; set; }
    public statusType Status { get; set; }

    public enum statusType
    {
      Ativo,
      Inativo,
    }
  }

}
