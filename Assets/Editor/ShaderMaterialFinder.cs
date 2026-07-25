using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PuzzleShowdown.EditorTools
{
	/// <summary>
	/// Drop a shader in, get every material in the project that uses it, selected.
	/// Open via Tools > Find Materials Using Shader, or right-click a shader in the
	/// Project window and pick "Find Materials Using This Shader".
	/// </summary>
	public class ShaderMaterialFinder : EditorWindow
	{
		private Shader m_shader;
		private readonly List<Material> m_results = new List<Material>();
		private Vector2 m_scroll;
		private bool m_hasSearched;

		[MenuItem("Tools/Find Materials Using Shader")]
		public static ShaderMaterialFinder Open()
		{
			ShaderMaterialFinder window = GetWindow<ShaderMaterialFinder>("Shader Users");
			window.minSize = new Vector2(320f, 220f);
			return window;
		}

		[MenuItem("Assets/Find Materials Using This Shader", true)]
		private static bool FindFromProjectValidate()
		{
			return Selection.activeObject is Shader;
		}

		[MenuItem("Assets/Find Materials Using This Shader", false, 30)]
		private static void FindFromProject()
		{
			Shader shader = Selection.activeObject as Shader;
			ShaderMaterialFinder window = Open();
			window.m_shader = shader;
			window.Search();
		}

		private void OnGUI()
		{
			EditorGUILayout.Space();

			EditorGUI.BeginChangeCheck();
			m_shader = (Shader)EditorGUILayout.ObjectField("Shader", m_shader, typeof(Shader), false);
			if (EditorGUI.EndChangeCheck())
			{
				// Re-run automatically so dragging a shader in is the whole interaction.
				m_results.Clear();
				m_hasSearched = false;
				if (m_shader != null)
				{
					Search();
				}
			}

			DrawDropArea();

			using (new EditorGUI.DisabledScope(m_shader == null))
			{
				if (GUILayout.Button("Find & Select Materials"))
				{
					Search();
				}
			}

			EditorGUILayout.Space();
			DrawResults();
		}

		private void DrawDropArea()
		{
			Rect rect = GUILayoutUtility.GetRect(0f, 40f, GUILayout.ExpandWidth(true));
			GUI.Box(rect, "Drop a .shader here", EditorStyles.helpBox);

			Event evt = Event.current;
			if (!rect.Contains(evt.mousePosition))
			{
				return;
			}
			if (evt.type != EventType.DragUpdated && evt.type != EventType.DragPerform)
			{
				return;
			}

			Shader dragged = DragAndDrop.objectReferences.OfType<Shader>().FirstOrDefault();
			DragAndDrop.visualMode = (dragged != null) ? DragAndDropVisualMode.Copy : DragAndDropVisualMode.Rejected;
			if (evt.type == EventType.DragPerform && dragged != null)
			{
				DragAndDrop.AcceptDrag();
				m_shader = dragged;
				Search();
			}
			evt.Use();
		}

		private void DrawResults()
		{
			if (!m_hasSearched)
			{
				return;
			}

			if (m_results.Count == 0)
			{
				EditorGUILayout.HelpBox("No materials use " + m_shader.name + ".", MessageType.Info);
				return;
			}

			EditorGUILayout.LabelField(
				string.Format("{0} material{1} using {2}", m_results.Count, (m_results.Count == 1) ? "" : "s", m_shader.name),
				EditorStyles.boldLabel);

			if (GUILayout.Button("Re-select All"))
			{
				SelectResults();
			}

			m_scroll = EditorGUILayout.BeginScrollView(m_scroll);
			foreach (Material mat in m_results)
			{
				// A material can be deleted while the window is open.
				if (mat == null)
				{
					continue;
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.ObjectField(mat, typeof(Material), false);
					if (GUILayout.Button("Ping", GUILayout.Width(44f)))
					{
						EditorGUIUtility.PingObject(mat);
					}
				}
			}
			EditorGUILayout.EndScrollView();
		}

		private void Search()
		{
			m_results.Clear();
			m_hasSearched = true;
			if (m_shader == null)
			{
				return;
			}

			string[] guids = AssetDatabase.FindAssets("t:Material");
			try
			{
				for (int i = 0; i < guids.Length; i++)
				{
					if (guids.Length > 200 && EditorUtility.DisplayCancelableProgressBar(
						"Find Materials Using Shader", "Scanning materials...", (float)i / guids.Length))
					{
						break;
					}

					string path = AssetDatabase.GUIDToAssetPath(guids[i]);
					// LoadAllAssetsAtPath also catches materials embedded in models/prefabs,
					// which LoadAssetAtPath alone would miss.
					foreach (Object asset in AssetDatabase.LoadAllAssetsAtPath(path))
					{
						Material mat = asset as Material;
						if (mat != null && mat.shader == m_shader && !m_results.Contains(mat))
						{
							m_results.Add(mat);
						}
					}
				}
			}
			finally
			{
				EditorUtility.ClearProgressBar();
			}

			m_results.Sort((a, b) => string.Compare(a.name, b.name, System.StringComparison.OrdinalIgnoreCase));
			SelectResults();
			Repaint();
		}

		private void SelectResults()
		{
			if (m_results.Count == 0)
			{
				return;
			}
			Selection.objects = m_results.ToArray();
			EditorGUIUtility.PingObject(m_results[0]);
		}
	}
}
