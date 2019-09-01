using System;
using System.Diagnostics.Contracts;
using System.Text;
using JetBrains.Annotations;

namespace KsWare.CaliburnMicro.Extensions
{
	public static class TypeExtension
	{
		/// <summary>
		/// Generates the full name of the type for use in code (C#).
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>System.String.</returns>
		/// <example>
		/// <c>IEnumerable&lt;Action&gt;</c> generates <c>System.Collections.Generic.IEnumerable&lt;System.Action&gt;</c>
		/// in contrast to <see cref="Type.FullName"/> which would return <c>System.Collections.Generic.IEnumerable`1[[System.Action, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]</c>.
		/// </example>
		public static string GenerateFullTypeName([NotNull] this Type type)
		{
			Contract.Requires(type != null);
			
			var retType = new StringBuilder();

			if (type.IsGenericType)
			{
				var parentType = type.FullName.Split('`');
				var arguments = type.GetGenericArguments();

				var argList = new StringBuilder();
				foreach (var t in arguments)
				{
					var arg = GenerateFullTypeName(t);
					if (argList.Length > 0)
					{
						argList.AppendFormat(", {0}", arg);
					}
					else
					{
						argList.Append(arg);
					}
				}

				if (argList.Length > 0)
				{
					retType.AppendFormat("{0}<{1}>", parentType[0], argList);
				}
			}
			else
			{
				return type.FullName;
			}

			return retType.ToString();
		}

	}
}
