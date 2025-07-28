using System;

namespace حافظ.Models;

public class MemorizationEntry
{
    public DateTime ScheduledDate { get; set; } = DateTime.Now;
    public int Repetitions { get; set; } = 1;
    public string Text { get; set; } = string.Empty;

    public void Reschedule()
    {
        ScheduledDate = ScheduledDate.AddHours(2 * Math.Pow(2, Repetitions));
        Repetitions++;
    }
}