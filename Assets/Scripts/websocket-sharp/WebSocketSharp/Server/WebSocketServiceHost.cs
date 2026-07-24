namespace WebSocketSharp.Server
{
	public abstract class WebSocketServiceHost
	{
		private global::WebSocketSharp.Logger _log;

		private string _path;

		private global::WebSocketSharp.Server.WebSocketSessionManager _sessions;

		internal global::WebSocketSharp.Server.ServerState State => _sessions.State;

		protected global::WebSocketSharp.Logger Log => _log;

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

		public global::WebSocketSharp.Server.WebSocketSessionManager Sessions => _sessions;

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

		protected WebSocketServiceHost(string path, global::WebSocketSharp.Logger log)
		{
			_path = path;
			_log = log;
			_sessions = new global::WebSocketSharp.Server.WebSocketSessionManager(log);
		}

		internal void Start()
		{
			_sessions.Start();
		}

		internal void StartSession(global::WebSocketSharp.Net.WebSockets.WebSocketContext context)
		{
			CreateSession().Start(context, _sessions);
		}

		internal void Stop(ushort code, string reason)
		{
			_sessions.Stop(code, reason);
		}

		protected abstract global::WebSocketSharp.Server.WebSocketBehavior CreateSession();
	}
	internal class WebSocketServiceHost<TBehavior> : global::WebSocketSharp.Server.WebSocketServiceHost where TBehavior : global::WebSocketSharp.Server.WebSocketBehavior, new()
	{
		private global::System.Func<TBehavior> _creator;

		public override global::System.Type BehaviorType => typeof(TBehavior);

		internal WebSocketServiceHost(string path, global::System.Action<TBehavior> initializer, global::WebSocketSharp.Logger log)
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

		protected override global::WebSocketSharp.Server.WebSocketBehavior CreateSession()
		{
			return _creator();
		}
	}
}
