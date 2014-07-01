using System.Reflection;

namespace UnityAopSpike.Core.UnitTests.Interceptors
{
    public class MyParameterInfo : ParameterInfo
    {
        public override string ToString()
        {
            return "something";
        }
    }
}