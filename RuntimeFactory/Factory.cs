using System;
using System.Collections.Generic;
using System.Reflection;

namespace RuntimeFactory
{
    public class Factory
    {
        readonly IMessageTypeProvider _typeProvider;
        readonly ICreateObjects _createObjects;

        public Factory(IMessageTypeProvider typeProvider,
            ICreateObjects createObjects)
        {
            _typeProvider = typeProvider;
            _createObjects = createObjects;
        }

	public static Factory Create(Func<Assembly[]> assemblyLoadDelegate)
	{
	    return new Factory(new TypeMapLoader(assemblyLoadDelegate), new ObjectCreatorActivator());
	}

        private MsgBase ReturnMessage(int id)
        {
            var messageTypes = _typeProvider.MessageTypes();

	    if (messageTypes.ContainsKey(id))
	    {
	        var type = messageTypes[id];
	        return _createObjects.Create(type);

	    }
	    return null;
	}

        public MsgBase GetMessage(int id)
        {
            return ReturnMessage(id);
        }

	public T GetMessage<T>(int id) where T : MsgBase
	{
	    return (T) GetMessage(id);
	}
    }

    public interface ICreateObjects
    {
        MsgBase Create(Type type);
    }

    public class ObjectCreatorActivator : ICreateObjects
    {
        public MsgBase Create(Type type)
        {
            return (MsgBase)Activator.CreateInstance(type);
        }
    }
 
    public interface IMessageTypeProvider
    {
        IDictionary<int, Type> MessageTypes();
    }
}