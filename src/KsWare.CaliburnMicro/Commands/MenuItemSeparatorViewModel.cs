namespace KsWare.CaliburnMicro.Commands
{
	public class MenuItemSeparatorViewModel : MenuItemViewModel
	{
		public MenuItemSeparatorViewModel() : base("---", null, null)
		{
			IsSeparator = true;
		}

	}
}