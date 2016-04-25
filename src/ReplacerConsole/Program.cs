using System;
using System.IO;

namespace Replacer
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Run(args);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        public static void Run(string[] args)
        {
            if (args.Length < 4)
                Help ();
            else {
                string workingDir = args [0];

                string fileQuery = args [1];

                string replaceFrom = args [2];

                string replaceWith = args [3];

                Console.WriteLine("Working directory: " + workingDir);
                Console.WriteLine("Query: " + fileQuery);
                Console.WriteLine("Replace from: " + replaceFrom);
                Console.WriteLine("Replace with: " + replaceWith);

                var replacer = new Replacer (Path.GetFullPath(workingDir));
                replacer.IsVerbose = true; // TODO: Allow user to set this via an argument
                replacer.CommitChanges = true;

                replacer.Replace (fileQuery, replaceFrom, replaceWith);
            }
        }

        static public void ShowError(Exception ex)
        {
            Console.WriteLine("===== Error =====");
            Console.WriteLine(ex.ToString());
        }

        static public void Help()
        {
            Console.WriteLine ("-------------------");
            Console.WriteLine ("  replacer - Help  ");
            Console.WriteLine ("-------------------");
            Console.WriteLine ();
            Console.WriteLine ("Syntax:");
            Console.WriteLine ("  mono replacer.exe [path] [fileName] [oldValue] [newValue]");
            Console.WriteLine ();
            Console.WriteLine ("Example:");
            Console.WriteLine ("  mono replacer.exe . file.txt \"oldValue\" \"newValue\"");
            Console.WriteLine ();

        }
    }
}