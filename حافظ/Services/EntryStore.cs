using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using حافظ.Models;

namespace حافظ.Services;


public class EntryStore
{
    private const string SaveFile = "entries.json";
    private List<MemorizationEntry> entries = new();

    public IReadOnlyList<MemorizationEntry> Entries => entries;

    public void AddEntry(MemorizationEntry entry)
    {
        entries.Add(entry);
        Save();
    }

    public void RescheduleEntry(MemorizationEntry entry)
    {
        entry.Reschedule();
        Save();
    }

    public List<MemorizationEntry> GetEntriesDueToday()
    {
        return entries
            .Where(e => e.ScheduledDate.Date == DateTime.Now.Date)
            .OrderBy(e => e.ScheduledDate)
            .ToList();
    }

    public void Load()
    {
        if (File.Exists(SaveFile))
        {
            var json = File.ReadAllText(SaveFile);
            entries = JsonSerializer.Deserialize<List<MemorizationEntry>>(json) ?? new();
        }
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SaveFile, json);
    }
}
