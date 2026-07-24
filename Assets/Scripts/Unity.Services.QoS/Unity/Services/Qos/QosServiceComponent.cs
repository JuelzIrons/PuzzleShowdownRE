namespace Unity.Services.Qos
{
	internal class QosServiceComponent : global::Unity.Services.Qos.IQosServiceComponent, global::Unity.Services.Core.Internal.IServiceComponent
	{
		public global::Unity.Services.Qos.IQosService Service { get; }

		internal QosServiceComponent(global::Unity.Services.Qos.IQosService qos)
		{
			Service = qos;
		}
	}
}
