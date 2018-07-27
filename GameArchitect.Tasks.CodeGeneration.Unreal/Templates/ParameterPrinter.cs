namespace GameArchitect.Tasks.CodeGeneration.Unreal.Templates
{
    internal class ParameterPrinter : CXX.Templates.ParameterPrinter
    {
        protected override INameTransformer NameTransformer { get; } = new UnrealNameTransformer();
        protected override ITypeTransformer TypeTransformer { get; } = new UnrealTypeTransformer();
    }
}