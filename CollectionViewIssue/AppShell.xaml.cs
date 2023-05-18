namespace CollectionViewIssue;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("test", typeof(TestPage));
    }
}
