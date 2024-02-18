using ContactsManager.Data;
using ContactsManager.Models;

namespace ContactsManager;

public partial class AddContact : ContentPage
{
	private readonly ContactsDbService _contactsDbService;
	private ContactsModel _contactsModel;
	public AddContact(ContactsDbService contactsDbService)
	{
		InitializeComponent();
		_contactsDbService = contactsDbService;
	}

	private async void Addbtn(object sender, EventArgs e)
	{

		await _contactsDbService.create(new ContactsModel()
		{
			Name = ConName.Text,
			Email = ConEmail.Text,
			Cell = ConCell.Text,
			Cell2 = Concell2.Text
		});
		await Navigation.PushAsync(new MainPage(_contactsDbService));
	}

	private void Cancelbtn(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage(_contactsDbService));
	}
}