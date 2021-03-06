﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface ITrimProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }
	}

	public class TrimProcessor : ProcessorBase, ITrimProcessor
	{
		public Field Field { get; set; }
		protected override string Name => "trim";
	}

	public class TrimProcessorDescriptor<T>
		: ProcessorDescriptorBase<TrimProcessorDescriptor<T>, ITrimProcessor>, ITrimProcessor
		where T : class
	{
		protected override string Name => "trim";

		Field ITrimProcessor.Field { get; set; }

		public TrimProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public TrimProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);
	}
}
