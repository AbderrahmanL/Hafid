using System;

namespace حافظ.Models;

public class MemorizationEntry
{
    public string Text { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public int Repetitions { get; set; } = 0;
} 