namespace KmsDev.MaxBot.Full
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class EnumValueByStringAttribute : Attribute
    {
        public string StringValue { get; }

        public EnumValueByStringAttribute(string stringValue)
        {
            StringValue = stringValue;
        }
    }
}
