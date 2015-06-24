using System;
using System.Collections.Generic;
using RuntimeFactory.Messages.A;
using Xunit;

namespace RuntimeFactory.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void TheFactoryReturnsTheMessage()
        {
            var sut = new Factory(new FakeMessageTypeProvider(), new ObjectCreatorActivator());
            var message = sut.GetMessage(1);

            Assert.IsType<SimpleMessage>(message);
        }

    }

    public class FakeMessageTypeProvider : IMessageTypeProvider
    {
        public IDictionary<int, Type> MessageTypes()
        {
            return new Dictionary<int, Type>
                {
                    {1, typeof (SimpleMessage)}
                };
        }
    }
}