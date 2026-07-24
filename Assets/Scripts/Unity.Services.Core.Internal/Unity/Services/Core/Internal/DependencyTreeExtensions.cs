namespace Unity.Services.Core.Internal
{
	internal static class DependencyTreeExtensions
	{
		internal static string ToJson(this global::Unity.Services.Core.Internal.DependencyTree tree, global::System.Collections.Generic.ICollection<int> order = null)
		{
			global::Newtonsoft.Json.Linq.JArray jArray = new global::Newtonsoft.Json.Linq.JArray();
			global::Newtonsoft.Json.Linq.JProperty jProperty = new global::Newtonsoft.Json.Linq.JProperty("ordered", jArray);
			if (order != null)
			{
				foreach (int item in order)
				{
					global::Newtonsoft.Json.Linq.JObject packageJObject = GetPackageJObject(tree, item);
					jArray.Add(new global::Newtonsoft.Json.Linq.JObject(packageJObject));
				}
			}
			global::Newtonsoft.Json.Linq.JArray jArray2 = new global::Newtonsoft.Json.Linq.JArray();
			global::Newtonsoft.Json.Linq.JProperty jProperty2 = new global::Newtonsoft.Json.Linq.JProperty("packages", jArray2);
			foreach (int key in tree.PackageTypeHashToInstance.Keys)
			{
				global::Newtonsoft.Json.Linq.JObject packageJObject2 = GetPackageJObject(tree, key);
				jArray2.Add(packageJObject2);
			}
			global::Newtonsoft.Json.Linq.JArray jArray3 = new global::Newtonsoft.Json.Linq.JArray();
			global::Newtonsoft.Json.Linq.JProperty jProperty3 = new global::Newtonsoft.Json.Linq.JProperty("components", jArray3);
			foreach (int key2 in tree.ComponentTypeHashToInstance.Keys)
			{
				global::Newtonsoft.Json.Linq.JObject componentJObject = GetComponentJObject(tree, key2);
				jArray3.Add(componentJObject);
			}
			return new global::Newtonsoft.Json.Linq.JObject(jProperty, jProperty2, jProperty3).ToString();
		}

		internal static bool IsOptional(this global::Unity.Services.Core.Internal.DependencyTree tree, int componentTypeHash)
		{
			if (tree.ComponentTypeHashToInstance.TryGetValue(componentTypeHash, out var value))
			{
				return value == null;
			}
			return false;
		}

		internal static bool IsProvided(this global::Unity.Services.Core.Internal.DependencyTree tree, int componentTypeHash)
		{
			return tree.ComponentTypeHashToPackageTypeHash.ContainsKey(componentTypeHash);
		}

		private static global::Newtonsoft.Json.Linq.JObject GetPackageJObject(global::Unity.Services.Core.Internal.DependencyTree tree, int packageHash)
		{
			global::Newtonsoft.Json.Linq.JProperty jProperty = new global::Newtonsoft.Json.Linq.JProperty("packageHash", packageHash);
			tree.PackageTypeHashToInstance.TryGetValue(packageHash, out var value);
			global::Newtonsoft.Json.Linq.JProperty jProperty2 = new global::Newtonsoft.Json.Linq.JProperty("packageProvider", (value != null) ? value.GetType().Name : "null");
			global::Newtonsoft.Json.Linq.JArray jArray = new global::Newtonsoft.Json.Linq.JArray();
			global::Newtonsoft.Json.Linq.JProperty jProperty3 = new global::Newtonsoft.Json.Linq.JProperty("packageDependencies", jArray);
			if (tree.PackageTypeHashToComponentTypeHashDependencies.TryGetValue(packageHash, out var value2))
			{
				foreach (int item2 in value2)
				{
					global::Newtonsoft.Json.Linq.JProperty jProperty4 = new global::Newtonsoft.Json.Linq.JProperty("dependencyHash", item2);
					tree.ComponentTypeHashToInstance.TryGetValue(item2, out var value3);
					global::Newtonsoft.Json.Linq.JProperty jProperty5 = new global::Newtonsoft.Json.Linq.JProperty("dependencyComponent", GetComponentIdentifier(value3));
					global::Newtonsoft.Json.Linq.JProperty jProperty6 = new global::Newtonsoft.Json.Linq.JProperty("dependencyProvided", tree.IsProvided(item2) ? "true" : "false");
					global::Newtonsoft.Json.Linq.JProperty jProperty7 = new global::Newtonsoft.Json.Linq.JProperty("dependencyOptional", tree.IsOptional(item2) ? "true" : "false");
					global::Newtonsoft.Json.Linq.JObject item = new global::Newtonsoft.Json.Linq.JObject(jProperty4, jProperty5, jProperty6, jProperty7);
					jArray.Add(item);
				}
			}
			return new global::Newtonsoft.Json.Linq.JObject(jProperty, jProperty2, jProperty3);
		}

		private static global::Newtonsoft.Json.Linq.JObject GetComponentJObject(global::Unity.Services.Core.Internal.DependencyTree tree, int componentHash)
		{
			global::Newtonsoft.Json.Linq.JProperty jProperty = new global::Newtonsoft.Json.Linq.JProperty("componentHash", componentHash);
			tree.ComponentTypeHashToInstance.TryGetValue(componentHash, out var value);
			global::Newtonsoft.Json.Linq.JProperty jProperty2 = new global::Newtonsoft.Json.Linq.JProperty("component", GetComponentIdentifier(value));
			tree.ComponentTypeHashToPackageTypeHash.TryGetValue(componentHash, out var value2);
			global::Newtonsoft.Json.Linq.JProperty jProperty3 = new global::Newtonsoft.Json.Linq.JProperty("componentPackageHash", value2);
			global::Unity.Services.Core.Internal.IInitializablePackage value3;
			bool flag = tree.PackageTypeHashToInstance.TryGetValue(value2, out value3);
			global::Newtonsoft.Json.Linq.JProperty jProperty4 = new global::Newtonsoft.Json.Linq.JProperty("componentPackage", flag ? value3.GetType().Name : "null");
			return new global::Newtonsoft.Json.Linq.JObject(jProperty, jProperty2, jProperty3, jProperty4);
		}

		private static string GetComponentIdentifier(global::Unity.Services.Core.Internal.IServiceComponent component)
		{
			if (component == null)
			{
				return "null";
			}
			if (component is global::Unity.Services.Core.Internal.MissingComponent missingComponent)
			{
				return missingComponent.IntendedType.Name;
			}
			return component.GetType().Name;
		}
	}
}
