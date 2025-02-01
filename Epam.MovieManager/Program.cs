using System;
using System.Reflection;
using log4net;
using log4net.Config;
namespace Epam.MovieManager
{
    internal static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure(); // This reads configuration from app.config

            log.Info("Application Started");

            Console.WriteLine("Logging Initialized. Check Logs folder.");
            TestLogging();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MovieManager());
        }
        static void TestLogging()
        {
            log.Debug("This is a debug message.");
            log.Info("This is an info message.");
            log.Warn("This is a warning.");
            log.Error("This is an error.");
        }
    }
}
