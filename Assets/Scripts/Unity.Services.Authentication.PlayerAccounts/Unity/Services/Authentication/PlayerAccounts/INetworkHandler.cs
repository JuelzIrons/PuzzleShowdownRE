namespace Unity.Services.Authentication.PlayerAccounts
{
	internal interface INetworkHandler
	{
		global::System.Threading.Tasks.Task<T> GetAsync<T>(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null);

		global::System.Threading.Tasks.Task<T> PostAsync<T>(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null);

		global::System.Threading.Tasks.Task<T> PostAsync<T>(string url, string payload, global::System.Collections.Generic.IDictionary<string, string> headers = null);

		global::System.Threading.Tasks.Task<T> PutAsync<T>(string url, object payload, global::System.Collections.Generic.IDictionary<string, string> headers = null);

		global::System.Threading.Tasks.Task DeleteAsync(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null);

		global::System.Threading.Tasks.Task<T> DeleteAsync<T>(string url, global::System.Collections.Generic.IDictionary<string, string> headers = null);
	}
}
