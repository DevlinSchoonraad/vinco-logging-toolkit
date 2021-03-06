﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;
using System.Threading;


namespace Elmah.Everywhere.Appenders
{
    public class DetailAppender : BaseAppender
    {
        public override void Append(ErrorInfo errorInfo)
        {
            Assembly assembly = errorInfo.GetType().Assembly;
           
            var pairs = new Dictionary<string, string>();
            pairs.Add("Date", DateTime.Now.ToString());
            pairs.Add("Culture", CultureInfo.CurrentCulture.Name);

#if !SILVERLIGHT

            pairs.Add("User", string.Format(CultureInfo.InvariantCulture, @"{0}\{1}", Environment.UserDomainName, Environment.UserName).Trim('\\'));
            pairs.Add("Machine Name", Environment.MachineName);
            pairs.Add("App Start Time", Process.GetCurrentProcess().StartTime.ToLocalTime().ToString(CultureInfo.InvariantCulture));
            pairs.Add("App Up Time", (DateTime.Now - Process.GetCurrentProcess().StartTime.ToLocalTime()).ToString());
            pairs.Add("Worker process", GetWorkerProcess());
            pairs.Add("AppDomain", AppDomainDetail(AppDomain.CurrentDomain));
            pairs.Add("Deployment", (assembly.GlobalAssemblyCache) ? "GAC" : "bin");

#endif
            pairs.Add("Thread Id", Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture));
            pairs.Add("Full Name", new AssemblyName(assembly.FullName).FullName);
            pairs.Add("Operating System Version", Environment.OSVersion.ToString());
            pairs.Add("Common Language Runtime Version", Environment.Version.ToString());
            pairs.Add("Elmah.Everywhere Version", new AssemblyName(typeof(Diagnostics.ExceptionHandler).Assembly.FullName).Version.ToString());

            errorInfo.AddDetail(this.Name, pairs);
        }

#if !SILVERLIGHT

        private static string AppDomainDetail(AppDomain appDomain)
        {
            PropertyInfo propertyInfoHomogenous = typeof(AppDomain).GetProperty("IsHomogenous");
            if (propertyInfoHomogenous == null)
            {
                return "unknown";
            }
            var de = (Func<bool>) Delegate.CreateDelegate(typeof (Func<bool>), appDomain, propertyInfoHomogenous.GetGetMethod());
            string str = string.Format(CultureInfo.InvariantCulture, "Homogenous = {0}", de());
            return str;
        }

        internal static string GetWorkerProcess()
        {
            string processName = "Unknown";
            object currentProcess = typeof(Process).GetMethod("GetCurrentProcess", Type.EmptyTypes).Invoke(null, null);
            object processModule = typeof(Process).GetProperty("MainModule").GetValue(currentProcess, null);
            processName = (string)typeof(ProcessModule).GetProperty("ModuleName").GetValue(processModule, null);
            return processName;
        }

#endif

        public override int Order
        {
            get { return 1; }
        }

        public override string Name
        {
            get { return "Detail Appender"; }
        }
    }
}
