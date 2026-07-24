namespace Unity.VisualScripting
{
	public class ProfiledSegmentCollection : global::System.Collections.ObjectModel.KeyedCollection<string, global::Unity.VisualScripting.ProfiledSegment>
	{
		protected override string GetKeyForItem(global::Unity.VisualScripting.ProfiledSegment item)
		{
			return item.name;
		}
	}
}
