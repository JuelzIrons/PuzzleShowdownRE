internal class EventBusExamples
{
	public class CheatCodeController : global::UnityEngine.MonoBehaviour
	{
		public const string CheatCodeActivated = "CheatCodeActivated";

		private static readonly global::UnityEngine.KeyCode[] famousCheatCode = new global::UnityEngine.KeyCode[10]
		{
			global::UnityEngine.KeyCode.UpArrow,
			global::UnityEngine.KeyCode.UpArrow,
			global::UnityEngine.KeyCode.DownArrow,
			global::UnityEngine.KeyCode.DownArrow,
			global::UnityEngine.KeyCode.LeftArrow,
			global::UnityEngine.KeyCode.RightArrow,
			global::UnityEngine.KeyCode.LeftArrow,
			global::UnityEngine.KeyCode.RightArrow,
			global::UnityEngine.KeyCode.B,
			global::UnityEngine.KeyCode.A
		};

		private int index;

		private global::Unity.VisualScripting.EventHook cheatCodeHook;

		private global::System.Action<global::Unity.VisualScripting.EmptyEventArgs> godModeDelegate;

		public global::UnityEngine.GameObject player;

		private void Start()
		{
			cheatCodeHook = new global::Unity.VisualScripting.EventHook("CheatCodeActivated");
			godModeDelegate = delegate
			{
				EnableGodMode();
			};
			global::Unity.VisualScripting.EventBus.Register(cheatCodeHook, godModeDelegate);
		}

		private void Update()
		{
			if (global::UnityEngine.Input.anyKeyDown)
			{
				if (global::UnityEngine.Input.GetKeyDown(famousCheatCode[index]))
				{
					index++;
				}
				else
				{
					index = 0;
				}
				if (index >= famousCheatCode.Length)
				{
					global::Unity.VisualScripting.EventBus.Trigger("CheatCodeActivated");
					global::Unity.VisualScripting.EventBus.Trigger(new global::Unity.VisualScripting.EventHook("CheatCodeActivated", player.GetComponent<global::Unity.VisualScripting.ScriptMachine>()));
					index = 0;
				}
			}
		}

		private void OnDestroy()
		{
			global::Unity.VisualScripting.EventBus.Unregister(cheatCodeHook, godModeDelegate);
		}

		private void EnableGodMode()
		{
			global::UnityEngine.Debug.Log("Cheat code has been entered. Enabling god mode.");
		}
	}

	[global::Unity.VisualScripting.UnitTitle("On Cheat Code Enabled")]
	public sealed class CheatCodeEnabled : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "CheatCodeActivated";
	}
}
