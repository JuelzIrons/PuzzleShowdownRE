namespace Unity.Jobs
{
	[global::Unity.Jobs.LowLevel.Unsafe.JobProducerType(typeof(global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<>))]
	public interface IJobFilter
	{
		bool Execute(int index);
	}
}
