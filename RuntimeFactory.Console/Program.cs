using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using RuntimeFactory.Messages.A;

namespace RuntimeFactory.Console
{
    class Program
    {
        static void Main(string[] args)
        {
	    Test1();
            System.Console.ReadKey();
        }


	static void Test1()
	{
	    var factory = Factory.Create(() => AppDomain.CurrentDomain.GetAssemblies());
	    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            AssembliesFromPath(baseDirectory);
	 
	    var message = (SimpleMessage) factory.GetMessage(1);
	    
	    if (message == null)
	    {
	        System.Console.WriteLine("Message is null");
	    }
            System.Console.WriteLine(message.Name);
	   
	    var otherMessage = factory.GetMessage<AnotherMessage>(5);
	    System.Console.WriteLine("{0} : {1}", otherMessage.Id, otherMessage.Date);

	}

        static void Spike1()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            AssembliesFromPath(baseDirectory);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                                 .Where(x => x.FullName.Contains("RuntimeFactory"))
                                 .SelectMany(x => x.GetTypes());

            var typeMap = GetMessageTypes(types);

            foreach (var type in typeMap)
            {
                System.Console.WriteLine(type.Key + " " + type.Value);
            }

            System.Console.ReadKey();
        }

        static Dictionary<int, Type> GetMessageTypes(IEnumerable<Type> things)
        {
            var typeMap = new Dictionary<int, Type>();

            foreach (var type in things)
            {
                foreach (var t in type.CustomAttributes
			    .Where(t => t.AttributeType == typeof (MessageIdentifyAttribute)))
                {
                    typeMap.Add((int)t.ConstructorArguments.First().Value, type);
                }
            }
            
            return typeMap;
        }

        static void AssembliesFromPath(string path)
        {
            var assemblyFiles = Directory.GetFiles(path)
                .Where(file =>
                    {
                        var extension = Path.GetExtension(file);
                        return extension != null && extension.Equals(".dll", StringComparison.OrdinalIgnoreCase);
                    });

            foreach (var assemblyFile in assemblyFiles)
            {
                Assembly.LoadFrom(assemblyFile);
            }
        }

    }
}
