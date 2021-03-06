﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IGsubProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="pattern")]
		string Pattern { get; set; }

		[DataMember(Name ="replacement")]
		string Replacement { get; set; }
	}

	public class GsubProcessor : ProcessorBase, IGsubProcessor
	{
		public Field Field { get; set; }

		public string Pattern { get; set; }

		public string Replacement { get; set; }
		protected override string Name => "gsub";
	}

	public class GsubProcessorDescriptor<T>
		: ProcessorDescriptorBase<GsubProcessorDescriptor<T>, IGsubProcessor>, IGsubProcessor
		where T : class
	{
		protected override string Name => "gsub";

		Field IGsubProcessor.Field { get; set; }
		string IGsubProcessor.Pattern { get; set; }
		string IGsubProcessor.Replacement { get; set; }

		public GsubProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public GsubProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public GsubProcessorDescriptor<T> Pattern(string pattern) => Assign(pattern, (a, v) => a.Pattern = v);

		public GsubProcessorDescriptor<T> Replacement(string replacement) => Assign(replacement, (a, v) => a.Replacement = v);
	}
}
