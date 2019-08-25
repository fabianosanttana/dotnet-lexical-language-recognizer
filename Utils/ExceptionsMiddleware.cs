using System;

namespace AFD.Utils
{
    public static class Exceptions
    {
        public static void Handler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Log.LogMessage(e.Message);
        }
    }
}