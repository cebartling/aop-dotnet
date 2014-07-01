using Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityAopSpike.Core.UnitTests.Interceptors
{
    public interface IInvokeInterceptionBehaviorDelegate
    {
        IMethodReturn InvokeInterceptionBehaviorDelegate(IMethodInvocation input,
            GetNextInterceptionBehaviorDelegate getNext);
    }
}