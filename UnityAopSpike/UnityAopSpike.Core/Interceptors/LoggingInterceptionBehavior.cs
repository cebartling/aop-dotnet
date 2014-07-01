using System;
using System.Collections.Generic;
using log4net;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityAopSpike.Core.Interceptors
{
    public class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get { return true; }
        }

        /// <summary>
        /// Invokes this interceptor in the interceptor chain.
        /// </summary>
        /// <param name="input">The captured method invocation.</param>
        /// <param name="getNext">The next interceptor in the chain.</param>
        /// <returns>The method invocation result.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var logger = LogManager.GetLogger(input.Target.GetType());

            logger.Info(string.Format("START: {0}", input.MethodBase));
            var arguments = input.Arguments;
            for (int i = 0; i < arguments.Count; i++)
            {
                var parameterName = arguments.ParameterName(i);
                var parameterInfo = arguments.GetParameterInfo(i);
                logger.Info(string.Format("  ARGUMENT: Name: {0}, Info: {1}", parameterName, parameterInfo));
            }

            // Invoke the next behavior in the chain.
            var invokeInterceptionBehaviorDelegate = getNext();
            IMethodReturn result = invokeInterceptionBehaviorDelegate(input, getNext);

            // After invoking the method on the original target.
            if (result.Exception != null)
            {
                string message = string.Format("EXCEPTION: {0} threw exception: {1}", input.MethodBase,
                    result.Exception.Message);
                logger.Error(message, result.Exception);
            }
            else
            {
                logger.Info(string.Format("FINISH: {0} ", input.MethodBase));
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
    }
}