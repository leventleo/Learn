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
    public class ViewLevelDal : RepositoryBase<ViewLevel,LessonDbContext>, IViewLevel
    {
        public override async Task<List<ViewLevel>> GetList(Expression<Func<ViewLevel, bool>> filter = null)
        {
            var getlist =await base.GetList(filter);
            return getlist.Where(x => !x.Tombstone.HasValue).ToList();
        }
    }

}
