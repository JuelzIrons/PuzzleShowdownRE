namespace Unity.Services.Qos.Runner
{
	internal interface IQosJob : global::Unity.Jobs.IJob
	{
		global::Unity.Collections.NativeArray<global::Unity.Networking.QoS.InternalQosResult> QosResults { get; }

		global::Unity.Jobs.JobHandle Schedule<T>(global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJob;

		void Dispose();
	}
}
