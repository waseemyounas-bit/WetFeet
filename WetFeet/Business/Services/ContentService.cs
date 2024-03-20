using Business.IServices;
using Data.Context;
using Data.Entities;
using Data.Utility;
using DataAccess.Repository;
using DataAccess.UoW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ContentService : IContentService
    {
        private readonly IRepository<Content> _repository;
        private readonly IRepository<ContentFile> _contentFileRepository;
        private readonly IUnitofWork _unitofWork;
        private readonly DataContext _dbContext;

        public ContentService(IRepository<Content> repository,
            IRepository<ContentFile> contentFileRepository,
            IUnitofWork unitofWork, DataContext dbContext)
        {
            this._repository = repository;
            this._contentFileRepository = contentFileRepository;
            this._unitofWork = unitofWork;
            this._dbContext = dbContext;
        }
        public Content AddContent(Content content)
        {
            this._repository.Add(content);
            this._unitofWork.saveChanges();
            return content;
        }

        public int AddContentFiles(List<ContentFile> contentFiles)
        {
            this._contentFileRepository.AddRange(contentFiles);
            return this._unitofWork.saveChanges();
        }

        public int DeleteContent(Guid id)
        {
            DeleteContentFiles(id);
            this._repository.Delete(id);
            return this._unitofWork.saveChanges();
        }

        public int DeleteContentFiles(Guid id)
        {
            var contents = this._contentFileRepository.GetDataFiltered(x => x.ContentId == id);
            this._contentFileRepository.Delete(contents);
            return this._unitofWork.saveChanges();
        }

        public Content GetContent(Guid id)
        {
            return this._repository.GetById(id);
        }

        public List<ContentFile> GetContentFiles(Guid id)
        {
            return this._contentFileRepository.GetDataFiltered(x => x.ContentId == id);
        }

        public async Task<PaginatedList<Content>> GetContentList(string userId, int? pageNo, int pageSize = 10)
        {
            var contents = this._dbContext.Contents
                .Include(x => x.ContentFiles)
                .Where(x => x.UserId == userId);

            return await PaginatedList<Content>.CreateAsync(contents.AsNoTracking(), pageNo ?? 1, pageSize);
        }

        public Content UpdateContent(Content content)
        {
            this._repository.Update(content);
            this._unitofWork.saveChanges();
            return content;
        }
    }
}
