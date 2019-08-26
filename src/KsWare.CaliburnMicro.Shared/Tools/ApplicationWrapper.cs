using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace KsWare.CaliburnMicro.Tools
{
	public static class ApplicationWrapper
	{
		public static Dispatcher Dispatcher
		{
			get
			{
				if (Application.Current != null)
				{
					return Application.Current.Dispatcher;
				}
				else
				{
					throw new NotImplementedException();
				}
			}
		}

		public static IntPtr MainWindowHandle => new WindowInteropHelper(MainWindow).Handle;
		public static Window MainWindow => Application.Current.MainWindow;
	}
}
