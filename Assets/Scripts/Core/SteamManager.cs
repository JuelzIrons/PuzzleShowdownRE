/// <summary>
/// Steam integration has been removed. The native steam_api64 binary is not part of
/// this project, so every Steamworks P/Invoke threw DllNotFoundException, and Steam
/// gated nothing that the game actually needs: online play runs on Unity Relay with
/// anonymous Unity Authentication.
///
/// The component is kept as an inert stub so the references to it in
/// Assets/Scenes/LaunchScenes/MainMenu.unity stay valid instead of turning into
/// "missing script" entries.
/// </summary>
[global::UnityEngine.DisallowMultipleComponent]
public class SteamManager : global::UnityEngine.MonoBehaviour
{
}
