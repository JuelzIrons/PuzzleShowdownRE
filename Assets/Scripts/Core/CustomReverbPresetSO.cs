[global::UnityEngine.CreateAssetMenu(fileName = "CustomReverbPreset", menuName = "Audio/Custom Reverb Preset")]
public class CustomReverbPresetSO : global::UnityEngine.ScriptableObject
{
	[global::UnityEngine.Header("Mirrors AudioReverbFilter's 'User' mode fields")]
	public float DryLevel;

	public float Room;

	public float RoomHF;

	public float RoomLF;

	public float DecayTime = 1f;

	public float DecayHFRatio = 0.5f;

	public float ReflectionsLevel = -10000f;

	public float ReflectionsDelay;

	public float ReverbLevel;

	public float ReverbDelay = 0.04f;

	public float HFReference = 5000f;

	public float LFReference = 250f;

	public float Diffusion = 100f;

	public float Density = 100f;

	public void ApplyTo(global::UnityEngine.AudioReverbFilter filter)
	{
		filter.reverbPreset = global::UnityEngine.AudioReverbPreset.User;
		filter.dryLevel = DryLevel;
		filter.room = Room;
		filter.roomHF = RoomHF;
		filter.roomLF = RoomLF;
		filter.decayTime = DecayTime;
		filter.decayHFRatio = DecayHFRatio;
		filter.reflectionsLevel = ReflectionsLevel;
		filter.reflectionsDelay = ReflectionsDelay;
		filter.reverbLevel = ReverbLevel;
		filter.reverbDelay = ReverbDelay;
		filter.hfReference = HFReference;
		filter.lfReference = LFReference;
		filter.diffusion = Diffusion;
		filter.density = Density;
	}
}
