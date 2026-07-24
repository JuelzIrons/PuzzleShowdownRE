namespace UnityWebSocketSharp.Server
{
	internal abstract class WebSocketServiceHost
	{
		private global::UnityWebSocketSharp.Logger _log;

		private string _path;

		private global::UnityWebSocketSharp.Server.WebSocketSessionManager _sessions;

		internal global::UnityWebSocketSharp.Server.ServerState State => _sessions.State;

		protected global::UnityWebSocketSharp.Logger Log => _log;

		public bool KeepClean
		{
			get
			{
				return _sessions.KeepClean;
			}
			set
			{
				_sessions.KeepClean = value;
			}
		}

		public string Path => _path;

		public global::UnityWebSocketSharp.Server.WebSocketSessionManager Sessions => _sessions;

		public abstract global::System.Type BehaviorType { get; }

		public global::System.TimeSpan WaitTime
		{
			get
			{
				return _sessions.WaitTime;
			}
			set
			{
				_sessions.WaitTime = value;
			}
		}

		protected WebSocketServiceHost(string path, global::UnityWebSocketSharp.Logger log)
		{
			_path = path;
			_log = log;
			_sessions = new global::UnityWebSocketSharp.Server.WebSocketSessionManager(log);
		}

		internal void Start()
		{
			_sessions.Start();
		}

		internal void StartSession(global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext context)
		{
			CreateSession().Start(context, _sessions);
		}

		internal void Stop(ushort code, string reason)
		{
			_sessions.Stop(code, reason);
		}

		protected abstract global::UnityWebSocketSharp.Server.WebSocketBehavior CreateSession();
	}
	internal class WebSocketServiceHost<TBehavior> : global::UnityWebSocketSharp.Server.WebSocketServiceHost where TBehavior : global::UnityWebSocketSharp.Server.WebSocketBehavior, new()
	{
		private global::System.Func<TBehavior> _creator;

		public override global::System.Type BehaviorType => typeof(TBehavior);

		internal WebSocketServiceHost(string path, global::System.Action<TBehavior> initializer, global::UnityWebSocketSharp.Logger log)
			: base(path, log)
		{
			_creator = createSessionCreator(initializer);
		}

		private static global::System.Func<TBehavior> createSessionCreator(global::System.Action<TBehavior> initializer)
		{
			if (initializer == null)
			{
				return () => new TBehavior();
			}
			return delegate
			{
				TBehavior val = new TBehavior();
				initializer(val);
				return val;
			};
		}

		protected override global::UnityWebSocketSharp.Server.WebSocketBehavior CreateSession()
		{
			return _creator();
		}
	}
}
