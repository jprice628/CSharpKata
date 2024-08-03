using LanguageExt;
using LanguageExt.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LanguageExt.Prelude;

namespace PersistenceTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AppData()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            Console.WriteLine(appData);
        }

        [TestMethod]
        public void WriteData()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);            
            var path = Path.Combine(appData, "Things", "test.text");
            var dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir!);

            File.AppendAllLines(path, ["Lorem ipsum dolor sit amet..."]);
        }

        [TestMethod]
        public void DirectoryInfo()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appData, "Things");
            var dir = Directory.CreateDirectory(path);

            Console.WriteLine("FullName: " + dir.FullName);
            Console.WriteLine("Exists: " + dir.Exists);
        }

        [TestMethod]
        public void ListFile()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appData, "Things");
            var dir = Directory.CreateDirectory(path);
            var files = dir.EnumerateFiles("Thing-*.json");
            
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
            }
        }
    }
}