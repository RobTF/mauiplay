using CollectionViewIssue.ViewModels;

namespace CollectionViewIssue;

public partial class TestPage : ContentPage
{
	public TestPage()
	{
		InitializeComponent();
		BindingContext = new TestPageViewModel();
	}
}
