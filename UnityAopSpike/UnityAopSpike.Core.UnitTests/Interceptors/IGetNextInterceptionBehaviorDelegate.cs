using Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityAopSpike.Core.UnitTests.Interceptors
{
    public interface IGetNextInterceptionBehaviorDelegate
    {
        InvokeInterceptionBehaviorDelegate GetNextInterceptionBehaviorDelegate();
    }
}