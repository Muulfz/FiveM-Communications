namespace FiveMCommunications.Server.Rpc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CitizenFX.Core;
    using Communications;
    using Communications.Contacts;
    using Core;
    using Core.Diagnostics;
    using Core.Events;
    using Diagnostics;

    public static class RpcManager
	{
		private static readonly Serializer Serializer = new Serializer();
		private static ILogger _logger;
		private static EventHandlerDictionary _events;
		private static PlayerList _players;

		internal static void Configure(LogLevel level, EventHandlerDictionary eventHandler, PlayerList playerList)
		{
			_logger = new FivemCommunicationsLogger(level, "RPC");
			_events = eventHandler;
			_players = playerList;
		}

		public static void OnRaw(string @event, Delegate callback)
		{
			_logger.Trace($"OnRaw: \"{@event}\" attached to \"{PrintCallback(callback)}\"");

			_events[@event] += callback;
		}

		public static void On(string @event, IClient target, Action<ICommunicationMessage> callback)
		{
			InternalOn(@event, target, callback, m => new object[0]);
		}

		public static void On<T>(string @event, IClient target, Action<ICommunicationMessage, T> callback)
		{
			InternalOn(@event, target, callback, m => new object[]
			{
				Serializer.Deserialize<T>(m.Payloads[0])
			});
		}

		public static void On<T1, T2>(string @event, IClient target, Action<ICommunicationMessage, T1, T2> callback)
		{
			InternalOn(@event, target, callback, m => new object[]
			{
				Serializer.Deserialize<T1>(m.Payloads[0]),
				Serializer.Deserialize<T2>(m.Payloads[1])
			});
		}

		public static void On<T1, T2, T3>(string @event, IClient target, Action<ICommunicationMessage, T1, T2, T3> callback)
		{
			InternalOn(@event, target, callback, m => new object[]
			{
				Serializer.Deserialize<T1>(m.Payloads[0]),
				Serializer.Deserialize<T2>(m.Payloads[1]),
				Serializer.Deserialize<T3>(m.Payloads[2])
			});
		}

		public static void On<T1, T2, T3, T4>(string @event, IClient target, Action<ICommunicationMessage, T1, T2, T3, T4> callback)
		{
			InternalOn(@event, target, callback, m => new object[]
			{
				Serializer.Deserialize<T1>(m.Payloads[0]),
				Serializer.Deserialize<T2>(m.Payloads[1]),
				Serializer.Deserialize<T3>(m.Payloads[2]),
				Serializer.Deserialize<T4>(m.Payloads[3])
			});
		}

		public static void On<T1, T2, T3, T4, T5>(string @event, IClient target, Action<ICommunicationMessage, T1, T2, T3, T4, T5> callback)
		{
			InternalOn(@event, target, callback, m => new object[]
			{
				Serializer.Deserialize<T1>(m.Payloads[0]),
				Serializer.Deserialize<T2>(m.Payloads[1]),
				Serializer.Deserialize<T3>(m.Payloads[2]),
				Serializer.Deserialize<T4>(m.Payloads[3]),
				Serializer.Deserialize<T5>(m.Payloads[4])
			});
		}

		public static void OnRequest(string @event, IClient target, Action<ICommunicationMessage> callback)
		{
			InternalOnRequest(@event, target, callback, m => new object[0]);
		}

		public static void OnRequest<T>(string @event, IClient target, Action<ICommunicationMessage, T> callback)
		{
			InternalOnRequest(@event, target, callback, m =>
			{
				_logger.Trace($"Got payload: {m.Payloads[0]}, converting to type {typeof(T).FullName}");
				_logger.Trace($"Got message: {new Serializer().Serialize(m)}");

				return new object[]
				{
					Serializer.Deserialize<T>(m.Payloads[0])
				};
			});
		}

		public static void OnRequest<T1, T2>(string @event, IClient target, Action<ICommunicationMessage, T1, T2> callback)
		{
			InternalOnRequest(@event, target, callback, m => new object[]
			{
				Serializer.Deserialize<T1>(m.Payloads[0]),
				Serializer.Deserialize<T2>(m.Payloads[1])
			});
		}

		public static void OnRequest<T1, T2, T3>(string @event, IClient target, Action<ICommunicationMessage, T1, T2, T3> callback)
		{
			InternalOnRequest(@event, target, callback, m => new object[]
			{
				Serializer.Deserialize<T1>(m.Payloads[0]),
				Serializer.Deserialize<T2>(m.Payloads[1]),
				Serializer.Deserialize<T3>(m.Payloads[2])
			});
		}

		public static void OnRequest<T1, T2, T3, T4>(string @event, IClient target, Action<ICommunicationMessage, T1, T2, T3, T4> callback)
		{
			InternalOnRequest(@event, target, callback, m => new object[]
			{
				Serializer.Deserialize<T1>(m.Payloads[0]),
				Serializer.Deserialize<T2>(m.Payloads[1]),
				Serializer.Deserialize<T3>(m.Payloads[2]),
				Serializer.Deserialize<T4>(m.Payloads[3])
			});
		}

		public static void OnRequest<T1, T2, T3, T4, T5>(string @event, IClient target, Action<ICommunicationMessage, T1, T2, T3, T4, T5> callback)
		{
			InternalOnRequest(@event, target, callback, m => new object[]
			{
				Serializer.Deserialize<T1>(m.Payloads[0]),
				Serializer.Deserialize<T2>(m.Payloads[1]),
				Serializer.Deserialize<T3>(m.Payloads[2]),
				Serializer.Deserialize<T4>(m.Payloads[3]),
				Serializer.Deserialize<T5>(m.Payloads[4])
			});
		}

		public static void Off(string @event, Delegate callback)
		{
			_logger.Trace($"Off: \"{@event}\" detached from \"{PrintCallback(callback)}\"");

			_events[@event] -= callback;
		}

		public static async Task Request(string @event, IClient target, params object[] payloads)
		{
			await InternalRequest(@event, target, payloads);
		}

		public static async Task<T> Request<T>(string @event, IClient target, params object[] payloads)
		{
			var results = await InternalRequest(@event, target, payloads);

			return Serializer.Deserialize<T>(results.Payloads[0]);
		}

		public static async Task<Tuple<T1, T2>> Request<T1, T2>(string @event, IClient target, params object[] payloads)
		{
			var results = await InternalRequest(@event, target, payloads);

			return Tuple.Create(
				Serializer.Deserialize<T1>(results.Payloads[0]),
				Serializer.Deserialize<T2>(results.Payloads[1])
			);
		}

		public static async Task<Tuple<T1, T2, T3>> Request<T1, T2, T3>(string @event, IClient target, params object[] payloads)
		{
			var results = await InternalRequest(@event, target, payloads);

			return Tuple.Create(
				Serializer.Deserialize<T1>(results.Payloads[0]),
				Serializer.Deserialize<T2>(results.Payloads[1]),
				Serializer.Deserialize<T3>(results.Payloads[2])
			);
		}

		public static async Task<Tuple<T1, T2, T3, T4>> Request<T1, T2, T3, T4>(string @event, IClient target, params object[] payloads)
		{
			var results = await InternalRequest(@event, target, payloads);

			return Tuple.Create(
				Serializer.Deserialize<T1>(results.Payloads[0]),
				Serializer.Deserialize<T2>(results.Payloads[1]),
				Serializer.Deserialize<T3>(results.Payloads[2]),
				Serializer.Deserialize<T4>(results.Payloads[3])
			);
		}

		public static async Task<Tuple<T1, T2, T3, T4, T5>> Request<T1, T2, T3, T4, T5>(string @event, IClient target, params object[] payloads)
		{
			var results = await InternalRequest(@event, target, payloads);

			return Tuple.Create(
				Serializer.Deserialize<T1>(results.Payloads[0]),
				Serializer.Deserialize<T2>(results.Payloads[1]),
				Serializer.Deserialize<T3>(results.Payloads[2]),
				Serializer.Deserialize<T4>(results.Payloads[3]),
				Serializer.Deserialize<T5>(results.Payloads[4])
			);
		}

		private static async void Emit(OutboundMessage message)
		{
			_logger.Trace($"Fire: \"{PrintOutboundMessage(message)}\"");

			// Marshall back to the main thread in order to use a native call.
			await BaseScript.Delay(0);

			if (message.Target != null)
			{
				_logger.Trace($"TriggerClientEvent: Using PlayerList with {(_players.Count() > 1 ? "players" : "player")}");
				BaseScript.TriggerClientEvent(_players[message.Target.Handle], message.Event, message.Pack());
			}
			else
			{
				_logger.Trace("TriggerClientEvent: All clients");
				BaseScript.TriggerClientEvent(message.Event, message.Pack());
			}
		}

		public static void Emit(string @event, IClient target, params object[] payloads)
		{
			Emit(new OutboundMessage
			{
				Id = Guid.NewGuid(),
				Target = target,
				Event = @event,
				Payloads = payloads.Select(p => Serializer.Serialize(p)).ToList()
			});
		}

		private static async Task<InboundMessage> InternalRequest(string @event, IClient target, params object[] payloads)
		{
			var tcs = new TaskCompletionSource<InboundMessage>();

			var callback = new Action<byte[]>(data =>
			{
				var message = InboundMessage.From(data);

				_logger.Trace($"Request Received: {PrintInboundMessage(message)}");

				tcs.SetResult(message);
			});

			var msg = new OutboundMessage
			{
				Id = Guid.NewGuid(),
				Target = target,
				Event = @event,
				Payloads = payloads.Select(p => Serializer.Serialize(p)).ToList()
			};

			try
			{
				_events[$"{msg.Id}:{@event}"] += callback;

				Emit(msg);

				return await tcs.Task;
			}
			finally
			{
				_events[$"{msg.Id}:{@event}"] -= callback;
			}
		}

		private static void InternalOn(string @event, IClient target, Delegate callback, Func<InboundMessage, IEnumerable<object>> func)
		{
			_logger.Trace($"On: \"{@event}\" attached to \"{PrintCallback(callback)}\"");

			_events[@event] += new Action<byte[]>(data =>
			{
				var message = InboundMessage.From(data);

				if (target != null && message.Source != target.Handle)
				{
					_logger.Trace($"Ignoring event {@event} triggered by: {message.Source} | expected: {target.Handle}");
					return;
				}

				if (message.Event != CommunicationEvents.LogMirror) _logger.Trace($"OnRequest Received: {PrintInboundMessage(message)}");

				var args = new List<object>
				{
					new CommunicationMessage(@event, new Client(message.Source))
				};

				args.AddRange(func(message));

				callback.DynamicInvoke(args.ToArray());
			});
		}

		private static void InternalOnRequest(string @event, IClient target, Delegate callback, Func<InboundMessage, IEnumerable<object>> func)
		{
			_logger.Trace($"OnRequest: \"{@event}\" attached to \"{PrintCallback(callback)}\"");

			_events[@event] += new Action<byte[]>(data =>
			{
				var message = InboundMessage.From(data);

				if (target != null && message.Source != target.Handle)
				{
					_logger.Trace($"Ignoring event {@event} triggered by: {message.Source} | expected: {target.Handle}");
					return;
				}

				if (message.Event != CommunicationEvents.LogMirror) _logger.Trace($"OnRequest Received: {PrintInboundMessage(message)}");

				var args = new List<object>
				{
					new CommunicationMessage(@event, message.Id, new Client(message.Source))
				};

				args.AddRange(func(message));

				_logger.Trace($"DynamicInvoke: {PrintCallback(callback)} with {string.Join(", ", args.ToString())}");

				callback.DynamicInvoke(args.ToArray());
			});
		}

		private static string PrintCallback(Delegate callback)
		{
			return $"{callback.Method.DeclaringType?.Name}.{callback.Method.Name}({string.Join(", ", callback.Method.GetParameters().Select(p => p.ParameterType + " " + p.Name))})";
		}

		private static string PrintInboundMessage(InboundMessage message)
		{
			var str = $"\"{message.Event}\" with ";

			if (message.Payloads.Count < 1) return str + "no payloads";

			return str + $"payload:{Environment.NewLine}\t{string.Join($"{Environment.NewLine}\t", message.Payloads)}";
		}

		private static string PrintOutboundMessage(OutboundMessage message)
		{
			var str = $"\"{message.Event}\" with ";

			if (message.Target != null) str += $"to {message.Target.Handle} ";

			if (message.Payloads.Count < 1) return str + "no payloads";

			return str + $"payload:{Environment.NewLine}\t{string.Join($"{Environment.NewLine}\t", message.Payloads)}";
		}
	}
}
