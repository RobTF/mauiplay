namespace ImageGridIssue;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void LeftClicked(object sender, EventArgs e)
    {
		Console.WriteLine("LEFT BUTTON TAPPED!!");
    }

    private void RightClicked(object sender, EventArgs e)
    {

    }
}

