using Ninject.Modules;
using ServiceStack.Redis;

namespace LearnCode.MvcUI.Ninject
{
    public class DependecyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRedisClient>().To<RedisClient>();
            
            
        }
    }
}
