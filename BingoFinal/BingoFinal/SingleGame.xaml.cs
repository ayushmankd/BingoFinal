using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BingoFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleGame : ContentPage
    {
        public static int count1 = 1;
        public static int count2 = 1;
        public static bool Flicker = true;
        public static List<string> Grid1 = new List<string>(25);
        public static List<string> ElementsDone1 = new List<string>(25);
        public static List<string> ElementsDone2 = new List<string>(25);
        public static List<string> ElementsCrossed1 = new List<string>(25);
        public static List<string> ElementsCrossed2 = new List<string>(25);
        public static List<string> Grid2 = new List<string>(25);
        public static bool CheckVar = false;
        public static int lblCross1 = 0;
        public static int lblCross2 = 0;
        public SingleGame()
        {
            InitializeComponent();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            Button tempBtn = (Button)sender;
      
            //Initializing
            if (tempBtn.Text == "" && Flicker && count1 <= 25)
            {
                tempBtn.Text = count1.ToString();
                count1++;
                if (count1 == 26)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        Button tempGridBtn = (Button)Grid.Children[i + 5];
                        Grid1.Add(tempGridBtn.Text);
                    }
                    for (int i = 0; i < 25; i++)
                    {
                        plyrName.Text = "Player 2";
                        plyrName.TextColor = Color.Navy;
                        try
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                Label tem = (Label)Grid.Children[j];
                                tem.BackgroundColor = Color.Lime;
                            }
                        }
                        catch(Exception ex) { await DisplayAlert(ex.Message, ex.Source, "OK"); }

                        Button tempGridBtn = (Button)Grid.Children[i + 5];
                        tempGridBtn.Text = "";
                        //Grid.BackgroundColor = Color.Teal;
                        //Content.BackgroundColor = Color.Teal;
                    }
                    Flicker = !Flicker;
                    await DisplayAlert("Pass!!!", "Pass to Oppositon", "OK");
                }
                return;
            }
            else if (tempBtn.Text == "" && (!Flicker) && count2 <= 25)
            {
                tempBtn.Text = count2.ToString();
                count2++;
                if (count2 == 26)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        Button tempGridBtn = (Button)Grid.Children[i + 5];
                        Grid2.Add(tempGridBtn.Text);
                    }
                    for (int i = 0; i < 25; i++)
                    {
                        Button tempGridBtn = (Button)Grid.Children[i + 5];
                        tempBtn.Text = "";
                    }
                    await DisplayAlert("Pass!!!", "Pass to Oppositon", "OK");
                    for (int i = 0; i < 25; i++)
                    {
                        plyrName.Text = "Player1";
                        Button tempGridBtn = (Button)Grid.Children[i + 5];
                        tempGridBtn.Text = Grid1[i];
                        Grid.BackgroundColor = Color.Silver;
                    }
                    Flicker = !Flicker;
                }
                return;
            }

            //Checking
            if (count1 > 25 && count2 > 25 && !ElementsDone1.Contains(tempBtn.Text) && !ElementsDone2.Contains(tempBtn.Text))
            {
                if (Flicker)
                {
                    tempBtn.BackgroundColor = Color.White;
                    ElementsDone1.Add(tempBtn.Text);
                }
                else if (!Flicker)
                {
                    tempBtn.BackgroundColor = Color.Orange;
                    ElementsDone2.Add(tempBtn.Text);
                }
                if (ElementsDone1.Count + ElementsDone2.Count > 5)
                {
                    if (Flicker)
                    {
                        {
                            for (int i = 0, n = 0; i < 25; i += 5)
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid1[i + j] == ElementsDone1[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDone2.Count; k++)
                                        {
                                            if (Grid1[i + j] == ElementsDone2[k])
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
                                        if (!ElementsCrossed1.Contains(Grid1[i + n]))
                                        {
                                            ElementsCrossed1.Add(Grid1[i + n]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross1++;
                                        if (lblCross1 == 5)
                                        {
                                            await DisplayAlert("Winner", "Player1 Wins!!", "OK");
                                            await Navigation.PushModalAsync(new MainPage());
                                            count1 = 1;
                                            count2 = 1;
                                            Flicker = true;
                                            Grid1 = new List<string>(25);
                                            ElementsDone1 = new List<string>(25);
                                            ElementsDone2 = new List<string>(25);
                                            ElementsCrossed1 = new List<string>(25);
                                            ElementsCrossed2 = new List<string>(25);
                                            Grid2 = new List<string>(25);
                                            CheckVar = false;
                                            lblCross1 = 0;
                                            lblCross2 = 0;
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        {
                            for (int i = 0, n = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 25; j += 5)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid1[i + j] == ElementsDone1[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDone2.Count; k++)
                                        {
                                            if (Grid1[i + j] == ElementsDone2[k])
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
                                        if (!ElementsCrossed1.Contains(Grid1[i + (n * 5)]))
                                        {
                                            ElementsCrossed1.Add(Grid1[i + (n * 5)]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross1++;
                                        if (lblCross1 == 5)
                                        {
                                            await DisplayAlert("Winner", "Player1 Wins!!", "OK");
                                            await Navigation.PushModalAsync(new MainPage());
                                            count1 = 1;
                                            count2 = 1;
                                            Flicker = true;
                                            Grid1 = new List<string>(25);
                                            ElementsDone1 = new List<string>(25);
                                            ElementsDone2 = new List<string>(25);
                                            ElementsCrossed1 = new List<string>(25);
                                            ElementsCrossed2 = new List<string>(25);
                                            Grid2 = new List<string>(25);
                                            CheckVar = false;
                                            lblCross1 = 0;
                                            lblCross2 = 0;
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        {
                            int n = 0;
                            for (int i = 0; i < 25; i += 6)
                            {
                                int temp = n;
                                for (int k = 0; k < ElementsDone2.Count; k++)
                                {
                                    if (Grid1[i] == ElementsDone2[k])
                                    {
                                        n++;
                                        break;
                                    }
                                }
                                if (temp == n)
                                {
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid1[i] == ElementsDone1[k])
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
                                    if (!ElementsCrossed1.Contains(Grid1[(n * 6)]))
                                    {
                                        ElementsCrossed1.Add(Grid1[(n * 6)]);
                                        CheckVar = true;
                                    }
                                }
                                if (CheckVar)
                                {
                                    lblCross1++;
                                    if (lblCross1 == 5)
                                    {
                                        await DisplayAlert("Winner", "Player1 Wins!!", "OK");
                                        await Navigation.PushModalAsync(new MainPage());
                                        count1 = 1;
                                        count2 = 1;
                                        Flicker = true;
                                        Grid1 = new List<string>(25);
                                        ElementsDone1 = new List<string>(25);
                                        ElementsDone2 = new List<string>(25);
                                        ElementsCrossed1 = new List<string>(25);
                                        ElementsCrossed2 = new List<string>(25);
                                        Grid2 = new List<string>(25);
                                        CheckVar = false;
                                        lblCross1 = 0;
                                        lblCross2 = 0;
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
                                for (int k = 0; k < ElementsDone2.Count; k++)
                                {
                                    if (Grid1[i] == ElementsDone2[k])
                                    {
                                        n++;
                                        break;
                                    }
                                }
                                if (n == temp)
                                {
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid1[i] == ElementsDone1[k])
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
                                    if (!ElementsCrossed1.Contains(Grid1[(n * 4)]))
                                    {
                                        ElementsCrossed1.Add(Grid1[(n * 4)]);
                                        CheckVar = true;
                                    }
                                    n--;
                                }
                                if (CheckVar)
                                {
                                    lblCross1++;
                                    if (lblCross1 == 5)
                                    {
                                        await DisplayAlert("Winner", "Player1 Wins!!", "OK");
                                        await Navigation.PushModalAsync(new MainPage());
                                        count1 = 1;
                                        count2 = 1;
                                        Flicker = true;
                                        Grid1 = new List<string>(25);
                                        ElementsDone1 = new List<string>(25);
                                        ElementsDone2 = new List<string>(25);
                                        ElementsCrossed1 = new List<string>(25);
                                        ElementsCrossed2 = new List<string>(25);
                                        Grid2 = new List<string>(25);
                                        CheckVar = false;
                                        lblCross1 = 0;
                                        lblCross2 = 0;
                                        return;
                                    }
                                    CheckVar = false;
                                }
                            }
                        }
                        {
                            for (int i = 0, n = 0; i < 25; i += 5)
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone2.Count; k++)
                                    {
                                        if (Grid2[i + j] == ElementsDone2[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDone1.Count; k++)
                                        {
                                            if (Grid2[i + j] == ElementsDone1[k])
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
                                        if (!ElementsCrossed2.Contains(Grid2[i + n]))
                                        {
                                            ElementsCrossed2.Add(Grid2[i + n]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross2++;
                                        if (lblCross2 == 5)
                                        {
                                            await DisplayAlert("Winner", "Player2 Wins!!", "OK");
                                            await Navigation.PushModalAsync(new MainPage());
                                            count1 = 1;
                                            count2 = 1;
                                            Flicker = true;
                                            Grid1 = new List<string>(25);
                                            ElementsDone1 = new List<string>(25);
                                            ElementsDone2 = new List<string>(25);
                                            ElementsCrossed1 = new List<string>(25);
                                            ElementsCrossed2 = new List<string>(25);
                                            Grid2 = new List<string>(25);
                                            CheckVar = false;
                                            lblCross1 = 0;
                                            lblCross2 = 0;
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        {
                            for (int i = 0, n = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 25; j += 5)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone2.Count; k++)
                                    {
                                        if (Grid2[i + j] == ElementsDone2[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDone1.Count; k++)
                                        {
                                            if (Grid2[i + j] == ElementsDone1[k])
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
                                        if (!ElementsCrossed2.Contains(Grid2[i + (n * 5)]))
                                        {
                                            ElementsCrossed2.Add(Grid2[i + (n * 5)]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross2++;
                                        if (lblCross2 == 5)
                                        {
                                            await DisplayAlert("Winner", "Player2 Wins!!", "OK");
                                            await Navigation.PushModalAsync(new MainPage());
                                            count1 = 1;
                                            count2 = 1;
                                            Flicker = true;
                                            Grid1 = new List<string>(25);
                                            ElementsDone1 = new List<string>(25);
                                            ElementsDone2 = new List<string>(25);
                                            ElementsCrossed1 = new List<string>(25);
                                            ElementsCrossed2 = new List<string>(25);
                                            Grid2 = new List<string>(25);
                                            CheckVar = false;
                                            lblCross1 = 0;
                                            lblCross2 = 0;
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        {
                            int n = 0;
                            for (int i = 0; i < 25; i += 6)
                            {
                                int temp = n;
                                for (int k = 0; k < ElementsDone2.Count; k++)
                                {
                                    if (Grid2[i] == ElementsDone2[k])
                                    {
                                        n++;
                                        break;
                                    }
                                }
                                if (n == temp)
                                {
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid2[i] == ElementsDone1[k])
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
                                    if (!ElementsCrossed2.Contains(Grid2[(n * 6)]))
                                    {
                                        ElementsCrossed2.Add(Grid2[(n * 6)]);
                                        CheckVar = true;
                                    }
                                }
                                if (CheckVar)
                                {
                                    lblCross2++;
                                    if (lblCross2 == 5)
                                    {
                                        await DisplayAlert("Winner", "Player2 Wins!!", "OK");
                                        await Navigation.PushModalAsync(new MainPage());
                                        count1 = 1;
                                        count2 = 1;
                                        Flicker = true;
                                        Grid1 = new List<string>(25);
                                        ElementsDone1 = new List<string>(25);
                                        ElementsDone2 = new List<string>(25);
                                        ElementsCrossed1 = new List<string>(25);
                                        ElementsCrossed2 = new List<string>(25);
                                        Grid2 = new List<string>(25);
                                        CheckVar = false;
                                        lblCross1 = 0;
                                        lblCross2 = 0;
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
                                for (int k = 0; k < ElementsDone2.Count; k++)
                                {
                                    if (Grid2[i] == ElementsDone2[k])
                                    {
                                        n++;
                                        break;
                                    }
                                }
                                if (temp == n)
                                {
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid2[i] == ElementsDone1[k])
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
                                    if (!ElementsCrossed2.Contains(Grid2[(n * 4)]))
                                    {
                                        ElementsCrossed2.Add(Grid2[(n * 4)]);
                                        CheckVar = true;
                                    }
                                    n--;
                                }
                                if (CheckVar)
                                {
                                    lblCross2++;
                                    if (lblCross2 == 5)
                                    {
                                        await DisplayAlert("Winner", "Player2 Wins!!", "OK");
                                        await Navigation.PushModalAsync(new MainPage());
                                        count1 = 1;
                                        count2 = 1;
                                        Flicker = true;
                                        Grid1 = new List<string>(25);
                                        ElementsDone1 = new List<string>(25);
                                        ElementsDone2 = new List<string>(25);
                                        ElementsCrossed1 = new List<string>(25);
                                        ElementsCrossed2 = new List<string>(25);
                                        Grid2 = new List<string>(25);
                                        CheckVar = false;
                                        lblCross1 = 0;
                                        lblCross2 = 0;
                                        return;
                                    }
                                    CheckVar = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        {
                            for (int i = 0, n = 0; i < 25; i += 5)
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone2.Count; k++)
                                    {
                                        if (Grid2[i + j] == ElementsDone2[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDone1.Count; k++)
                                        {
                                            if (Grid2[i + j] == ElementsDone1[k])
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
                                        if (!ElementsCrossed2.Contains(Grid2[i + n]))
                                        {
                                            ElementsCrossed2.Add(Grid2[i + n]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross2++;
                                        if (lblCross2 == 5)
                                        {
                                            await DisplayAlert("Winner", "Player2 Wins!!", "OK");
                                            await Navigation.PushModalAsync(new MainPage());
                                            count1 = 1;
                                            count2 = 1;
                                            Flicker = true;
                                            Grid1 = new List<string>(25);
                                            ElementsDone1 = new List<string>(25);
                                            ElementsDone2 = new List<string>(25);
                                            ElementsCrossed1 = new List<string>(25);
                                            ElementsCrossed2 = new List<string>(25);
                                            Grid2 = new List<string>(25);
                                            CheckVar = false;
                                            lblCross1 = 0;
                                            lblCross2 = 0;
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        {
                            for (int i = 0, n = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 25; j += 5)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone2.Count; k++)
                                    {
                                        if (Grid2[i + j] == ElementsDone2[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDone1.Count; k++)
                                        {
                                            if (Grid2[i + j] == ElementsDone1[k])
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
                                        if (!ElementsCrossed2.Contains(Grid2[i + (n * 5)]))
                                        {
                                            ElementsCrossed2.Add(Grid2[i + (n * 5)]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross2++;
                                        if (lblCross2 == 5)
                                        {
                                            await DisplayAlert("Winner", "Player2 Wins!!", "OK");
                                            await Navigation.PushModalAsync(new MainPage());
                                            count1 = 1;
                                            count2 = 1;
                                            Flicker = true;
                                            Grid1 = new List<string>(25);
                                            ElementsDone1 = new List<string>(25);
                                            ElementsDone2 = new List<string>(25);
                                            ElementsCrossed1 = new List<string>(25);
                                            ElementsCrossed2 = new List<string>(25);
                                            Grid2 = new List<string>(25);
                                            CheckVar = false;
                                            lblCross1 = 0;
                                            lblCross2 = 0;
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        {
                            int n = 0;
                            for (int i = 0; i < 25; i += 6)
                            {
                                int temp = n;
                                for (int k = 0; k < ElementsDone2.Count; k++)
                                {
                                    if (Grid2[i] == ElementsDone2[k])
                                    {
                                        n++;
                                        break;
                                    }
                                }
                                if (n == temp)
                                {
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid2[i] == ElementsDone1[k])
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
                                    if (!ElementsCrossed2.Contains(Grid2[(n * 6)]))
                                    {
                                        ElementsCrossed2.Add(Grid2[(n * 6)]);
                                        CheckVar = true;
                                    }
                                }
                                if (CheckVar)
                                {
                                    lblCross2++;
                                    if (lblCross2 == 5)
                                    {
                                        await DisplayAlert("Winner", "Player2 Wins!!", "OK");
                                        await Navigation.PushModalAsync(new MainPage());
                                        count1 = 1;
                                        count2 = 1;
                                        Flicker = true;
                                        Grid1 = new List<string>(25);
                                        ElementsDone1 = new List<string>(25);
                                        ElementsDone2 = new List<string>(25);
                                        ElementsCrossed1 = new List<string>(25);
                                        ElementsCrossed2 = new List<string>(25);
                                        Grid2 = new List<string>(25);
                                        CheckVar = false;
                                        lblCross1 = 0;
                                        lblCross2 = 0;
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
                                for (int k = 0; k < ElementsDone2.Count; k++)
                                {
                                    if (Grid2[i] == ElementsDone2[k])
                                    {
                                        n++;
                                        break;
                                    }
                                }
                                if (temp == n)
                                {
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid2[i] == ElementsDone1[k])
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
                                    if (!ElementsCrossed2.Contains(Grid2[(n * 4)]))
                                    {
                                        ElementsCrossed2.Add(Grid2[(n * 4)]);
                                        CheckVar = true;
                                    }
                                    n--;
                                }
                                if (CheckVar)
                                {
                                    lblCross2++;
                                    if (lblCross2 == 5)
                                    {
                                        await DisplayAlert("Winner", "Player2 Wins!!", "OK");
                                        await Navigation.PushModalAsync(new MainPage());
                                        count1 = 1;
                                        count2 = 1;
                                        Flicker = true;
                                        Grid1 = new List<string>(25);
                                        ElementsDone1 = new List<string>(25);
                                        ElementsDone2 = new List<string>(25);
                                        ElementsCrossed1 = new List<string>(25);
                                        ElementsCrossed2 = new List<string>(25);
                                        Grid2 = new List<string>(25);
                                        CheckVar = false;
                                        lblCross1 = 0;
                                        lblCross2 = 0;
                                        return;
                                    }
                                    CheckVar = false;
                                }
                            }
                        }
                        {
                            for (int i = 0, n = 0; i < 25; i += 5)
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid1[i + j] == ElementsDone1[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDone2.Count; k++)
                                        {
                                            if (Grid1[i + j] == ElementsDone2[k])
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
                                        if (!ElementsCrossed1.Contains(Grid1[i + n]))
                                        {
                                            ElementsCrossed1.Add(Grid1[i + n]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross1++;
                                        if (lblCross1 == 5)
                                        {
                                            await DisplayAlert("Winner", "Player1 Wins!!", "OK");
                                            await Navigation.PushModalAsync(new MainPage());
                                            count1 = 1;
                                            count2 = 1;
                                            Flicker = true;
                                            Grid1 = new List<string>(25);
                                            ElementsDone1 = new List<string>(25);
                                            ElementsDone2 = new List<string>(25);
                                            ElementsCrossed1 = new List<string>(25);
                                            ElementsCrossed2 = new List<string>(25);
                                            Grid2 = new List<string>(25);
                                            CheckVar = false;
                                            lblCross1 = 0;
                                            lblCross2 = 0;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        {
                            for (int i = 0, n = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 25; j += 5)
                                {
                                    int temp = n;
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid1[i + j] == ElementsDone1[k])
                                        {
                                            n++;
                                            break;
                                        }
                                    }
                                    if (n == temp)
                                    {
                                        for (int k = 0; k < ElementsDone2.Count; k++)
                                        {
                                            if (Grid1[i + j] == ElementsDone2[k])
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
                                        if (!ElementsCrossed1.Contains(Grid1[i + (n * 5)]))
                                        {
                                            ElementsCrossed1.Add(Grid1[i + (n * 5)]);
                                            CheckVar = true;
                                        }
                                    }
                                    if (CheckVar)
                                    {
                                        lblCross1++;
                                        if (lblCross1 == 5)
                                        {
                                            await DisplayAlert("Winner", "Player1 Wins!!", "OK");
                                            await Navigation.PushModalAsync(new MainPage());
                                            count1 = 1;
                                            count2 = 1;
                                            Flicker = true;
                                            Grid1 = new List<string>(25);
                                            ElementsDone1 = new List<string>(25);
                                            ElementsDone2 = new List<string>(25);
                                            ElementsCrossed1 = new List<string>(25);
                                            ElementsCrossed2 = new List<string>(25);
                                            Grid2 = new List<string>(25);
                                            CheckVar = false;
                                            lblCross1 = 0;
                                            lblCross2 = 0;
                                            return;
                                        }
                                        CheckVar = false;
                                    }
                                }
                            }
                        }
                        {
                            int n = 0;
                            for (int i = 0; i < 25; i += 6)
                            {
                                int temp = n;
                                for (int k = 0; k < ElementsDone2.Count; k++)
                                {
                                    if (Grid1[i] == ElementsDone2[k])
                                    {
                                        n++;
                                        break;
                                    }
                                }
                                if (temp == n)
                                {
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid1[i] == ElementsDone1[k])
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
                                    if (!ElementsCrossed1.Contains(Grid1[(n * 6)]))
                                    {
                                        ElementsCrossed1.Add(Grid1[(n * 6)]);
                                        CheckVar = true;
                                    }
                                }
                                if (CheckVar)
                                {
                                    lblCross1++;
                                    if (lblCross1 == 5)
                                    {
                                        await DisplayAlert("Winner", "Player1 Wins!!", "OK");
                                        await Navigation.PushModalAsync(new MainPage());
                                        count1 = 1;
                                        count2 = 1;
                                        Flicker = true;
                                        Grid1 = new List<string>(25);
                                        ElementsDone1 = new List<string>(25);
                                        ElementsDone2 = new List<string>(25);
                                        ElementsCrossed1 = new List<string>(25);
                                        ElementsCrossed2 = new List<string>(25);
                                        Grid2 = new List<string>(25);
                                        CheckVar = false;
                                        lblCross1 = 0;
                                        lblCross2 = 0;
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
                                for (int k = 0; k < ElementsDone2.Count; k++)
                                {
                                    if (Grid1[i] == ElementsDone2[k])
                                    {
                                        n++;
                                        break;
                                    }
                                }
                                if (n == temp)
                                {
                                    for (int k = 0; k < ElementsDone1.Count; k++)
                                    {
                                        if (Grid1[i] == ElementsDone1[k])
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
                                    if (!ElementsCrossed1.Contains(Grid1[(n * 4)]))
                                    {
                                        ElementsCrossed1.Add(Grid1[(n * 4)]);
                                        CheckVar = true;
                                    }
                                    n--;
                                }
                                if (CheckVar)
                                {
                                    lblCross1++;
                                    if (lblCross1 == 5)
                                    {
                                        await DisplayAlert("Winner", "Player1 Wins!!", "OK");
                                        await Navigation.PushModalAsync(new MainPage());
                                        count1 = 1;
                                        count2 = 1;
                                        Flicker = true;
                                        Grid1 = new List<string>(25);
                                        ElementsDone1 = new List<string>(25);
                                        ElementsDone2 = new List<string>(25);
                                        ElementsCrossed1 = new List<string>(25);
                                        ElementsCrossed2 = new List<string>(25);
                                        Grid2 = new List<string>(25);
                                        CheckVar = false;
                                        lblCross1 = 0;
                                        lblCross2 = 0;
                                        return;
                                    }
                                    CheckVar = false;
                                }
                            }
                        }

                    }
                }
                if (Flicker)
                {
                    for (int i = 5; i < 30; i++)
                    {
                        Button temp = (Button)Grid.Children[i];
                        temp.Text = "";
                    }
                    await DisplayAlert("Pass!!!", "Pass to Oppositon", "OK");
                    Flicker = !Flicker;
                    for (int i = 5; i < 30; i++)
                    {
                        plyrName.Text = "Player2";
                        Button temp = (Button)Grid.Children[i];
                        temp.Text = Grid2[i - 5];
                        if (ElementsCrossed2.Contains(temp.Text))
                            temp.BackgroundColor = Color.Purple;
                        else if (ElementsDone1.Contains(temp.Text))
                            temp.BackgroundColor = Color.White;
                        else if (ElementsDone2.Contains(temp.Text))
                            temp.BackgroundColor = Color.Orange;
                        else
                            temp.BackgroundColor = Color.Transparent;
                        //Grid.BackgroundColor = Color.Teal;
                        //Content.BackgroundColor = Color.Teal;
                        
                        
                    }
                    for (int i = 0; i < lblCross2; i++)
                    {
                        Label l = (Label)Grid.Children[i];
                        l.BackgroundColor = Color.Transparent;
                        l.FontAttributes = FontAttributes.Italic;
                    }
                }
                else
                {
                    for (int i = 5; i < 30; i++)
                    {
                        Button temp = (Button)Grid.Children[i];
                        temp.Text = "";
                    }
                    await DisplayAlert("Pass!!!", "Pass to Oppositon", "OK");
                    Flicker = !Flicker;
                    for (int i = 5; i < 30; i++)
                    {
                        plyrName.Text = "Player1";
                        Button temp = (Button)Grid.Children[i];
                        temp.Text = Grid1[i - 5];
                        if (ElementsCrossed1.Contains(temp.Text))
                            temp.BackgroundColor = Color.Olive;
                        else if (ElementsDone2.Contains(temp.Text))
                            temp.BackgroundColor = Color.Orange;
                        else if (ElementsDone1.Contains(temp.Text))
                            temp.BackgroundColor = Color.White;
                        else
                            temp.BackgroundColor = Color.Transparent;
                        Grid.BackgroundColor = Color.Silver;
                    }
                    for (int i = 0; i < lblCross1; i++)
                    {
                        Label l = (Label)Grid.Children[i];
                        l.BackgroundColor = Color.Transparent;
                        l.FontAttributes = FontAttributes.Italic;
                    }
                }
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
            count1 = 1;
            count2 = 1;
            Flicker = true;
            Grid1 = new List<string>(25);
            ElementsDone1 = new List<string>(25);
            ElementsDone2 = new List<string>(25);
            ElementsCrossed1 = new List<string>(25);
            ElementsCrossed2 = new List<string>(25);
            Grid2 = new List<string>(25);
            CheckVar = false;
            lblCross1 = 0;
            lblCross2 = 0;
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SingleGame());
            count1 = 1;
            count2 = 1;
            Flicker = true;
            Grid1 = new List<string>(25);
            ElementsDone1 = new List<string>(25);
            ElementsDone2 = new List<string>(25);
            ElementsCrossed1 = new List<string>(25);
            ElementsCrossed2 = new List<string>(25);
            Grid2 = new List<string>(25);
            CheckVar = false;
            lblCross1 = 0;
            lblCross2 = 0;
    }

       
    }
}

