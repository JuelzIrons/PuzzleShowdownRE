namespace Unity.Services.Authentication.PlayerAccounts
{
	internal interface IBrowserUtils
	{
		global::System.Threading.Tasks.Task LaunchUrlAsync(string url);

		bool Bind();

		void Dismiss();

		string GetRedirectUri();
	}
}
