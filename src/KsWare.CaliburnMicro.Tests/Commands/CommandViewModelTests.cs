using System;
using System.Threading.Tasks;
using System.Windows.Input;
using KsWare.CaliburnMicro.Commands;
using Moq;
using NUnit.Framework;

namespace KsWare.CaliburnMicro.Tests.Commands
{
	[TestFixture]
	public class CommandViewModelTests
	{
		[Test]
		public void Constructor_Callback()
		{
			var actionInvoked=0;
			var canExecuteInvoked=0;
			var action = new Action(() => actionInvoked++);
			var canExecuteFunc = new Func<bool>(() => {canExecuteInvoked++; return true;});

			var sut = new CommandViewModel(action, canExecuteFunc);

			Assert.That(sut.CanExecute(null), Is.EqualTo(true));
			Assert.That(canExecuteInvoked,Is.EqualTo(1));
			sut.Execute(null);
			Assert.That(actionInvoked, Is.EqualTo(1));
		}

		[Test]
		public async Task Constructor_AsyncCallback()
		{
			var actionInvoked = 0;
			var canExecuteInvoked = 0;
			var action = new Func<Task>(() => { actionInvoked++; return Task.CompletedTask;});
			var canExecuteFunc = new Func<bool>(() => { canExecuteInvoked++; return true; });

			var sut = new CommandViewModel(action, canExecuteFunc);

			Assert.That(sut.CanExecute(null), Is.EqualTo(true));
			Assert.That(canExecuteInvoked, Is.EqualTo(1));
			await sut.ExecuteAsync(null).ConfigureAwait(false);
			Assert.That(actionInvoked, Is.EqualTo(1));
		}

		[Test]
		public void Constructor_Command()
		{
			var commandMock = new Mock<ICommand>();
			commandMock.Setup(c => c.CanExecute(null)).Returns(true);

			var sut = new CommandViewModel(commandMock.Object);

			Assert.That(sut.CanExecute(null), Is.EqualTo(true));
			commandMock.Verify(c => c.CanExecute(null), Times.AtLeastOnce);
			sut.Execute(null);
			commandMock.Verify(c => c.Execute(null), Times.Exactly(1));
		}
	}
}