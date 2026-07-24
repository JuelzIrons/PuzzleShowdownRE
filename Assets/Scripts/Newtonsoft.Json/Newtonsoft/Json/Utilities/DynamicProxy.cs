namespace Newtonsoft.Json.Utilities
{
	internal class DynamicProxy<T>
	{
		public virtual global::System.Collections.Generic.IEnumerable<string> GetDynamicMemberNames(T instance)
		{
			return global::Newtonsoft.Json.Utilities.CollectionUtils.ArrayEmpty<string>();
		}

		public virtual bool TryBinaryOperation(T instance, global::System.Dynamic.BinaryOperationBinder binder, object arg, out object? result)
		{
			result = null;
			return false;
		}

		public virtual bool TryConvert(T instance, global::System.Dynamic.ConvertBinder binder, out object? result)
		{
			result = null;
			return false;
		}

		public virtual bool TryCreateInstance(T instance, global::System.Dynamic.CreateInstanceBinder binder, object[] args, out object? result)
		{
			result = null;
			return false;
		}

		public virtual bool TryDeleteIndex(T instance, global::System.Dynamic.DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		public virtual bool TryDeleteMember(T instance, global::System.Dynamic.DeleteMemberBinder binder)
		{
			return false;
		}

		public virtual bool TryGetIndex(T instance, global::System.Dynamic.GetIndexBinder binder, object[] indexes, out object? result)
		{
			result = null;
			return false;
		}

		public virtual bool TryGetMember(T instance, global::System.Dynamic.GetMemberBinder binder, out object? result)
		{
			result = null;
			return false;
		}

		public virtual bool TryInvoke(T instance, global::System.Dynamic.InvokeBinder binder, object[] args, out object? result)
		{
			result = null;
			return false;
		}

		public virtual bool TryInvokeMember(T instance, global::System.Dynamic.InvokeMemberBinder binder, object[] args, out object? result)
		{
			result = null;
			return false;
		}

		public virtual bool TrySetIndex(T instance, global::System.Dynamic.SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		public virtual bool TrySetMember(T instance, global::System.Dynamic.SetMemberBinder binder, object value)
		{
			return false;
		}

		public virtual bool TryUnaryOperation(T instance, global::System.Dynamic.UnaryOperationBinder binder, out object? result)
		{
			result = null;
			return false;
		}
	}
}
