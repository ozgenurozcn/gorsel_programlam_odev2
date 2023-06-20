using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Görsel_Programlama_Ödev2.Views;

public partial class Ayarlar : ContentPage
{
    public Ayarlar()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public bool IsDarkTheme
    {
        get { return Application.Current.UserAppTheme == AppTheme.Dark; }
        set
        {
            if (value)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
        }
    }

    private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        IsDarkTheme = e.Value;
    }
}