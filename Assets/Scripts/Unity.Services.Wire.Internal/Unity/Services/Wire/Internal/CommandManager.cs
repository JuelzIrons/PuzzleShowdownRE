namespace Unity.Services.Wire.Internal
{
	internal class CommandManager
	{
		private readonly global::System.Collections.Concurrent.ConcurrentDictionary<uint, global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Wire.Protocol.Internal.Reply>> m_Commands;

		public global::Unity.Services.Wire.Internal.Configuration Config;

		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		public CommandManager(global::Unity.Services.Wire.Internal.Configuration configuration, global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler)
		{
			m_Commands = new global::System.Collections.Concurrent.ConcurrentDictionary<uint, global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Wire.Protocol.Internal.Reply>>();
			m_ActionScheduler = actionScheduler;
			Config = configuration;
		}

		public void Clear()
		{
			m_Commands.Clear();
		}

		public void RegisterCommand(uint id)
		{
			global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Wire.Protocol.Internal.Reply> commandTCS = new global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Wire.Protocol.Internal.Reply>();
			m_ActionScheduler.ScheduleAction(delegate
			{
				commandTCS?.TrySetCanceled();
			}, Config.CommandTimeoutInSeconds);
			if (!m_Commands.TryAdd(id, commandTCS))
			{
				throw new global::Unity.Services.Wire.Internal.CommandAlreadyExists(id);
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Wire.Protocol.Internal.Reply> WaitForCommandAsync(uint id)
		{
			if (!m_Commands.TryGetValue(id, out var value))
			{
				throw new global::Unity.Services.Wire.Internal.CommandNotFoundException(id);
			}
			try
			{
				return await value.Task;
			}
			catch (global::System.Threading.Tasks.TaskCanceledException)
			{
				throw new global::System.TimeoutException($"Command {id} timed out.");
			}
			finally
			{
				m_Commands.TryRemove(id, out var _);
			}
		}

		public void OnDisconnect(global::System.Exception exceptionToThrow)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Wire.Protocol.Internal.Reply>> command in m_Commands)
			{
				command.Value.TrySetException(exceptionToThrow);
			}
		}

		public void OnCommandReplyReceived(global::Unity.Services.Wire.Protocol.Internal.Reply reply)
		{
			if (m_Commands.TryGetValue(reply.id, out var value))
			{
				if (reply.HasError())
				{
					value.TrySetException(CentrifugeErrorToException(reply.error));
				}
				else
				{
					value.TrySetResult(reply);
				}
				return;
			}
			throw new global::Unity.Services.Wire.Internal.UnknownCommandReplyException(reply.id);
		}

		private global::System.Exception CentrifugeErrorToException(global::Unity.Services.Wire.Protocol.Internal.Error error)
		{
			if (error.code == global::Unity.Services.Wire.Protocol.Internal.CentrifugeErrorCode.ErrorUnauthorized)
			{
				return new global::Unity.Services.Core.RequestFailedException(23007, error.message);
			}
			return new global::Unity.Services.Core.RequestFailedException(23000, error.message);
		}
	}
}
