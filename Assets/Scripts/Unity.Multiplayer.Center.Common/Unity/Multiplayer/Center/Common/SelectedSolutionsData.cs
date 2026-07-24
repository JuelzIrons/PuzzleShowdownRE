namespace Unity.Multiplayer.Center.Common
{
	[global::System.Serializable]
	public class SelectedSolutionsData
	{
		public enum HostingModel
		{
			None = 0,
			ClientHosted = 1,
			DedicatedServer = 2,
			CloudCode = 3,
			DistributedAuthority = 4
		}

		public enum NetcodeSolution
		{
			None = 0,
			NGO = 1,
			N4E = 2,
			CustomNetcode = 3,
			NoNetcode = 4
		}

		public global::Unity.Multiplayer.Center.Common.SelectedSolutionsData.HostingModel SelectedHostingModel;

		public global::Unity.Multiplayer.Center.Common.SelectedSolutionsData.NetcodeSolution SelectedNetcodeSolution;
	}
}
