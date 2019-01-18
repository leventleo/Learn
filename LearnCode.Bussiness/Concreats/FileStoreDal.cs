using LearnCode.Bussiness.Interfaces;
using LearnCode.DataAccess;
using LearnCode.Entities;

namespace LearnCode.Bussiness.Concreats
{
    public class FileStoreDal:RepositoryBase<FileStore,LessonDbContext>,IFileStore
    {


    }

}
