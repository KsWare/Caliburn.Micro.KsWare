using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using KsWare.CaliburnMicro.Common;

namespace KsWare.CaliburnMicro.Commands
{
	public abstract class UserCommand : ICommand, IPartImportsSatisfiedNotification
	{
		private event EventHandler _canExecuteChanged;

		public abstract void Do();

		public void Undo() { throw new InvalidOperationException(); }

		public bool CanUndo { get; set; }

		bool ICommand.CanExecute(object parameter) => CanDo();

		void ICommand.Execute(object parameter) => Do();

		event EventHandler ICommand.CanExecuteChanged { add => _canExecuteChanged += value; remove => _canExecuteChanged -= value; }

		void IPartImportsSatisfiedNotification.OnImportsSatisfied() => OnImportsSatisfied();

		protected virtual void OnImportsSatisfied() { }

		protected virtual bool CanDo() => true;
	}

	public abstract class UserCommand<TCommandParameter> : UserCommand
	{
		[Import] private ParameterImport _parameterImport;
		public TCommandParameter Parameter { get; set; }

		
		protected override void OnImportsSatisfied()
		{
			Parameter = _parameterImport.Get<TCommandParameter>("Parameter");
		}
	}
}