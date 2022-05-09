using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Countries
{
    public partial class MainPage : ContentPage
    {
        public List<Country> riigid { get; set; }
        Label lbl_list, error;
        ListView list;
        Button kustuta, ok;
        //TableView tabelview;
        Entry nimetus, pealinn, rahvaarv;
        //Editor rahvaarv;
        public MainPage()
        {
            lbl_list = new Label
            {
                Text = "Euroopa riigid",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };

            error = new Label
            {
                Text = "",
                TextColor = Color.Red,
                HorizontalOptions = LayoutOptions.Center
            };

            riigid = new List<Country>
            {
                new Country {Nimetus="France", Pealinn="Paris", Rahvaarv=67390000, Lipp="france.png"},
                new Country {Nimetus="Germany", Pealinn="Berlin", Rahvaarv=83240000, Lipp="germany.png"},
                new Country {Nimetus="Poland", Pealinn="Warsaw", Rahvaarv=37950000, Lipp="poland.jpg"}
            };

            list = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = riigid,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.Black };
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Lipp");
                    imageCell.SetBinding(ImageCell.TextProperty, "Nimetus");
                    return imageCell;

                })
            };
            list.ItemTapped += List_ItemTapped;
            list.IsPullToRefreshEnabled = true; //возможность обновит список 

            nimetus = new Entry
            {
                Placeholder = "Enter country name:",
                Keyboard = Keyboard.Default,
                IsEnabled = true
            };

            pealinn = new Entry
            {
                Placeholder = "Enter country capital:",
                Keyboard = Keyboard.Default,
                IsEnabled = true
            };

            rahvaarv = new Entry
            {
                Placeholder = "Enter country population:",
                Keyboard = Keyboard.Numeric,
                IsEnabled = true
            };

            ok = new Button
            {
                Text = "Add",
                BackgroundColor = Color.Green
            };
            ok.Clicked += button_Click;


            kustuta = new Button 
            { 
                Text = "Delete",
                BackgroundColor = Color.Red
            };
            kustuta.Clicked += button_Click;

            this.Content = new StackLayout { Children = { lbl_list, list, error, nimetus, pealinn, rahvaarv, ok, kustuta } };
        }
        int taps;

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (sender == kustuta)
            {
                Country riik = list.SelectedItem as Country;
                if (riik != null)
                {
                    riigid.Remove(riik);
                    list.SelectedItem = null;
                    this.Content = new StackLayout { Children = { lbl_list, list, error, nimetus, pealinn, rahvaarv, ok, kustuta } };
                }
            }
            else if(sender == ok)
            {
                if (nimetus != null || pealinn != null || rahvaarv != null)
                {
                    riigid.Add(new Country { Nimetus = nimetus.Text, Pealinn = pealinn.Text, Rahvaarv = Convert.ToInt32(rahvaarv.Text), Lipp = "empty.jpg" });
                    this.Content = new StackLayout { Children = { lbl_list, list, error, nimetus, pealinn, rahvaarv, ok, kustuta } };
                    nimetus.Text = "";
                    pealinn.Text = "";
                    rahvaarv.Text = "";
                }
                error.Text = "Please write some info to fields! I can't add null value!!";
                
            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            taps++;
            Country selectedCountry = e.Item as Country;
            if (taps % 2 == 0)
            {
                if (selectedCountry != null)
                    await DisplayAlert($"{selectedCountry.Nimetus}", $"Capital - {selectedCountry.Pealinn}, Population - {selectedCountry.Rahvaarv}", "OK");
            }
            else
            {
                
            }
            
        }
    }
}
