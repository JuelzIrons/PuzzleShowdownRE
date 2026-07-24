namespace UnityEngine.InputSystem
{
	[global::System.Serializable]
	public struct InputBinding : global::System.IEquatable<global::UnityEngine.InputSystem.InputBinding>
	{
		[global::System.Flags]
		public enum DisplayStringOptions
		{
			DontUseShortDisplayNames = 1,
			DontOmitDevice = 2,
			DontIncludeInteractions = 4,
			IgnoreBindingOverrides = 8
		}

		[global::System.Flags]
		internal enum MatchOptions
		{
			EmptyGroupMatchesAny = 1
		}

		[global::System.Flags]
		internal enum Flags
		{
			None = 0,
			Composite = 4,
			PartOfComposite = 8
		}

		public const char Separator = ';';

		internal const string kSeparatorString = ";";

		[global::UnityEngine.SerializeField]
		private string m_Name;

		[global::UnityEngine.SerializeField]
		internal string m_Id;

		[global::UnityEngine.Tooltip("Path of the control to bind to. Matched at runtime to controls from InputDevices present at the time.\n\nCan either be graphically from the control picker dropdown UI or edited manually in text mode by clicking the 'T' button. Internally, both methods result in control path strings that look like, for example, \"<Gamepad>/buttonSouth\".")]
		[global::UnityEngine.SerializeField]
		private string m_Path;

		[global::UnityEngine.SerializeField]
		private string m_Interactions;

		[global::UnityEngine.SerializeField]
		private string m_Processors;

		[global::UnityEngine.SerializeField]
		internal string m_Groups;

		[global::UnityEngine.SerializeField]
		private string m_Action;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputBinding.Flags m_Flags;

		[global::System.NonSerialized]
		private string m_OverridePath;

		[global::System.NonSerialized]
		private string m_OverrideInteractions;

		[global::System.NonSerialized]
		private string m_OverrideProcessors;

		public string name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
			}
		}

		public global::System.Guid id
		{
			get
			{
				if (string.IsNullOrEmpty(m_Id))
				{
					return default(global::System.Guid);
				}
				return new global::System.Guid(m_Id);
			}
			set
			{
				m_Id = value.ToString();
			}
		}

		public string path
		{
			get
			{
				return m_Path;
			}
			set
			{
				m_Path = value;
			}
		}

		public string overridePath
		{
			get
			{
				return m_OverridePath;
			}
			set
			{
				m_OverridePath = value;
			}
		}

		public string interactions
		{
			get
			{
				return m_Interactions;
			}
			set
			{
				m_Interactions = value;
			}
		}

		public string overrideInteractions
		{
			get
			{
				return m_OverrideInteractions;
			}
			set
			{
				m_OverrideInteractions = value;
			}
		}

		public string processors
		{
			get
			{
				return m_Processors;
			}
			set
			{
				m_Processors = value;
			}
		}

		public string overrideProcessors
		{
			get
			{
				return m_OverrideProcessors;
			}
			set
			{
				m_OverrideProcessors = value;
			}
		}

		public string groups
		{
			get
			{
				return m_Groups;
			}
			set
			{
				m_Groups = value;
			}
		}

		public string action
		{
			get
			{
				return m_Action;
			}
			set
			{
				m_Action = value;
			}
		}

		public bool isComposite
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.InputBinding.Flags.Composite) == global::UnityEngine.InputSystem.InputBinding.Flags.Composite;
			}
			set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputBinding.Flags.Composite;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputBinding.Flags.Composite;
				}
			}
		}

		public bool isPartOfComposite
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.InputBinding.Flags.PartOfComposite) == global::UnityEngine.InputSystem.InputBinding.Flags.PartOfComposite;
			}
			set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputBinding.Flags.PartOfComposite;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputBinding.Flags.PartOfComposite;
				}
			}
		}

		public bool hasOverrides
		{
			get
			{
				if (overridePath == null && overrideProcessors == null)
				{
					return overrideInteractions != null;
				}
				return true;
			}
		}

		public string effectivePath => overridePath ?? path;

		public string effectiveInteractions => overrideInteractions ?? interactions;

		public string effectiveProcessors => overrideProcessors ?? processors;

		internal bool isEmpty
		{
			get
			{
				if (string.IsNullOrEmpty(effectivePath) && string.IsNullOrEmpty(action))
				{
					return string.IsNullOrEmpty(groups);
				}
				return false;
			}
		}

		public InputBinding(string path, string action = null, string groups = null, string processors = null, string interactions = null, string name = null)
		{
			m_Path = path;
			m_Action = action;
			m_Groups = groups;
			m_Processors = processors;
			m_Interactions = interactions;
			m_Name = name;
			m_Id = null;
			m_Flags = global::UnityEngine.InputSystem.InputBinding.Flags.None;
			m_OverridePath = null;
			m_OverrideInteractions = null;
			m_OverrideProcessors = null;
		}

		public string GetNameOfComposite()
		{
			if (!isComposite)
			{
				return null;
			}
			return global::UnityEngine.InputSystem.Utilities.NameAndParameters.Parse(effectivePath).name;
		}

		internal void GenerateId()
		{
			m_Id = global::System.Guid.NewGuid().ToString();
		}

		internal void RemoveOverrides()
		{
			m_OverridePath = null;
			m_OverrideInteractions = null;
			m_OverrideProcessors = null;
		}

		public static global::UnityEngine.InputSystem.InputBinding MaskByGroup(string group)
		{
			return new global::UnityEngine.InputSystem.InputBinding
			{
				groups = group
			};
		}

		public static global::UnityEngine.InputSystem.InputBinding MaskByGroups(params string[] groups)
		{
			return new global::UnityEngine.InputSystem.InputBinding
			{
				groups = string.Join(";", global::System.Linq.Enumerable.Where(groups, (string x) => !string.IsNullOrEmpty(x)))
			};
		}

		public bool Equals(global::UnityEngine.InputSystem.InputBinding other)
		{
			if (string.Equals(effectivePath, other.effectivePath, global::System.StringComparison.InvariantCultureIgnoreCase) && string.Equals(effectiveInteractions, other.effectiveInteractions, global::System.StringComparison.InvariantCultureIgnoreCase) && string.Equals(effectiveProcessors, other.effectiveProcessors, global::System.StringComparison.InvariantCultureIgnoreCase) && string.Equals(groups, other.groups, global::System.StringComparison.InvariantCultureIgnoreCase))
			{
				return string.Equals(action, other.action, global::System.StringComparison.InvariantCultureIgnoreCase);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.InputSystem.InputBinding other)
			{
				return Equals(other);
			}
			return false;
		}

		public static bool operator ==(global::UnityEngine.InputSystem.InputBinding left, global::UnityEngine.InputSystem.InputBinding right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.InputSystem.InputBinding left, global::UnityEngine.InputSystem.InputBinding right)
		{
			return !(left == right);
		}

		public override int GetHashCode()
		{
			return (((((((((effectivePath != null) ? effectivePath.GetHashCode() : 0) * 397) ^ ((effectiveInteractions != null) ? effectiveInteractions.GetHashCode() : 0)) * 397) ^ ((effectiveProcessors != null) ? effectiveProcessors.GetHashCode() : 0)) * 397) ^ ((groups != null) ? groups.GetHashCode() : 0)) * 397) ^ ((action != null) ? action.GetHashCode() : 0);
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			if (!string.IsNullOrEmpty(action))
			{
				stringBuilder.Append(action);
				stringBuilder.Append(':');
			}
			string value = effectivePath;
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append(value);
			}
			if (!string.IsNullOrEmpty(groups))
			{
				stringBuilder.Append('[');
				stringBuilder.Append(groups);
				stringBuilder.Append(']');
			}
			return stringBuilder.ToString();
		}

		public string ToDisplayString(global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions options = (global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions)0, global::UnityEngine.InputSystem.InputControl control = null)
		{
			string deviceLayoutName;
			string controlPath;
			return ToDisplayString(out deviceLayoutName, out controlPath, options, control);
		}

		public string ToDisplayString(out string deviceLayoutName, out string controlPath, global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions options = (global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions)0, global::UnityEngine.InputSystem.InputControl control = null)
		{
			if (isComposite)
			{
				deviceLayoutName = null;
				controlPath = null;
				return string.Empty;
			}
			global::UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptions humanReadableStringOptions = global::UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptions.None;
			if ((options & global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions.DontOmitDevice) == 0)
			{
				humanReadableStringOptions |= global::UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptions.OmitDevice;
			}
			if ((options & global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions.DontUseShortDisplayNames) == 0)
			{
				humanReadableStringOptions |= global::UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptions.UseShortNames;
			}
			string text = global::UnityEngine.InputSystem.InputControlPath.ToHumanReadableString(((options & global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions.IgnoreBindingOverrides) != 0) ? path : effectivePath, out deviceLayoutName, out controlPath, humanReadableStringOptions, control);
			if (!string.IsNullOrEmpty(effectiveInteractions) && (options & global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions.DontIncludeInteractions) == 0)
			{
				string text2 = string.Empty;
				foreach (global::UnityEngine.InputSystem.Utilities.NameAndParameters item in global::UnityEngine.InputSystem.Utilities.NameAndParameters.ParseMultiple(effectiveInteractions))
				{
					string displayName = global::UnityEngine.InputSystem.InputInteraction.GetDisplayName(item.name);
					if (!string.IsNullOrEmpty(displayName))
					{
						text2 = (string.IsNullOrEmpty(text2) ? displayName : (text2 + " or " + displayName));
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					text = text2 + " " + text;
				}
			}
			return text;
		}

		internal bool TriggersAction(global::UnityEngine.InputSystem.InputAction action)
		{
			if (string.Compare(action.name, this.action, global::System.StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				return this.action == action.m_Id;
			}
			return true;
		}

		public bool Matches(global::UnityEngine.InputSystem.InputBinding binding)
		{
			return Matches(ref binding);
		}

		internal bool Matches(ref global::UnityEngine.InputSystem.InputBinding binding, global::UnityEngine.InputSystem.InputBinding.MatchOptions options = (global::UnityEngine.InputSystem.InputBinding.MatchOptions)0)
		{
			if (!string.IsNullOrEmpty(name) && (string.IsNullOrEmpty(binding.name) || !global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(name, binding.name, ';')))
			{
				return false;
			}
			if (path != null && (binding.path == null || !global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(path, binding.path, ';')))
			{
				return false;
			}
			if (action != null && (binding.action == null || !global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(action, binding.action, ';')))
			{
				return false;
			}
			if (groups != null)
			{
				bool flag = !string.IsNullOrEmpty(binding.groups);
				if (!flag && (options & global::UnityEngine.InputSystem.InputBinding.MatchOptions.EmptyGroupMatchesAny) == 0)
				{
					return false;
				}
				if (flag && !global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(groups, binding.groups, ';'))
				{
					return false;
				}
			}
			if (!string.IsNullOrEmpty(m_Id) && binding.id != id)
			{
				return false;
			}
			return true;
		}
	}
}
