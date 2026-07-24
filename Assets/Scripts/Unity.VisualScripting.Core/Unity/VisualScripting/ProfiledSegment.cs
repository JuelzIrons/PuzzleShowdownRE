namespace Unity.VisualScripting
{
	public class ProfiledSegment
	{
		public string name { get; private set; }

		public global::System.Diagnostics.Stopwatch stopwatch { get; private set; }

		public long calls { get; set; }

		public global::Unity.VisualScripting.ProfiledSegment parent { get; private set; }

		public global::Unity.VisualScripting.ProfiledSegmentCollection children { get; private set; }

		public ProfiledSegment(global::Unity.VisualScripting.ProfiledSegment parent, string name)
		{
			this.parent = parent;
			this.name = name;
			stopwatch = new global::System.Diagnostics.Stopwatch();
			children = new global::Unity.VisualScripting.ProfiledSegmentCollection();
		}
	}
}
