namespace Newtonsoft.Json.Serialization
{
	internal abstract class JsonSerializerInternalBase
	{
		private class ReferenceEqualsEqualityComparer : global::System.Collections.Generic.IEqualityComparer<object>
		{
			bool global::System.Collections.Generic.IEqualityComparer<object>.Equals(object? x, object? y)
			{
				return x == y;
			}

			int global::System.Collections.Generic.IEqualityComparer<object>.GetHashCode(object obj)
			{
				return global::System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
			}
		}

		private global::Newtonsoft.Json.Serialization.ErrorContext? _currentErrorContext;

		private global::Newtonsoft.Json.Utilities.BidirectionalDictionary<string, object>? _mappings;

		internal readonly global::Newtonsoft.Json.JsonSerializer Serializer;

		internal readonly global::Newtonsoft.Json.Serialization.ITraceWriter? TraceWriter;

		protected global::Newtonsoft.Json.Serialization.JsonSerializerProxy? InternalSerializer;

		internal global::Newtonsoft.Json.Utilities.BidirectionalDictionary<string, object> DefaultReferenceMappings
		{
			get
			{
				if (_mappings == null)
				{
					_mappings = new global::Newtonsoft.Json.Utilities.BidirectionalDictionary<string, object>(global::System.Collections.Generic.EqualityComparer<string>.Default, new global::Newtonsoft.Json.Serialization.JsonSerializerInternalBase.ReferenceEqualsEqualityComparer(), "A different value already has the Id '{0}'.", "A different Id has already been assigned for value '{0}'. This error may be caused by an object being reused multiple times during deserialization and can be fixed with the setting ObjectCreationHandling.Replace.");
				}
				return _mappings;
			}
		}

		protected JsonSerializerInternalBase(global::Newtonsoft.Json.JsonSerializer serializer)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(serializer, "serializer");
			Serializer = serializer;
			TraceWriter = serializer.TraceWriter;
		}

		protected global::Newtonsoft.Json.NullValueHandling ResolvedNullValueHandling(global::Newtonsoft.Json.Serialization.JsonObjectContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty property)
		{
			return property.NullValueHandling ?? containerContract?.ItemNullValueHandling ?? Serializer._nullValueHandling;
		}

		private global::Newtonsoft.Json.Serialization.ErrorContext GetErrorContext(object? currentObject, object? member, string path, global::System.Exception error)
		{
			if (_currentErrorContext == null)
			{
				_currentErrorContext = new global::Newtonsoft.Json.Serialization.ErrorContext(currentObject, member, path, error);
			}
			if (_currentErrorContext.Error != error)
			{
				throw new global::System.InvalidOperationException("Current error context error is different to requested error.");
			}
			return _currentErrorContext;
		}

		protected void ClearErrorContext()
		{
			if (_currentErrorContext == null)
			{
				throw new global::System.InvalidOperationException("Could not clear error context. Error context is already null.");
			}
			_currentErrorContext = null;
		}

		protected bool IsErrorHandled(object? currentObject, global::Newtonsoft.Json.Serialization.JsonContract? contract, object? keyValue, global::Newtonsoft.Json.IJsonLineInfo? lineInfo, string path, global::System.Exception ex)
		{
			global::Newtonsoft.Json.Serialization.ErrorContext errorContext = GetErrorContext(currentObject, keyValue, path, ex);
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Error && !errorContext.Traced)
			{
				errorContext.Traced = true;
				string text = ((GetType() == typeof(global::Newtonsoft.Json.Serialization.JsonSerializerInternalWriter)) ? "Error serializing" : "Error deserializing");
				if (contract != null)
				{
					text = text + " " + contract.UnderlyingType;
				}
				text = text + ". " + ex.Message;
				if (!(ex is global::Newtonsoft.Json.JsonException))
				{
					text = global::Newtonsoft.Json.JsonPosition.FormatMessage(lineInfo, path, text);
				}
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Error, text, ex);
			}
			if (contract != null && currentObject != null)
			{
				contract.InvokeOnError(currentObject, Serializer.Context, errorContext);
			}
			if (!errorContext.Handled)
			{
				Serializer.OnError(new global::Newtonsoft.Json.Serialization.ErrorEventArgs(currentObject, errorContext));
			}
			return errorContext.Handled;
		}
	}
}
