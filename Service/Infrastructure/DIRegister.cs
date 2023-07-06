using Autofac;
using Autofac.Core;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Infrastructure
{
    public sealed class DIRegister
    {

        public static DIRegister Instence { get { return _Instence.Value; } }
        static readonly Lazy<DIRegister> _Instence = new Lazy<DIRegister>(() => new DIRegister());
        public IContainer Register(IContainer container)
        {
            var builder =new ContainerBuilder();
            builder.RegisterType<FileSeekService>().As<IFileSeekService>();
            container = builder.Build();
            return container;
        }
    }
}
