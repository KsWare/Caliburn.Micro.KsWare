namespace KsWare.CaliburnMicro.Common
{
	public delegate void StartupTask();

	public class StartupTasksBase
	{
		protected readonly IServiceLocator ServiceLocator;

		public StartupTasksBase(IServiceLocator serviceLocator)
		{
			ServiceLocator = serviceLocator;
		}
	}
}