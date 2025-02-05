using System;

namespace Shared.Events.RateChangedEvent
{
    public class RateChangedEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public string InstrumentId { get; set; }
        public decimal OldRate { get; set; }
        public decimal NewRate { get; set; }
        public decimal PercentageChange { get; set; }
    }
}


