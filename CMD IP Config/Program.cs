using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace CMD_IP_Config
{
    class Program
    {
        #region Fields
        /// <summary>
        /// Path of Output File
        /// </summary>
        public static string Path;

        /// <summary>
        /// Output Path
        /// </summary>
        public static string SecondPath = @".\output.txt";
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
        static public string ShowInfo(string path)
        {
            StreamReader streamReader = new StreamReader(path);

            var context = File.ReadAllText(path);
            var output = "";

            MatchCollection matches =
                Regex.Matches(context, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

            for (int i = 0; i < matches.Count; i++)
            {
                output += matches[i].Value + Environment.NewLine;
            }

            Console.WriteLine(output);
            return output;
        }
        #endregion

        #region Write Result in file
        /// <summary>
        /// Write output to File
        /// </summary>
        /// <param name="text"></param>
        static public void WriteToFile(string text)
        {
            var streamWriter = new StreamWriter(SecondPath);
            streamWriter.Write(text);
            streamWriter.Close();
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
            WriteToFile(ShowInfo(Path));

            Console.WriteLine("Jobs Done!");
            Console.ReadKey();
        }
        #endregion
    }
}
