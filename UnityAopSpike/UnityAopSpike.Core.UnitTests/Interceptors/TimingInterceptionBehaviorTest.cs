using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnityAopSpike.Core.Interceptors;

namespace UnityAopSpike.Core.UnitTests.Interceptors
{
    [TestClass]
    public class TimingInterceptionBehaviorTest
    {
        private Mock<IGetNextInterceptionBehaviorDelegate> _getNextInterceptionBehaviorDelegateMock;
        private TimingInterceptionBehavior _interceptor;
        private Mock<IInvokeInterceptionBehaviorDelegate> _invokeInterceptionBehaviorDelegateMock;
        private Mock<IMethodInvocation> _methodInvocationMock;
        private Mock<IMethodReturn> _methodReturnMock;

        [TestInitialize]
        public void Setup()
        {
            _interceptor = new TimingInterceptionBehavior();
            _methodInvocationMock = new Mock<IMethodInvocation>();
            _methodReturnMock = new Mock<IMethodReturn>();
            _invokeInterceptionBehaviorDelegateMock = new Mock<IInvokeInterceptionBehaviorDelegate>();
            _invokeInterceptionBehaviorDelegateMock.Setup(
                call =>
                    call.InvokeInterceptionBehaviorDelegate(It.IsAny<IMethodInvocation>(),
                        It.IsAny<GetNextInterceptionBehaviorDelegate>())).
                Returns(_methodReturnMock.Object);
            _getNextInterceptionBehaviorDelegateMock = new Mock<IGetNextInterceptionBehaviorDelegate>();
            _getNextInterceptionBehaviorDelegateMock.Setup(call => call.GetNextInterceptionBehaviorDelegate()).
                Returns(_invokeInterceptionBehaviorDelegateMock.Object.InvokeInterceptionBehaviorDelegate);
        }

        [TestMethod]
        public void Invoke_DefaultGetNextInterceptionBehaviorInvocations()
        {
            IMethodReturn methodReturn = _interceptor.Invoke(_methodInvocationMock.Object,
                _getNextInterceptionBehaviorDelegateMock.Object.GetNextInterceptionBehaviorDelegate);

            _getNextInterceptionBehaviorDelegateMock.Verify(call => call.GetNextInterceptionBehaviorDelegate(),
                Times.Once());
        }
    }
}