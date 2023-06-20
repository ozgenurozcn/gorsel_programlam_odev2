namespace Görsel_Programlama_Ödev2.Views;

public partial class Anasayfa : FlyoutPage
{ 
	public Anasayfa()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        Detail = new Anasayfa();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Detail = new Kurlar();
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        Detail = new haber();
    }

    private void Button_Clicked_3(object sender, EventArgs e)
    {
        Detail = new SehirlerHavaDurumu();
    }

    private void Button_Clicked_4(object sender, EventArgs e)
    {
        Detail = new Yapilacaklar();
    }

    private void Button_Clicked_5(object sender, EventArgs e)
    {
        Detail = new Ayarlar();
    }
}