namespace Unity.VisualScripting.FullSerializer
{
	public struct fsResult
	{
		private static readonly string[] EmptyStringArray = new string[0];

		private bool _success;

		private global::System.Collections.Generic.List<string> _messages;

		public static global::Unity.VisualScripting.FullSerializer.fsResult Success = new global::Unity.VisualScripting.FullSerializer.fsResult
		{
			_success = true
		};

		public bool Failed => !_success;

		public bool Succeeded => _success;

		public bool HasWarnings
		{
			get
			{
				if (_messages != null)
				{
					return global::System.Linq.Enumerable.Any(_messages);
				}
				return false;
			}
		}

		public global::System.Exception AsException
		{
			get
			{
				if (!Failed && !global::System.Linq.Enumerable.Any(RawMessages))
				{
					throw new global::System.Exception("Only a failed result can be converted to an exception");
				}
				return new global::System.Exception(FormattedMessages);
			}
		}

		public global::System.Collections.Generic.IEnumerable<string> RawMessages
		{
			get
			{
				if (_messages != null)
				{
					return _messages;
				}
				return EmptyStringArray;
			}
		}

		public string FormattedMessages => string.Join(",\n", global::System.Linq.Enumerable.ToArray(RawMessages));

		public void AddMessage(string message)
		{
			if (_messages == null)
			{
				_messages = new global::System.Collections.Generic.List<string>();
			}
			_messages.Add(message);
		}

		public void AddMessages(global::Unity.VisualScripting.FullSerializer.fsResult result)
		{
			if (result._messages != null)
			{
				if (_messages == null)
				{
					_messages = new global::System.Collections.Generic.List<string>();
				}
				_messages.AddRange(result._messages);
			}
		}

		public global::Unity.VisualScripting.FullSerializer.fsResult Merge(global::Unity.VisualScripting.FullSerializer.fsResult other)
		{
			_success = _success && other._success;
			if (other._messages != null)
			{
				if (_messages == null)
				{
					_messages = new global::System.Collections.Generic.List<string>(other._messages);
				}
				else
				{
					_messages.AddRange(other._messages);
				}
			}
			return this;
		}

		public static global::Unity.VisualScripting.FullSerializer.fsResult Warn(string warning)
		{
			return new global::Unity.VisualScripting.FullSerializer.fsResult
			{
				_success = true,
				_messages = new global::System.Collections.Generic.List<string> { warning }
			};
		}

		public static global::Unity.VisualScripting.FullSerializer.fsResult Fail(string warning)
		{
			return new global::Unity.VisualScripting.FullSerializer.fsResult
			{
				_success = false,
				_messages = new global::System.Collections.Generic.List<string> { warning }
			};
		}

		public static global::Unity.VisualScripting.FullSerializer.fsResult operator +(global::Unity.VisualScripting.FullSerializer.fsResult a, global::Unity.VisualScripting.FullSerializer.fsResult b)
		{
			return a.Merge(b);
		}

		public global::Unity.VisualScripting.FullSerializer.fsResult AssertSuccess()
		{
			if (Failed)
			{
				throw AsException;
			}
			return this;
		}

		public global::Unity.VisualScripting.FullSerializer.fsResult AssertSuccessWithoutWarnings()
		{
			if (Failed || global::System.Linq.Enumerable.Any(RawMessages))
			{
				throw AsException;
			}
			return this;
		}
	}
}
