using System.ComponentModel.Composition;
using System.Windows;

namespace KsWare.CaliburnMicro.Common
{
	[Export(typeof(IThemeManager))]
	public class ThemeManager : IThemeManager
	{
		private readonly ResourceDictionary _themeResources;

		public ThemeManager()
		{
			_themeResources = new ResourceDictionary
			{
				//TODO themes
//				Source = new Uri("pack://application:,,,/PhotoManager;component/Resources/Theme1.xaml")
			};
		}

		public ResourceDictionary GetThemeResources()
		{
			return _themeResources;
		}
	}
}