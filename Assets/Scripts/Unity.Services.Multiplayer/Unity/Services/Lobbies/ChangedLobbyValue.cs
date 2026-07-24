namespace Unity.Services.Lobbies
{
	public struct ChangedLobbyValue<T>
	{
		public T Value { get; }

		public bool Changed { get; }

		public bool Added { get; internal set; }

		public ChangedLobbyValue(T value)
		{
			Value = value;
			Changed = true;
			Added = false;
		}
	}
}
