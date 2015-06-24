using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RuntimeFactory
{
    public class TypeMapLoader : IMessageTypeProvider
    {
        readonly Func<Assembly[]> _assemblyLoader;
        IEnumerable<Type> _types; 

        public TypeMapLoader(Func<Assembly[]> assemblyLoader)
        {
            _assemblyLoader = assemblyLoader;
        }

	public void LoadTypes()
	{
	    _types = _assemblyLoader().Where(x => x.FullName.Contains("RuntimeFactory"))
                                 .SelectMany(x => x.GetTypes());
 
	}

        public Dictionary<int,Type> CreateMap(IEnumerable<Type> types)
        {
            var typeMap = new Dictionary<int, Type>();

            foreach (var type in types)
            {
                foreach (var t in type.CustomAttributes
			    .Where(t => t.AttributeType == typeof (MessageIdentifyAttribute)))
                {
                    typeMap.Add((int)t.ConstructorArguments.First().Value, type);
                }
            }

            return typeMap;
        }

        public IDictionary<int, Type> MessageTypes()
        {
	    LoadTypes();
            return CreateMap(_types);
        }
    }
}