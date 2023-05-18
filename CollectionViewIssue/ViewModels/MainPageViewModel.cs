using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionViewIssue.ViewModels;

public sealed partial class MainPageViewModel : ObservableObject
{
    [RelayCommand]
    public async Task GoToTestAsync()
    {
        await Shell.Current.GoToAsync("test");
    }
}
