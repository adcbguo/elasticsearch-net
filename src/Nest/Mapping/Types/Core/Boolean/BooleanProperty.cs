using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IBooleanProperty : IDocValuesProperty
	{
		[DataMember(Name = "boost")]
		double? Boost { get; set; }

		[DataMember(Name = "fielddata")]
		INumericFielddata Fielddata { get; set; }

		[DataMember(Name = "index")]
		bool? Index { get; set; }

		[DataMember(Name = "null_value")]
		bool? NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BooleanProperty : DocValuesPropertyBase, IBooleanProperty
	{
		public BooleanProperty() : base(FieldType.Boolean) { }

		public double? Boost { get; set; }
		public INumericFielddata Fielddata { get; set; }

		public bool? Index { get; set; }
		public bool? NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BooleanPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<BooleanPropertyDescriptor<T>, IBooleanProperty, T>, IBooleanProperty
		where T : class
	{
		public BooleanPropertyDescriptor() : base(FieldType.Boolean) { }

		double? IBooleanProperty.Boost { get; set; }
		INumericFielddata IBooleanProperty.Fielddata { get; set; }
		bool? IBooleanProperty.Index { get; set; }
		bool? IBooleanProperty.NullValue { get; set; }

		public BooleanPropertyDescriptor<T> Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public BooleanPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		public BooleanPropertyDescriptor<T> NullValue(bool? nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);

		public BooleanPropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(selector(new NumericFielddataDescriptor()), (a, v) => a.Fielddata = v);
	}
}
