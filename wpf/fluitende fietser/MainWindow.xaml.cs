using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace fluitende_fietser
{
    public partial class MainWindow : Window
    {
        private RekenMachine rekenMachine;

        private double totaalbedragVanBestelling = 0.0;
        private double totaalbedrag = 0.0;
        DispatcherTimer inActiveTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            inActiveTimer.Interval = TimeSpan.FromSeconds(1);
            inActiveTimer.Tick += InActiveTimer_Tick;
            inActiveTimer.Start();
            Tijdlijn.Maximum = 60;

            // Maak een instantie van het tweede venster
            rekenMachine = new RekenMachine();
            // Abonneer op het event van het tweede venster
            
        }
        private void RekenMachine_ResetTimerRequested(object sender, EventArgs e)
        {
            // Reset de timer
            Tijdlijn.Value = 0;
        }
        private void InActiveTimer_Tick(object sender, EventArgs e)
        {
            Tijdlijn.Value++;

            if (Tijdlijn.Value >= Tijdlijn.Maximum)
            {
                MessageBox.Show("De applicatie wordt gesloten door inactiviteit");
                Close();
            }
        }

        public void ResetTimer()
        {
            Tijdlijn.Value = 0;
        }

        private void Bestellen_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();

            List<ComboBox> comboBoxes = new List<ComboBox> { comboBoxFietsen, comboBoxVerzekeringen, comboBoxServices };

            foreach (ComboBox comboBoxsoort in comboBoxes)
            {
                if (comboBoxsoort.SelectedItem != null)
                {

                    int aantalDagen = int.Parse(AantalDagenTextBox.Text);
                    string selectedItem = (comboBoxsoort.SelectedItem as ComboBoxItem).Content.ToString();

                    string[] itemPrijs = selectedItem.Split('€', ',', '/');


                    // Controleren of het aantal dagen geldig is
                    if (aantalDagen <= 0)
                    {
                        MessageBox.Show("Voer een geldig aantal dagen in.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    double prijsPerDag = Convert.ToDouble(itemPrijs[1]);
                    // Bereken hier de prijs voor het geselecteerde item
                    double totaalPrijs = prijsPerDag * aantalDagen;

                    totaalbedragVanBestelling += totaalPrijs;


                    ListBoxItem item = new ListBoxItem();
                    item.Content = $"{selectedItem},   Aantal dagen:{aantalDagen},             Totaalprijs: €{totaalPrijs:F2}";
                    BestelOverzichtListBox.Items.Add(item);
                }

            }
            totaalbedrag += totaalbedragVanBestelling;
            // hier is replace gebruikt om de oude text te vervangen met de nieuwe bedrag
            BedragTeBetalenlabel.Text.Replace("Te betalen bedrag: €", $"Te betalen bedrag: €{totaalbedrag:F2}");

            ResetSelecties();
            UpdateBetaalBedrag();
        }

        private void UpdateBetaalBedrag()
        {
            BedragTeBetalenlabel.Text = $"Te betalen bedrag: €{totaalbedrag:F2}";
        }

        private void ResetSelecties()
        {
            // Reset geselecteerde items en aantal dagen na het bestelen
            List<ComboBox> comboBoxes = new List<ComboBox> { comboBoxFietsen, comboBoxVerzekeringen, comboBoxServices };
            foreach (ComboBox comboBoxsoort in comboBoxes)
            {
                comboBoxsoort.SelectedItem = null;
            }
            AantalDagenTextBox.Text = "1";
        }

        private void Verwijderen_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
            // Een lijst maken om de geselecteerde items op te slaan
            List<ListBoxItem> itemsToRemove = new List<ListBoxItem>();

            // Alle geselecteerde items toevoegen aan de lijst
            foreach (var selectedItem in BestelOverzichtListBox.SelectedItems)
            {
                itemsToRemove.Add((ListBoxItem)selectedItem);
            }

            // Geselecteerde items verwijderen uit de ListBox
            foreach (var itemToRemove in itemsToRemove)
            {
                BestelOverzichtListBox.Items.Remove(itemToRemove);
                string itemContent = itemToRemove.ToString();
                string[] itemParts = itemContent.Split("€", StringSplitOptions.RemoveEmptyEntries);

                // Haal de prijsstring op uit de laatste deel van de gesplitste inhoud
                string priceString = itemParts[itemParts.Length - 1].Trim();

                // Converteer de prijsstring naar een double-getal
                double itemPrice = Convert.ToDouble(priceString);
                totaalbedrag -= itemPrice;
            }
            UpdateBetaalBedrag();
        }

        private void VolgendeKlant_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
            totaalbedragVanBestelling = 0;
            BestelOverzichtListBox.Items.Clear();
            ResetSelecties();
            UpdateBetaalBedrag();


        }

        private void RekenMachine_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
            RekenMachine rekenMachine = new RekenMachine();
            rekenMachine.Show();
        }

        private void Afrekenen_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
        }
    }

}

