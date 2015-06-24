using System;
using System.Collections.Generic;
using RuntimeFactory.Messages.A;
using Xunit;

namespace RuntimeFactory.Tests
{
    public class TypeLoaderTests
    {
        [Fact]
        public void Given_a_type_with_a_message_returns_a_type_map()
        {
            var types = new List<Type> {typeof (SimpleMessage)};
            var loader = new TypeMapLoader(() => AppDomain.CurrentDomain.GetAssemblies());
            var result = loader.CreateMap(types);

	    Assert.True(result.ContainsKey(1));
        }

	[Fact]
	public void Is_an_type_map_provider()
	{
	    var sut = new TypeMapLoader(() => null);
	    Assert.IsAssignableFrom<IMessageTypeProvider>(sut);
	}
    }
}