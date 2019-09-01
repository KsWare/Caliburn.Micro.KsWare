using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using KsWare.CaliburnMicro.Extensions;

namespace KsWare.CaliburnMicro.Common
{
	/// <summary>
	/// Class ParameterImport.
	/// </summary>
	/// <example>
	/// <code language="csharp">
	///	public class MyClass&lt;TParameter&gt; : IPartImportsSatisfiedNotification
	///	{
	///		[Import] private ParameterImport _parameterImport;
	///		public TParameter Parameter { get; set; }
	///
	///		void IPartImportsSatisfiedNotification.OnImportsSatisfied()
	///		{
	///			Parameter = _parameterImport.Get&lt;TParameter&gt;("MyParameterName");
	///		}
	///	}
	/// </code>
	/// </example>
	[Export, PartCreationPolicy(CreationPolicy.Shared)]
	public class ParameterImport
	{
		public IDictionary<string, object> Parameter { get; set; } = new Dictionary<string, object>();

		public void Set<TParameter>(string parameterName, TParameter value)
		{
			var key = BuildKey(typeof(TParameter), parameterName);

			if (Parameter.ContainsKey(key))
				Parameter[key] = value;
			else
				Parameter.Add(key, value);
		}

		public void Remove<TParameter>(string parameterName)
		{
			var key = BuildKey(typeof(TParameter), parameterName);
			Parameter.Remove(key);
		}

		public TParameter Get<TParameter>(string parameterName)
		{
			var key = BuildKey(typeof(TParameter), parameterName);
			if (Parameter.TryGetValue(key, out var value) && value is TParameter parameter)
				return parameter;
			return default;
		}

		internal static string BuildKey(Type type, string parameterName)
		{
			var key = $"{parameterName}{{{type.GenerateFullTypeName()}}}";
			return key;
		}
	}
}