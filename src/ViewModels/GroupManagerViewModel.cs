using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AddressBookPlus.Models;
using AddressBookPlus.Services;

namespace AddressBookPlus.ViewModels;

public partial class GroupManagerViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    public GroupManagerViewModel(IDataService dataService)
    {
        _dataService = dataService;
        Groups = new ObservableCollection<ContactGroup>();
        Title = "Manage Groups";
    }

    [ObservableProperty]
    private ObservableCollection<ContactGroup> _groups;

    [RelayCommand]
    private async Task LoadGroupsAsync()
    {
        IsBusy = true;
        try
        {
            var groups = await _dataService.GetGroupsAsync();
            Groups.Clear();
            foreach (var group in groups)
            {
                Groups.Add(group);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task RenameGroupAsync(ContactGroup group)
    {
        if (group == null) return;

        var page = Application.Current?.Windows[0]?.Page;
        if (page == null) return;

        var newName = await page.DisplayPromptAsync(
            "Rename Group",
            "Enter new group name:",
            "Save",
            "Cancel",
            group.Name,
            maxLength: 50);

        if (!string.IsNullOrWhiteSpace(newName) && newName != group.Name)
        {
            group.Name = newName;
            await _dataService.SaveGroupAsync(group);
            await LoadGroupsAsync(); // Refresh the list
        }
    }

    [RelayCommand]
    private async Task AddGroupAsync()
    {
        // Find next available slot (max 9 groups)
        var existingSlots = Groups.Select(g => g.Id).OrderBy(id => id).ToList();
        var nextSlot = 1;
        for (int i = 1; i <= 9; i++)
        {
            if (!existingSlots.Contains(i))
            {
                nextSlot = i;
                break;
            }
        }

        if (nextSlot > 9)
        {
            var errorPage = Application.Current?.Windows[0]?.Page;
            if (errorPage != null)
                await errorPage.DisplayAlertAsync("Error", "Maximum of 9 groups allowed.", "OK");
            return;
        }

        var promptPage = Application.Current?.Windows[0]?.Page;
        if (promptPage == null) return;

        var groupName = await promptPage.DisplayPromptAsync(
            "New Group",
            "Enter group name:",
            "Create",
            "Cancel",
            placeholder: "Group Name",
            maxLength: 50);

        if (!string.IsNullOrWhiteSpace(groupName))
        {
            var newGroup = new ContactGroup
            {
                Id = nextSlot,
                Name = groupName
            };

            await _dataService.SaveGroupAsync(newGroup);
            await LoadGroupsAsync(); // Refresh the list
        }
    }

    [RelayCommand]
    private async Task DeleteGroupAsync(ContactGroup group)
    {
        if (group == null) return;

        var page = Application.Current?.Windows[0]?.Page;
        if (page == null) return;

        var result = await page.DisplayAlertAsync(
            "Delete Group",
            $"Are you sure you want to delete the group '{group.Name}'? This will not delete the contacts in the group.",
            "Delete",
            "Cancel");

        if (result == true)
        {
            await _dataService.DeleteGroupAsync(group);
            await LoadGroupsAsync(); // Refresh the list
        }
    }
}