using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LiskovChecker.Tests.Acceptance
{
    class ConsoleRunner
    {
        public static List<string> Run(string command, string arguments)
        {
            Process commandShell = new Process();
            commandShell.StartInfo.FileName = command;

            commandShell.StartInfo.Arguments = arguments;                
            commandShell.StartInfo.UseShellExecute = false;
            commandShell.StartInfo.RedirectStandardOutput = true;
            commandShell.Start();

            List<string> consoleOutputs = new List<string>();
            while (!commandShell.StandardOutput.EndOfStream)
            {
                string line = commandShell.StandardOutput.ReadLine();
                consoleOutputs.Add(line);
            }
            return consoleOutputs;
        }
    }
}
