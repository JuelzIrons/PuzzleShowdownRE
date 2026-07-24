namespace Newtonsoft.Json.Serialization
{
	public class JsonDynamicContract : global::Newtonsoft.Json.Serialization.JsonContainerContract
	{
		private readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<string, global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object>>> _callSiteGetters = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<string, global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object>>>(CreateCallSiteGetter);

		private readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<string, global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object?, object>>> _callSiteSetters = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<string, global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object, object>>>(CreateCallSiteSetter);

		public global::Newtonsoft.Json.Serialization.JsonPropertyCollection Properties { get; }

		public global::System.Func<string, string>? PropertyNameResolver { get; set; }

		private static global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object>> CreateCallSiteGetter(string name)
		{
			return global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object>>.Create(new global::Newtonsoft.Json.Utilities.NoThrowGetBinderMember((global::System.Dynamic.GetMemberBinder)global::Newtonsoft.Json.Utilities.DynamicUtils.BinderWrapper.GetMember(name, typeof(global::Newtonsoft.Json.Utilities.DynamicUtils))));
		}

		private static global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object?, object>> CreateCallSiteSetter(string name)
		{
			return global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object, object>>.Create(new global::Newtonsoft.Json.Utilities.NoThrowSetBinderMember((global::System.Dynamic.SetMemberBinder)global::Newtonsoft.Json.Utilities.DynamicUtils.BinderWrapper.SetMember(name, typeof(global::Newtonsoft.Json.Utilities.DynamicUtils))));
		}

		public JsonDynamicContract(global::System.Type underlyingType)
			: base(underlyingType)
		{
			ContractType = global::Newtonsoft.Json.Serialization.JsonContractType.Dynamic;
			Properties = new global::Newtonsoft.Json.Serialization.JsonPropertyCollection(base.UnderlyingType);
		}

		internal bool TryGetMember(global::System.Dynamic.IDynamicMetaObjectProvider dynamicProvider, string name, out object? value)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object>> callSite = _callSiteGetters.Get(name);
			object obj = callSite.Target(callSite, dynamicProvider);
			if (obj != global::Newtonsoft.Json.Utilities.NoThrowExpressionVisitor.ErrorResult)
			{
				value = obj;
				return true;
			}
			value = null;
			return false;
		}

		internal bool TrySetMember(global::System.Dynamic.IDynamicMetaObjectProvider dynamicProvider, string name, object? value)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			global::System.Runtime.CompilerServices.CallSite<global::System.Func<global::System.Runtime.CompilerServices.CallSite, object, object, object>> callSite = _callSiteSetters.Get(name);
			return callSite.Target(callSite, dynamicProvider, value) != global::Newtonsoft.Json.Utilities.NoThrowExpressionVisitor.ErrorResult;
		}
	}
}
