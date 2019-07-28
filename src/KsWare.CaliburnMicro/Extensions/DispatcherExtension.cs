using System;
using System.Windows.Threading;

namespace KsWare.CaliburnMicro.Extensions
{
	public static class DispatcherExtension
	{
		public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action callback, DispatcherPriority priority = DispatcherPriority.Normal)
			=> dispatcher.BeginInvoke(callback,priority);
	}
}
