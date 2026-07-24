[global::UnityEngine.CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpriteData", order = 1)]
public class SpriteDataSO : global::UnityEngine.ScriptableObject
{
	public GarbageTiles GarbageBlocks;

	public TypeSection Teal;

	public TypeSection Orange;

	public TypeSection Green;

	public TypeSection Yellow;

	public TypeSection Purple;

	public TypeSection Ghost;

	public global::UnityEngine.Sprite MissingSprite;

	public global::UnityEngine.Sprite GhostSprite;

	public global::UnityEngine.Sprite GetSprite(SpriteType type, SquareComponent sqr, int index = 0, bool shouldReveal = false)
	{
		if (sqr.IsGarbage && !shouldReveal && type != SpriteType.FlashingGarbage && type != SpriteType.SingleGarbage)
		{
			return sqr.CachedGarbageShape;
		}
		TypeSection spriteTable = GetSpriteTable(sqr.Type);
		if (sqr.Type == BlockType.Ghost)
		{
			return spriteTable.LandSprites[0];
		}
		return type switch
		{
			SpriteType.Default => spriteTable.LandSprites[3], 
			SpriteType.LandSprites => spriteTable.LandSprites[index], 
			SpriteType.DanceSprites => spriteTable.DanceSprites[index], 
			SpriteType.DestroySprites => spriteTable.DestroySprites[index], 
			SpriteType.FrozenSprite => spriteTable.FrozenSprite, 
			SpriteType.DestroyedSprite => spriteTable.DestroyedSprite, 
			SpriteType.FlashingGarbage => GarbageBlocks.SingleFlashing, 
			SpriteType.SingleGarbage => GarbageBlocks.Single, 
			_ => MissingSprite, 
		};
	}

	public TypeSection GetSpriteTable(BlockType type)
	{
		return type switch
		{
			BlockType.Teal => Teal, 
			BlockType.Green => Green, 
			BlockType.Purple => Purple, 
			BlockType.Orange => Orange, 
			BlockType.Yellow => Yellow, 
			BlockType.Ghost => Ghost, 
			_ => Ghost, 
		};
	}
}
