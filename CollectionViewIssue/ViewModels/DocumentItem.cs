using CommunityToolkit.Mvvm.ComponentModel;

namespace CollectionViewIssue.ViewModels;

public sealed partial class DocumentItem : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _description;
}
