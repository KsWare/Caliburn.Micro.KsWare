namespace KsWare.CaliburnMicro.Common
{
	public interface IDataContextObserver
	{
		void OnDataContextAssigned();
		void OnDataContextReleased(string reason);
	}
}