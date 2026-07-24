namespace Newtonsoft.Json.Utilities
{
	internal class NoThrowGetBinderMember : global::System.Dynamic.GetMemberBinder
	{
		private readonly global::System.Dynamic.GetMemberBinder _innerBinder;

		public NoThrowGetBinderMember(global::System.Dynamic.GetMemberBinder innerBinder)
			: base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			_innerBinder = innerBinder;
		}

		public override global::System.Dynamic.DynamicMetaObject FallbackGetMember(global::System.Dynamic.DynamicMetaObject target, global::System.Dynamic.DynamicMetaObject? errorSuggestion)
		{
			global::System.Dynamic.DynamicMetaObject dynamicMetaObject = _innerBinder.Bind(target, global::Newtonsoft.Json.Utilities.CollectionUtils.ArrayEmpty<global::System.Dynamic.DynamicMetaObject>());
			return new global::System.Dynamic.DynamicMetaObject(new global::Newtonsoft.Json.Utilities.NoThrowExpressionVisitor().Visit(dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
		}
	}
}
