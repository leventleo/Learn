using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnCode.MvcUI.Ninject
{
    public class DependecyResolver
    {
        public static T  GetInstance<T>(T Instance)
        {

            var Kernel = new StandardKernel(new DependecyModule());
            return Kernel.Get<T>();

        }

    }
}
