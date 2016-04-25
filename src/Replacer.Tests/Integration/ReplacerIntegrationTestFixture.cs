using System;
using NUnit.Framework;
using System.IO;

namespace Replacer.Tests
{
    [TestFixture(Category="Integration")]
    public class ReplacerIntegrationTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_Replace_FileInSubDirectory()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var directoryName = "hello";

            var directoryPath = Path.GetFullPath (directoryName);

            var fileName = "hello.txt";

            var oldValue = "hello";

            var newValue = "hi";

            var filePath = Path.GetFullPath (directoryName + "/" + fileName);

            Directory.CreateDirectory (Path.GetFullPath (directoryName));

            File.WriteAllText (filePath, "hello world");

            var replacer = new Replacer (Environment.CurrentDirectory);
            replacer.IsVerbose = true;
            replacer.CommitChanges = true;
            replacer.IncludeSubDirectories = true;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            replacer.Replace ("*", oldValue, newValue);
            replacer.IsVerbose = true;

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            var newDirectoryPath = directoryPath.Replace (oldValue, newValue);
            var newFilePath = filePath.Replace (oldValue, newValue);

            Assert.IsTrue (Directory.Exists (newDirectoryPath));
            Assert.IsFalse (Directory.Exists (directoryPath));

            Assert.IsTrue (File.Exists (newFilePath));

            var newContent = File.ReadAllText (newFilePath);

            var expectedContent = "hi world";

            Assert.AreEqual (expectedContent, newContent);
        }

        [Test]
        public void Test_Replace_FileInDeepSubDirectory()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var directoryName = "hello1/hello2";

            var directoryPath = Path.GetFullPath (directoryName);

            var fileName = "hello.txt";

            var oldValue = "hello";

            var newValue = "hi";

            var filePath = Path.GetFullPath (directoryName + "/" + fileName);

            Directory.CreateDirectory (Path.GetFullPath (directoryName));

            File.WriteAllText (filePath, "hello world");

            var replacer = new Replacer (Environment.CurrentDirectory);
            replacer.IsVerbose = true;
            replacer.CommitChanges = true;
            replacer.IncludeSubDirectories = true;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            replacer.Replace ("*", oldValue, newValue);
            replacer.IsVerbose = true;

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            var newDirectoryPath = directoryPath.Replace (oldValue, newValue);
            var newFilePath = filePath.Replace (oldValue, newValue);

            Assert.IsTrue (Directory.Exists (newDirectoryPath));
            Assert.IsFalse (Directory.Exists (directoryPath));

            Assert.IsTrue (File.Exists (newFilePath));

            var newContent = File.ReadAllText (newFilePath);

            var expectedContent = "hi world";

            Assert.AreEqual (expectedContent, newContent);
        }
    }
}

