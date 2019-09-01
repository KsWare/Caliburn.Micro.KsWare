using System;
using System.ComponentModel.Composition;

namespace KsWare.CaliburnMicro.Common
{
	public static class MefServiceLocatorParameterExtension
	{
		public static T GetInstance<T>(this IServiceLocator serviceLocator, string p1Name, object p1)
		{
			var container = ((MefServiceLocator)serviceLocator).container;
			var parameterImport = container.GetExportedValue<ParameterImport>();
			parameterImport.Set(p1Name, p1);
			try
			{
				var instance = container.GetExportedValue<T>();
				return instance;
			}
			finally
			{
				parameterImport.Remove<T>(p1Name);
			}
		}

		public static T CreateInstanceDirect<T>(this IServiceLocator serviceLocator, params object[] args)
		{ //DRAFT
			var container = ((MefServiceLocator)serviceLocator).container;
			var instance = (T)Activator.CreateInstance(typeof(T), args);
			container.ComposeParts(instance);
			return instance;
		}
	}
}
