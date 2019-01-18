using LearnCode.Bussiness.Interfaces;
using LearnCode.DataAccess;
using LearnCode.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearnCode.Bussiness.Concreats
{
    public class SourceFileDal : RepositoryBase<SourceFile, LessonDbContext>, ISourceFile
    {
        public override async Task<List<SourceFile>> GetList(Expression<Func<SourceFile, bool>> filter = null)
        {
            var getlist = await base.GetList(filter);
            return getlist.Where(x => !x.Tombstone.HasValue).ToList();
        }
    }
}
