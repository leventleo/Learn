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
    public class CommandIndexDal : RepositoryBase<CommandIndex, LessonDbContext>, ICommandIndex
    {
        public override async Task<List<CommandIndex>> GetList(Expression<Func<CommandIndex, bool>> filter = null)
        {
            var getlist = await base.GetList(filter);
            return getlist.Where(x => !x.Tombstone.HasValue).ToList();
        }
    }

}
