using System;

namespace Common.Thesaurus.Helpers.ExceptionHelper
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// Gets all needed information from provided exception in a string.
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>exception details in string</returns>
        public static string GetMessage(this Exception ex)
        {
            return string.Concat("Exception message: ",
                                 ex.Message,
                                 " Stack trace: ",
                                 ex.StackTrace,
                                 ". Inner exception message: ",
                                 ex.InnerException?.Message);
        }
    }
}
