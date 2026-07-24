namespace Unity.Multiplayer.Tools.NetworkProfiler.Runtime
{
	internal interface ICounterFactory
	{
		global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounter Construct(string name);
	}
}
