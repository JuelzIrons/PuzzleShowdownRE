namespace TMPro
{
	public struct CaretInfo
	{
		public int index;

		public global::TMPro.CaretPosition position;

		public CaretInfo(int index, global::TMPro.CaretPosition position)
		{
			this.index = index;
			this.position = position;
		}
	}
}
