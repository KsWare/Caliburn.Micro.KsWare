using System;
using System.ComponentModel.Composition;

namespace KsWare.CaliburnMicro.Common
{
	public class MefServiceLocator : IServiceLocator
	{

		internal readonly DebugCompositionContainer container;

		public MefServiceLocator(DebugCompositionContainer container)
		{
			this.container = container;
		}

		public T GetInstance<T>()
		{
			try
			{
				var instance = container.GetExportedValue<T>();
				return instance;
			}
			catch (Exception ex)
			{
				throw new CompositionException(
					$"Could not locate any instances of contract {typeof(T)}.\nTrace\n{container.GetFailedExportsTrace()}",ex);
			}
		}
	}
}