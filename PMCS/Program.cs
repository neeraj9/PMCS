using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using PMCS.Classes;

namespace PMCS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
                RunInCommandLineMode(args);
            else
                RunWithGUI();
        }

        private static void RunInCommandLineMode(string[] args)
        {
            string sourcePath = null;
            string outputFile = null;
            foreach(string arg in args)
            {
                if(arg.StartsWith("/"))
                {
                    if(arg.StartsWith("/o:"))
                        outputFile = arg.Substring(3);
                }
                else
                {
                    sourcePath = arg;
                }
            }

            InputSource inputSource = new InputSource();
            inputSource.ElementID = 0;
            inputSource.ListOfNamespaces.Clear();
            
            Log("Counting files in {0}", sourcePath);
            inputSource.ProjectFileCount(sourcePath);

            Log("Reading files");
            inputSource.ReadProject(sourcePath, x => Log("- {0}", x));
            inputSource.ProcessNamespaceHierarchy();
 
            Log("Writing output to {0}", outputFile);
            var writer = (outputFile != null) ? new StreamWriter(outputFile) : Console.Out;
            new MseOutput().WriteMse(inputSource.ListOfNamespaces, writer);
            if(outputFile != null)
                writer.Close();
        }

        private static void RunWithGUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        private static void Log(string message, params string[] args)
        {
            Console.WriteLine(String.Format(message, args));
        }
    }
}