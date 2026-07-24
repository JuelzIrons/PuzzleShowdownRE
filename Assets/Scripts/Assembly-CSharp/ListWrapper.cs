[global::System.Serializable]
public struct ListWrapper<T>
{
	public global::System.Collections.Generic.List<T> list;

	public ListWrapper<T> Clone()
	{
		return new ListWrapper<T>
		{
			list = new global::System.Collections.Generic.List<T>(list)
		};
	}
}
