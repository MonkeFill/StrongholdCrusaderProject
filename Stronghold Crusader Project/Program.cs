using System;
using Stronghold_Crusader_Project.Other; 

namespace Stronghold_Crusader_Project
{
    public static class Program
    {
        public static EventLogger Logger;
        public static Exception LatestException;
        public static void Main()
        {
            bool UseExceptionHandler = true; //If error handling should be with the default handler
            Logger = new EventLogger(); //Starting the logger
            if (UseExceptionHandler)
            {
                try
                {
                    RunGame();
                }
                catch (Exception ExceptionError)
                {
                    Logger.LogEvent($"{ExceptionError.StackTrace} - {ExceptionError.Message}", EventLogger.LogType.Error); //Logging the error it has had and where
                    LatestException = ExceptionError; //Storing the exception
                }
            }
            else
            {
                RunGame();
            }
        }

        private static void RunGame()
        {
            using var game = new Game1(Logger);
            game.Run();
        }
    }
}
