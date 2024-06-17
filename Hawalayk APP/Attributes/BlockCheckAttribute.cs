namespace Hawalayk_APP.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BlockCheckAttribute : Attribute
    {
        public string ParameterName { get; }

        public BlockCheckAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }
    }

}
