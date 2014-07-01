using System;
using System.Collections.Generic;
using System.Diagnostics;
using log4net;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityAopSpike.Core.Interceptors
{
    public class TimingInterceptionBehavior : IInterceptionBehavior
    {
        /// <summary>
        ///     Invokes this interceptor in the interceptor chain.
        /// </summary>
        /// <param name="input">The captured method invocation.</param>
        /// <param name="getNext">The next interceptor in the chain.</param>
        /// <returns>The method invocation result.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var logger = LogManager.GetLogger(typeof (TimingInterceptionBehavior));
            var stopwatch = Stopwatch.StartNew();

            // Invoke the next behavior in the chain.
            IMethodReturn result = getNext()(input, getNext);

            stopwatch.Stop();
            logger.Info(string.Format("TIMING: {0} milliseconds elapsed during method execution: {1}", 
                stopwatch.ElapsedMilliseconds, input.MethodBase));
            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}