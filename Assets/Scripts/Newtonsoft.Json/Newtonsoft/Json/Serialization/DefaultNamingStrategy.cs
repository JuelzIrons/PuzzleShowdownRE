namespace Newtonsoft.Json.Serialization
{
	public class DefaultNamingStrategy : global::Newtonsoft.Json.Serialization.NamingStrategy
	{
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}
	}
}
