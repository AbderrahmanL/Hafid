using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using حافظ.Models;

namespace حافظ.Services;


public class EntryStore
{
    private readonly string _filePath = GetSavePath();
    private readonly List<MemorizationEntry> _entries = new();

    private static string GetSavePath()
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(folder, "Hafidh");

        if (!Directory.Exists(appFolder))
            Directory.CreateDirectory(appFolder);

        return Path.Combine(appFolder, "entries.json");
    }

    public void Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var loaded = JsonSerializer.Deserialize<List<MemorizationEntry>>(json);
            if (loaded != null)
                _entries.Clear();
            _entries.AddRange(loaded);
        }
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(_entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public void AddEntry(MemorizationEntry entry)
    {
        _entries.Add(entry);
        Save();
    }

    public void RescheduleEntry(MemorizationEntry entry)
    {
        entry.Repetitions++;
        entry.ScheduledDate = DateTime.Now.AddHours(2 * Math.Pow(2, entry.Repetitions));
        Save();
    }

    public IEnumerable<MemorizationEntry> GetEntriesDueToday()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        return _entries.Where(e => e.ScheduledDate >= today && e.ScheduledDate < tomorrow).OrderBy(e => e.ScheduledDate);
    }
} 
