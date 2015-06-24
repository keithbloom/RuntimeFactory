using System;

namespace RuntimeFactory.Messages.A
{
    [MessageIdentify(5)]
    public class AnotherMessage : MsgBase
    {
        public int Id { get { return 1; } }
        public DateTime Date { get { return DateTime.Now; } }
    }
}