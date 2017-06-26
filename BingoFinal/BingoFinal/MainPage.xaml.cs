using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BingoFinal
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //InitializeComponent();
            StackLayout s = new StackLayout();
            Button sp = new Button();
            sp.Text = "Single Player";
            sp.Clicked += Sp_Clicked;
            Button mp = new Button();
            mp.Text = "Multi Player";
            mp.Clicked += Sp_Clicked;
            /*Button exit = new Button()
            {
                Text = "Exit",
            };
            exit.Clicked += Exit_Clicked;*/
            s.Children.Add(sp);
            s.Children.Add(mp);
            //s.Children.Add(exit);
            s.HorizontalOptions = LayoutOptions.CenterAndExpand;
            s.VerticalOptions = LayoutOptions.CenterAndExpand;
            sp.WidthRequest = 250;
            mp.WidthRequest = 250;
            sp.BorderRadius = 2;
            sp.BorderWidth = 5;
            sp.Margin = 8;
            mp.BorderRadius = 2;
            mp.BorderWidth = 5;
            mp.Margin = 8;
            this.Content = s;
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        async private void Sp_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Button tempBtn = (Button)sender;
            if (tempBtn.Text == "Single Player")
                await Navigation.PushModalAsync(new SingleGame());
            else if (tempBtn.Text == "Multi Player")
                await Navigation.PushModalAsync(new MultiGameName());
        }
    }
}
