using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnityAopSpike.Core.Interceptors;

namespace UnityAopSpike.Core.UnitTests.Interceptors
{
    [TestClass]
    public class LoggingInterceptionBehaviorTest
    {
        private LoggingInterceptionBehavior _interceptor;
        private Mock<IMethodInvocation> _methodInvocationMock;
        private Mock<IMethodReturn> _methodReturnMock;
        private Mock<IInvokeInterceptionBehaviorDelegate> _invokeInterceptionBehaviorDelegateMock;
        private Mock<IGetNextInterceptionBehaviorDelegate> _getNextInterceptionBehaviorDelegateMock;
        private Mock<IParameterCollection> _argumentsMock;

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

            _getNextInterceptionBehaviorDelegateMock.Verify(call => call.GetNextInterceptionBehaviorDelegate(), Times.Once());
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
    }

    public interface IGetNextInterceptionBehaviorDelegate
    {
        InvokeInterceptionBehaviorDelegate GetNextInterceptionBehaviorDelegate();
    }

    public interface IInvokeInterceptionBehaviorDelegate
    {
        IMethodReturn InvokeInterceptionBehaviorDelegate(IMethodInvocation input,
            GetNextInterceptionBehaviorDelegate getNext);
    }

    public class MyMethodBase : MethodBase
    {
        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override ParameterInfo[] GetParameters()
        {
            throw new NotImplementedException();
        }

        public override MethodImplAttributes GetMethodImplementationFlags()
        {
            throw new NotImplementedException();
        }

        public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override MemberTypes MemberType
        {
            get { throw new NotImplementedException(); }
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public override Type ReflectedType
        {
            get { throw new NotImplementedException(); }
        }

        public override RuntimeMethodHandle MethodHandle
        {
            get { throw new NotImplementedException(); }
        }

        public override MethodAttributes Attributes
        {
            get { throw new NotImplementedException(); }
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }
    }

    public class MyParameterInfo : ParameterInfo
    {
        public override string ToString()
        {
            return "something";
        }
    }

}