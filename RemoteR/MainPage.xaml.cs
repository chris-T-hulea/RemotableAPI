namespace RemoteR;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}

