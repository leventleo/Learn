

using Microsoft.EntityFrameworkCore;

namespace LearnCode.Entities
{
    public class LessonDbContext : Microsoft.EntityFrameworkCore.DbContext

    {
        public LessonDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=LEO-NOTEBOOK\LEO;initial catalog=LessonDbContext;user id=sa;password=123456; MultipleActiveResultSets=True");
            optionsBuilder.EnableSensitiveDataLogging(true);
            Database.AutoTransactionsEnabled = true;
           

        }


        public DbSet<Lesson> Lessons  { get; set; }
        public DbSet<Subject>  Subjects { get; set; }
        public DbSet<Content>  Contents { get; set; }
        public DbSet<ViewLevel>  ViewLevels { get; set; }
        public DbSet<CommandIndex> CommandIndices { get; set; }
        public DbSet<SourceFile>  SourceFiles { get; set; }
        public DbSet<FileStore> FileStores { get; set; }

    }


}


 