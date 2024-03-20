using Data.Entities;
using Data.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IContentService
    {
        Content AddContent(Content content);
        Content UpdateContent(Content content);
        Content GetContent(Guid id);
        Task<PaginatedList<Content>> GetContentList(string userId, int? pageNo = 1, int pageSize = 10);

        int AddContentFiles(List<ContentFile> contentFiles);
        int DeleteContent(Guid id);

        int DeleteContentFiles(Guid id);

        List<ContentFile> GetContentFiles(Guid id);
    }
}
