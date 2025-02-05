using System;

namespace Shared.IEvent
{
	public interface IEvent
	{
		DateTime Timestamp { get; set; }
	}
}