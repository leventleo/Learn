using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LearnCode.DataAccess;
using LearnCode.Entities;
using LLearnCode.Bussiness.Interfaces;

namespace LearnCode.Bussiness.Concreats
{
    public class UserDal : RepositoryBase<User, LessonDbContext>, IUser
    {
        public override async Task<List<User>> GetList(Expression<Func<User, bool>> filter = null)
        {
            var getlist = await base.GetList(filter);
            return getlist.Where(x => !x.Tombstone.HasValue).ToList();
        }
    }
}
