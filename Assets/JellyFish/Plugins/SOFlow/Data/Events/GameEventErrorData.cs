// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

namespace SOFlow
{
    public class GameEventErrorData
    {
	    /// <summary>
	    ///     The error file.
	    /// </summary>
	    public string ErrorFile;

	    /// <summary>
	    ///     The error line.
	    /// </summary>
	    public int ErrorLine;

	    /// <summary>
	    ///     The error method.
	    /// </summary>
	    public string ErrorMethod;

        public GameEventErrorData(string errorMethod, string errorFile, int errorLine)
        {
            ErrorMethod = errorMethod;
            ErrorFile   = errorFile;
            ErrorLine   = errorLine;
        }
    }
}