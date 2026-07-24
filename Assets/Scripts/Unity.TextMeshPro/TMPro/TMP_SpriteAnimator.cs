namespace TMPro
{
	[global::UnityEngine.DisallowMultipleComponent]
	public class TMP_SpriteAnimator : global::UnityEngine.MonoBehaviour
	{
		private global::System.Collections.Generic.Dictionary<int, bool> m_animations = new global::System.Collections.Generic.Dictionary<int, bool>(16);

		private global::TMPro.TMP_Text m_TextComponent;

		private void Awake()
		{
			m_TextComponent = GetComponent<global::TMPro.TMP_Text>();
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void StopAllAnimations()
		{
			StopAllCoroutines();
			m_animations.Clear();
		}

		public void DoSpriteAnimation(int currentCharacter, global::TMPro.TMP_SpriteAsset spriteAsset, int start, int end, int framerate)
		{
			if (!m_animations.TryGetValue(currentCharacter, out var _))
			{
				StartCoroutine(DoSpriteAnimationInternal(currentCharacter, spriteAsset, start, end, framerate));
				m_animations.Add(currentCharacter, value: true);
			}
		}

		private global::System.Collections.IEnumerator DoSpriteAnimationInternal(int currentCharacter, global::TMPro.TMP_SpriteAsset spriteAsset, int start, int end, int framerate)
		{
			if (m_TextComponent == null)
			{
				yield break;
			}
			yield return null;
			int currentFrame = start;
			if (end > spriteAsset.spriteCharacterTable.Count)
			{
				end = spriteAsset.spriteCharacterTable.Count - 1;
			}
			global::TMPro.TMP_CharacterInfo charInfo = m_TextComponent.textInfo.characterInfo[currentCharacter];
			int materialIndex = charInfo.materialReferenceIndex;
			int vertexIndex = charInfo.vertexIndex;
			global::TMPro.TMP_MeshInfo meshInfo = m_TextComponent.textInfo.meshInfo[materialIndex];
			float baseSpriteScale = spriteAsset.spriteCharacterTable[start].scale * spriteAsset.spriteCharacterTable[start].glyph.scale;
			float elapsedTime = 0f;
			float targetTime = 1f / (float)global::UnityEngine.Mathf.Abs(framerate);
			while (true)
			{
				if (elapsedTime > targetTime)
				{
					elapsedTime = 0f;
					uint character = m_TextComponent.textInfo.characterInfo[currentCharacter].character;
					if (character == 3 || character == 8230)
					{
						break;
					}
					global::TMPro.TMP_SpriteCharacter tMP_SpriteCharacter = spriteAsset.spriteCharacterTable[currentFrame];
					global::UnityEngine.Vector3[] vertices = meshInfo.vertices;
					global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(charInfo.origin, charInfo.baseLine);
					float num = charInfo.scale / baseSpriteScale * tMP_SpriteCharacter.scale * tMP_SpriteCharacter.glyph.scale;
					global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(vector.x + tMP_SpriteCharacter.glyph.metrics.horizontalBearingX * num, vector.y + (tMP_SpriteCharacter.glyph.metrics.horizontalBearingY - tMP_SpriteCharacter.glyph.metrics.height) * num);
					global::UnityEngine.Vector3 vector3 = new global::UnityEngine.Vector3(vector2.x, vector.y + tMP_SpriteCharacter.glyph.metrics.horizontalBearingY * num);
					global::UnityEngine.Vector3 vector4 = new global::UnityEngine.Vector3(vector.x + (tMP_SpriteCharacter.glyph.metrics.horizontalBearingX + tMP_SpriteCharacter.glyph.metrics.width) * num, vector3.y);
					global::UnityEngine.Vector3 vector5 = new global::UnityEngine.Vector3(vector4.x, vector2.y);
					vertices[vertexIndex] = vector2;
					vertices[vertexIndex + 1] = vector3;
					vertices[vertexIndex + 2] = vector4;
					vertices[vertexIndex + 3] = vector5;
					global::UnityEngine.Vector4[] uvs = meshInfo.uvs0;
					global::UnityEngine.Vector2 vector6 = new global::UnityEngine.Vector2((float)tMP_SpriteCharacter.glyph.glyphRect.x / (float)spriteAsset.spriteSheet.width, (float)tMP_SpriteCharacter.glyph.glyphRect.y / (float)spriteAsset.spriteSheet.height);
					global::UnityEngine.Vector2 vector7 = new global::UnityEngine.Vector2(vector6.x, (float)(tMP_SpriteCharacter.glyph.glyphRect.y + tMP_SpriteCharacter.glyph.glyphRect.height) / (float)spriteAsset.spriteSheet.height);
					global::UnityEngine.Vector2 vector8 = new global::UnityEngine.Vector2((float)(tMP_SpriteCharacter.glyph.glyphRect.x + tMP_SpriteCharacter.glyph.glyphRect.width) / (float)spriteAsset.spriteSheet.width, vector7.y);
					global::UnityEngine.Vector2 vector9 = new global::UnityEngine.Vector2(vector8.x, vector6.y);
					uvs[vertexIndex] = vector6;
					uvs[vertexIndex + 1] = vector7;
					uvs[vertexIndex + 2] = vector8;
					uvs[vertexIndex + 3] = vector9;
					meshInfo.mesh.vertices = vertices;
					meshInfo.mesh.SetUVs(0, uvs);
					m_TextComponent.UpdateGeometry(meshInfo.mesh, materialIndex);
					currentFrame = ((framerate > 0) ? ((currentFrame >= end) ? start : (currentFrame + 1)) : ((currentFrame <= start) ? end : (currentFrame - 1)));
				}
				elapsedTime += global::UnityEngine.Time.deltaTime;
				yield return null;
			}
			m_animations.Remove(currentCharacter);
		}
	}
}
