using CommunityToolkit.Mvvm.ComponentModel;

namespace AddressBookPlus.Models;

public partial class GroupSelection : ObservableObject
{
    public int GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;

    [ObservableProperty]
    private bool _isMember;
}
