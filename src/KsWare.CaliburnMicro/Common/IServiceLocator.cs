namespace KsWare.CaliburnMicro.Common
{
    public interface IServiceLocator
    {
	    T GetInstance<T>();
    }
}