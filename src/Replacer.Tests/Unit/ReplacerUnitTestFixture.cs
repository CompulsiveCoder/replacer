using System;
using NUnit.Framework;
using System.IO;

namespace Replacer.Tests
{
    [TestFixture(Category="Integration")]
    public class ReplacerUnitTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_ReplaceInFile()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var fileName = "hello.txt";

            var oldValue = "hello";

            var newValue = "hi";

            var filePath = Path.GetFullPath (fileName);

            File.WriteAllText (filePath, "hello world");

            var replacer = new Replacer (Environment.CurrentDirectory);
            replacer.IsVerbose = true;
            replacer.CommitChanges = true;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            replacer.ReplaceInFile (fileName, oldValue, newValue);
            replacer.IsVerbose = true;

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.IsTrue (File.Exists (filePath));

            var newContent = File.ReadAllText (filePath);

            var expectedContent = "hi world";

            Assert.AreEqual (expectedContent, newContent);
        }

        // TODO: Remove if not needed
        /*
        [Test]
        public void Test_ReplaceInFile_InSubDirectory()
        {
        }*/

        [Test]
        public void Test_ReplaceInFileName()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var fileName = "hello.txt";

            var oldValue = "hello";

            var newValue = "hi";

            var filePath = Path.GetFullPath (fileName);

            File.WriteAllText (filePath, "hello world");

            var replacer = new Replacer (Environment.CurrentDirectory);
            replacer.IsVerbose = true;
            replacer.CommitChanges = true;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            replacer.ReplaceInFileName (filePath, oldValue, newValue);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            var newFilePath = filePath.Replace (oldValue, newValue);

            Assert.IsTrue (File.Exists (newFilePath));

            var newContent = File.ReadAllText (newFilePath);

            var expectedContent = "hello world";

            Assert.AreEqual (expectedContent, newContent);
        }

        [Test]
        public void Test_ReplaceInDirectoryName()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var directoryName = "hello";

            var directoryPath = Path.GetFullPath (directoryName);

            var fileName = "hello.txt";

            var oldValue = "hello";

            var newValue = "hi";

            Directory.CreateDirectory (Path.GetFullPath (directoryName));

            var filePath = Path.GetFullPath (directoryName + "/" + fileName);

            File.WriteAllText (filePath, "hello world");

            var replacer = new Replacer (Environment.CurrentDirectory);
            replacer.IsVerbose = true;
            replacer.CommitChanges = true;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            replacer.ReplaceInDirectoryName (directoryPath, oldValue, newValue);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            var newDirectoryPath = directoryPath.Replace (oldValue, newValue);
            var newFilePath = Path.Combine(newDirectoryPath, fileName);

            Assert.IsTrue (Directory.Exists (newDirectoryPath));
            Assert.IsFalse (Directory.Exists (directoryPath));

            var newContent = File.ReadAllText (newFilePath);

            var expectedContent = "hello world";

            Assert.AreEqual (expectedContent, newContent);
        }
    }
}

