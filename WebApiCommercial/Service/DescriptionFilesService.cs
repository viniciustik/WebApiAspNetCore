using Microsoft.AspNetCore.Http;
using Model;
using Model.Registrations;
using Repository;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using File = Model.Registrations.File;

namespace Service
{

  public class DescriptionFilesService : BaseService<DescriptionFiles>, IDescriptionFilesService
   {
      private readonly IFileService fileService;
      public DescriptionFilesService(IGenericRepository<DescriptionFiles> repository,
         IFileService fileService) : base(repository)
      {
         this.fileService = fileService;
      }

      public async Task alter(DescriptionFiles descriptionFiles, List<IFormFile> body)
      {
         await base.Alter(descriptionFiles);

         byte[] fileBytes = null;
         var filename = string.Empty;
         var contentType = string.Empty;

         if (body.Count > 0)
         {

               var dataFiles = await fileService.GetAllByIdDescriptionFiles(descriptionFiles.Id);
               foreach (var item in dataFiles)
               {
                  await fileService.Delete(item);
               }
           
            foreach (var item in body)
            {
               using (var memoryStream = new MemoryStream())
               {
                  await item.CopyToAsync(memoryStream);
                  fileBytes = memoryStream.ToArray();
               }

               filename = item.FileName;
               contentType = item.ContentType;

               File file = new File();
               file.IdDescriptionFiles = descriptionFiles.Id;
               file.Files = fileBytes;
               file.FileName = filename;
               file.ContentType = contentType;

               await fileService.Create(file);
            }
         }
      }

      public async Task save(DescriptionFiles descriptionFiles, List<IFormFile> body)
      {
         await base.Create(descriptionFiles);


         byte[] fileBytes = null;
         var filename = string.Empty;
         var contentType = string.Empty;
         int type = 0;

         if (body.Count > 0)
         {
            foreach (var item in body)
            {
               using (var memoryStream = new MemoryStream())
               {
                  await item.CopyToAsync(memoryStream);
                  fileBytes = memoryStream.ToArray();
               }

               filename = item.FileName;
               contentType = item.ContentType;

               File file = new File();

               file.IdDescriptionFiles = descriptionFiles.Id;
               file.Files = fileBytes;
               file.FileName = filename;
               file.ContentType = contentType;


               await fileService.Create(file);
            }

         }
      }
      public Task<PagedResult<DescriptionFiles>> GetAllPaged(Filters filters)
      {
         return (repository as IDescriptionFilesRepository).GetAllPaged(filters);
      }
      public Task<PagedResult<DescriptionFiles>> GetSearchPaged(string name, int codGroup){
         return (repository as IDescriptionFilesRepository).GetSearchPaged(name,codGroup);
      }
   }
   public interface IDescriptionFilesService : IBaseService<DescriptionFiles>
   {
      Task save(DescriptionFiles descriptionFiles, List<IFormFile> body);
      Task<PagedResult<DescriptionFiles>> GetAllPaged(Filters filters);
      Task<PagedResult<DescriptionFiles>> GetSearchPaged(string name,int codGroup);
      Task alter(DescriptionFiles descriptionFiles, List<IFormFile> body);
      
   }
   }


