namespace Unity.Services.Wire.Internal
{
	internal interface IBackoffStrategy
	{
		float GetNext();

		void Reset();
	}
}
