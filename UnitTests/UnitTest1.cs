using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string p = @"C:\Users\iammr\Desktop\Task4\Task4\bin\Debug\New folder\Новый текстовый документ.txt";
            DirectoryInfo directoryInfo = new DirectoryInfo(p);

            string extention = Path.GetExtension(p);
        
            


        }
    }
}
