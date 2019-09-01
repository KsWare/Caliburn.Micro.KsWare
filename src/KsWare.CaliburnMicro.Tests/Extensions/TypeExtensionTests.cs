using System;
using System.Diagnostics;
using KsWare.CaliburnMicro.Extensions;
using NUnit.Framework;

namespace KsWare.CaliburnMicro.Tests.Extensions
{
	[TestFixture]
	public class TypeExtensionTests
	{
		[TestCase(typeof(System.Action), "System.Action")]
		[TestCase(typeof(System.Collections.Generic.IEnumerable<System.Action>), "System.Collections.Generic.IEnumerable<System.Action>")]
		[TestCase(typeof(System.Collections.Generic.IEnumerable<System.Action<System.Boolean>>), "System.Collections.Generic.IEnumerable<System.Action<System.Boolean>>")]
		[TestCase(typeof(System.Collections.Generic.Dictionary<System.String, System.Object>), "System.Collections.Generic.Dictionary<System.String, System.Object>")]
		[TestCase(typeof(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<System.Action<System.Boolean>>>), "System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<System.Action<System.Boolean>>>")]
		[TestCase(typeof(System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.IEnumerable<System.Action<System.Boolean>>>), "System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.IEnumerable<System.Action<System.Boolean>>>")]

		public void GenerateFullTypeNameTest(Type type, string expectedResult)
		{
			Assert.That(type.GenerateFullTypeName(), Is.EqualTo(expectedResult));
		}

		[TestCase(typeof(System.Collections.Generic.IEnumerable<System.Action>), "System.Collections.Generic.IEnumerable`1[[System.Action, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")]
		public void FullName(Type type, string expectedResult)
		{
			Debug.WriteLine(type.FullName);
			Assert.That(type.FullName, Is.EqualTo(expectedResult));
		}
	}
}