using System;
using System.Windows;

namespace KsWare.CaliburnMicro.Common
{
    public interface IViewLocator
    {
        UIElement GetOrCreateViewType(Type viewType);
    }
}