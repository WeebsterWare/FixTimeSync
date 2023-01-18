using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FixTimeSync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            void consoleLog(string message) =>
                Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message}");
            consoleLog("Started up script.");

            while (true)
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/C w32tm /resync";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    if (output.Contains("The command completed successfully."))
                    {
                        consoleLog("Time has been synced successfully.");
                        Console.ReadLine();
                        break;
                    }
                    else
                        consoleLog("Error while syncing time, trying again in 1000ms");
                }
                Thread.Sleep(1000);
            }

        }
    }
}
