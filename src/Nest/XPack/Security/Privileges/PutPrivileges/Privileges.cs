using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	[ReadAs(typeof(Privileges))]
	[JsonFormatter(typeof(PrivilegesFormatter))]
	public interface IPrivileges : IIsADictionary<string, IPrivilegesActions> { }

	public class Privileges : IsADictionaryBase<string, IPrivilegesActions>, IPrivileges
	{
		public Privileges() { }

		internal Privileges(IDictionary<string, IPrivilegesActions> backingDictionary) : base(backingDictionary) { }

		public void Add(string name, IPrivilegesActions actions) => BackingDictionary.Add(name, actions);
	}

	public class PrivilegesDescriptor : IsADictionaryDescriptorBase<PrivilegesDescriptor, IPrivileges, string, IPrivilegesActions>
	{
		public PrivilegesDescriptor() : base(new Privileges()) { }

		public PrivilegesDescriptor Privilege(string privilegesName, IPrivilegesActions actions) => Assign(privilegesName, actions);

		public PrivilegesDescriptor Privilege(string privilegesName, Func<PrivilegesActionsDescriptor, IPrivilegesActions> selector) =>
			Assign(privilegesName, selector?.Invoke(new PrivilegesActionsDescriptor()));
	}

	internal class PrivilegesFormatter : IJsonFormatter<IPrivileges>
	{
		public void Serialize(ref JsonWriter writer, IPrivileges value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<IDictionary<string, IPrivilegesActions>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		public IPrivileges Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			formatterResolver.GetFormatter<Privileges>().Deserialize(ref reader, formatterResolver);
	}
}
