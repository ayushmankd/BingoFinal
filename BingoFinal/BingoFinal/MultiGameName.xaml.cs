using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace BingoFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiGameName : ContentPage
    {
        public MultiGameName()
        {
            InitializeComponent();
        }
        public class PlayerData
        {
            public string plyrName { get; set; }
        }
        public static PlayerData pd = new PlayerData();
        private async void b_Clicked(object sender, EventArgs e)
        {
            if (entry.Text != "")
            {
                try
                {
                    pd.plyrName = entry.Text;
                    HttpClient hc = new HttpClient();
                    HttpResponseMessage hr = new HttpResponseMessage();
                    var json = JsonConvert.SerializeObject(pd);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    hr = await hc.PostAsync("https://sheltered-bayou-89978.herokuapp.com/postPlyrName", content);
                    if (hr.IsSuccessStatusCode)
                    {
                        await Navigation.PushModalAsync(new MultiGame());
                    }
                    else
                    {
                        await DisplayAlert("Name Mismatch!!", "Change Your Name", "OK");
                    }
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Error Connecting","Check your Connectivity","OK");
                    
                }
            }
        }
    }
}
