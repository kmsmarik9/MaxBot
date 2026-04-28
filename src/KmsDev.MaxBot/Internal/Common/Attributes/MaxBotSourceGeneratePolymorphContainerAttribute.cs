namespace KmsDev.MaxBot
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal sealed class MaxBotSourceGeneratePolymorphContainerAttribute : Attribute 
    { 
        public string DiscriminatorFieldName { get; }

        public MaxBotSourceGeneratePolymorphContainerAttribute(string discriminatorFieldName)
        {
            DiscriminatorFieldName = discriminatorFieldName.ToLowerInvariant();
        }
    }
}