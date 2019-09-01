using System;
using System.ComponentModel.Composition;

namespace KsWare.CaliburnMicro.Common
{
	public static class IServiceLocatorExtension
	{
		public static T GetInstance<T>(this IServiceLocator serviceLocator, Action<T> init)
		{
			var instance = serviceLocator.GetInstance<T>();
			init?.Invoke(instance);
			return instance;
		}
	}
}
