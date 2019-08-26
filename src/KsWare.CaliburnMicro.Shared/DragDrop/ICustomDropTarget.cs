using System.Windows;

namespace KsWare.CaliburnMicro.DragDrop
{
	public interface ICustomDropTarget
	{
		void OnDrop(object sender, DragEventArgs dragEventArgs);
		void OnDragEnter(object sender, DragEventArgs dragEventArgs);
		void OnGiveFeedback(object sender, GiveFeedbackEventArgs dragEventArgs);
		void OnDragOver(object sender, DragEventArgs dragEventArgs);
		void OnDragLeave(object sender, DragEventArgs dragEventArgs);
	}
}