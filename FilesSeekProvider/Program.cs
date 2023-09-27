using Autofac;
using Autofac.Core;
using Service.Interface;
using System.Net.Http.Headers;

namespace FilesSeeker
{
    internal static class Program
    {
        private static IContainer container { get; set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            BenchmarkDotNet.Running.BenchmarkRunner.Run<Service.FileSeekService>();

            ApplicationConfiguration.Initialize();

            container = Service.Infrastructure.DIRegister.Instence.Register(container);

            using (var scope = container.BeginLifetimeScope())
            {
                //Application.Run(new FormMain(scope.Resolve<IFileSeekService>()));
                Application.Run(new FormFileSeeker(scope.Resolve<IFileSeekService>()));
            }
        }
    }
}