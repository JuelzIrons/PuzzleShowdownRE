namespace Unity.Services.Multiplayer
{
	public abstract class BaseSessionOptions
	{
		public string Type { get; set; } = global::System.Guid.NewGuid().ToString();

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> PlayerProperties { get; set; } = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty>();

		internal global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Services.Multiplayer.IModuleOption> Options { get; set; } = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Services.Multiplayer.IModuleOption>();

		internal bool HasOption<T>() where T : class, global::Unity.Services.Multiplayer.IModuleOption
		{
			return Options.ContainsKey(typeof(T));
		}

		internal T GetOption<T>() where T : class, global::Unity.Services.Multiplayer.IModuleOption
		{
			if (!Options.ContainsKey(typeof(T)))
			{
				return null;
			}
			return Options[typeof(T)] as T;
		}
	}
}
