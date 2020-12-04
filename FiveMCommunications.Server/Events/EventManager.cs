namespace FiveMCommunications.Server.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Communications;
    using Communications.Contacts;
    using Core.Diagnostics;
    using Diagnostics;

    public class EventManager
	{
		private readonly ILogger _logger;
		private readonly Dictionary<string, List<Delegate>> _subscriptions = new Dictionary<string, List<Delegate>>();

		public EventManager(LogLevel level)
		{
			this._logger = new FivemCommunicationsLogger(level, "Events");
		}

		public void Off(string @event, Delegate action)
		{
			lock (this._subscriptions)
			{
				if (this._subscriptions.ContainsKey(@event) && this._subscriptions[@event].Contains(action))
				{
					this._subscriptions[@event].Remove(action);
				}
			}
		}

		public void Emit(string @event, params object[] args)
		{
			lock (this._subscriptions)
			{
				if (!this._subscriptions.ContainsKey(@event)) return;

				var message = new CommunicationMessage(@event, this);

				this._logger.Trace(args.Length > 0 ? $"Emit: \"{@event}\" with {args.Length} payload(s): {string.Join(", ", args.Select(a => a?.ToString() ?? "NULL"))}" : $"Emit: \"{@event}\" without payload");

				foreach (var subscription in this._subscriptions[@event])
				{
					var payload = new List<object> { message };
					payload.AddRange(args);

					subscription.DynamicInvoke(payload.ToArray());
				}
			}
		}

		internal void On(string @event, Delegate action)
		{
			lock (this._subscriptions)
			{
				if (!this._subscriptions.ContainsKey(@event))
				{
					this._subscriptions.Add(@event, new List<Delegate>());
				}

				this._subscriptions[@event].Add(action);

				this._logger.Trace($"On: \"{@event}\" attached to \"{action.Method.DeclaringType?.Name}.{action.Method.Name}({string.Join(", ", action.Method.GetParameters().Select(p => p.ParameterType + " " + p.Name))})\"");
			}
		}

		internal void OnRequest(string @event, Delegate action)
		{
			lock (this._subscriptions)
			{
				if (!this._subscriptions.ContainsKey(@event))
				{
					this._subscriptions.Add(@event, new List<Delegate>());
				}

				this._subscriptions[@event].Add(action);

				this._logger.Trace($"On: \"{@event}\" attached to \"{action.Method.DeclaringType?.Name}.{action.Method.Name}({string.Join(", ", action.Method.GetParameters().Select(p => p.ParameterType + " " + p.Name))})\"");
			}
		}


		internal async Task<TReturn> Request<TReturn>(string @event, params object[] args)
		{
			var message = new CommunicationMessage(@event, this);
			var tcs = new TaskCompletionSource<TReturn>();

			try
			{
				On($"{message.Id}:{@event}", new Action<ICommunicationMessage, TReturn>((e, data) =>
				{
					tcs.SetResult(data);
				}));

				this._logger.Trace(args.Length > 0 ? $"Request Emit: \"{@event}\" with {args.Length} payload(s): {string.Join(", ", args.Select(a => a?.ToString() ?? "NULL"))}" : $"Fire: \"{@event}\" without payload");

				lock (this._subscriptions)
				{
					var payload = new List<object> { message };
					payload.AddRange(args);

					this._subscriptions.Single(s => s.Key == @event).Value.Single().DynamicInvoke(payload.ToArray());
				}

				return await tcs.Task;
			}
			finally
			{
				lock (this._subscriptions)
				{
					this._subscriptions.Remove($"{message.Id}:{@event}");
				}
			}
		}
	}
}
