using LearnCode.DataAccess;
using LearnCode.Entities;
using LLearnCode.Bussiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LearnCode.Bussiness.Concreats
{
    public class SubjectDal : RepositoryBase<Subject, LessonDbContext>, ISubject
    {
        public override async Task<List<Subject>> GetList(Expression<Func<Subject, bool>> filter = null)
        {
            var getlist =await base.GetList(filter);
            return getlist.Where(x => !x.Tombstone.HasValue).ToList();
        }
    }

}
