using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using KsWare.Presentation.StaticWrapper;

namespace KsWare.CaliburnMicro.Common {
	public class BootstrapperBase : Caliburn.Micro.BootstrapperBase
	{
		protected DebugCompositionContainer Container;
		private bool _isApplicationDirectoryComposed;
		private bool _isCommonComposed;

		public BootstrapperBase()
		{
			Initialize();
		}

		protected override void BuildUp(object instance)
		{
			Container.SatisfyImportsOnce(instance);
		}

		protected override void Configure()
		{
			if (Container == null)
			{
				var catalog = new AggregateCatalog();
				Container = new DebugCompositionContainer(catalog);
			}

			ComposeApplicationDirectory();
			ComposeCommon();
			

			//TODO?? ConventionManager.AddElementConvention<MenuItem>(ItemsControl.ItemsSourceProperty, "DataContext", "Click");
			//TODO add mapping convention to support 'VM' suffix
		}

		protected virtual void ComposeCommon()
		{
			if (_isCommonComposed) return;
			var batch = new CompositionBatch();
			batch.AddExportedValue<IWindowManager>(new WindowManager());
			batch.AddExportedValue<IEventAggregator>(new EventAggregator());
			batch.AddExportedValue<IApplication>(AssemblyBootstrapper.ApplicationWrapper);
			batch.AddExportedValue<IApplicationDispatcher>(AssemblyBootstrapper.ApplicationDispatcher);
			//batch.AddExportedValue(_container); // DISABLED Warning: A CompositionContainer should never import itself, or a part that has a reference to it. Such a reference could allow an untrusted part to gain access all the parts in the container.
			batch.AddExportedValue<IServiceLocator>(new MefServiceLocator(Container));
			batch.AddExportedValue(Container.Catalog);
			Container.Compose(batch);
			_isCommonComposed = true;
		}

		protected virtual void ComposeApplicationDirectory() 
		{
			if(_isApplicationDirectoryComposed) return;
			var catalog = (AggregateCatalog)Container.Catalog;
			var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
			var dir = new DirectoryInfo(Path.GetDirectoryName(assembly.Location));
			foreach (var file in dir.GetFiles("*.dll").Concat(dir.GetFiles("*.exe")))
			{
				assembly = Assembly.LoadFile(file.FullName);
				byte[] assemblykey = assembly.GetName().GetPublicKey();
				Debug.WriteLine($"Compose: {assembly.GetName().FullName}");
				catalog.Catalogs.Add(new AssemblyCatalog(assembly));
			}

			_isApplicationDirectoryComposed = true;
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

		protected override object GetInstance(Type serviceType, string key)
		{
			var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
			var exports = Container.GetExportedValues<object>(contract);

			if (exports.Any())
			{
				return exports.First();
			}

			throw new Exception($"Could not locate any instances of contract {contract}.\nTrace:\n{Container.GetFailedExportsTrace()}");
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			var startupTasks =
				GetAllInstances(typeof(StartupTask))
					.Cast<ExportedDelegate>()
					.Select(exportedDelegate => (StartupTask)exportedDelegate.CreateDelegate(typeof(StartupTask)));

			startupTasks.Apply(s => s());

			DisplayRootViewFor<IShell>();
		}
	}
}