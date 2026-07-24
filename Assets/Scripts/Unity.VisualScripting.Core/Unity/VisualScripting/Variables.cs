namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("Visual Scripting/Variables")]
	[global::Unity.VisualScripting.DisableAnnotation]
	[global::Unity.VisualScripting.IncludeInSettings(false)]
	public class Variables : global::Unity.VisualScripting.LudiqBehaviour, global::Unity.VisualScripting.IAotStubbable
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		public global::Unity.VisualScripting.VariableDeclarations declarations { get; internal set; } = new global::Unity.VisualScripting.VariableDeclarations
		{
			Kind = global::Unity.VisualScripting.VariableKind.Object
		};

		public static global::Unity.VisualScripting.VariableDeclarations ActiveScene => Scene(global::UnityEngine.SceneManagement.SceneManager.GetActiveScene());

		public static global::Unity.VisualScripting.VariableDeclarations Application => global::Unity.VisualScripting.ApplicationVariables.current;

		public static global::Unity.VisualScripting.VariableDeclarations Saved => global::Unity.VisualScripting.SavedVariables.current;

		public static bool ExistInActiveScene => ExistInScene(global::UnityEngine.SceneManagement.SceneManager.GetActiveScene());

		public static global::Unity.VisualScripting.VariableDeclarations Graph(global::Unity.VisualScripting.GraphPointer pointer)
		{
			global::Unity.VisualScripting.Ensure.That("pointer").IsNotNull(pointer);
			if (pointer.hasData)
			{
				return GraphInstance(pointer);
			}
			return GraphDefinition(pointer);
		}

		public static global::Unity.VisualScripting.VariableDeclarations GraphInstance(global::Unity.VisualScripting.GraphPointer pointer)
		{
			return pointer.GetGraphData<global::Unity.VisualScripting.IGraphDataWithVariables>().variables;
		}

		public static global::Unity.VisualScripting.VariableDeclarations GraphDefinition(global::Unity.VisualScripting.GraphPointer pointer)
		{
			return GraphDefinition((global::Unity.VisualScripting.IGraphWithVariables)pointer.graph);
		}

		public static global::Unity.VisualScripting.VariableDeclarations GraphDefinition(global::Unity.VisualScripting.IGraphWithVariables graph)
		{
			return graph.variables;
		}

		public static global::Unity.VisualScripting.VariableDeclarations Object(global::UnityEngine.GameObject go)
		{
			return go.GetOrAddComponent<global::Unity.VisualScripting.Variables>().declarations;
		}

		public static global::Unity.VisualScripting.VariableDeclarations Object(global::UnityEngine.Component component)
		{
			return Object(component.gameObject);
		}

		public static global::Unity.VisualScripting.VariableDeclarations Scene(global::UnityEngine.SceneManagement.Scene? scene)
		{
			return global::Unity.VisualScripting.SceneVariables.For(scene);
		}

		public static global::Unity.VisualScripting.VariableDeclarations Scene(global::UnityEngine.GameObject go)
		{
			return Scene(go.scene);
		}

		public static global::Unity.VisualScripting.VariableDeclarations Scene(global::UnityEngine.Component component)
		{
			return Scene(component.gameObject);
		}

		public static bool ExistOnObject(global::UnityEngine.GameObject go)
		{
			return go.GetComponent<global::Unity.VisualScripting.Variables>() != null;
		}

		public static bool ExistOnObject(global::UnityEngine.Component component)
		{
			return ExistOnObject(component.gameObject);
		}

		public static bool ExistInScene(global::UnityEngine.SceneManagement.Scene? scene)
		{
			if (scene.HasValue)
			{
				return global::Unity.VisualScripting.SceneVariables.InstantiatedIn(scene.Value);
			}
			return false;
		}

		[global::UnityEngine.ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			foreach (global::Unity.VisualScripting.VariableDeclaration declaration in declarations)
			{
				global::System.Type type = declaration.value?.GetType();
				if (!(type == null) && (string.IsNullOrEmpty(type.FullName) || (!type.FullName.Contains("UnityEngine.Audio.AudioMixer") && !type.FullName.Contains("UnityEditor.Audio.AudioMixerController"))))
				{
					global::System.Reflection.ConstructorInfo publicDefaultConstructor = type.GetPublicDefaultConstructor();
					if (publicDefaultConstructor != null)
					{
						yield return publicDefaultConstructor;
					}
				}
			}
		}
	}
}
