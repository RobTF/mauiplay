using System.Collections.ObjectModel;
using Asynq;
using Bogus;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionViewIssue.ViewModels;

public sealed partial class TestPageViewModel : ObservableObject
{
    private const int BatchSize = 20;

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    private ObservableCollection<DocumentItem> _items = new();

    [ObservableProperty]
    private LoadState _state;

    private bool _hasMoreItems;
    private bool _isLoading;

    private Faker _faker = new();

    public TestPageViewModel()
    {
        LoadItemsAsync().FireAndForget();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        if (!IsRefreshing)
        {
            IsRefreshing = true;
        }
        
        await LoadItemsAsync();
    }

    [RelayCommand]
    public async Task LoadMoreItemsAsync()
    {
        if (_hasMoreItems)
        {
            await LoadItemsAsync(Items.Count);
        }
    }

    async Task LoadItemsAsync(int skip = 0)
    {
        if (_isLoading)
        {
            return;
        }

        _isLoading = true;

        await Task.Delay(750); // simulate server delay
        var items = Get(skip, BatchSize + 1);

        if (skip == 0)
        {
            Items = new ObservableCollection<DocumentItem>(items.Take(BatchSize));
        }
        else
        {
            foreach (var doc in items.Take(BatchSize))
            {
                Items.Add(doc);
            }
        }

        _hasMoreItems = items.Count() == BatchSize + 1;
        State = LoadState.Normal;

        _isLoading = false;
        IsRefreshing = false;
    }

    private IEnumerable<DocumentItem> Get(int skip = 0, int take = 100)
    {
        for (var i = 0; i < (take + skip); i++)
        {
            if (i < skip)
            {
                continue;
            }

            yield return new()
            {
                Name = _faker.Name.FullName(),
                Description = _faker.Lorem.Sentence(),
            };
        }
    }
}
