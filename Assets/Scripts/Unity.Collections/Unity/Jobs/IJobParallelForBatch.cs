namespace Unity.Jobs
{
	[global::Unity.Jobs.LowLevel.Unsafe.JobProducerType(typeof(global::Unity.Jobs.IJobParallelForBatchExtensions.JobParallelForBatchProducer<>))]
	public interface IJobParallelForBatch
	{
		void Execute(int startIndex, int count);
	}
}
