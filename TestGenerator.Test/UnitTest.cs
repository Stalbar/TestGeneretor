using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core;

namespace ProjectCheckFunctionalityTests
{
    [TestClass]
    public class UnitTest
    {
        private readonly string _writePath = @"D:\User Files\Programming\C#\TestGenerator\ResultDirectory";
        private readonly string _testClass1Path = @"D:\User Files\Programming\C#\TestGenerator\TestGenerator.Example\ExampleClass1.cs";
        private readonly string _testClass2Path = @"D:\User Files\Programming\C#\TestGenerator\TestGenerator.Example\ExampleClass2.cs";

        [TestMethod]
        public void CorrectUsingsTest()
        {
            var config = new TestGeneratorConfig(3, 3, 3,
                new List<string>()
                {
                    _testClass2Path
                }, _writePath);
            var generator = new TestGenerator.Core.TestGenerator(config);
            var expected = new List<string>()
            {
                "TestGenerator.Example",
                "System",
                "System.Collections.Generic",
                "System.Linq",
                "System.Text",
                "System.Threading.Tasks"
            };

            generator.Generate().Wait();
            var actual = CSharpSyntaxTree
                .ParseText(File.ReadAllText(Path.Combine(_writePath, "ExampleClass2Tests.cs")))
                .GetCompilationUnitRoot().Usings.Select(syntax => syntax.Name.ToString());

            CollectionAssert.AreEqual(expected, actual.ToList<string>());
        }

        [TestMethod]
        public void CorrectNamespaceTest()
        {
            var config = new TestGeneratorConfig(3, 3, 3,
                new List<string>()
                {
                    _testClass1Path
                }, _writePath);
            var generator = new TestGenerator.Core.TestGenerator(config);
            var expected = "TestGenerator.Example.Tests";

            generator.Generate().Wait();
            var actual = CSharpSyntaxTree
                .ParseText(File.ReadAllText(Path.Combine(_writePath, "ExampleClass1Tests.cs")))
                .GetCompilationUnitRoot().DescendantNodes().OfType<NamespaceDeclarationSyntax>().First().Name.ToString();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CorrectClassTest()
        {
            var config = new TestGeneratorConfig(3, 3, 3,
                 new List<string>()
                 {
                    _testClass1Path
                 }, _writePath);
            var generator = new TestGenerator.Core.TestGenerator(config);
            var expected = "ExampleClass1Tests";

            generator.Generate().Wait();
            var actual = CSharpSyntaxTree
                .ParseText(File.ReadAllText(Path.Combine(_writePath, "ExampleClass1Tests.cs")))
                .GetCompilationUnitRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();


            Assert.AreEqual(expected, actual.First().Identifier.Text);
        }

        [TestMethod]
        public void CorrectMethodNamesTest()
        {
            var config = new TestGeneratorConfig(3, 3, 3,
                  new List<string>()
                  {
                    _testClass1Path
                  }, _writePath);
            var generator = new TestGenerator.Core.TestGenerator(config);
            var expected = new List<string>()
            {
                "FirstMethodTest",
                "SecondMethodTest",
                "ThirdMethodTest",
                "ThirdMethodTest1",
            };

            generator.Generate().Wait();
            var actual = CSharpSyntaxTree
                .ParseText(File.ReadAllText(Path.Combine(_writePath, "ExampleClass1Tests.cs")))
                .GetCompilationUnitRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(syntax => syntax.Identifier.Text);

            CollectionAssert.AreEqual(expected, actual.ToList<string>());
        }
    }
}