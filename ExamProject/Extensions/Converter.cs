namespace ExamProject.Extensions
{
    public class LowerCaseControllerName : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            return LowerCaseConverter.Convert(value);
        }
    }
}
