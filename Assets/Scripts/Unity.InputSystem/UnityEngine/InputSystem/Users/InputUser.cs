namespace UnityEngine.InputSystem.Users
{
	public struct InputUser : global::System.IEquatable<global::UnityEngine.InputSystem.Users.InputUser>
	{
		public struct ControlSchemeChangeSyntax
		{
			internal int m_UserIndex;

			public global::UnityEngine.InputSystem.Users.InputUser.ControlSchemeChangeSyntax AndPairRemainingDevices()
			{
				UpdateControlSchemeMatch(m_UserIndex, autoPairMissing: true);
				return this;
			}
		}

		[global::System.Flags]
		internal enum UserFlags
		{
			BindToAllDevices = 1,
			UserAccountSelectionInProgress = 2
		}

		private struct UserData
		{
			public global::UnityEngine.InputSystem.Users.InputUserAccountHandle? platformUserAccountHandle;

			public string platformUserAccountName;

			public string platformUserAccountId;

			public int deviceCount;

			public int deviceStartIndex;

			public global::UnityEngine.InputSystem.IInputActionCollection actions;

			public global::UnityEngine.InputSystem.InputControlScheme? controlScheme;

			public global::UnityEngine.InputSystem.InputControlScheme.MatchResult controlSchemeMatch;

			public int lostDeviceCount;

			public int lostDeviceStartIndex;

			public global::UnityEngine.InputSystem.Users.InputUser.UserFlags flags;
		}

		private struct CompareDevicesByUserAccount : global::System.Collections.Generic.IComparer<global::UnityEngine.InputSystem.InputDevice>
		{
			public global::UnityEngine.InputSystem.Users.InputUserAccountHandle platformUserAccountHandle;

			public int Compare(global::UnityEngine.InputSystem.InputDevice x, global::UnityEngine.InputSystem.InputDevice y)
			{
				global::UnityEngine.InputSystem.Users.InputUserAccountHandle? userAccountHandleForDevice = GetUserAccountHandleForDevice(x);
				global::UnityEngine.InputSystem.Users.InputUserAccountHandle? userAccountHandleForDevice2 = GetUserAccountHandleForDevice(x);
				global::UnityEngine.InputSystem.Users.InputUserAccountHandle? inputUserAccountHandle = userAccountHandleForDevice;
				global::UnityEngine.InputSystem.Users.InputUserAccountHandle inputUserAccountHandle2 = platformUserAccountHandle;
				if (inputUserAccountHandle.HasValue && (!inputUserAccountHandle.HasValue || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2) && userAccountHandleForDevice2 == platformUserAccountHandle)
				{
					return 0;
				}
				if (userAccountHandleForDevice == platformUserAccountHandle)
				{
					return -1;
				}
				if (userAccountHandleForDevice2 == platformUserAccountHandle)
				{
					return 1;
				}
				return 0;
			}

			private static global::UnityEngine.InputSystem.Users.InputUserAccountHandle? GetUserAccountHandleForDevice(global::UnityEngine.InputSystem.InputDevice device)
			{
				return null;
			}
		}

		private struct OngoingAccountSelection
		{
			public global::UnityEngine.InputSystem.InputDevice device;

			public uint userId;
		}

		private struct GlobalState
		{
			internal int pairingStateVersion;

			internal uint lastUserId;

			internal int allUserCount;

			internal int allPairedDeviceCount;

			internal int allLostDeviceCount;

			internal global::UnityEngine.InputSystem.Users.InputUser[] allUsers;

			internal global::UnityEngine.InputSystem.Users.InputUser.UserData[] allUserData;

			internal global::UnityEngine.InputSystem.InputDevice[] allPairedDevices;

			internal global::UnityEngine.InputSystem.InputDevice[] allLostDevices;

			internal global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Users.InputUser.OngoingAccountSelection> ongoingAccountSelections;

			internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.Users.InputUser, global::UnityEngine.InputSystem.Users.InputUserChange, global::UnityEngine.InputSystem.InputDevice>> onChange;

			internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputControl, global::UnityEngine.InputSystem.LowLevel.InputEventPtr>> onUnpairedDeviceUsed;

			internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Func<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.LowLevel.InputEventPtr, bool>> onPreFilterUnpairedDeviceUsed;

			internal global::System.Action<object, global::UnityEngine.InputSystem.InputActionChange> actionChangeDelegate;

			internal global::System.Action<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.InputDeviceChange> onDeviceChangeDelegate;

			internal global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice> onEventDelegate;

			internal bool onActionChangeHooked;

			internal bool onDeviceChangeHooked;

			internal bool onEventHooked;

			internal int listenForUnpairedDeviceActivity;
		}

		public const uint InvalidId = 0u;

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputUserOnChangeMarker = new global::Unity.Profiling.ProfilerMarker("InputUser.onChange");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputCheckForUnpairMarker = new global::Unity.Profiling.ProfilerMarker("InputCheckForUnpairedDeviceActivity");

		private uint m_Id;

		private static global::UnityEngine.InputSystem.Users.InputUser.GlobalState s_GlobalState;

		public bool valid
		{
			get
			{
				if (m_Id == 0)
				{
					return false;
				}
				for (int i = 0; i < s_GlobalState.allUserCount; i++)
				{
					if (s_GlobalState.allUsers[i].m_Id == m_Id)
					{
						return true;
					}
				}
				return false;
			}
		}

		public int index
		{
			get
			{
				if (m_Id == 0)
				{
					throw new global::System.InvalidOperationException("Invalid user");
				}
				int num = TryFindUserIndex(m_Id);
				if (num == -1)
				{
					throw new global::System.InvalidOperationException($"User with ID {m_Id} is no longer valid");
				}
				return num;
			}
		}

		public uint id => m_Id;

		public global::UnityEngine.InputSystem.Users.InputUserAccountHandle? platformUserAccountHandle => s_GlobalState.allUserData[index].platformUserAccountHandle;

		public string platformUserAccountName => s_GlobalState.allUserData[index].platformUserAccountName;

		public string platformUserAccountId => s_GlobalState.allUserData[index].platformUserAccountId;

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> pairedDevices
		{
			get
			{
				int num = index;
				return new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>(s_GlobalState.allPairedDevices, s_GlobalState.allUserData[num].deviceStartIndex, s_GlobalState.allUserData[num].deviceCount);
			}
		}

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> lostDevices
		{
			get
			{
				int num = index;
				return new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>(s_GlobalState.allLostDevices, s_GlobalState.allUserData[num].lostDeviceStartIndex, s_GlobalState.allUserData[num].lostDeviceCount);
			}
		}

		public global::UnityEngine.InputSystem.IInputActionCollection actions => s_GlobalState.allUserData[index].actions;

		public global::UnityEngine.InputSystem.InputControlScheme? controlScheme => s_GlobalState.allUserData[index].controlScheme;

		public global::UnityEngine.InputSystem.InputControlScheme.MatchResult controlSchemeMatch => s_GlobalState.allUserData[index].controlSchemeMatch;

		public bool hasMissingRequiredDevices => s_GlobalState.allUserData[index].controlSchemeMatch.hasMissingRequiredDevices;

		public static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Users.InputUser> all => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Users.InputUser>(s_GlobalState.allUsers, 0, s_GlobalState.allUserCount);

		public static int listenForUnpairedDeviceActivity
		{
			get
			{
				return s_GlobalState.listenForUnpairedDeviceActivity;
			}
			set
			{
				if (value < 0)
				{
					throw new global::System.ArgumentOutOfRangeException("value", "Cannot be negative");
				}
				if (value > 0 && s_GlobalState.onUnpairedDeviceUsed.length > 0)
				{
					HookIntoEvents();
				}
				else if (value == 0)
				{
					UnhookFromDeviceStateChange();
				}
				s_GlobalState.listenForUnpairedDeviceActivity = value;
			}
		}

		public static event global::System.Action<global::UnityEngine.InputSystem.Users.InputUser, global::UnityEngine.InputSystem.Users.InputUserChange, global::UnityEngine.InputSystem.InputDevice> onChange
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onChange.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onChange.RemoveCallback(value);
			}
		}

		public static event global::System.Action<global::UnityEngine.InputSystem.InputControl, global::UnityEngine.InputSystem.LowLevel.InputEventPtr> onUnpairedDeviceUsed
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onUnpairedDeviceUsed.AddCallback(value);
				if (s_GlobalState.listenForUnpairedDeviceActivity > 0)
				{
					HookIntoEvents();
				}
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onUnpairedDeviceUsed.RemoveCallback(value);
				if (s_GlobalState.onUnpairedDeviceUsed.length == 0)
				{
					UnhookFromDeviceStateChange();
				}
			}
		}

		public static event global::System.Func<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.LowLevel.InputEventPtr, bool> onPrefilterUnpairedDeviceActivity
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onPreFilterUnpairedDeviceUsed.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onPreFilterUnpairedDeviceUsed.RemoveCallback(value);
			}
		}

		public override string ToString()
		{
			if (!valid)
			{
				return $"<Invalid> (id: {m_Id})";
			}
			string text = string.Join(",", pairedDevices);
			return $"User #{index} (id: {m_Id}, devices: {text}, actions: {actions})";
		}

		public void AssociateActionsWithUser(global::UnityEngine.InputSystem.IInputActionCollection actions)
		{
			int num = index;
			if (s_GlobalState.allUserData[num].actions == actions)
			{
				return;
			}
			global::UnityEngine.InputSystem.IInputActionCollection inputActionCollection = s_GlobalState.allUserData[num].actions;
			if (inputActionCollection != null)
			{
				inputActionCollection.devices = null;
				inputActionCollection.bindingMask = null;
			}
			s_GlobalState.allUserData[num].actions = actions;
			if (actions != null)
			{
				HookIntoActionChange();
				actions.devices = pairedDevices;
				if (s_GlobalState.allUserData[num].controlScheme.HasValue)
				{
					ActivateControlSchemeInternal(num, s_GlobalState.allUserData[num].controlScheme.Value);
				}
			}
		}

		public global::UnityEngine.InputSystem.Users.InputUser.ControlSchemeChangeSyntax ActivateControlScheme(string schemeName)
		{
			if (!string.IsNullOrEmpty(schemeName))
			{
				FindControlScheme(schemeName, out var scheme);
				return ActivateControlScheme(scheme);
			}
			return ActivateControlScheme(default(global::UnityEngine.InputSystem.InputControlScheme));
		}

		private bool TryFindControlScheme(string schemeName, out global::UnityEngine.InputSystem.InputControlScheme scheme)
		{
			if (string.IsNullOrEmpty(schemeName))
			{
				scheme = default(global::UnityEngine.InputSystem.InputControlScheme);
				return false;
			}
			if (s_GlobalState.allUserData[index].actions == null)
			{
				throw new global::System.InvalidOperationException($"Cannot set control scheme '{schemeName}' by name on user #{index} as not actions have been associated with the user yet (AssociateActionsWithUser)");
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControlScheme> controlSchemes = s_GlobalState.allUserData[index].actions.controlSchemes;
			for (int i = 0; i < controlSchemes.Count; i++)
			{
				if (string.Compare(controlSchemes[i].name, schemeName, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					scheme = controlSchemes[i];
					return true;
				}
			}
			scheme = default(global::UnityEngine.InputSystem.InputControlScheme);
			return false;
		}

		internal void FindControlScheme(string schemeName, out global::UnityEngine.InputSystem.InputControlScheme scheme)
		{
			if (TryFindControlScheme(schemeName, out scheme))
			{
				return;
			}
			throw new global::System.ArgumentException($"Cannot find control scheme '{schemeName}' in actions '{s_GlobalState.allUserData[index].actions}'");
		}

		public global::UnityEngine.InputSystem.Users.InputUser.ControlSchemeChangeSyntax ActivateControlScheme(global::UnityEngine.InputSystem.InputControlScheme scheme)
		{
			int num = index;
			global::UnityEngine.InputSystem.InputControlScheme? inputControlScheme = s_GlobalState.allUserData[num].controlScheme;
			global::UnityEngine.InputSystem.InputControlScheme inputControlScheme2 = scheme;
			if (!inputControlScheme.HasValue || (inputControlScheme.HasValue && inputControlScheme.GetValueOrDefault() != inputControlScheme2) || (scheme == default(global::UnityEngine.InputSystem.InputControlScheme) && s_GlobalState.allUserData[num].controlScheme.HasValue))
			{
				ActivateControlSchemeInternal(num, scheme);
				Notify(num, global::UnityEngine.InputSystem.Users.InputUserChange.ControlSchemeChanged, null);
			}
			return new global::UnityEngine.InputSystem.Users.InputUser.ControlSchemeChangeSyntax
			{
				m_UserIndex = num
			};
		}

		private void ActivateControlSchemeInternal(int userIndex, global::UnityEngine.InputSystem.InputControlScheme scheme)
		{
			bool flag = scheme == default(global::UnityEngine.InputSystem.InputControlScheme);
			if (flag)
			{
				s_GlobalState.allUserData[userIndex].controlScheme = null;
			}
			else
			{
				s_GlobalState.allUserData[userIndex].controlScheme = scheme;
			}
			if (s_GlobalState.allUserData[userIndex].actions == null)
			{
				return;
			}
			if (flag)
			{
				s_GlobalState.allUserData[userIndex].actions.bindingMask = null;
				s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
				s_GlobalState.allUserData[userIndex].controlSchemeMatch = default(global::UnityEngine.InputSystem.InputControlScheme.MatchResult);
				return;
			}
			s_GlobalState.allUserData[userIndex].actions.bindingMask = new global::UnityEngine.InputSystem.InputBinding
			{
				groups = scheme.bindingGroup
			};
			UpdateControlSchemeMatch(userIndex);
			if (s_GlobalState.allUserData[userIndex].controlSchemeMatch.isSuccessfulMatch)
			{
				RemoveLostDevicesForUser(userIndex);
			}
		}

		public void UnpairDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			int userIndex = index;
			if (global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.ContainsReference(pairedDevices, device))
			{
				RemoveDeviceFromUser(userIndex, device);
			}
		}

		public void UnpairDevices()
		{
			int num = index;
			RemoveLostDevicesForUser(num);
			using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolution())
			{
				while (s_GlobalState.allUserData[num].deviceCount > 0)
				{
					UnpairDevice(s_GlobalState.allPairedDevices[s_GlobalState.allUserData[num].deviceStartIndex + s_GlobalState.allUserData[num].deviceCount - 1]);
				}
			}
			if (s_GlobalState.allUserData[num].controlScheme.HasValue)
			{
				UpdateControlSchemeMatch(num);
			}
		}

		private static void RemoveLostDevicesForUser(int userIndex)
		{
			int lostDeviceCount = s_GlobalState.allUserData[userIndex].lostDeviceCount;
			if (lostDeviceCount <= 0)
			{
				return;
			}
			int lostDeviceStartIndex = s_GlobalState.allUserData[userIndex].lostDeviceStartIndex;
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseSliceWithCapacity(ref s_GlobalState.allLostDevices, ref s_GlobalState.allLostDeviceCount, lostDeviceStartIndex, lostDeviceCount);
			s_GlobalState.allUserData[userIndex].lostDeviceCount = 0;
			s_GlobalState.allUserData[userIndex].lostDeviceStartIndex = 0;
			for (int i = 0; i < s_GlobalState.allUserCount; i++)
			{
				if (s_GlobalState.allUserData[i].lostDeviceStartIndex > lostDeviceStartIndex)
				{
					s_GlobalState.allUserData[i].lostDeviceStartIndex -= lostDeviceCount;
				}
			}
		}

		public void UnpairDevicesAndRemoveUser()
		{
			UnpairDevices();
			RemoveUser(index);
			m_Id = 0u;
		}

		public static global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> GetUnpairedInputDevices()
		{
			global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> list = new global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice>(global::Unity.Collections.Allocator.Temp);
			GetUnpairedInputDevices(ref list);
			return list;
		}

		public static int GetUnpairedInputDevices(ref global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> list)
		{
			int count = list.Count;
			foreach (global::UnityEngine.InputSystem.InputDevice device in global::UnityEngine.InputSystem.InputSystem.devices)
			{
				if (!global::UnityEngine.InputSystem.Utilities.ArrayHelpers.ContainsReference(s_GlobalState.allPairedDevices, s_GlobalState.allPairedDeviceCount, device))
				{
					list.Add(device);
				}
			}
			return list.Count - count;
		}

		public static global::UnityEngine.InputSystem.Users.InputUser? FindUserPairedToDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			int num = TryFindUserIndex(device);
			if (num == -1)
			{
				return null;
			}
			return s_GlobalState.allUsers[num];
		}

		public static global::UnityEngine.InputSystem.Users.InputUser? FindUserByAccount(global::UnityEngine.InputSystem.Users.InputUserAccountHandle platformUserAccountHandle)
		{
			if (platformUserAccountHandle == default(global::UnityEngine.InputSystem.Users.InputUserAccountHandle))
			{
				throw new global::System.ArgumentException("Empty platform user account handle", "platformUserAccountHandle");
			}
			int num = TryFindUserIndex(platformUserAccountHandle);
			if (num == -1)
			{
				return null;
			}
			return s_GlobalState.allUsers[num];
		}

		public static global::UnityEngine.InputSystem.Users.InputUser CreateUserWithoutPairedDevices()
		{
			int num = AddUser();
			return s_GlobalState.allUsers[num];
		}

		public static global::UnityEngine.InputSystem.Users.InputUser PerformPairingWithDevice(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.Users.InputUser user = default(global::UnityEngine.InputSystem.Users.InputUser), global::UnityEngine.InputSystem.Users.InputUserPairingOptions options = global::UnityEngine.InputSystem.Users.InputUserPairingOptions.None)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (user != default(global::UnityEngine.InputSystem.Users.InputUser) && !user.valid)
			{
				throw new global::System.ArgumentException("Invalid user", "user");
			}
			int num;
			if (user == default(global::UnityEngine.InputSystem.Users.InputUser))
			{
				num = AddUser();
			}
			else
			{
				num = user.index;
				if ((options & global::UnityEngine.InputSystem.Users.InputUserPairingOptions.UnpairCurrentDevicesFromUser) != global::UnityEngine.InputSystem.Users.InputUserPairingOptions.None)
				{
					user.UnpairDevices();
				}
				if (global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.ContainsReference(user.pairedDevices, device))
				{
					if ((options & global::UnityEngine.InputSystem.Users.InputUserPairingOptions.ForcePlatformUserAccountSelection) != global::UnityEngine.InputSystem.Users.InputUserPairingOptions.None)
					{
						InitiateUserAccountSelection(num, device, options);
					}
					return user;
				}
			}
			if (!InitiateUserAccountSelection(num, device, options))
			{
				AddDeviceToUser(num, device);
			}
			return s_GlobalState.allUsers[num];
		}

		private static bool InitiateUserAccountSelection(int userIndex, global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.Users.InputUserPairingOptions options)
		{
			long num = (((options & global::UnityEngine.InputSystem.Users.InputUserPairingOptions.ForcePlatformUserAccountSelection) == 0) ? UpdatePlatformUserAccount(userIndex, device) : 0);
			if (((options & global::UnityEngine.InputSystem.Users.InputUserPairingOptions.ForcePlatformUserAccountSelection) != global::UnityEngine.InputSystem.Users.InputUserPairingOptions.None || (num != -1 && (num & 2) == 0L && (options & global::UnityEngine.InputSystem.Users.InputUserPairingOptions.ForceNoPlatformUserAccountSelection) == 0)) && InitiateUserAccountSelectionAtPlatformLevel(device))
			{
				s_GlobalState.allUserData[userIndex].flags |= global::UnityEngine.InputSystem.Users.InputUser.UserFlags.UserAccountSelectionInProgress;
				s_GlobalState.ongoingAccountSelections.Append(new global::UnityEngine.InputSystem.Users.InputUser.OngoingAccountSelection
				{
					device = device,
					userId = s_GlobalState.allUsers[userIndex].id
				});
				HookIntoDeviceChange();
				Notify(userIndex, global::UnityEngine.InputSystem.Users.InputUserChange.AccountSelectionInProgress, device);
				return true;
			}
			return false;
		}

		public bool Equals(global::UnityEngine.InputSystem.Users.InputUser other)
		{
			return m_Id == other.m_Id;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.InputSystem.Users.InputUser)
			{
				return Equals((global::UnityEngine.InputSystem.Users.InputUser)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (int)m_Id;
		}

		public static bool operator ==(global::UnityEngine.InputSystem.Users.InputUser left, global::UnityEngine.InputSystem.Users.InputUser right)
		{
			return left.m_Id == right.m_Id;
		}

		public static bool operator !=(global::UnityEngine.InputSystem.Users.InputUser left, global::UnityEngine.InputSystem.Users.InputUser right)
		{
			return left.m_Id != right.m_Id;
		}

		private static int AddUser()
		{
			uint num = ++s_GlobalState.lastUserId;
			int count = s_GlobalState.allUserCount;
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref s_GlobalState.allUsers, ref count, new global::UnityEngine.InputSystem.Users.InputUser
			{
				m_Id = num
			});
			int num2 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref s_GlobalState.allUserData, ref s_GlobalState.allUserCount, default(global::UnityEngine.InputSystem.Users.InputUser.UserData));
			Notify(num2, global::UnityEngine.InputSystem.Users.InputUserChange.Added, null);
			return num2;
		}

		private static void RemoveUser(int userIndex)
		{
			if (s_GlobalState.allUserData[userIndex].controlScheme.HasValue && s_GlobalState.allUserData[userIndex].actions != null)
			{
				s_GlobalState.allUserData[userIndex].actions.bindingMask = null;
			}
			s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
			RemoveLostDevicesForUser(userIndex);
			for (int i = 0; i < s_GlobalState.ongoingAccountSelections.length; i++)
			{
				if (s_GlobalState.ongoingAccountSelections[i].userId == s_GlobalState.allUsers[userIndex].id)
				{
					s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(i);
					i--;
				}
			}
			Notify(userIndex, global::UnityEngine.InputSystem.Users.InputUserChange.Removed, null);
			int count = s_GlobalState.allUserCount;
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(s_GlobalState.allUsers, ref count, userIndex);
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(s_GlobalState.allUserData, ref s_GlobalState.allUserCount, userIndex);
			if (s_GlobalState.allUserCount == 0)
			{
				UnhookFromDeviceChange();
				UnhookFromActionChange();
			}
		}

		private static void Notify(int userIndex, global::UnityEngine.InputSystem.Users.InputUserChange change, global::UnityEngine.InputSystem.InputDevice device)
		{
			if (s_GlobalState.onChange.length == 0)
			{
				return;
			}
			s_GlobalState.onChange.LockForChanges();
			for (int i = 0; i < s_GlobalState.onChange.length; i++)
			{
				try
				{
					s_GlobalState.onChange[i](s_GlobalState.allUsers[userIndex], change, device);
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing 'InputUser.onChange' callbacks");
					global::UnityEngine.Debug.LogException(ex);
				}
			}
			s_GlobalState.onChange.UnlockForChanges();
		}

		private static int TryFindUserIndex(uint userId)
		{
			for (int i = 0; i < s_GlobalState.allUserCount; i++)
			{
				if (s_GlobalState.allUsers[i].m_Id == userId)
				{
					return i;
				}
			}
			return -1;
		}

		private static int TryFindUserIndex(global::UnityEngine.InputSystem.Users.InputUserAccountHandle platformHandle)
		{
			for (int i = 0; i < s_GlobalState.allUserCount; i++)
			{
				if (s_GlobalState.allUserData[i].platformUserAccountHandle == platformHandle)
				{
					return i;
				}
			}
			return -1;
		}

		private static int TryFindUserIndex(global::UnityEngine.InputSystem.InputDevice device)
		{
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_GlobalState.allPairedDevices, device, s_GlobalState.allPairedDeviceCount);
			if (num == -1)
			{
				return -1;
			}
			for (int i = 0; i < s_GlobalState.allUserCount; i++)
			{
				int deviceStartIndex = s_GlobalState.allUserData[i].deviceStartIndex;
				if (deviceStartIndex <= num && num < deviceStartIndex + s_GlobalState.allUserData[i].deviceCount)
				{
					return i;
				}
			}
			return -1;
		}

		private static void AddDeviceToUser(int userIndex, global::UnityEngine.InputSystem.InputDevice device, bool asLostDevice = false, bool dontUpdateControlScheme = false)
		{
			int num = (asLostDevice ? s_GlobalState.allUserData[userIndex].lostDeviceCount : s_GlobalState.allUserData[userIndex].deviceCount);
			int num2 = (asLostDevice ? s_GlobalState.allUserData[userIndex].lostDeviceStartIndex : s_GlobalState.allUserData[userIndex].deviceStartIndex);
			s_GlobalState.pairingStateVersion++;
			if (num > 0)
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.MoveSlice(asLostDevice ? s_GlobalState.allLostDevices : s_GlobalState.allPairedDevices, num2, asLostDevice ? (s_GlobalState.allLostDeviceCount - num) : (s_GlobalState.allPairedDeviceCount - num), num);
				for (int i = 0; i < s_GlobalState.allUserCount; i++)
				{
					if (i != userIndex && (asLostDevice ? s_GlobalState.allUserData[i].lostDeviceStartIndex : s_GlobalState.allUserData[i].deviceStartIndex) > num2)
					{
						if (asLostDevice)
						{
							s_GlobalState.allUserData[i].lostDeviceStartIndex -= num;
						}
						else
						{
							s_GlobalState.allUserData[i].deviceStartIndex -= num;
						}
					}
				}
			}
			if (asLostDevice)
			{
				s_GlobalState.allUserData[userIndex].lostDeviceStartIndex = s_GlobalState.allLostDeviceCount - num;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref s_GlobalState.allLostDevices, ref s_GlobalState.allLostDeviceCount, device);
				s_GlobalState.allUserData[userIndex].lostDeviceCount++;
			}
			else
			{
				s_GlobalState.allUserData[userIndex].deviceStartIndex = s_GlobalState.allPairedDeviceCount - num;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref s_GlobalState.allPairedDevices, ref s_GlobalState.allPairedDeviceCount, device);
				s_GlobalState.allUserData[userIndex].deviceCount++;
				global::UnityEngine.InputSystem.IInputActionCollection inputActionCollection = s_GlobalState.allUserData[userIndex].actions;
				if (inputActionCollection != null)
				{
					inputActionCollection.devices = s_GlobalState.allUsers[userIndex].pairedDevices;
					if (!dontUpdateControlScheme && s_GlobalState.allUserData[userIndex].controlScheme.HasValue)
					{
						UpdateControlSchemeMatch(userIndex);
					}
				}
			}
			HookIntoDeviceChange();
			Notify(userIndex, asLostDevice ? global::UnityEngine.InputSystem.Users.InputUserChange.DeviceLost : global::UnityEngine.InputSystem.Users.InputUserChange.DevicePaired, device);
		}

		private static void RemoveDeviceFromUser(int userIndex, global::UnityEngine.InputSystem.InputDevice device, bool asLostDevice = false)
		{
			int num = (asLostDevice ? global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_GlobalState.allLostDevices, device, s_GlobalState.allLostDeviceCount) : global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_GlobalState.allPairedDevices, device, s_GlobalState.allUserData[userIndex].deviceStartIndex, s_GlobalState.allUserData[userIndex].deviceCount));
			if (num == -1)
			{
				return;
			}
			if (asLostDevice)
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(s_GlobalState.allLostDevices, ref s_GlobalState.allLostDeviceCount, num);
				s_GlobalState.allUserData[userIndex].lostDeviceCount--;
			}
			else
			{
				s_GlobalState.pairingStateVersion++;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(s_GlobalState.allPairedDevices, ref s_GlobalState.allPairedDeviceCount, num);
				s_GlobalState.allUserData[userIndex].deviceCount--;
			}
			for (int i = 0; i < s_GlobalState.allUserCount; i++)
			{
				if ((asLostDevice ? s_GlobalState.allUserData[i].lostDeviceStartIndex : s_GlobalState.allUserData[i].deviceStartIndex) > num)
				{
					if (asLostDevice)
					{
						s_GlobalState.allUserData[i].lostDeviceStartIndex--;
					}
					else
					{
						s_GlobalState.allUserData[i].deviceStartIndex--;
					}
				}
			}
			if (asLostDevice)
			{
				return;
			}
			for (int j = 0; j < s_GlobalState.ongoingAccountSelections.length; j++)
			{
				if (s_GlobalState.ongoingAccountSelections[j].userId == s_GlobalState.allUsers[userIndex].id && s_GlobalState.ongoingAccountSelections[j].device == device)
				{
					s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(j);
					j--;
				}
			}
			global::UnityEngine.InputSystem.IInputActionCollection inputActionCollection = s_GlobalState.allUserData[userIndex].actions;
			if (inputActionCollection != null)
			{
				inputActionCollection.devices = s_GlobalState.allUsers[userIndex].pairedDevices;
				if (s_GlobalState.allUsers[userIndex].controlScheme.HasValue)
				{
					UpdateControlSchemeMatch(userIndex);
				}
			}
			Notify(userIndex, global::UnityEngine.InputSystem.Users.InputUserChange.DeviceUnpaired, device);
		}

		private static void UpdateControlSchemeMatch(int userIndex, bool autoPairMissing = false)
		{
			if (!s_GlobalState.allUserData[userIndex].controlScheme.HasValue)
			{
				return;
			}
			s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
			global::UnityEngine.InputSystem.InputControlScheme.MatchResult matchResult = default(global::UnityEngine.InputSystem.InputControlScheme.MatchResult);
			try
			{
				global::UnityEngine.InputSystem.InputControlScheme value = s_GlobalState.allUserData[userIndex].controlScheme.Value;
				if (value.deviceRequirements.Count > 0)
				{
					global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> list = new global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice>(global::Unity.Collections.Allocator.Temp);
					try
					{
						list.AddSlice(s_GlobalState.allUsers[userIndex].pairedDevices);
						if (autoPairMissing)
						{
							int count = list.Count;
							int unpairedInputDevices = GetUnpairedInputDevices(ref list);
							if (s_GlobalState.allUserData[userIndex].platformUserAccountHandle.HasValue)
							{
								list.Sort(count, unpairedInputDevices, new global::UnityEngine.InputSystem.Users.InputUser.CompareDevicesByUserAccount
								{
									platformUserAccountHandle = s_GlobalState.allUserData[userIndex].platformUserAccountHandle.Value
								});
							}
						}
						matchResult = value.PickDevicesFrom(list);
						if (matchResult.isSuccessfulMatch && autoPairMissing)
						{
							s_GlobalState.allUserData[userIndex].controlSchemeMatch = matchResult;
							foreach (global::UnityEngine.InputSystem.InputDevice device in matchResult.devices)
							{
								if (!global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.ContainsReference(s_GlobalState.allUsers[userIndex].pairedDevices, device))
								{
									AddDeviceToUser(userIndex, device, asLostDevice: false, dontUpdateControlScheme: true);
								}
							}
						}
					}
					finally
					{
						list.Dispose();
					}
				}
				s_GlobalState.allUserData[userIndex].controlSchemeMatch = matchResult;
			}
			catch (global::System.Exception)
			{
				matchResult.Dispose();
				throw;
			}
		}

		private static long UpdatePlatformUserAccount(int userIndex, global::UnityEngine.InputSystem.InputDevice device)
		{
			global::UnityEngine.InputSystem.Users.InputUserAccountHandle? platformAccountHandle;
			string platformAccountName;
			string platformAccountId;
			long num = QueryPairedPlatformUserAccount(device, out platformAccountHandle, out platformAccountName, out platformAccountId);
			if (num == -1)
			{
				if ((s_GlobalState.allUserData[userIndex].flags & global::UnityEngine.InputSystem.Users.InputUser.UserFlags.UserAccountSelectionInProgress) != 0)
				{
					Notify(userIndex, global::UnityEngine.InputSystem.Users.InputUserChange.AccountSelectionCanceled, null);
				}
				s_GlobalState.allUserData[userIndex].platformUserAccountHandle = null;
				s_GlobalState.allUserData[userIndex].platformUserAccountName = null;
				s_GlobalState.allUserData[userIndex].platformUserAccountId = null;
				return num;
			}
			if ((s_GlobalState.allUserData[userIndex].flags & global::UnityEngine.InputSystem.Users.InputUser.UserFlags.UserAccountSelectionInProgress) != 0)
			{
				if ((num & 4) == 0L)
				{
					if ((num & 0x10) != 0L)
					{
						Notify(userIndex, global::UnityEngine.InputSystem.Users.InputUserChange.AccountSelectionCanceled, device);
					}
					else
					{
						s_GlobalState.allUserData[userIndex].flags &= ~global::UnityEngine.InputSystem.Users.InputUser.UserFlags.UserAccountSelectionInProgress;
						s_GlobalState.allUserData[userIndex].platformUserAccountHandle = platformAccountHandle;
						s_GlobalState.allUserData[userIndex].platformUserAccountName = platformAccountName;
						s_GlobalState.allUserData[userIndex].platformUserAccountId = platformAccountId;
						Notify(userIndex, global::UnityEngine.InputSystem.Users.InputUserChange.AccountSelectionComplete, device);
					}
				}
			}
			else
			{
				global::UnityEngine.InputSystem.Users.InputUserAccountHandle? inputUserAccountHandle = s_GlobalState.allUserData[userIndex].platformUserAccountHandle;
				global::UnityEngine.InputSystem.Users.InputUserAccountHandle? inputUserAccountHandle2 = platformAccountHandle;
				if (inputUserAccountHandle.HasValue != inputUserAccountHandle2.HasValue || (inputUserAccountHandle.HasValue && inputUserAccountHandle.GetValueOrDefault() != inputUserAccountHandle2.GetValueOrDefault()) || s_GlobalState.allUserData[userIndex].platformUserAccountId != platformAccountId)
				{
					s_GlobalState.allUserData[userIndex].platformUserAccountHandle = platformAccountHandle;
					s_GlobalState.allUserData[userIndex].platformUserAccountName = platformAccountName;
					s_GlobalState.allUserData[userIndex].platformUserAccountId = platformAccountId;
					Notify(userIndex, global::UnityEngine.InputSystem.Users.InputUserChange.AccountChanged, device);
				}
				else if (s_GlobalState.allUserData[userIndex].platformUserAccountName != platformAccountName)
				{
					Notify(userIndex, global::UnityEngine.InputSystem.Users.InputUserChange.AccountNameChanged, device);
				}
			}
			return num;
		}

		private static long QueryPairedPlatformUserAccount(global::UnityEngine.InputSystem.InputDevice device, out global::UnityEngine.InputSystem.Users.InputUserAccountHandle? platformAccountHandle, out string platformAccountName, out string platformAccountId)
		{
			global::UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand command = global::UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand.Create();
			long num = device.ExecuteCommand(ref command);
			if (num == -1)
			{
				platformAccountHandle = null;
				platformAccountName = null;
				platformAccountId = null;
				return -1L;
			}
			if ((num & 2) != 0L)
			{
				platformAccountHandle = new global::UnityEngine.InputSystem.Users.InputUserAccountHandle(device.description.interfaceName ?? "<Unknown>", command.handle);
				platformAccountName = command.name;
				platformAccountId = command.id;
			}
			else
			{
				platformAccountHandle = null;
				platformAccountName = null;
				platformAccountId = null;
			}
			return num;
		}

		private static bool InitiateUserAccountSelectionAtPlatformLevel(global::UnityEngine.InputSystem.InputDevice device)
		{
			global::UnityEngine.InputSystem.LowLevel.InitiateUserAccountPairingCommand command = global::UnityEngine.InputSystem.LowLevel.InitiateUserAccountPairingCommand.Create();
			long num = device.ExecuteCommand(ref command);
			if (num == -2)
			{
				throw new global::System.InvalidOperationException("User pairing already in progress");
			}
			return num == 1;
		}

		private static void OnActionChange(object obj, global::UnityEngine.InputSystem.InputActionChange change)
		{
			if (change != global::UnityEngine.InputSystem.InputActionChange.BoundControlsChanged)
			{
				return;
			}
			for (int i = 0; i < s_GlobalState.allUserCount; i++)
			{
				if (s_GlobalState.allUsers[i].actions == obj)
				{
					Notify(i, global::UnityEngine.InputSystem.Users.InputUserChange.ControlsChanged, null);
				}
			}
		}

		private static void OnDeviceChange(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputDeviceChange change)
		{
			switch (change)
			{
			case global::UnityEngine.InputSystem.InputDeviceChange.Removed:
			{
				for (int num6 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_GlobalState.allPairedDevices, device, s_GlobalState.allPairedDeviceCount); num6 != -1; num6 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_GlobalState.allPairedDevices, device, s_GlobalState.allPairedDeviceCount))
				{
					int userIndex2 = -1;
					for (int l = 0; l < s_GlobalState.allUserCount; l++)
					{
						int deviceStartIndex2 = s_GlobalState.allUserData[l].deviceStartIndex;
						if (deviceStartIndex2 <= num6 && num6 < deviceStartIndex2 + s_GlobalState.allUserData[l].deviceCount)
						{
							userIndex2 = l;
							break;
						}
					}
					AddDeviceToUser(userIndex2, device, asLostDevice: true);
					RemoveDeviceFromUser(userIndex2, device);
				}
				break;
			}
			case global::UnityEngine.InputSystem.InputDeviceChange.Added:
			{
				for (int num5 = FindLostDevice(device); num5 != -1; num5 = FindLostDevice(device, num5))
				{
					int userIndex = -1;
					for (int k = 0; k < s_GlobalState.allUserCount; k++)
					{
						int lostDeviceStartIndex = s_GlobalState.allUserData[k].lostDeviceStartIndex;
						if (lostDeviceStartIndex <= num5 && num5 < lostDeviceStartIndex + s_GlobalState.allUserData[k].lostDeviceCount)
						{
							userIndex = k;
							break;
						}
					}
					RemoveDeviceFromUser(userIndex, s_GlobalState.allLostDevices[num5], asLostDevice: true);
					Notify(userIndex, global::UnityEngine.InputSystem.Users.InputUserChange.DeviceRegained, device);
					AddDeviceToUser(userIndex, device);
				}
				break;
			}
			case global::UnityEngine.InputSystem.InputDeviceChange.ConfigurationChanged:
			{
				bool flag = false;
				for (int i = 0; i < s_GlobalState.ongoingAccountSelections.length; i++)
				{
					if (s_GlobalState.ongoingAccountSelections[i].device != device)
					{
						continue;
					}
					global::UnityEngine.InputSystem.Users.InputUser inputUser = new global::UnityEngine.InputSystem.Users.InputUser
					{
						m_Id = s_GlobalState.ongoingAccountSelections[i].userId
					};
					int num = inputUser.index;
					if ((UpdatePlatformUserAccount(num, device) & 4) == 0L)
					{
						flag = true;
						s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(i);
						i--;
						if (!global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.ContainsReference(s_GlobalState.allUsers[num].pairedDevices, device))
						{
							AddDeviceToUser(num, device);
						}
					}
				}
				if (flag)
				{
					break;
				}
				int num2 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_GlobalState.allPairedDevices, device, s_GlobalState.allPairedDeviceCount);
				while (num2 != -1)
				{
					int num3 = -1;
					for (int j = 0; j < s_GlobalState.allUserCount; j++)
					{
						int deviceStartIndex = s_GlobalState.allUserData[j].deviceStartIndex;
						if (deviceStartIndex <= num2 && num2 < deviceStartIndex + s_GlobalState.allUserData[j].deviceCount)
						{
							num3 = j;
							break;
						}
					}
					UpdatePlatformUserAccount(num3, device);
					int num4 = num2 + global::System.Math.Max(1, s_GlobalState.allUserData[num3].deviceCount);
					num2 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_GlobalState.allPairedDevices, device, num4, s_GlobalState.allPairedDeviceCount - num4);
				}
				break;
			}
			}
		}

		private static int FindLostDevice(global::UnityEngine.InputSystem.InputDevice device, int startIndex = 0)
		{
			int deviceId = device.deviceId;
			for (int i = startIndex; i < s_GlobalState.allLostDeviceCount; i++)
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = s_GlobalState.allLostDevices[i];
				if (device == inputDevice || inputDevice.deviceId == deviceId)
				{
					return i;
				}
			}
			return -1;
		}

		private static void OnEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.InputDevice device)
		{
			if (s_GlobalState.listenForUnpairedDeviceActivity == 0)
			{
				return;
			}
			global::UnityEngine.InputSystem.Utilities.FourCC type = eventPtr.type;
			if ((type != 1398030676 && type != 1145852993) || !device.enabled || global::UnityEngine.InputSystem.Utilities.ArrayHelpers.ContainsReference(s_GlobalState.allPairedDevices, s_GlobalState.allPairedDeviceCount, device) || !global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe_AnyCallbackReturnsTrue(ref s_GlobalState.onPreFilterUnpairedDeviceUsed, device, eventPtr, "InputUser.onPreFilterUnpairedDeviceActivity"))
			{
				return;
			}
			foreach (global::UnityEngine.InputSystem.InputControl item in eventPtr.EnumerateChangedControls(device, 0.0001f))
			{
				bool flag = false;
				s_GlobalState.onUnpairedDeviceUsed.LockForChanges();
				for (int i = 0; i < s_GlobalState.onUnpairedDeviceUsed.length; i++)
				{
					int pairingStateVersion = s_GlobalState.pairingStateVersion;
					try
					{
						s_GlobalState.onUnpairedDeviceUsed[i](item, eventPtr);
					}
					catch (global::System.Exception ex)
					{
						global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing 'InputUser.onUnpairedDeviceUsed' callbacks");
						global::UnityEngine.Debug.LogException(ex);
					}
					if (pairingStateVersion != s_GlobalState.pairingStateVersion && FindUserPairedToDevice(device).HasValue)
					{
						flag = true;
						break;
					}
				}
				s_GlobalState.onUnpairedDeviceUsed.UnlockForChanges();
				if (flag)
				{
					break;
				}
			}
		}

		internal static global::UnityEngine.InputSystem.Utilities.ISavedState SaveAndResetState()
		{
			global::UnityEngine.InputSystem.Utilities.SavedStructState<global::UnityEngine.InputSystem.Users.InputUser.GlobalState> result = new global::UnityEngine.InputSystem.Utilities.SavedStructState<global::UnityEngine.InputSystem.Users.InputUser.GlobalState>(ref s_GlobalState, delegate(ref global::UnityEngine.InputSystem.Users.InputUser.GlobalState state)
			{
				s_GlobalState = state;
			}, delegate
			{
				DisposeAndResetGlobalState();
			});
			s_GlobalState = default(global::UnityEngine.InputSystem.Users.InputUser.GlobalState);
			return result;
		}

		private static void HookIntoActionChange()
		{
			if (!s_GlobalState.onActionChangeHooked)
			{
				if (s_GlobalState.actionChangeDelegate == null)
				{
					s_GlobalState.actionChangeDelegate = OnActionChange;
				}
				global::UnityEngine.InputSystem.InputSystem.onActionChange += OnActionChange;
				s_GlobalState.onActionChangeHooked = true;
			}
		}

		private static void UnhookFromActionChange()
		{
			if (s_GlobalState.onActionChangeHooked)
			{
				global::UnityEngine.InputSystem.InputSystem.onActionChange -= OnActionChange;
				s_GlobalState.onActionChangeHooked = false;
			}
		}

		private static void HookIntoDeviceChange()
		{
			if (!s_GlobalState.onDeviceChangeHooked)
			{
				if (s_GlobalState.onDeviceChangeDelegate == null)
				{
					s_GlobalState.onDeviceChangeDelegate = OnDeviceChange;
				}
				global::UnityEngine.InputSystem.InputSystem.onDeviceChange += s_GlobalState.onDeviceChangeDelegate;
				s_GlobalState.onDeviceChangeHooked = true;
			}
		}

		private static void UnhookFromDeviceChange()
		{
			if (s_GlobalState.onDeviceChangeHooked)
			{
				global::UnityEngine.InputSystem.InputSystem.onDeviceChange -= s_GlobalState.onDeviceChangeDelegate;
				s_GlobalState.onDeviceChangeHooked = false;
			}
		}

		private static void HookIntoEvents()
		{
			if (!s_GlobalState.onEventHooked)
			{
				if (s_GlobalState.onEventDelegate == null)
				{
					s_GlobalState.onEventDelegate = OnEvent;
				}
				global::UnityEngine.InputSystem.InputSystem.onEvent += s_GlobalState.onEventDelegate;
				s_GlobalState.onEventHooked = true;
			}
		}

		private static void UnhookFromDeviceStateChange()
		{
			if (s_GlobalState.onEventHooked)
			{
				global::UnityEngine.InputSystem.InputSystem.onEvent -= s_GlobalState.onEventDelegate;
				s_GlobalState.onEventHooked = false;
			}
		}

		private static void DisposeAndResetGlobalState()
		{
			for (int i = 0; i < s_GlobalState.allUserCount; i++)
			{
				s_GlobalState.allUserData[i].controlSchemeMatch.Dispose();
			}
			uint lastUserId = s_GlobalState.lastUserId;
			s_GlobalState = default(global::UnityEngine.InputSystem.Users.InputUser.GlobalState);
			s_GlobalState.lastUserId = lastUserId;
		}

		internal static void ResetGlobals()
		{
			UnhookFromActionChange();
			UnhookFromDeviceChange();
			UnhookFromDeviceStateChange();
			DisposeAndResetGlobalState();
		}
	}
}
