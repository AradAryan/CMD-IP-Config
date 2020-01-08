using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace CMD_IP_Config
{
    class Program
    {
        #region Fields
        /// <summary>
        /// Path of Output File
        /// </summary>
        public static string Path;
        #endregion

        #region Run Commands
        /// <summary>
        /// Running Defined Command
        /// </summary>
        /// <param name="command"></param>
        static public void RunCommand(string command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C" + command;
            process.StartInfo = startInfo;
            process.Start();
            Path = @".\context.txt";
        }
        #endregion

        #region Show Result
        /// <summary>
        /// Write Output on Console 
        /// </summary>
        /// <param name="path"></param>
        static public void ShowInfo(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            var context = new string[30];
            context = File.ReadAllLines(path);

            foreach (var item in context)
            {
                if (item.Contains("IPv4 Address") || item.Contains("Subnet Mask") || item.Contains("Default Gateway"))
                    Console.WriteLine(item);
            }
        }
        #endregion

        #region Main Function
        /// <summary>
        /// Main Funtion
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunCommand("ipconfig > context.txt");
            ShowInfo(Path);

            Console.WriteLine("Jobs Done!");
            Console.ReadKey();
        }
        #endregion
    }
}
