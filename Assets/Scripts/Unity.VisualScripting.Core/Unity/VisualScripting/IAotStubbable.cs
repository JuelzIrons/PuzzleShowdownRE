namespace Unity.VisualScripting
{
	public interface IAotStubbable
	{
		global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited);
	}
}
