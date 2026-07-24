namespace Unity.Services.Core.Internal
{
	internal class UnityServicesInternal : global::Unity.Services.Core.IUnityServices
	{
		internal const string InitSuccessEventInvocationError = "Exception in services initialization success event handler: ";

		internal const string InitFailureEventInvocationError = "Exception in services initialization failure event handler: ";

		internal bool CanInitialize;

		private global::System.Threading.Tasks.TaskCompletionSource<object> m_Initialization;

		public global::Unity.Services.Core.ServicesInitializationState State { get; private set; }

		public global::Unity.Services.Core.InitializationOptions Options
		{
			get
			{
				return Registry.Options;
			}
			internal set
			{
				Registry.Options = value;
			}
		}

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Services.Core.Internal.CoreRegistry Registry { get; }

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Services.Core.Internal.CoreMetrics Metrics { get; }

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Services.Core.Internal.CoreDiagnostics Diagnostics { get; }

		public event global::System.Action Initialized;

		public event global::System.Action<global::System.Exception> InitializeFailed;

		public UnityServicesInternal([global::JetBrains.Annotations.NotNull] global::Unity.Services.Core.Internal.CoreRegistry registry, [global::JetBrains.Annotations.NotNull] global::Unity.Services.Core.Internal.CoreMetrics coreMetrics, [global::JetBrains.Annotations.NotNull] global::Unity.Services.Core.Internal.CoreDiagnostics coreDiagnostics)
		{
			Registry = registry;
			Metrics = coreMetrics;
			Diagnostics = coreDiagnostics;
		}

		public async global::System.Threading.Tasks.Task InitializeAsync(global::Unity.Services.Core.InitializationOptions options)
		{
			_ = 1;
			try
			{
				if (options == null)
				{
					options = new global::Unity.Services.Core.InitializationOptions();
				}
				if (!HasRequestedInitialization() || HasInitializationFailed())
				{
					Registry.Options = options;
					m_Initialization = new global::System.Threading.Tasks.TaskCompletionSource<object>();
				}
				if (CanInitialize && State == global::Unity.Services.Core.ServicesInitializationState.Uninitialized)
				{
					await InitializeServicesAsync();
				}
				else
				{
					await m_Initialization.Task;
				}
				TriggerInitializeSuccess();
			}
			catch (global::System.Exception initException)
			{
				TriggerInitializeFailed(initException);
				throw;
			}
			bool HasInitializationFailed()
			{
				if (m_Initialization.Task.IsCompleted)
				{
					return m_Initialization.Task.Status != global::System.Threading.Tasks.TaskStatus.RanToCompletion;
				}
				return false;
			}
		}

		public string GetIdentifier()
		{
			return Registry.InstanceId;
		}

		private void TriggerInitializeSuccess()
		{
			try
			{
				this.Initialized?.Invoke();
			}
			catch (global::System.Exception arg)
			{
				global::Unity.Services.Core.Internal.CoreLogger.LogError(string.Format("{0} {1}", "Exception in services initialization success event handler: ", arg));
			}
		}

		private void TriggerInitializeFailed(global::System.Exception initException)
		{
			try
			{
				this.InitializeFailed?.Invoke(initException);
			}
			catch (global::System.Exception arg)
			{
				global::Unity.Services.Core.Internal.CoreLogger.LogError(string.Format("{0} {1}", "Exception in services initialization failure event handler: ", arg));
			}
		}

		public T GetService<T>()
		{
			return Registry.GetService<T>();
		}

		private bool HasRequestedInitialization()
		{
			return m_Initialization != null;
		}

		private async global::System.Threading.Tasks.Task InitializeServicesAsync()
		{
			State = global::Unity.Services.Core.ServicesInitializationState.Initializing;
			global::System.Diagnostics.Stopwatch initStopwatch = new global::System.Diagnostics.Stopwatch();
			initStopwatch.Start();
			global::Unity.Services.Core.Internal.DependencyTree dependencyTree = Registry.PackageRegistry.Tree;
			if (dependencyTree == null)
			{
				global::System.NullReferenceException ex = new global::System.NullReferenceException("Services require a valid dependency tree to be initialized.");
				FailServicesInitialization(ex);
				throw ex;
			}
			global::System.Collections.Generic.List<int> sortedPackageTypeHashes = new global::System.Collections.Generic.List<int>(dependencyTree.PackageTypeHashToInstance.Count);
			try
			{
				SortPackages();
				await InitializePackagesAsync();
			}
			catch (global::System.Exception reason)
			{
				FailServicesInitialization(reason);
				throw;
			}
			SucceedServicesInitialization();
			void FailServicesInitialization(global::System.Exception exception)
			{
				State = global::Unity.Services.Core.ServicesInitializationState.Uninitialized;
				initStopwatch.Stop();
				m_Initialization.TrySetException(exception);
			}
			async global::System.Threading.Tasks.Task InitializePackagesAsync()
			{
				await new global::Unity.Services.Core.Internal.CoreRegistryInitializer(Registry, sortedPackageTypeHashes).InitializeRegistryAsync();
			}
			void SortPackages()
			{
				new global::Unity.Services.Core.Internal.DependencyTreeInitializeOrderSorter(dependencyTree, sortedPackageTypeHashes).SortRegisteredPackagesIntoTarget();
			}
			void SucceedServicesInitialization()
			{
				State = global::Unity.Services.Core.ServicesInitializationState.Initialized;
				Registry.LockComponentRegistration();
				initStopwatch.Stop();
				m_Initialization.TrySetResult(null);
			}
		}

		internal void SendInitializationMetrics(global::System.Collections.Generic.List<global::Unity.Services.Core.Internal.PackageInitializationInfo> packageInitInfos)
		{
			foreach (global::Unity.Services.Core.Internal.PackageInitializationInfo packageInitInfo in packageInitInfos)
			{
				Metrics.SendInitTimeMetricForPackage(packageInitInfo.PackageType, packageInitInfo.InitializationTimeInSeconds);
			}
		}

		internal void EnableInitialization()
		{
			CanInitialize = true;
		}

		internal async global::System.Threading.Tasks.Task EnableInitializationAsync()
		{
			CanInitialize = true;
			global::Unity.Services.Core.Internal.CorePackageRegistry.Instance.Lock();
			if (HasRequestedInitialization())
			{
				await InitializeServicesAsync();
			}
		}
	}
}
