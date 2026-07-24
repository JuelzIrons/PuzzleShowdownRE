namespace Newtonsoft.Json.Linq
{
	public class JsonLoadSettings
	{
		private global::Newtonsoft.Json.Linq.CommentHandling _commentHandling;

		private global::Newtonsoft.Json.Linq.LineInfoHandling _lineInfoHandling;

		private global::Newtonsoft.Json.Linq.DuplicatePropertyNameHandling _duplicatePropertyNameHandling;

		public global::Newtonsoft.Json.Linq.CommentHandling CommentHandling
		{
			get
			{
				return _commentHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.Linq.CommentHandling.Ignore || value > global::Newtonsoft.Json.Linq.CommentHandling.Load)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_commentHandling = value;
			}
		}

		public global::Newtonsoft.Json.Linq.LineInfoHandling LineInfoHandling
		{
			get
			{
				return _lineInfoHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.Linq.LineInfoHandling.Ignore || value > global::Newtonsoft.Json.Linq.LineInfoHandling.Load)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_lineInfoHandling = value;
			}
		}

		public global::Newtonsoft.Json.Linq.DuplicatePropertyNameHandling DuplicatePropertyNameHandling
		{
			get
			{
				return _duplicatePropertyNameHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.Linq.DuplicatePropertyNameHandling.Replace || value > global::Newtonsoft.Json.Linq.DuplicatePropertyNameHandling.Error)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_duplicatePropertyNameHandling = value;
			}
		}

		public JsonLoadSettings()
		{
			_lineInfoHandling = global::Newtonsoft.Json.Linq.LineInfoHandling.Load;
			_commentHandling = global::Newtonsoft.Json.Linq.CommentHandling.Ignore;
			_duplicatePropertyNameHandling = global::Newtonsoft.Json.Linq.DuplicatePropertyNameHandling.Replace;
		}
	}
}
