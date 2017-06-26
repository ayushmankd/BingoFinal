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
    public partial class MultiGame : ContentPage
    {
        public static int count = 1;
        public static List<string> Grid = new List<string>(25);
        public static List<string> ElementsDone = new List<string>(25);
        public static List<string> ElementsDoneOppositon = new List<string>(25);
        public static List<string> ElementsCrossed = new List<string>(25);
        public static bool CheckVar = false;
        public static int lblCross = 0;
        public static int lblCrossOpposition;
        public MultiGame()
        {
            InitializeComponent();
        }
        public class Data
        {
            public string[] arrDone { get; set; }
            public int lbl { get; set; }
        }
        public class First
        {
            public int first { get; set; }
            public First()
            {
                this.first = 9999;
            }
        }
        public class Oppositon
        {
            public string oppositon { get; set; }
        }
        public static Oppositon opp = new Oppositon();
        public static Data data = new Data();
        public static First fs = new First();
        public static HttpClient hc = new HttpClient();
        public static HttpResponseMessage hr = new HttpResponseMessage();
        public static async void APIPOST()
        {
            data.arrDone = ElementsDone.ToArray();
            data.lbl = lblCross;
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            hr = await hc.PostAsync("https://sheltered-bayou-89978.herokuapp.com/post/" + MultiGameName.pd.plyrName, content);
            string sa = hr.Content.ReadAsStringAsync().Result;
        }
        public static async void APIPOSTDelete()
        { 
            var json = JsonConvert.SerializeObject(MultiGameName.pd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            hr=await hc.PostAsync("https://sheltered-bayou-89978.herokuapp.com/Delete/" + MultiGameName.pd.plyrName, content);
            string sa = hr.Content.ReadAsStringAsync().Result;
        }
        public static int APIGETFirst()
        {
            string s = null;
            var response = hc.GetAsync("https://sheltered-bayou-89978.herokuapp.com/getFirst/" + MultiGameName.pd.plyrName).Result;
            if (response.IsSuccessStatusCode)
            {
                s = response.Content.ReadAsStringAsync().Result;
                fs.first = JsonConvert.DeserializeObject<First>(s).first;
                return fs.first;
            }
            else
            {
                return 404;
            }
        }
        public static void APIGETOpp()
        {
            var response = hc.GetAsync("https://sheltered-bayou-89978.herokuapp.com/opp/" + MultiGameName.pd.plyrName).Result;
            if (response.IsSuccessStatusCode)
            {
                string st = response.Content.ReadAsStringAsync().Result;
                if (st == "true")
                    opp.oppositon = "true";
                else
                    opp.oppositon = JsonConvert.DeserializeObject<Oppositon>(st).oppositon;
            }
            else
            {
                opp.oppositon= "false";
            }
        }
        public static bool APIGET()
        {
            string s = null;
            var res = hc.GetAsync("https://sheltered-bayou-89978.herokuapp.com/get/" + opp.oppositon).Result;
            if (res.IsSuccessStatusCode)
            {
                s = res.Content.ReadAsStringAsync().Result;
                data.arrDone = JsonConvert.DeserializeObject<Data>(s).arrDone;
                data.lbl = JsonConvert.DeserializeObject<Data>(s).lbl;
                ElementsDoneOppositon = data.arrDone.ToList<string>();
                lblCrossOpposition = data.lbl;   
                return true;
            }
            else
            {   
                return false;
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                Button btnTemp = (Button)sender;
                //-----Initialization----
                if (btnTemp.Text == "" && count <= 25)
                {
                    btnTemp.Text = count.ToString();
                    count++;
                    if (count == 26)
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            Button tem = (Button)GridMulti.Children[i + 5];
                            Grid.Add(tem.Text);
                        }
                        while (true)
                        {
                            int c = APIGETFirst();
                            if (fs.first == 9999 && c!=404)
                                continue;
                            else if (c == 404)
                            {
                                await DisplayAlert("Sorry!!", "Oppositon Backed Out", "OK");
                                count = 1;
                                Grid = new List<string>(25);
                                ElementsDone = new List<string>(25);
                                ElementsDoneOppositon = new List<string>(25);
                                ElementsCrossed = new List<string>(25);
                                CheckVar = false;
                                lblCross = 0;
                                await Navigation.PushModalAsync(new MainPage());
                                break;
                            }
                            else
                                break;
                        }
                        if (fs.first == 1)
                        {
                            await DisplayAlert("First", "You First", "OK");
                            return;
                        }
                        else if (fs.first == 0)
                        {
                            await DisplayAlert("First", "Oppositon First", "OK");
                            int temp = ElementsDoneOppositon.Count;
                            while (true)
                            {
                                APIGETOpp();
                                await DisplayAlert(opp.oppositon, "OK", "OK");
                                if (opp.oppositon == "true")
                                {
                                    await DisplayAlert("Sorry!!", "No Player Online..Wait..", "Cancel");
                                    continue;
                                }
                                else if (opp.oppositon == "false")
                                {
                                    await DisplayAlert("Sorry!!", "Oppositon Backed Out!", "OK");
                                    count = 1;
                                    Grid = new List<string>(25);
                                    ElementsDone = new List<string>(25);
                                    ElementsDoneOppositon = new List<string>(25);
                                    ElementsCrossed = new List<string>(25);
                                    CheckVar = false;
                                    lblCross = 0;
                                    await Navigation.PushModalAsync(new MainPage());
                                    break;
                                }
                                else
                                    break;
                            }
                            APIGET();
                            if(ElementsDoneOppositon.Count<=temp)
                            { 
                                await DisplayAlert("Wait", "Wait", "OK");
                                do
                                {
                                    if (APIGET())
                                    {
                                        if (ElementsDoneOppositon.Count <= temp)
                                            continue;
                                        else
                                            break;
                                    }
                                    else
                                    {
                                        await DisplayAlert("Sorry!!", "Oppositon Backed Out!", "OK");
                                        count = 1;
                                        Grid = new List<string>(25);
                                        ElementsDone = new List<string>(25);
                                        ElementsDoneOppositon = new List<string>(25);
                                        ElementsCrossed = new List<string>(25);
                                        CheckVar = false;
                                        lblCross = 0;
                                        await Navigation.PushModalAsync(new MainPage());
                                        break;
                                    }
                                } while (ElementsDoneOppositon.Count <= temp);
                            }
                            for (int i = 5; i < 30; i++)
                            {
                                Button tem = (Button)GridMulti.Children[i];
                                if (ElementsDoneOppositon.Contains(tem.Text))
                                {
                                    tem.BackgroundColor = Color.Olive;
                                }
                            }
                            count++;
                            return;
                        }
                    }
                }
                //---------Checking----------
                else if (count > 25)
                {
                    if(count==26)
                    {
                        btnTemp.BackgroundColor = Color.White;
                        ElementsDone.Add(btnTemp.Text);
                        APIPOST();
                        count++;
                        while (true)
                        {
                            int temp = ElementsDoneOppositon.Count;
                            while (true)
                            {
                                APIGETOpp();
                                await DisplayAlert(opp.oppositon, "OK", "OK");
                                if (opp.oppositon== "true")
                                {
                                    await DisplayAlert("Sorry!!", "No Player Online..Wait..", "Cancel");
                                    continue;
                                }
                                else if (opp.oppositon == "false")
                                {
                                    await DisplayAlert("Sorry!!", "Oppositon Backed Out!", "OK");
                                    count = 1;
                                    Grid = new List<string>(25);
                                    ElementsDone = new List<string>(25);
                                    ElementsDoneOppositon = new List<string>(25);
                                    ElementsCrossed = new List<string>(25);
                                    CheckVar = false;
                                    lblCross = 0;
                                    await Navigation.PushModalAsync(new MainPage());
                                    break;
                                }
                                else
                                    break;
                            }
                            if (APIGET())
                            {
                                if (ElementsDoneOppositon.Count > temp)
                                {
                                    await DisplayAlert("Your Turn", "Your Turn", "OK");
                                    break;
                                }
                                else
                                {
                                    await DisplayAlert("Wait", "Wait", "OK");
                                    continue;
                                }
                            }
                            else
                                break;
                        }
                        if (lblCrossOpposition == 5)
                        {
                            await DisplayAlert("Winner", "Oppositon Wins", "OK");
                            APIPOSTDelete();
                            return;
                        }
                        for (int i = 5; i < 30; i++)
                        {
                            Button tem = (Button)GridMulti.Children[i];
                            if (ElementsDoneOppositon.Contains(tem.Text))
                            {
                                tem.BackgroundColor = Color.Olive;
                            }
                        }
                        return;
                    }
                    else if (!ElementsDone.Contains(btnTemp.Text) && !ElementsDoneOppositon.Contains(btnTemp.Text))
                    {
                        btnTemp.BackgroundColor = Color.White;
                        ElementsDone.Add(btnTemp.Text);
                        if (ElementsDone.Count + ElementsDoneOppositon.Count >= 5)
                        {
                            for (int i = 0, n = 0; i < 25; i += 5)
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone.Count; k++)
                                    {
                                        if (Grid[i + j] == ElementsDone[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDoneOppositon.Count; k++)
                                        {
                                            if (Grid[i + j] == ElementsDoneOppositon[k])
                                            {
                                                n++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (n < 5)
                                    n = 0;
                                else
                                {
                                    while (n != 0)
                                    {
                                        n--;
                                        if (!ElementsCrossed.Contains(Grid[i + n]))
                                        {
                                            ElementsCrossed.Add(Grid[i + n]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross++;
                                        if (lblCross == 5)
                                        {
                                            await DisplayAlert("Winner", "You Win!!", "OK");
                                            APIPOSTDelete();
                                            await Navigation.PushModalAsync(new MainPage());
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                            for (int i = 0, n = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 25; j += 5)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone.Count; k++)
                                    {
                                        if (Grid[i + j] == ElementsDone[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDoneOppositon.Count; k++)
                                        {
                                            if (Grid[i + j] == ElementsDoneOppositon[k])
                                            {
                                                n++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (n < 5)
                                    n = 0;
                                else
                                {
                                    while (n != 0)
                                    {
                                        n--;
                                        if (!ElementsCrossed.Contains(Grid[i + (n * 5)]))
                                        {
                                            ElementsCrossed.Add(Grid[i + (n * 5)]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross++;
                                        if (lblCross == 5)
                                        {
                                            await DisplayAlert("Winner", "You Win!!", "OK");
                                            APIPOSTDelete();
                                            await Navigation.PushModalAsync(new MainPage());
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                            {
                                int n = 0;
                                for (int i = 0; i < 25; i += 6)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone.Count; k++)
                                    {
                                        if (Grid[i] == ElementsDone[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (temp == n)
                                    {
                                        for (int k = 0; k < ElementsDoneOppositon.Count; k++)
                                        {
                                            if (Grid[i] == ElementsDoneOppositon[k])
                                            {
                                                n++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (n < 5)
                                    n = 0;
                                else
                                {
                                    while (n != 0)
                                    {
                                        n--;
                                        if (!ElementsCrossed.Contains(Grid[(n * 6)]))
                                        {
                                            ElementsCrossed.Add(Grid[(n * 6)]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross++;
                                        if (lblCross == 5)
                                        {
                                            await DisplayAlert("Winner", "You Win!!", "OK");
                                            APIPOSTDelete();
                                            await Navigation.PushModalAsync(new MainPage());
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                            {
                                int n = 0;
                                for (int i = 4; i <= 20; i += 4)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone.Count; k++)
                                    {
                                        if (Grid[i] == ElementsDone[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (temp == n)
                                    {
                                        for (int k = 0; k < ElementsDoneOppositon.Count; k++)
                                        {
                                            if (Grid[i] == ElementsDoneOppositon[k])
                                            {
                                                n++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (n < 5)
                                    n = 0;
                                else
                                {
                                    while (n != 0)
                                    {
                                        if (!ElementsCrossed.Contains(Grid[(n * 4)]))
                                        {
                                            ElementsCrossed.Add(Grid[(n * 4)]);
                                            CheckVar = true;
                                        }
                                        n--;
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross++;
                                        if (lblCross == 5)
                                        {
                                            await DisplayAlert("Winner", "You Win!!", "OK");
                                            APIPOSTDelete();
                                            await Navigation.PushModalAsync(new MainPage());
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        APIPOST();
                        for (int i = 5; i < 30; i++)
                        {
                            Button btn = (Button)GridMulti.Children[i];
                            if (ElementsCrossed.Contains(btn.Text))
                                btn.BackgroundColor = Color.Black;
                        }
                        for (int i = 0; i < lblCross; i++)
                        {
                            Label l = (Label)GridMulti.Children[i];
                            l.BackgroundColor = Color.Transparent;
                        }
                        while (true)
                        {
                            int temp = ElementsDoneOppositon.Count;
                            while (true)
                            {
                                APIGETOpp();
                                if (opp.oppositon== "true")
                                {
                                    await DisplayAlert("Sorry!!", "No Player Online..Wait..", "Cancel");
                                    continue;
                                }
                                else if (opp.oppositon== "false")
                                {
                                    await DisplayAlert("Sorry!!", "Oppositon Backed Out!", "OK");
                                    count = 1;
                                    Grid = new List<string>(25);
                                    ElementsDone = new List<string>(25);
                                    ElementsDoneOppositon = new List<string>(25);
                                    ElementsCrossed = new List<string>(25);
                                    CheckVar = false;
                                    lblCross = 0;
                                    await Navigation.PushModalAsync(new MainPage());
                                    break;
                                }
                                else
                                    break;
                            }
                            if (APIGET())
                            {
                                if (ElementsDoneOppositon.Count > temp)
                                {
                                    await DisplayAlert("Your Turn", "Your Turn", "OK");
                                    break;
                                }
                                else
                                {
                                    await DisplayAlert("Wait", "Wait", "OK");
                                    continue;
                                }
                            }
                            else
                                break;
                        }
                        if (lblCrossOpposition == 5)
                        {
                            await DisplayAlert("Winner", "Oppositon Wins", "OK");
                            APIPOSTDelete();
                            return;
                        }
                        for (int i = 5; i < 30; i++)
                        {
                            Button tem = (Button)GridMulti.Children[i];
                            if (ElementsDoneOppositon.Contains(tem.Text))
                            {
                                tem.BackgroundColor = Color.Olive;
                            }
                        }
                        if (ElementsDone.Count + ElementsDoneOppositon.Count >= 5)
                        {
                            for (int i = 0, n = 0; i < 25; i += 5)
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone.Count; k++)
                                    {
                                        if (Grid[i + j] == ElementsDone[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDoneOppositon.Count; k++)
                                        {
                                            if (Grid[i + j] == ElementsDoneOppositon[k])
                                            {
                                                n++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (n < 5)
                                    n = 0;
                                else
                                {
                                    while (n != 0)
                                    {
                                        n--;
                                        if (!ElementsCrossed.Contains(Grid[i + n]))
                                        {
                                            ElementsCrossed.Add(Grid[i + n]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross++;
                                        if (lblCross == 5)
                                        {
                                            await DisplayAlert("Winner", "You Win!!", "OK");
                                            APIPOSTDelete();
                                            await Navigation.PushModalAsync(new MainPage());
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                            for (int i = 0, n = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 25; j += 5)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone.Count; k++)
                                    {
                                        if (Grid[i + j] == ElementsDone[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDoneOppositon.Count; k++)
                                        {
                                            if (Grid[i + j] == ElementsDoneOppositon[k])
                                            {
                                                n++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (n < 5)
                                    n = 0;
                                else
                                {
                                    while (n != 0)
                                    {
                                        n--;
                                        if (!ElementsCrossed.Contains(Grid[i + (n * 5)]))
                                        {
                                            ElementsCrossed.Add(Grid[i + (n * 5)]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross++;
                                        if (lblCross == 5)
                                        {
                                            await DisplayAlert("Winner", "You Win!!", "OK");
                                            APIPOSTDelete();
                                            await Navigation.PushModalAsync(new MainPage());
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                            {
                                int n = 0;
                                for (int i = 0; i < 25; i += 6)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone.Count; k++)
                                    {
                                        if (Grid[i] == ElementsDone[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (temp == n)
                                    {
                                        for (int k = 0; k < ElementsDoneOppositon.Count; k++)
                                        {
                                            if (Grid[i] == ElementsDoneOppositon[k])
                                            {
                                                n++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (n < 5)
                                    n = 0;
                                else
                                {
                                    while (n != 0)
                                    {
                                        n--;
                                        if (!ElementsCrossed.Contains(Grid[(n * 6)]))
                                        {
                                            ElementsCrossed.Add(Grid[(n * 6)]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross++;
                                        if (lblCross == 5)
                                        {
                                            await DisplayAlert("Winner", "You Win!!", "OK");
                                            APIPOSTDelete();
                                            await Navigation.PushModalAsync(new MainPage());
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                            {
                                int n = 0;
                                for (int i = 4; i <= 20; i += 4)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone.Count; k++)
                                    {
                                        if (Grid[i] == ElementsDone[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (temp == n)
                                    {
                                        for (int k = 0; k < ElementsDoneOppositon.Count; k++)
                                        {
                                            if (Grid[i] == ElementsDoneOppositon[k])
                                            {
                                                n++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (n < 5)
                                    n = 0;
                                else
                                {
                                    while (n != 0)
                                    {
                                        if (!ElementsCrossed.Contains(Grid[(n * 4)]))
                                        {
                                            ElementsCrossed.Add(Grid[(n * 4)]);
                                            CheckVar = true;
                                        }
                                        n--;
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross++;
                                        if (lblCross == 5)
                                        {
                                            await DisplayAlert("Winner", "You Win!!", "OK");
                                            APIPOSTDelete();
                                            await Navigation.PushModalAsync(new MainPage());
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        return;
                    }

                }
            }
            catch(Exception exc)
            {
                await DisplayAlert(exc.Message, exc.Source, "OK");
            }
        }
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            count = 1;
            Grid = new List<string>(25);
            ElementsDone = new List<string>(25);
            ElementsDoneOppositon = new List<string>(25);
            ElementsCrossed = new List<string>(25);
            CheckVar = false;
            lblCross = 0;
            Navigation.PushModalAsync(new MainPage());
            APIPOSTDelete();
            return;
        }
    }
}

