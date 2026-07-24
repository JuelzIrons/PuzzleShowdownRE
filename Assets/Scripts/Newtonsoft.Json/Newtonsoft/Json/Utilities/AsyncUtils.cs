namespace Newtonsoft.Json.Utilities
{
	internal static class AsyncUtils
	{
		public static readonly global::System.Threading.Tasks.Task<bool> False = global::System.Threading.Tasks.Task.FromResult(result: false);

		public static readonly global::System.Threading.Tasks.Task<bool> True = global::System.Threading.Tasks.Task.FromResult(result: true);

		internal static readonly global::System.Threading.Tasks.Task CompletedTask = global::System.Threading.Tasks.Task.Delay(0);

		internal static global::System.Threading.Tasks.Task<bool> ToAsync(this bool value)
		{
			if (!value)
			{
				return False;
			}
			return True;
		}

		public static global::System.Threading.Tasks.Task? CancelIfRequestedAsync(this global::System.Threading.CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled();
		}

		public static global::System.Threading.Tasks.Task<T>? CancelIfRequestedAsync<T>(this global::System.Threading.CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled<T>();
		}

		public static global::System.Threading.Tasks.Task FromCanceled(this global::System.Threading.CancellationToken cancellationToken)
		{
			return new global::System.Threading.Tasks.Task(delegate
			{
			}, cancellationToken);
		}

		public static global::System.Threading.Tasks.Task<T> FromCanceled<T>(this global::System.Threading.CancellationToken cancellationToken)
		{
			return new global::System.Threading.Tasks.Task<T>(() => default(T), cancellationToken);
		}

		public static global::System.Threading.Tasks.Task WriteAsync(this global::System.IO.TextWriter writer, char value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		public static global::System.Threading.Tasks.Task WriteAsync(this global::System.IO.TextWriter writer, string? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		public static global::System.Threading.Tasks.Task WriteAsync(this global::System.IO.TextWriter writer, char[] value, int start, int count, global::System.Threading.CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value, start, count);
			}
			return cancellationToken.FromCanceled();
		}

		public static global::System.Threading.Tasks.Task<int> ReadAsync(this global::System.IO.TextReader reader, char[] buffer, int index, int count, global::System.Threading.CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return reader.ReadAsync(buffer, index, count);
			}
			return cancellationToken.FromCanceled<int>();
		}

		public static bool IsCompletedSuccessfully(this global::System.Threading.Tasks.Task task)
		{
			return task.Status == global::System.Threading.Tasks.TaskStatus.RanToCompletion;
		}
	}
}
