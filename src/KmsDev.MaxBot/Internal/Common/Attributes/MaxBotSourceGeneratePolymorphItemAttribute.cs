namespace KmsDev.MaxBot
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    internal sealed class MaxBotSourceGeneratePolymorphItemAttribute : Attribute 
    {
        public string DiscriminatorFieldValue { get; }

        public MaxBotSourceGeneratePolymorphItemAttribute(string discriminatorFieldValue)
        {
            DiscriminatorFieldValue = discriminatorFieldValue;
        }
    }
}
