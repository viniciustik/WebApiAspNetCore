using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
  public class Filters
  {
    public string textOption { get; set; }
    public FilterType selectOption { get; set; }
    public string cellPhoneOption { get; set; }
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    public int codGroup { get; set; }
    public Filters()
    {
      this.pageNumber = 1;
      this.pageSize = 10;
    }
    public int idCompany { get; set; }

  }

  public enum FilterType
  {
    Name,
    Cpf,
  }

}
