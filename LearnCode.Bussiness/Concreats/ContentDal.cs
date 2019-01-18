using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LearnCode.Bussiness.Interfaces;
using LearnCode.DataAccess;
using LearnCode.Entities;

namespace LearnCode.Bussiness.Concreats
{
    public class ContentDal : RepositoryBase<Content, LessonDbContext>, IContent
    {
        public override async Task<List<Content>> GetList(Expression<Func<Content, bool>> filter = null)
        {
            var getlist=await  base.GetList(filter);
            return getlist.Where(x => !x.Tombstone.HasValue).ToList();
        }
    }

   

}
