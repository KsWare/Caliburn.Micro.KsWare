using System.Windows;
using System.Windows.Interactivity;

namespace KsWare.CaliburnMicro.DragDrop
{
	public class DropTargetBehavior : Behavior<FrameworkElement>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.Drop+=AssociatedObject_Drop;
			AssociatedObject.DragEnter+=AssociatedObject_DragEnter;
			AssociatedObject.DragLeave+=AssociatedObject_DragLeave;
			AssociatedObject.DragOver+=AssociatedObject_DragOver;
			AssociatedObject.GiveFeedback+=AssociatedObject_GiveFeedback;

			AssociatedObject.AllowDrop = true;
		}

		private void AssociatedObject_GiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			if (AssociatedObject.DataContext is ICustomDropTarget dropTarget)
			{
				dropTarget.OnGiveFeedback(sender, e);
				return;
			}
		}

		private void AssociatedObject_DragOver(object sender, DragEventArgs e)
		{
			if (AssociatedObject.DataContext is ICustomDropTarget dropTarget)
			{
				dropTarget.OnDragOver(sender, e);
				return;
			}
		}

		private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
		{
			if (AssociatedObject.DataContext is ICustomDropTarget dropTarget)
			{
				dropTarget.OnDragLeave(sender, e);
				return;
			}
		}

		private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
		{
			if (AssociatedObject.DataContext is ICustomDropTarget dropTarget)
			{
				dropTarget.OnDragEnter(sender, e);
				return;
			}
		}

		private void AssociatedObject_Drop(object sender, DragEventArgs e)
		{
			if (AssociatedObject.DataContext is ICustomDropTarget dropTarget)
			{
				dropTarget.OnDrop(sender, e);
				return;
			}
		}
	}
}
