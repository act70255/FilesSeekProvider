using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSeeker.Infrastructure
{
    public sealed class StartUp
    {
        public static StartUp Instence
        {
            get
            {
                return _Instence.Value;
            }
        }
        static readonly Lazy<StartUp> _Instence = new Lazy<StartUp>(() => new StartUp());
        public StartUp() { }
        public void Initialize(ref IContainer container)
        {
            Service.Infrastructure.DIRegister.Instence.Register(container);
        }

    }
}
