namespace UnityEngine.Rendering
{
	public class DebugDisplaySettingsVolume : global::UnityEngine.Rendering.IDebugDisplaySettingsData, global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		private static class Styles
		{
			public static readonly global::UnityEngine.GUIContent none = new global::UnityEngine.GUIContent("None");
		}

		private static class Strings
		{
			public static readonly string cameraNeedsRendering = "Values might not be fully updated if the camera you are inspecting is not rendered.";

			public static readonly string none = "None";

			public static readonly string parameter = "Parameter";

			public static readonly string component = "Component";

			public static readonly string debugViewNotSupported = "N/A";

			public static readonly string volumeInfo = "Volume Info";

			public static readonly string gameObject = "GameObject";

			public static readonly string priority = "Priority";

			public static readonly string resultValue = "Result";

			public static readonly string resultValueTooltip = "The interpolated result value of the parameter. This value is used to render the camera.";

			public static readonly string globalDefaultValue = "Graphics Settings";

			public static readonly string globalDefaultValueTooltip = "Default value for this parameter, defined by the Default Volume Profile in Global Settings.";

			public static readonly string qualityLevelValue = "Quality Settings";

			public static readonly string qualityLevelValueTooltip = "Override value for this parameter, defined by the Volume Profile in the current SRP Asset.";

			public static readonly string global = "Global";

			public static readonly string local = "Local";

			public static readonly string volumeProfile = "Volume Profile";

			public static readonly string parameterNotCalculated = "N/A";
		}

		internal static class WidgetFactory
		{
			private struct VolumeParameterChain
			{
				public global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip nameAndTooltip;

				public global::UnityEngine.Rendering.VolumeProfile volumeProfile;

				public global::UnityEngine.Rendering.VolumeComponent volumeComponent;

				public global::UnityEngine.Rendering.Volume volume;
			}

			private static global::UnityEngine.Rendering.DebugUI.Value s_EmptyDebugUIValue = new global::UnityEngine.Rendering.DebugUI.Value
			{
				getter = () => string.Empty
			};

			public static global::UnityEngine.Rendering.DebugUI.EnumField CreateComponentSelector(global::UnityEngine.Rendering.DebugDisplaySettingsVolume.SettingsPanel panel, global::System.Action<global::UnityEngine.Rendering.DebugUI.Field<int>, int> refresh)
			{
				int num = 0;
				global::System.Collections.Generic.List<global::UnityEngine.GUIContent> list = new global::System.Collections.Generic.List<global::UnityEngine.GUIContent> { global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Styles.none };
				global::System.Collections.Generic.List<int> list2 = new global::System.Collections.Generic.List<int> { num++ };
				foreach (var item in panel.data.volumeComponentsPathAndType)
				{
					global::UnityEngine.GUIContent gUIContent = new global::UnityEngine.GUIContent();
					(gUIContent.text, _) = item;
					list.Add(gUIContent);
					list2.Add(num++);
				}
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					displayName = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.component,
					getter = () => panel.data.selectedComponent,
					setter = delegate(int value)
					{
						panel.data.selectedComponent = value;
					},
					enumNames = list.ToArray(),
					enumValues = list2.ToArray(),
					getIndex = () => panel.data.volumeComponentEnumIndex,
					setIndex = delegate(int value)
					{
						panel.data.volumeComponentEnumIndex = value;
					},
					onValueChanged = refresh
				};
			}

			public static global::UnityEngine.Rendering.DebugUI.CameraSelector CreateCameraSelector(global::UnityEngine.Rendering.DebugDisplaySettingsVolume.SettingsPanel panel, global::System.Action<global::UnityEngine.Rendering.DebugUI.Field<global::UnityEngine.Object>, global::UnityEngine.Object> refresh)
			{
				return new global::UnityEngine.Rendering.DebugUI.CameraSelector
				{
					getter = () => panel.data.selectedCamera,
					setter = delegate(global::UnityEngine.Object value)
					{
						panel.data.selectedCamera = value as global::UnityEngine.Camera;
					},
					onValueChanged = refresh
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateVolumeParameterWidget(string name, bool isResultParameter, global::UnityEngine.Rendering.VolumeParameter param)
			{
				return new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = name,
					getter = () => global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.parameterNotCalculated
				};
			}

			private static global::UnityEngine.Rendering.VolumeComponent GetSelectedVolumeComponent(global::UnityEngine.Rendering.VolumeProfile profile, global::System.Type selectedType)
			{
				if (profile != null)
				{
					foreach (global::UnityEngine.Rendering.VolumeComponent component in profile.components)
					{
						if (component.GetType() == selectedType)
						{
							return component;
						}
					}
				}
				return null;
			}

			private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain> GetResolutionChain(global::UnityEngine.Rendering.DebugDisplaySettingsVolume data)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain>();
				global::System.Type selectedComponentType = data.selectedComponentType;
				if (data.selectedCamera == null || selectedComponentType == null)
				{
					return list;
				}
				if (data.resultVolumeComponent == null)
				{
					return list;
				}
				global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain item = new global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain
				{
					nameAndTooltip = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
					{
						name = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.resultValue,
						tooltip = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.resultValueTooltip
					},
					volumeComponent = data.resultVolumeComponent
				};
				list.Add(item);
				global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.Volume> volumesList = data.GetVolumesList();
				for (int num = volumesList.Count - 1; num >= 0; num--)
				{
					global::UnityEngine.Rendering.Volume volume = volumesList[num];
					global::UnityEngine.Rendering.VolumeProfile volumeProfile = (volume.HasInstantiatedProfile() ? volume.profile : volume.sharedProfile);
					global::UnityEngine.Rendering.VolumeComponent selectedVolumeComponent = GetSelectedVolumeComponent(volumeProfile, selectedComponentType);
					if (selectedVolumeComponent != null)
					{
						global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain item2 = new global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain
						{
							nameAndTooltip = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
							{
								name = volumeProfile.name,
								tooltip = volumeProfile.name
							},
							volumeProfile = volumeProfile,
							volumeComponent = selectedVolumeComponent,
							volume = volume
						};
						list.Add(item2);
					}
				}
				if (global::UnityEngine.Rendering.VolumeManager.instance.customDefaultProfiles != null)
				{
					foreach (global::UnityEngine.Rendering.VolumeProfile customDefaultProfile in global::UnityEngine.Rendering.VolumeManager.instance.customDefaultProfiles)
					{
						global::UnityEngine.Rendering.VolumeComponent selectedVolumeComponent2 = GetSelectedVolumeComponent(customDefaultProfile, selectedComponentType);
						if (selectedVolumeComponent2 != null)
						{
							global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain item3 = new global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain
							{
								nameAndTooltip = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
								{
									name = customDefaultProfile.name,
									tooltip = customDefaultProfile.name
								},
								volumeProfile = customDefaultProfile,
								volumeComponent = selectedVolumeComponent2
							};
							list.Add(item3);
						}
					}
				}
				if (global::UnityEngine.Rendering.VolumeManager.instance.qualityDefaultProfile != null)
				{
					global::UnityEngine.Rendering.VolumeComponent selectedVolumeComponent3 = GetSelectedVolumeComponent(global::UnityEngine.Rendering.VolumeManager.instance.qualityDefaultProfile, selectedComponentType);
					if (selectedVolumeComponent3 != null)
					{
						global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain item4 = new global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain
						{
							nameAndTooltip = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
							{
								name = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.qualityLevelValue,
								tooltip = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.qualityLevelValueTooltip
							},
							volumeProfile = global::UnityEngine.Rendering.VolumeManager.instance.qualityDefaultProfile,
							volumeComponent = selectedVolumeComponent3
						};
						list.Add(item4);
					}
				}
				if (global::UnityEngine.Rendering.VolumeManager.instance.globalDefaultProfile != null)
				{
					global::UnityEngine.Rendering.VolumeComponent selectedVolumeComponent4 = GetSelectedVolumeComponent(global::UnityEngine.Rendering.VolumeManager.instance.globalDefaultProfile, selectedComponentType);
					if (selectedVolumeComponent4 != null)
					{
						global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain item5 = new global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain
						{
							nameAndTooltip = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
							{
								name = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.globalDefaultValue,
								tooltip = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.globalDefaultValueTooltip
							},
							volumeProfile = global::UnityEngine.Rendering.VolumeManager.instance.globalDefaultProfile,
							volumeComponent = selectedVolumeComponent4
						};
						list.Add(item5);
					}
				}
				return list;
			}

			public static global::UnityEngine.Rendering.DebugUI.Table CreateVolumeTable(global::UnityEngine.Rendering.DebugDisplaySettingsVolume data)
			{
				global::System.Func<bool> isHiddenCallback = () => true;
				global::UnityEngine.Rendering.DebugUI.Table table = new global::UnityEngine.Rendering.DebugUI.Table
				{
					displayName = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.parameter,
					isReadOnly = true,
					isHiddenCallback = isHiddenCallback
				};
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain> resolutionChain = GetResolutionChain(data);
				if (resolutionChain.Count == 0)
				{
					return table;
				}
				GenerateTableRows(table, resolutionChain);
				GenerateTableColumns(table, data, resolutionChain);
				return table;
			}

			private static void GenerateTableColumns(global::UnityEngine.Rendering.DebugUI.Table table, global::UnityEngine.Rendering.DebugDisplaySettingsVolume data, global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain> resolutionChain)
			{
				for (int i = 0; i < resolutionChain.Count; i++)
				{
					global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain chain = resolutionChain[i];
					int num = -1;
					if (chain.volume != null)
					{
						((global::UnityEngine.Rendering.DebugUI.Table.Row)table.children[++num]).children.Add(new global::UnityEngine.Rendering.DebugUI.Value
						{
							nameAndTooltip = chain.nameAndTooltip,
							getter = delegate
							{
								string text = (chain.volume.isGlobal ? global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.global : global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.local);
								float volumeWeight = data.GetVolumeWeight(chain.volume);
								return chain.volumeComponent.active ? $"{text} ({volumeWeight * 100f:F2}%)" : (text + " (disabled)");
							},
							refreshRate = 0.2f
						});
						((global::UnityEngine.Rendering.DebugUI.Table.Row)table.children[++num]).children.Add(new global::UnityEngine.Rendering.DebugUI.ObjectField
						{
							displayName = string.Empty,
							getter = () => chain.volume
						});
						((global::UnityEngine.Rendering.DebugUI.Table.Row)table.children[++num]).children.Add(new global::UnityEngine.Rendering.DebugUI.Value
						{
							nameAndTooltip = chain.nameAndTooltip,
							getter = () => chain.volume.priority
						});
					}
					else
					{
						((global::UnityEngine.Rendering.DebugUI.Table.Row)table.children[++num]).children.Add(new global::UnityEngine.Rendering.DebugUI.Value
						{
							nameAndTooltip = chain.nameAndTooltip,
							getter = () => string.Empty
						});
						((global::UnityEngine.Rendering.DebugUI.Table.Row)table.children[++num]).children.Add(s_EmptyDebugUIValue);
						((global::UnityEngine.Rendering.DebugUI.Table.Row)table.children[++num]).children.Add(s_EmptyDebugUIValue);
					}
					((global::UnityEngine.Rendering.DebugUI.Table.Row)table.children[++num]).children.Add((chain.volumeProfile != null) ? ((global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ObjectField
					{
						displayName = string.Empty,
						getter = () => chain.volumeProfile
					}) : ((global::UnityEngine.Rendering.DebugUI.Widget)s_EmptyDebugUIValue));
					((global::UnityEngine.Rendering.DebugUI.Table.Row)table.children[++num]).children.Add(s_EmptyDebugUIValue);
					bool isResultParameter = i == 0;
					for (int num2 = 0; num2 < chain.volumeComponent.parameterList.Length; num2++)
					{
						global::UnityEngine.Rendering.VolumeParameter param = chain.volumeComponent.parameterList[num2];
						((global::UnityEngine.Rendering.DebugUI.Table.Row)table.children[++num]).children.Add(CreateVolumeParameterWidget(chain.nameAndTooltip.name, isResultParameter, param));
					}
				}
			}

			private static void GenerateTableRows(global::UnityEngine.Rendering.DebugUI.Table table, global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.VolumeParameterChain> resolutionChain)
			{
				global::UnityEngine.Rendering.DebugUI.Table.Row item = new global::UnityEngine.Rendering.DebugUI.Table.Row
				{
					displayName = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.volumeInfo,
					opened = true
				};
				table.children.Add(item);
				global::UnityEngine.Rendering.DebugUI.Table.Row item2 = new global::UnityEngine.Rendering.DebugUI.Table.Row
				{
					displayName = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.gameObject
				};
				table.children.Add(item2);
				global::UnityEngine.Rendering.DebugUI.Table.Row item3 = new global::UnityEngine.Rendering.DebugUI.Table.Row
				{
					displayName = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.priority
				};
				table.children.Add(item3);
				global::UnityEngine.Rendering.DebugUI.Table.Row item4 = new global::UnityEngine.Rendering.DebugUI.Table.Row
				{
					displayName = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.volumeProfile
				};
				table.children.Add(item4);
				global::UnityEngine.Rendering.DebugUI.Table.Row item5 = new global::UnityEngine.Rendering.DebugUI.Table.Row
				{
					displayName = string.Empty
				};
				table.children.Add(item5);
				global::UnityEngine.Rendering.VolumeComponent volumeComponent = resolutionChain[0].volumeComponent;
				for (int i = 0; i < volumeComponent.parameterList.Length; i++)
				{
					_ = volumeComponent.parameterList[i];
					string displayName = i.ToString();
					table.children.Add(new global::UnityEngine.Rendering.DebugUI.Table.Row
					{
						displayName = displayName
					});
				}
			}
		}

		[global::UnityEngine.Rendering.DisplayInfo(name = "Volume", order = int.MaxValue)]
		internal class SettingsPanel : global::UnityEngine.Rendering.DebugDisplaySettingsPanel<global::UnityEngine.Rendering.DebugDisplaySettingsVolume>
		{
			private global::UnityEngine.Rendering.DebugUI.Table m_VolumeTable;

			public override global::UnityEngine.Rendering.DebugUI.Flags Flags => global::UnityEngine.Rendering.DebugUI.Flags.EditorForceUpdate;

			public override void Dispose()
			{
				base.Dispose();
				base.data.GetVolumesList().ItemAdded -= OnVolumeInfluenceChanged;
				base.data.GetVolumesList().ItemRemoved -= OnVolumeInfluenceChanged;
			}

			public SettingsPanel(global::UnityEngine.Rendering.DebugDisplaySettingsVolume data)
				: base(data)
			{
				global::UnityEngine.Rendering.DebugDisplaySettingsVolume.SettingsPanel settingsPanel = this;
				global::UnityEngine.Rendering.DebugUI.CameraSelector cameraSelector = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.CreateCameraSelector(this, delegate
				{
					settingsPanel.Refresh();
				});
				global::System.Collections.Generic.List<global::UnityEngine.Camera> list = cameraSelector.getObjects() as global::System.Collections.Generic.List<global::UnityEngine.Camera>;
				if (data.selectedCamera == null && list != null && list.Count > 0)
				{
					data.selectedCamera = list[0];
				}
				AddWidget(cameraSelector);
				AddWidget(global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.CreateComponentSelector(this, delegate
				{
					settingsPanel.Refresh();
				}));
				global::System.Func<bool> isHiddenCallback = () => data.selectedCamera == null || data.selectedComponent <= 0;
				AddWidget(new global::UnityEngine.Rendering.DebugUI.MessageBox
				{
					displayName = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.cameraNeedsRendering,
					style = global::UnityEngine.Rendering.DebugUI.MessageBox.Style.Warning,
					isHiddenCallback = isHiddenCallback
				});
				m_VolumeTable = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.CreateVolumeTable(data);
				AddWidget(m_VolumeTable);
				data.GetVolumesList().ItemAdded += OnVolumeInfluenceChanged;
				data.GetVolumesList().ItemRemoved += OnVolumeInfluenceChanged;
			}

			private void OnVolumeInfluenceChanged(global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.Volume> sender, global::UnityEngine.Rendering.ListChangedEventArgs<global::UnityEngine.Rendering.Volume> e)
			{
				Refresh();
				global::UnityEngine.Rendering.DebugManager.instance.ReDrawOnScreenDebug();
			}

			private void Refresh()
			{
				if (global::UnityEngine.Rendering.DebugManager.instance.GetPanel(PanelName) == null)
				{
					return;
				}
				bool flag = false;
				if (m_Data.selectedComponent > 0 && m_Data.selectedCamera != null)
				{
					flag = true;
					global::UnityEngine.Rendering.DebugUI.Table table = global::UnityEngine.Rendering.DebugDisplaySettingsVolume.WidgetFactory.CreateVolumeTable(m_Data);
					m_VolumeTable.children.Clear();
					foreach (global::UnityEngine.Rendering.DebugUI.Widget child in table.children)
					{
						m_VolumeTable.children.Add(child);
					}
				}
				if (flag)
				{
					global::UnityEngine.Rendering.DebugManager.instance.ReDrawOnScreenDebug();
				}
			}
		}

		private int m_SelectedComponentIndex = -1;

		private global::UnityEngine.Camera m_SelectedCamera;

		private global::UnityEngine.Rendering.VolumeComponent m_VolumeInterpolatedResults;

		private bool m_StoreStackInterpolatedValues;

		private global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.Volume> m_InfluenceVolumes = new global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.Volume>();

		private global::System.Collections.Generic.List<(global::UnityEngine.Rendering.Volume volume, float weight)> m_VolumesWeights = new global::System.Collections.Generic.List<(global::UnityEngine.Rendering.Volume, float)>();

		internal int volumeComponentEnumIndex;

		private const string k_PanelTitle = "Volume";

		[global::System.Obsolete("This property has been obsoleted and will be removed in a future version. #from(6000.2)")]
		public global::UnityEngine.Rendering.IVolumeDebugSettings volumeDebugSettings { get; }

		public int selectedComponent
		{
			get
			{
				return m_SelectedComponentIndex;
			}
			set
			{
				if (value != m_SelectedComponentIndex)
				{
					m_SelectedComponentIndex = value;
					OnSelectionChanged();
				}
			}
		}

		public global::System.Type selectedComponentType
		{
			get
			{
				if (selectedComponent <= 0)
				{
					return null;
				}
				return volumeComponentsPathAndType[selectedComponent - 1].Item2;
			}
			set
			{
				int num = volumeComponentsPathAndType.FindIndex(((string, global::System.Type) t) => t.Item2 == value);
				if (num != -1)
				{
					selectedComponent = num + 1;
				}
			}
		}

		public global::System.Collections.Generic.List<(string, global::System.Type)> volumeComponentsPathAndType => global::UnityEngine.Rendering.VolumeManager.instance.GetVolumeComponentsForDisplay(global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipelineAssetType);

		public global::UnityEngine.Camera selectedCamera
		{
			get
			{
				return m_SelectedCamera;
			}
			set
			{
				if (value != m_SelectedCamera)
				{
					m_SelectedCamera = value;
					OnSelectionChanged();
				}
			}
		}

		internal global::UnityEngine.Rendering.VolumeComponent resultVolumeComponent
		{
			get
			{
				if (m_VolumeInterpolatedResults == null)
				{
					m_VolumeInterpolatedResults = global::UnityEngine.ScriptableObject.CreateInstance(selectedComponentType) as global::UnityEngine.Rendering.VolumeComponent;
				}
				return m_VolumeInterpolatedResults;
			}
		}

		public bool AreAnySettingsActive => false;

		private void DestroyVolumeInterpolatedResults()
		{
			if (m_VolumeInterpolatedResults != null)
			{
				global::UnityEngine.Object.DestroyImmediate(m_VolumeInterpolatedResults);
			}
		}

		private void OnSelectionChanged()
		{
			ClearInterpolationData();
			DestroyVolumeInterpolatedResults();
		}

		private void ClearInterpolationData()
		{
			m_VolumesWeights.Clear();
		}

		private static bool AreVolumesChanged(global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.Volume> influenceVolumes, global::System.Collections.Generic.List<(global::UnityEngine.Rendering.Volume volume, float weight)> volumesWeights)
		{
			if (influenceVolumes.Count != volumesWeights.Count)
			{
				return true;
			}
			for (int i = 0; i < influenceVolumes.Count; i++)
			{
				if (influenceVolumes[i] != volumesWeights[i].volume)
				{
					return true;
				}
			}
			return false;
		}

		private void OnBeginVolumeStackUpdate(global::UnityEngine.Rendering.VolumeStack stack, global::UnityEngine.Camera camera)
		{
			if (camera == selectedCamera)
			{
				ClearInterpolationData();
				m_StoreStackInterpolatedValues = selectedCamera != null && selectedComponentType != null;
			}
		}

		private void OnEndVolumeStackUpdate(global::UnityEngine.Rendering.VolumeStack stack, global::UnityEngine.Camera camera)
		{
			if (!m_StoreStackInterpolatedValues)
			{
				return;
			}
			if (AreVolumesChanged(m_InfluenceVolumes, m_VolumesWeights))
			{
				m_InfluenceVolumes.Clear();
				foreach (var volumesWeight in m_VolumesWeights)
				{
					m_InfluenceVolumes.Add(volumesWeight.volume);
				}
			}
			global::UnityEngine.Rendering.VolumeComponent component = stack.GetComponent(selectedComponentType);
			for (int i = 0; i < component.parameters.Count; i++)
			{
				resultVolumeComponent.parameters[i].SetValue(component.parameters[i]);
			}
			m_StoreStackInterpolatedValues = false;
		}

		private void OnVolumeStackInterpolated(global::UnityEngine.Rendering.VolumeStack stack, global::UnityEngine.Rendering.Volume volume, float interpolationFactor)
		{
			if (m_StoreStackInterpolatedValues)
			{
				m_VolumesWeights.Add((volume, interpolationFactor));
			}
		}

		public float GetVolumeWeight(global::UnityEngine.Rendering.Volume volume)
		{
			if (m_VolumesWeights.Count == 0)
			{
				return 0f;
			}
			foreach (var volumesWeight in m_VolumesWeights)
			{
				if (volume == volumesWeight.volume)
				{
					return volumesWeight.weight;
				}
			}
			return 0f;
		}

		public global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.Volume> GetVolumesList()
		{
			return m_InfluenceVolumes;
		}

		void global::UnityEngine.Rendering.IDebugDisplaySettingsData.Reset()
		{
			ClearInterpolationData();
			DestroyVolumeInterpolatedResults();
		}

		[global::System.Obsolete("This constructor has been obsoleted and will be removed in a future version. #from(6000.2)")]
		public DebugDisplaySettingsVolume(global::UnityEngine.Rendering.IVolumeDebugSettings volumeDebugSettings)
			: this()
		{
			this.volumeDebugSettings = volumeDebugSettings;
		}

		public DebugDisplaySettingsVolume()
		{
		}

		internal static string ExtractResult(global::UnityEngine.Rendering.VolumeParameter param)
		{
			if (param == null)
			{
				return global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.parameterNotCalculated;
			}
			global::System.Reflection.PropertyInfo property = param.GetType().GetProperty("value");
			if (property == null)
			{
				return "-";
			}
			object value = property.GetValue(param);
			global::System.Type propertyType = property.PropertyType;
			if (value == null || value.Equals(null))
			{
				return global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.none + " (" + propertyType.Name + ")";
			}
			global::System.Reflection.MethodInfo method = propertyType.GetMethod("ToString", global::System.Type.EmptyTypes);
			if (method == null || method.DeclaringType == typeof(object) || method.DeclaringType == typeof(global::UnityEngine.Object))
			{
				global::System.Reflection.PropertyInfo property2 = property.PropertyType.GetProperty("name");
				if (property2 == null)
				{
					return global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.debugViewNotSupported;
				}
				return $"{property2.GetValue(value)}" ?? global::UnityEngine.Rendering.DebugDisplaySettingsVolume.Strings.none;
			}
			return value.ToString();
		}

		public global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new global::UnityEngine.Rendering.DebugDisplaySettingsVolume.SettingsPanel(this);
		}
	}
}
