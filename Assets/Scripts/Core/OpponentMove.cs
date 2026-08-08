public class OpponentMove
{
	public float TS;

	public Moves MoveType;

	public global::UnityEngine.Vector2 Dir;

	public int MoveFrameOrder;

	public OpponentMove(float TS, Moves MoveType, int moveOrder, global::UnityEngine.Vector2 Dir = default(global::UnityEngine.Vector2))
	{
		this.TS = TS;
		this.MoveType = MoveType;
		this.Dir = Dir;
		MoveFrameOrder = moveOrder;
	}
}
