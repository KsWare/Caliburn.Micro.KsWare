using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using KsWare.CaliburnMicro.Common;
using NUnit.Framework;

namespace KsWare.CaliburnMicro.Tests.Common
{
	[TestFixture]
	public class ParameterImportTests
	{
		private AggregateCatalog _catalog;
		private DebugCompositionContainer _container;
		private IServiceLocator _serviceLocator;

		[SetUp]
		public void Setup()
		{
			_catalog = new AggregateCatalog();
			_container = new DebugCompositionContainer(_catalog);
			_catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
			_catalog.Catalogs.Add(new AssemblyCatalog(typeof(ParameterImport).Assembly));
			_serviceLocator = new MefServiceLocator(_container);
		}

		[Test]
		public void GetInstance()
		{
			var c = _serviceLocator.GetInstance<MyClass<object>>("MyParameterName", "test");
			Assert.That(c.Parameter,Is.EqualTo("test"));
		}

		[Export,PartCreationPolicy(CreationPolicy.NonShared)]
		public class MyClass<TParameter> : IPartImportsSatisfiedNotification
		{
			[Import] private ParameterImport _parameterImport;
			public TParameter Parameter { get; set; }
		
			void IPartImportsSatisfiedNotification.OnImportsSatisfied()
			{
				Parameter = _parameterImport.Get<TParameter>("MyParameterName");
			}
		}		
	}
}