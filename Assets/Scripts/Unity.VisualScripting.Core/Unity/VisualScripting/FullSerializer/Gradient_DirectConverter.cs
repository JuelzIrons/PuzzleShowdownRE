namespace Unity.VisualScripting.FullSerializer
{
	public class Gradient_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.Gradient>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.Gradient model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			success += SerializeMember(serialized, null, "alphaKeys", model.alphaKeys);
			success += SerializeMember(serialized, null, "colorKeys", model.colorKeys);
			try
			{
				success += SerializeMember(serialized, null, "mode", model.mode);
			}
			catch (global::System.Exception)
			{
				LogWarning("serialized");
			}
			return success;
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.Gradient model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::UnityEngine.GradientAlphaKey[] value = model.alphaKeys;
			success += DeserializeMember<global::UnityEngine.GradientAlphaKey[]>(data, null, "alphaKeys", out value);
			model.alphaKeys = value;
			global::UnityEngine.GradientColorKey[] value2 = model.colorKeys;
			success += DeserializeMember<global::UnityEngine.GradientColorKey[]>(data, null, "colorKeys", out value2);
			model.colorKeys = value2;
			try
			{
				global::UnityEngine.GradientMode value3 = model.mode;
				success += DeserializeMember<global::UnityEngine.GradientMode>(data, null, "mode", out value3);
				model.mode = value3;
			}
			catch (global::System.Exception)
			{
				LogWarning("deserialized");
			}
			return success;
		}

		private static void LogWarning(string phase)
		{
			string text = "2021.3.9f1";
			text = "2022.2.0a18";
			global::UnityEngine.Debug.LogWarning("Gradient.mode could not be " + phase + ". Please use Unity " + text + " or newer to resolve this issue.");
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return new global::UnityEngine.Gradient();
		}
	}
}
