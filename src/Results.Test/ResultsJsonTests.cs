using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Results.Json.Tests
{
    [TestClass]
    public class ResultsJsonTests
    {
        private const string TestJson = "{\"Name\":\"John\",\"Age\":30}";

        [TestMethod]
        public void From_WithValidJsonString_ReturnsOkResult()
        {
            string jsonString = TestJson;

            Result<Person> result = ResultsJson<Person>.From(jsonString);

            Assert.IsTrue(result.Success);
            Assert.IsInstanceOfType<Person>(result.Value);
            Assert.AreEqual("John", result.Value.Name);
            Assert.AreEqual(30, result.Value.Age);
        }

        [TestMethod]
        public void From_WithInvalidJsonString_ReturnsFailResult()
        {
            string jsonString = "Invalid JSON";

            Result<Person> result = ResultsJson<Person>.From(jsonString);

            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsInstanceOfType<ExceptionError>(result.Error);
        }

        [TestMethod]
        public void Load_WithValidFilePath_ReturnsOkResult()
        {
            string filename = "test.json";
            var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), filename);

            File.WriteAllText(path, TestJson);

            try
            {
                Result<Person> result = ResultsJson<Person>.Load(path);

                Assert.IsTrue(result.Success);
                Assert.AreEqual("John", result.Value.Name);
                Assert.AreEqual(30, result.Value.Age);
                Assert.IsInstanceOfType<Person>(result.Value);
            }
            finally
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        [TestMethod]
        public void Load_WithValidFileJson_ReturnsFailResult()
        {
            string filename = "test.json";
            var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), filename);

            File.WriteAllText(path, "Invalid JSON");

            try
            {
                Result<Person> result = ResultsJson<Person>.Load(path);

                Assert.IsFalse(result.Success);
                Assert.IsNotNull(result.Error);
                Assert.IsInstanceOfType<ExceptionError>(result.Error);

            }
            finally
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        [TestMethod]
        public void Load_WithInvalidFilePath_ReturnsFailResult()
        {
            string filePath = "nonexistent.json";

            Result<Person> result = ResultsJson<Person>.Load(filePath);

            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsInstanceOfType<ExceptionError>(result.Error);
        }

        [TestMethod]
        public void From_WithValidStream_ReturnsOkResult()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(TestJson));

            Result<Person> result = ResultsJson<Person>.From(stream);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("John", result.Value.Name);
            Assert.AreEqual(30, result.Value.Age);
            Assert.IsInstanceOfType<Person>(result.Value);
        }

        [TestMethod]
        public void From_WithInvalidStream_ReturnsFailResult()
        {
            byte[] invalidBytes = Encoding.UTF8.GetBytes("Invalid JSON");
            var stream = new MemoryStream(invalidBytes);

            Result<Person> result = ResultsJson<Person>.From(stream);

            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsInstanceOfType<ExceptionError>(result.Error);
        }

        [TestMethod]
        public void From_WithValidByteSpan_ReturnsOkResult()
        {
            var byteSpan = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(TestJson));

            Result<Person> result = ResultsJson<Person>.From(byteSpan);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("John", result.Value.Name);
            Assert.AreEqual(30, result.Value.Age);
            Assert.IsInstanceOfType<Person>(result.Value);
        }

        [TestMethod]
        public void From_WithInvalidByteSpan_ReturnsFailResult()
        {
            byte[] invalidBytes = Encoding.UTF8.GetBytes("Invalid JSON");
            var byteSpan = new ReadOnlySpan<byte>(invalidBytes);

            Result<Person> result = ResultsJson<Person>.From(byteSpan);

            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsInstanceOfType<ExceptionError>(result.Error);
        }
    }

    public class Person
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
    }
}

