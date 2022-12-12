using TestGenerator.Core;

namespace ExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new TestGeneratorConfig(3, 6, 3,
                new List<string>()
                {
                    @"D:\User Files\Programming\C#\TestGenerator\TestGenerator.Example\ExampleClass2.cs",
                    @"D:\User Files\Programming\C#\TestGenerator\TestGenerator.Example\ExampleClass1.cs",
                }, @"D:\User Files\Programming\C#\TestGenerator\ResultDirectory");

            var generator = new TestGenerator.Core.TestGenerator(config);

            generator.Generate().Wait();
        }
    }
}