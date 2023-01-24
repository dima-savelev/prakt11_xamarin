using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {
        Label[] labels;
        public RegPage()
        {
            InitializeComponent();
            labels = new Label[] { label1, label2, label3, label4, label5  };
        }
        protected override bool OnBackButtonPressed()
        {
            return false;
        }
        private string _entryPassword;
        private string _password = "12345";
        private int _count;
        private async void Number_Clicked(object sender, EventArgs e)
        {
            if (labels[4].TextColor == Color.Green)
            {
                return;
            }
            if (_count < 6)
            {
                _count++;
                for (int i = 0; i < _count; i++)
                {
                    labels[i].TextColor= Color.Green;
                }
                Button button = sender as Button;
                _entryPassword += button.Text;
            }
            if (labels[4].TextColor == Color.Green && _entryPassword != _password)
            {
                await DisplayAlert("Ошибка!", "Пароль введен неверно", "ОK");
                _count = 0;
                _entryPassword = "";
                for (int i = 0; i < 5; i++)
                {
                    labels[i].TextColor = Color.LightGray;
                }

            }
            if (_entryPassword == _password)
            {
                await Navigation.PushModalAsync(new MainPage());
                return;
            }
        }
        private void Remove_Clicked(object sender, EventArgs e)
        {
            if (_count > 0)
            {
                labels[_count - 1].TextColor = Color.LightGray;
                _entryPassword = _entryPassword.Remove(_entryPassword.Length - 1);
                _count--;
            }
        }

        private async void Fingerprint_Clicked(object sender, EventArgs e)
        {
            var request = new AuthenticationRequestConfiguration("Приложи свой палец!", "");
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            if (result.Authenticated)
            {
                await Navigation.PushModalAsync(new MainPage());
            }
        }
    }
}