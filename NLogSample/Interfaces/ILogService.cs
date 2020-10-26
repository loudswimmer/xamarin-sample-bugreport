using System;
using System.Reflection;

namespace NLogSample.Interfaces
{
    public interface ILogService
    {
        void Initialize(Assembly assembly, string assemblyName);
    }
}
