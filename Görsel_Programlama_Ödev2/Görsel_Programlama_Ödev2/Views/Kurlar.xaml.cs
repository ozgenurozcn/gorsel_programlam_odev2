using System.ComponentModel;
using System.Text.Json;

namespace Görsel_Programlama_Ödev2.Views;

public partial class Kurlar : ContentPage
{
	public Kurlar()
	{
		InitializeComponent();
	}

    private static Kurlar instance;
    public static Kurlar Page
    {
        get
        {
            if (instance == null) ;
            instance = new Kurlar();
            return instance;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Load();
    }

    Root kurlar;

    private async Task Load()
    {
        string jsondata = await GetKurlar();

        kurlar = JsonSerializer.Deserialize<Root>(jsondata);

        dovizliste.ItemsSource = new List<Doviz>()
        {
            new Doviz()
            {
                DName = "Dolar",
                FAlis = kurlar.USD.alis,
                FSatis = kurlar.USD.satis,
                Fark = kurlar.USD.degisim,
                Yon = GetImage(kurlar.USD.d_yon),
            },

            new Doviz()
            {
                DName = "Euro",
                FAlis = kurlar.EUR.alis,
                FSatis = kurlar.EUR.satis,
                Fark = kurlar.EUR.degisim,
                Yon = GetImage(kurlar.EUR.d_yon),
            },

            new Doviz()
            {
                DName = "Sterlin",
                FAlis = kurlar.GBP.alis,
                FSatis = kurlar.GBP.satis,
                Fark = kurlar.GBP.degisim,
                Yon = GetImage(kurlar.GBP.d_yon),
            },

            new Doviz()
            {
                DName = "Çeyrek Altın",
                FAlis = kurlar.C.alis,
                FSatis = kurlar.C.satis,
                Fark = kurlar.C.degisim,
                Yon = GetImage(kurlar.C.d_yon),

            },
                
             
             new Doviz()
             {
                DName = "Gümüş",
                FAlis = kurlar.GAG.alis,
                FSatis = kurlar.GAG.satis,
                Fark = kurlar.GAG.degisim,
                Yon = GetImage(kurlar.GAG.d_yon),
             }
               

        };
    }

    private string GetImage(string str)
    {
        if (str.Contains("up")) return "up.png";
        if (str.Contains("down")) return "down.png";
        if (str.Contains("minus")) return "minus.png";
        return "";
    }

    private async Task<string> GetKurlar()
    {
        string url = "https://api.genelpara.com/embed/altin.json";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client .GetAsync(url);
        response.EnsureSuccessStatusCode();

        string jsondata = await response.Content.ReadAsStringAsync();
        return jsondata;
    }

    public class Doviz
    {
        public string DName { get; set; }
        public string FAlis { get; set; }
        public string FSatis { get; set; }
        public string Fark { get; set; }
        public string Yon { get; set; }

    }

        public class Root
    {
        public USD USD { get; set; }
        public EUR EUR { get; set; }
        public GBP GBP { get; set; }
        public GA GA { get; set; }
        public C C { get; set; }
        public GAG GAG { get; set; }
        public BTC BTC { get; set; }
        public ETH ETH { get; set; }
        public XU100 XU100 { get; set; }
    }
    public class BTC
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class C
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class ETH
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class EUR
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class GA
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class GAG
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class GBP
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }


    public class USD
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class XU100
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
    }
}