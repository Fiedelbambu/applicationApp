using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;

namespace BerwerbungsApp;

public partial class MainPage : ContentPage
{
    // ObservableCollection für die Tabelle
// Observable collection for managing Bewerbungen (applications)
    public ObservableCollection<Bewerbung> Bewerbungen { get; set; }

    private readonly DateiService _dateiService;

    public MainPage()
    {
// Initialize UI components
        InitializeComponent();

        // Initialisierung
        _dateiService = new DateiService();

        // ObservableCollection initialisieren
        Bewerbungen = new ObservableCollection<Bewerbung>();

        // BindingContext setzen
        BindingContext = this;

        // Daten laden beim Start
        _ = LadeDatenAsync();
                
    }

    // Methode zum Laden von Daten und Aktualisieren der Tabelle
    public async Task LadeDatenAsync()
    {
// Define the file path to save/load Bewerbungen data
        var dateipfad = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bewerbungen.json");

        // Daten mit DateiService laden
        var daten = await _dateiService.LadeDatenAusDateiAsync(dateipfad);

        // Tabelle aktualisieren
        Bewerbungen.Clear();
        foreach (var bewerbung in daten)
        {
            Bewerbungen.Add(bewerbung);
        }
    }


    private async void OnBewerbungSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
            return;

        var ausgewählteBewerbung = e.CurrentSelection.FirstOrDefault() as Bewerbung;

        if (ausgewählteBewerbung == null)
            return;

        var aktion = await DisplayActionSheet(
            "Optionen für die Bewerbung",
            "Abbrechen",
            null,
            "Termin ändern",
            "Abgesagt",
            "Löschen");
            
        if (string.IsNullOrEmpty(aktion) || aktion == "Abbrechen")
            return;

        switch (aktion)
        {
            case "Termin ändern":
                DateTime? bestehenderTermin = null;
                if (!string.IsNullOrEmpty(ausgewählteBewerbung.Bewerbungstermin) &&
                    DateTime.TryParse(ausgewählteBewerbung.Bewerbungstermin, out var parsedDate))
                {
                    bestehenderTermin = parsedDate;
                }

                var neuerTermin = await DisplayDatePickerDialog(bestehenderTermin);

                if (neuerTermin.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine($"Vor Änderung: {ausgewählteBewerbung.Bewerbungstermin}");
                    ausgewählteBewerbung.Bewerbungstermin = neuerTermin.Value.ToString("dd.MM.yyyy");
                    System.Diagnostics.Debug.WriteLine($"Nach Änderung: {ausgewählteBewerbung.Bewerbungstermin}");
                }
                break;

            case "Abgesagt":
                ausgewählteBewerbung.Status = "Abgesagt";
                break;

            case "Löschen":
                var bestätigen = await DisplayAlert(
                    "Löschen",
                    $"Möchten Sie die Bewerbung bei {ausgewählteBewerbung.Firma} wirklich löschen?",
                    "Ja",
                    "Nein");

                if (bestätigen)
                {
                    Bewerbungen.Remove(ausgewählteBewerbung); // Eintrag entfernen
                    await SpeichernAktualisierteDaten();       // Änderungen speichern
                    await DisplayAlert("Erfolg", "Die Bewerbung wurde gelöscht.", "OK");
                }
                break;
        }

        await SpeichernAktualisierteDaten();

        ((CollectionView)sender).SelectedItem = null;
    }


    private TaskCompletionSource<DateTime?> _datePickerCompletionSource;

    private async Task<DateTime?> DisplayDatePickerDialog(DateTime? aktuellesDatum = null)
    {
        // Initialisiere die TaskCompletionSource
        _datePickerCompletionSource = new TaskCompletionSource<DateTime?>();

        // Erstelle den DatePicker
        var datePicker = new DatePicker
        {
            Format = "dd.MM.yyyy",
            Date = aktuellesDatum ?? DateTime.Now,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        // Erstelle den Bestätigen-Button
        var confirmButton = new Button
        {
            Text = "Bestätigen",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        confirmButton.Clicked += async (s, e) =>
        {
            // Speichere das ausgewählte Datum und schließe den Dialog
            _datePickerCompletionSource.SetResult(datePicker.Date);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        };

        // Popup-Layout erstellen
        var popupContent = new StackLayout
        {
            Padding = 20,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            Children =
        {
            new Label
            {
                Text = "Bitte wählen Sie ein Datum aus:",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center
            },
            datePicker,
            confirmButton
        }
        };

        var popupPage = new ContentPage
        {
            Content = popupContent,
            BackgroundColor = new Color(0, 0, 0, 15) // Halbtransparenter Hintergrund
        };

        // Zeige die modale Seite
        await Application.Current.MainPage.Navigation.PushModalAsync(popupPage);

        // Warte auf das ausgewählte Datum
        return await _datePickerCompletionSource.Task;
    }


    private async Task SpeichernAktualisierteDaten()
    {
// Define the file path to save/load Bewerbungen data
        var dateipfad = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bewerbungen.json");

        // Speichere die aktualisierte Liste in die JSON-Datei
        var daten = Bewerbungen.ToList(); // ObservableCollection in eine Liste konvertieren
        var json = JsonSerializer.Serialize(daten, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(dateipfad, json);
    }

    // Methode zum Speichern der Daten
    private async Task SpeichernAsync(Bewerbung neueBewerbung)
    {
// Define the file path to save/load Bewerbungen data
        var dateipfad = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bewerbungen.json");

        // Bestehende Daten laden
        var daten = await _dateiService.LadeDatenAusDateiAsync(dateipfad);

        // Neue Bewerbung hinzufügen
        daten.Add(neueBewerbung);

        // Daten speichern
        await _dateiService.SpeichereDatenInDateiAsync(daten, dateipfad);

        // Tabelle aktualisieren
        Bewerbungen.Clear();
        foreach (var bewerbung in daten)
        {
            Bewerbungen.Add(bewerbung);
        }
    }


    // Button-Event für das Speichern
    private async void OnSpeichernButtonClicked(object sender, EventArgs e)
    {
        // Werte aus den Eingabefeldern lesen
        string firma = firmaEntry.Text; // Name der Firma aus dem Textfeld
        DateTime beworbenAm = beworbenAmDatePicker.Date; // Datum aus dem DatePicker

        // Validierung
        if (string.IsNullOrEmpty(firma))
        {
            await DisplayAlert("Fehler", "Bitte geben Sie einen Firmennamen ein.", "OK");
            return;
        }

        // Neue Bewerbung erstellen
        var neueBewerbung = new Bewerbung
        {
            Firma = firma,
            BeworbenAm = beworbenAm.ToString("dd.MM.yyyy"),
            Bewerbungstermin = string.Empty, // Noch leer
            Status = "Offen"
        };

        // Speichern und Tabelle aktualisieren
        await SpeichernAsync(neueBewerbung);

        // Eingabefelder bereinigen
        firmaEntry.Text = string.Empty; // Textfeld leeren
        beworbenAmDatePicker.Date = DateTime.Now; // DatePicker auf das heutige Datum setzen

    }
}






    // Modell-Klasse für die Tabelle


public class Bewerbung : INotifyPropertyChanged
{
    private string? _firma;
    public string? Firma
    {
        get => _firma;
        set
        {
            if (_firma != value)
            {
                _firma = value;
                OnPropertyChanged(nameof(Firma));
            }
        }
    }

    private string? _beworbenAm;
    public string? BeworbenAm
    {
        get => _beworbenAm;
        set
        {
            if (_beworbenAm != value)
            {
                _beworbenAm = value;
                OnPropertyChanged(nameof(BeworbenAm));
            }
        }
    }

    private string? _bewerbungstermin;
    public string? Bewerbungstermin
    {
        get => _bewerbungstermin;
        set
        {
            if (_bewerbungstermin != value)
            {
                _bewerbungstermin = value;
                OnPropertyChanged(nameof(Bewerbungstermin));
            }
        }
    }

    private string? _status;
    public string? Status
    {
        get => _status;
        set
        {
            if (_status != value)
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

