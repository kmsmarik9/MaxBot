using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace KmsDev.MaxBot.Full.Requests
{
    [Inheritable]
    internal class GetRequestNameForInheritAttribute : TypeAspect
    {
        public override void BuildAspect(IAspectBuilder<INamedType> builder)
        {
            if (builder.Target.IsAbstract || builder.Target.TypeKind != TypeKind.Class)
            {
                builder.SkipAspect();
                return;
            }

            builder.IntroduceMethod
            (
                nameof(GetRequestNameTemplate),
                IntroductionScope.Instance,
                OverrideStrategy.Override,
                buildMethod: bm =>
                {
                    bm.Name = nameof(IMaxBotRequest.GetRequestName);
                    bm.Accessibility = Accessibility.Public;
                },
                args: new { T = builder.Target }
            );
        }

        [Template]
        private string GetRequestNameTemplate<[CompileTime] T>()
            where T: IMaxBotRequest
        {
            return MaxBotRequestNameCache<T>.Value;
        }
    }
}
