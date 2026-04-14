using Serilog;

namespace Ground.Utilities.SerilogRegistration.Extensions
{
    public class SerilogExtensions
    {
        /// <summary>
        /// Wraps the execution of the provided action with Serilog logging for startup, unhandled exceptions, and shutdown.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="startUpMessage">The message to log at startup.</param>
        /// <param name="exceptionMessage">The message to log in case of an unhandled exception.</param>
        /// <param name="shutdownMessage">The message to log at shutdown.</param>
        public static void RunWithSerilogExceptionHandling(Action action, string startUpMessage = "Starting up", string exceptionMessage = "Unhandled exception", string shutdownMessage = "Shutdown completed")
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
            Log.Information(startUpMessage);
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, exceptionMessage);
            }
            finally
            {
                Log.Information(shutdownMessage);
                Log.CloseAndFlush();
            }
        }
    }
}
