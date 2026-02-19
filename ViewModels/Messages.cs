using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AddressBookPlus.ViewModels;

public class ContactSavedMessage : ValueChangedMessage<int>
{
    public ContactSavedMessage(int contactId) : base(contactId) { }
}

public class ContactDeletedMessage : ValueChangedMessage<int>
{
    public ContactDeletedMessage(int contactId) : base(contactId) { }
}