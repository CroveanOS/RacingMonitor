using System.Diagnostics;

namespace RM.Server.Controls
{
    public class Settings
    {
        /// <summary>
        /// Used to display the current Line in the Logging
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        public static int GetLine(StackTrace tt)
        {
            return tt.GetFrame(0).GetFileLineNumber();
        }
    }
}