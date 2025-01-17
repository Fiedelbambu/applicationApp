using System.Collections.ObjectModel;
using System.Text.Json;

namespace BerwerbungsApp;

public class DateiService
{
    // Daten aus der Datei laden
// Load data from a file asynchronously
    public async Task<ObservableCollection<Bewerbung>> LadeDatenAusDateiAsync(string dateipfad)
    {
        if (!File.Exists(dateipfad))
        {
            return new ObservableCollection<Bewerbung>();
        }

        var json = await File.ReadAllTextAsync(dateipfad);
        var daten = JsonSerializer.Deserialize<List<Bewerbung>>(json) ?? new List<Bewerbung>();
        return new ObservableCollection<Bewerbung>(daten);
    }

    // Daten in die Datei speichern
// Save data to a file asynchronously
    public async Task SpeichereDatenInDateiAsync(IEnumerable<Bewerbung> daten, string dateipfad)
    {
        var json = JsonSerializer.Serialize(daten, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(dateipfad, json);
        System.Diagnostics.Debug.WriteLine($"Dateipfad: {dateipfad}");

    }
}
