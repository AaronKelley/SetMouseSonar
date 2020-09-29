using System;
using System.Runtime.InteropServices;

namespace SetMouseSonar
{
    /// <summary>
    /// A single class for the entire program.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfoa"/>
    class Program
    {
        /// <summary>
        /// Main program.  Fetches the current state of the mouse sonar feature, and enables or disables it if requested.
        /// </summary>
        /// <param name="args">0 to disable mouse sonar, and 1 to enable it</param>
        static int Main(string[] args)
        {
            bool sonarEnabled = false;
            bool result = SystemParametersInfo(SPI.GETMOUSESONAR, 0, ref sonarEnabled, 0);
            Console.WriteLine("Initial state: Mouse sonar is {0}.", sonarEnabled ? "enabled" : "disabled");

            if (args.Length != 1)
            {
                // Parameters are bad, print a usage message.
                Usage();
            }
            else if (args[0] == "0")
            {
                // User requested to disable mouse sonar, but only proceed if it is currently enabled.
                if (sonarEnabled)
                {
                    sonarEnabled = false;
                    result = SystemParametersInfo(SPI.SETMOUSESONAR, 0, sonarEnabled, SPIF.UPDATEINIFILE);
                    Console.WriteLine("Mouse sonar is now disabled.");
                }
                else
                {
                    Console.WriteLine("Nothing to do.");
                }
            }
            else if (args[0] == "1")
            {
                // User requested to enable mouse sonar, but only proceed if it is currently disabled.
                if (!sonarEnabled)
                {
                    sonarEnabled = true;
                    result = SystemParametersInfo(SPI.SETMOUSESONAR, 0, sonarEnabled, SPIF.UPDATEINIFILE);
                    Console.WriteLine("Mouse sonar is now enabled.");
                }
                else
                {
                    Console.WriteLine("Nothing to do.");
                }
            }
            else
            {
                // Not sure what the user was asking for.
                Usage();
            }

            return result ? 1 : 0;
        }

        /// <summary>
        /// Print out a brief usage message.
        /// </summary>
        private static void Usage()
        {
            Console.WriteLine("Add command line parameter \"0\" to disable mouse sonar, or \"1\" to enable mouse sonar.");
        }

        // Two different versions of "SystemParametersInfo" are needed becuase of the different type necessary for the "pvParam" parameter.

        /// <summary>
        /// Retrieves or sets the value of one of the system-wide parameters.
        /// </summary>
        /// <param name="uiAction">The system-wide parameter to be retrieved or set</param>
        /// <param name="uiParam">A parameter whose usage and format depends on the system parameter being queried or set</param>
        /// <param name="pvParam">A parameter whose usage and format depends on the system parameter being queried or set</param>
        /// <param name="fWinIni">If a system parameter is being set, specifies whether the user profile is to be updated, and if so, whether the WM_SETTINGCHANGE message is to be broadcast to all top-level windows to notify them of the change</param>
        /// <returns>If the function succeeds, the return value is a nonzero value</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(SPI uiAction, uint uiParam, ref bool pvParam, SPIF fWinIni);

        /// <summary>
        /// Retrieves or sets the value of one of the system-wide parameters.
        /// </summary>
        /// <param name="uiAction">The system-wide parameter to be retrieved or set</param>
        /// <param name="uiParam">A parameter whose usage and format depends on the system parameter being queried or set</param>
        /// <param name="pvParam">A parameter whose usage and format depends on the system parameter being queried or set</param>
        /// <param name="fWinIni">If a system parameter is being set, specifies whether the user profile is to be updated, and if so, whether the WM_SETTINGCHANGE message is to be broadcast to all top-level windows to notify them of the change</param>
        /// <returns>If the function succeeds, the return value is a nonzero value</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(SPI uiAction, uint uiParam, bool pvParam, SPIF fWinIni);

        /// <summary>
        /// Values for the "uiAction" parameter of the "SystemParametersInfo" method.
        /// </summary>
        enum SPI : uint
        {
            /// <summary>
            /// Retrieves the state of the Mouse Sonar feature
            /// </summary>
            GETMOUSESONAR = 0x101C,

            /// <summary>
            /// Turns the Sonar accessibility feature on or off
            /// </summary>
            SETMOUSESONAR = 0x101D
        }

        enum SPIF : uint
        {
            /// <summary>
            /// Writes the new system-wide parameter setting to the user profile
            /// </summary>
            UPDATEINIFILE = 0x01
        }
    }
}
