namespace Newtonsoft.Json.Converters
{
	internal class XProcessingInstructionWrapper : global::Newtonsoft.Json.Converters.XObjectWrapper
	{
		private global::System.Xml.Linq.XProcessingInstruction ProcessingInstruction => (global::System.Xml.Linq.XProcessingInstruction)base.WrappedNode;

		public override string? LocalName => ProcessingInstruction.Target;

		public override string? Value
		{
			get
			{
				return ProcessingInstruction.Data;
			}
			set
			{
				ProcessingInstruction.Data = value ?? string.Empty;
			}
		}

		public XProcessingInstructionWrapper(global::System.Xml.Linq.XProcessingInstruction processingInstruction)
			: base(processingInstruction)
		{
		}
	}
}
