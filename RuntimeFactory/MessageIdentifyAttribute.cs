using System;

namespace RuntimeFactory
{
    public class MessageIdentifyAttribute : Attribute
    {
        public MessageIdentifyAttribute(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}