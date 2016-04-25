using System;
using System.IO;
using System.Reflection;

namespace Replacer
{
    public class Replacer
    {
        public bool CommitChanges = false;
        public string WorkingDirectory { get;set; }
        public bool ReplaceInFileNames = true;
        public bool ReplaceInFiles = true;
        public bool ReplaceInDirectoryNames = true;
        public bool IsVerbose = false;

        public bool IncludeSubDirectories = false;

        public bool IncludeHidden = false;

        public int FilesModified = 0;
        public int FileNamesModified = 0;
        public int DirectoryNamesModified = 0;

        public Replacer (string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }

        public void Replace(string fileQuery, string replaceFrom, string replaceWith)
        {
            string[] files = QueryFiles(fileQuery);

            foreach (string file in files)
            {
                if (!IsHidden (file) || IncludeHidden) {
                    if (ReplaceInFiles)
                        ReplaceInFile (file, replaceFrom, replaceWith);

                    if (ReplaceInFileNames)
                        ReplaceInFileName (file, replaceFrom, replaceWith);
                }
            }

            var directories = QueryDirectories (fileQuery);

            if (ReplaceInDirectoryNames) {
                foreach (var directory in directories) {
                    if (!IsHidden (directory) || IncludeHidden) {
                        ReplaceInDirectoryName (directory, replaceFrom, replaceWith);
                    }
                }
            }
        }

        public void ReplaceInFile(string file, string replaceFrom, string replaceWith)
        {
            string content = OpenFile(file);

            if (content.Contains (replaceFrom)) {
                if (IsVerbose)
                    Console.WriteLine (file);
                
                if (CommitChanges) {
                    content = content.Replace (replaceFrom, replaceWith);
                
                    SaveFile (file, content);

                    FilesModified++;
                }
            }
        }

        public void ReplaceInFileName(string filePath, string replaceFrom, string replaceWith)
        {
            if (filePath.Contains (replaceFrom)) {
                if (IsVerbose) {
                    Console.WriteLine("File name: " + filePath.Replace(WorkingDirectory, ""));
                }

                if (CommitChanges) {
                    var fileName = Path.GetFileName (filePath);

                    var newFileName = fileName.Replace (replaceFrom, replaceWith);

                    var newFilePath = Path.GetDirectoryName (filePath)
                                      + "/" + newFileName;

                    File.Move (filePath, newFilePath);

                    FileNamesModified++;
                }
            }
        }


        public void ReplaceInDirectoryName(string directoryPath, string replaceFrom, string replaceWith)
        {
            if (directoryPath.Contains (replaceFrom)) {
                if (IsVerbose)
                    Console.WriteLine (directoryPath);

                if (CommitChanges) {
                    var newDirectory = directoryPath.Replace (replaceFrom, replaceWith);

                    Directory.Move (directoryPath, newDirectory);

                    DirectoryNamesModified++;
                }
            }
        }

        public string OpenFile(string filePath)
        {
            string content = String.Empty;

            using (StreamReader reader = new StreamReader(File.OpenRead(filePath)))
            {
                content = reader.ReadToEnd();
                reader.Close();
            }

            return content;
        }

        public void SaveFile(string filePath, string content)
        {
            using (StreamWriter writer = new StreamWriter(File.Create(filePath)))
            {
                writer.Write(content);
                writer.Close();
            }
        }

        public string[] QueryFiles(string fileQuery)
        {
            var option = (IncludeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            return Directory.GetFiles(WorkingDirectory, fileQuery, option);
        }

        public string[] QueryDirectories(string fileQuery)
        {
            var option = (IncludeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            return Directory.GetDirectories(WorkingDirectory, fileQuery, option);
        }

        public void ClearTotals()
        {
            FilesModified = 0;
            FileNamesModified = 0;
            DirectoryNamesModified = 0;
        }

        public bool IsHidden(string fileOrDirectoryPath)
        {
            return fileOrDirectoryPath.StartsWith(".")
                || fileOrDirectoryPath.Contains(Path.DirectorySeparatorChar + ".");
        }
    }
}

