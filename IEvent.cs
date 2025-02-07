using System;

namespace Shared.Events
{
	public interface IEvent
	{
		DateTime Timestamp { get; set; }
	}
}