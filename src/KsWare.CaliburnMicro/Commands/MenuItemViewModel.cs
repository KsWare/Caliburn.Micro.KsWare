using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Action = System.Action;

namespace KsWare.CaliburnMicro.Commands
{
	public class MenuItemViewModel : UiCommandViewModel, IMenuItemViewModel
	{
		private string _toolTip;
		private BindableCollection<IMenuItemViewModel> _subItems;

		private MenuItemViewModel() : base(null, null, null)
		{
		}

		public MenuItemViewModel(string displayName) : this(displayName, new IMenuItemViewModel[0])
		{
			_subItems = new BindableCollection<IMenuItemViewModel>();
		}

		public MenuItemViewModel(string displayName, IEnumerable<IMenuItemViewModel> subItems) : base(displayName, null, null)
		{
			
			if (subItems is BindableCollection<IMenuItemViewModel> bindableCollection)
				_subItems = bindableCollection;
			else
				_subItems = new BindableCollection<IMenuItemViewModel>(subItems);
		}

		public MenuItemViewModel(string displayName, Action executeCallback, Func<bool> canExecuteCallback = null) : base(displayName, executeCallback, canExecuteCallback)
		{
		}

		public MenuItemViewModel(string displayName, Func<Task> executeCallback, Func<bool> canExecuteCallback = null) : base(displayName, executeCallback, canExecuteCallback)
		{
		}

		public MenuItemViewModel(string displayName, ICommand command) : base(displayName, command)
		{
		}



		public bool IsSeparator { get; protected set; }
		public string ToolTip { get => _toolTip; set => Set(ref _toolTip, value);}

		public BindableCollection<IMenuItemViewModel> SubItems { get => _subItems; set => Set(ref _subItems, value);}
	}
}