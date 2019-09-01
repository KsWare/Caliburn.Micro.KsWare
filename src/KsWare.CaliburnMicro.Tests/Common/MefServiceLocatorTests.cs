using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using KsWare.CaliburnMicro.Common;
using NUnit.Framework;

namespace KsWare.CaliburnMicro.Tests.Common
{
	[TestFixture]
	public class MefServiceLocatorTests
	{
		private AggregateCatalog _catalog;
		private DebugCompositionContainer _container;
		private MefServiceLocator sut;

		[SetUp]
		public void Setup()
		{
			_catalog = new AggregateCatalog();
			_container = new DebugCompositionContainer(_catalog);

			_catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

			var batch = new CompositionBatch();
			batch.AddExportedValue(new ClassA());
			_container.Compose(batch);

			sut = new MefServiceLocator(_container);
		}

		[Test]
		public void GetInstance()
		{
			var classA = sut.GetInstance<ClassA>();
			var classB = sut.GetInstance<ClassB>();
		}

//		[Test]
//		public void GetInstance_WithParameter1()
//		{
//			var batch = new CompositionBatch();
//			batch.AddExportedValue("Parameter", new ClassD());
//			_container.Compose(batch);
//
//			var classC = sut.GetInstance<ClassC>();
//		}

//		[Test]
//		public void GetInstance_WithParameter2()
//		{
//			var parameter = new Dictionary<string, object> {{"Parameter", new ClassD()}};
//
//			var batch = new CompositionBatch();
//			foreach (var p in parameter) batch.AddExportedValue(p.Key, p.Value);
//			_container.Compose(batch);
//
//			var classC = sut.GetInstance<ClassC>();
//		}

//		[Test]
//		public void GetInstance_WithParameterArray()
//		{
//			// step1
//			var d1 = new ClassD();
//			var classC1 = sut.GetInstance<ClassC>(new Dictionary<string, object>{{ "Parameter", d1 } });
//
//			// step2
//			var d2 = new ClassD();
//			var classC2 = sut.GetInstance<ClassC>(new Dictionary<string, object> { { "Parameter", d2 } });
//
//			Assert.That(classC1, Is.Not.EqualTo(classC2));
//			Assert.That(classC1.Parameter, Is.EqualTo(d1));
//			Assert.That(classC2.Parameter, Is.EqualTo(d2));
//		}

//		[Test]
//		public void GetInstance_With1Parameter()
//		{
//			var d1 = new ClassD();
//			var classC1 = sut.GetInstance<ClassC, ClassD>("Parameter", d1);
//			Assert.That(classC1.Parameter, Is.EqualTo(d1));
//
//			var d2 = new ClassD();
//			var classC2 = sut.GetInstance<ClassC, ClassD>("Parameter", d2);
//
//			Assert.That(classC1,Is.Not.EqualTo(classC2));
//			Assert.That(classC1.Parameter, Is.EqualTo(d1));
//			Assert.That(classC2.Parameter, Is.EqualTo(d2));
//		}

		public class ClassA
		{

		}

		[Export]
		public class ClassB
		{
			[Import]
			public ClassA ClassA { get; set; }
		}

		[Export,PartCreationPolicy(CreationPolicy.NonShared)]
		public class ClassC
		{
			[Import("Parameter", AllowRecomposition = true)]
			public ClassD Parameter { get; set; }
		}

		public class ClassD
		{

		}
	}
}