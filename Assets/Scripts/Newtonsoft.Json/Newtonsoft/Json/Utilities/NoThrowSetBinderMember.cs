namespace Newtonsoft.Json.Utilities
{
	internal class NoThrowSetBinderMember : global::System.Dynamic.SetMemberBinder
	{
		private readonly global::System.Dynamic.SetMemberBinder _innerBinder;

		public NoThrowSetBinderMember(global::System.Dynamic.SetMemberBinder innerBinder)
			: base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			_innerBinder = innerBinder;
		}

		public override global::System.Dynamic.DynamicMetaObject FallbackSetMember(global::System.Dynamic.DynamicMetaObject target, global::System.Dynamic.DynamicMetaObject value, global::System.Dynamic.DynamicMetaObject? errorSuggestion)
		{
			global::System.Dynamic.DynamicMetaObject dynamicMetaObject = _innerBinder.Bind(target, new global::System.Dynamic.DynamicMetaObject[1] { value });
			return new global::System.Dynamic.DynamicMetaObject(new global::Newtonsoft.Json.Utilities.NoThrowExpressionVisitor().Visit(dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
		}
	}
}
