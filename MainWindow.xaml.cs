using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


//TODO 01: XAML opmaken (via snippet)
//TODO 02: Maak klasse FireAlarm
namespace APIFireAlarmTester_221119
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO 03: HTTP-client maken
        HttpClient _httpClient = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            //TODO 04: Client instellen
            _httpClient.BaseAddress = new Uri("https://localhost:44341/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/json"));
        }

        //TODO 05: GET-method aanspreken om alle data op te vragen
        private async void UpdateListView()
        {
            List<FireAlarm> fireAlarms = null;
            HttpResponseMessage httpResponseMessage = 
                await _httpClient.GetAsync(
                    new Uri("https://localhost:44341/api/firealarms"));

            //TODO 06: Via nuget Microsoft.AspNet.WebApi.Client installeren
            if (httpResponseMessage.IsSuccessStatusCode)
                fireAlarms = 
                    await httpResponseMessage.Content.
                    ReadAsAsync<List<FireAlarm>>();

            fireAlarms.Sort();                          //Hiervoor hebben we IComparable geïmplementeerd
            lstFireAlarms.ItemsSource = fireAlarms;     //Data binding
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            //TODO 07: Lijst updaten
            UpdateListView();
        }

        private async void btnPost_Click(object sender, RoutedEventArgs e)
        {
            //TODO 08: Post
            FireAlarm fireAlarm = new FireAlarm();
            fireAlarm.Location = tbxLocationPost.Text;
            fireAlarm.Active = (bool)cbxActivePost.IsChecked;
            fireAlarm.Reason = 
                ((ComboBoxItem)cbxReasonPost.SelectedValue).Content.ToString();

            HttpResponseMessage httpResponseMessage =
                await _httpClient.PostAsJsonAsync("api/firealarms", fireAlarm);
            httpResponseMessage.EnsureSuccessStatusCode();

            UpdateListView();
        }

        private async void btnPut_Click(object sender, RoutedEventArgs e)
        {
            //TODO 10: Bestaand object aanpassen
            FireAlarm fireAlarm = new FireAlarm();
            fireAlarm.ID = Convert.ToUInt64(tbxIdPut.Text);
            fireAlarm.Location = tbxLocationPut.Text;
            fireAlarm.Reason = 
                ((ComboBoxItem)cbxReasonPut.SelectedValue).Content.ToString();
            fireAlarm.Active = (bool)cbxActivePut.IsChecked;

            HttpResponseMessage httpResponseMessage =
                await _httpClient.PutAsJsonAsync(
                    $"api/firealarms/{tbxIdPut.Text}", 
                    fireAlarm);
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //TODO 09: Code voor deleten
            HttpResponseMessage httpResponseMessage = 
                await _httpClient.DeleteAsync(
                    $"api/firealarms/{tbxIdDelete.Text}");

            UpdateListView();
        }

        private void Focus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Clear();
        }
    }
}
