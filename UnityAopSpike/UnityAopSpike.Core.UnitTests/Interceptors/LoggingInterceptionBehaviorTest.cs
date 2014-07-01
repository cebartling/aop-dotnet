using System;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnityAopSpike.Core.Interceptors;

namespace UnityAopSpike.Core.UnitTests.Interceptors
{
    [TestClass]
    public class LoggingInterceptionBehaviorTest
    {
        private Mock<IParameterCollection> _argumentsMock;
        private Mock<IGetNextInterceptionBehaviorDelegate> _getNextInterceptionBehaviorDelegateMock;
        private LoggingInterceptionBehavior _interceptor;
        private Mock<IInvokeInterceptionBehaviorDelegate> _invokeInterceptionBehaviorDelegateMock;
        private Mock<IMethodInvocation> _methodInvocationMock;
        private Mock<IMethodReturn> _methodReturnMock;

        [TestInitialize]
        public void SetUp()
        {
            _interceptor = new LoggingInterceptionBehavior();
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

            // Method invocation collaborations
            _methodInvocationMock.SetupGet(x => x.Target).Returns(new object());
            _methodInvocationMock.SetupGet(x => x.MethodBase).Returns(new MyMethodBase());
            _argumentsMock = new Mock<IParameterCollection>();
            _argumentsMock.SetupGet(x => x.Count).Returns(1);
            _argumentsMock.Setup(x => x.ParameterName(0)).Returns("Invoke");
            _argumentsMock.Setup(x => x.GetParameterInfo(0)).Returns(new MyParameterInfo());
            _methodInvocationMock.SetupGet(x => x.Arguments).Returns(_argumentsMock.Object);
        }

        [TestMethod]
        public void Invoke_DefaultGetNextInterceptionBehaviorInvocations()
        {
            IMethodReturn methodReturn = _interceptor.Invoke(_methodInvocationMock.Object,
                _getNextInterceptionBehaviorDelegateMock.Object.GetNextInterceptionBehaviorDelegate);

            _getNextInterceptionBehaviorDelegateMock.Verify(call => call.GetNextInterceptionBehaviorDelegate(),
                Times.Once());
        }

        [TestMethod]
        public void Invoke_MethodInvocationGetTargetInvocation()
        {
            IMethodReturn methodReturn = _interceptor.Invoke(_methodInvocationMock.Object,
                _getNextInterceptionBehaviorDelegateMock.Object.GetNextInterceptionBehaviorDelegate);

            _methodInvocationMock.VerifyGet(x => x.Target, Times.Once());
        }

        [TestMethod]
        public void Invoke_MethodInvocationGetMethodBaseInvocation()
        {
            IMethodReturn methodReturn = _interceptor.Invoke(_methodInvocationMock.Object,
                _getNextInterceptionBehaviorDelegateMock.Object.GetNextInterceptionBehaviorDelegate);

            _methodInvocationMock.VerifyGet(x => x.MethodBase, Times.Exactly(2));
        }

        [TestMethod]
        public void Invoke_MethodInvocationGetArgumentsInvocation()
        {
            IMethodReturn methodReturn = _interceptor.Invoke(_methodInvocationMock.Object,
                _getNextInterceptionBehaviorDelegateMock.Object.GetNextInterceptionBehaviorDelegate);

            _methodInvocationMock.VerifyGet(x => x.Arguments, Times.Once());
        }

        [TestMethod]
        public void Invoke_ArgumentsInterrogation()
        {
            IMethodReturn methodReturn = _interceptor.Invoke(_methodInvocationMock.Object,
                _getNextInterceptionBehaviorDelegateMock.Object.GetNextInterceptionBehaviorDelegate);

            _argumentsMock.VerifyGet(x => x.Count, Times.Exactly(2));
            _argumentsMock.Verify(x => x.ParameterName(0));
            _argumentsMock.Verify(x => x.GetParameterInfo(0));
        }

        [TestMethod]
        public void Invoke_ThrowsException()
        {
            _methodReturnMock.SetupGet(x => x.Exception).Returns(new Exception("Test"));

            IMethodReturn methodReturn = _interceptor.Invoke(_methodInvocationMock.Object,
                _getNextInterceptionBehaviorDelegateMock.Object.GetNextInterceptionBehaviorDelegate);

            _methodReturnMock.VerifyGet(x => x.Exception);
            _methodInvocationMock.VerifyGet(x => x.MethodBase, Times.Exactly(2));
        }

        [TestMethod]
        public void WillExecute_ReturnsTrue()
        {
            Assert.IsTrue(_interceptor.WillExecute);
        }

        [TestMethod]
        public void GetRequiredInterfaces_ReturnsEmptyEnumerator()
        {
            Assert.AreEqual(Type.EmptyTypes, _interceptor.GetRequiredInterfaces());
        }
    }
}