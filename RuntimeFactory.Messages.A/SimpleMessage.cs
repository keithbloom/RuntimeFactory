namespace RuntimeFactory.Messages.A
{
    [MessageIdentify(1)]
    public class SimpleMessage : MsgBase
    {
        public string Name { get { return "Name"; } } 
    }
}