﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
  public static class QueryExtension
  {
    public async static Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query,
                                       int page, int pageSize) where T : class, new()
    {
      var result = new PagedResult<T>();
      result.CurrentPage = page;
      result.PageSize = pageSize;
      result.RowCount = await query.CountAsync();


      var pageCount = (double)result.RowCount / pageSize;
      result.PageCount = (int)Math.Ceiling(pageCount);

      var skip = (page - 1) * pageSize;
      result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

      return result;
    }
  }

  public abstract class PagedResultBase
  {
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }

    public int FirstRowOnPage
    {

      get { return (CurrentPage - 1) * PageSize + 1; }
    }

    public int LastRowOnPage
    {
      get { return Math.Min(CurrentPage * PageSize, RowCount); }
    }
  }

  public class PagedResult<T> : PagedResultBase where T : class, new()
  {
    public IList<T> Results { get; set; }

    public PagedResult()
    {
      Results = new List<T>();
    }
  }
}
