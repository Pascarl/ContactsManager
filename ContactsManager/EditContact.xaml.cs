using ContactsManager.Data;
using ContactsManager.Models;

namespace ContactsManager;

public partial class EditContact : ContentPage
{
	private readonly ContactsDbService _contactsDbService;
	private ContactsModel _contactsModel;
	public EditContact(ContactsDbService contactsDbService, ContactsModel contactsModel)
	{
		InitializeComponent();
		_contactsDbService = contactsDbService;
		_contactsModel = contactsModel;
		BindingContext = _contactsModel;
	}

	private async void Acceptbtn(object sender, EventArgs e)
	{
		if(ConName.Text == null)
		{
			await DisplayAlert("Error!", "Please enter a contact name!", "ok");
		}
		else if(ConCell.Text == null)
		{
            await DisplayAlert("Error!", "Please enter a contact cell!", "ok");

        }
		else
		{
			_contactsModel.Name = ConName.Text;
			_contactsModel.Email = ConEmail.Text;
			_contactsModel.Cell = ConCell.Text;
			_contactsModel.Cell2 = ConCell2.Text;
            await _contactsDbService.update(_contactsModel);
			await Navigation.PushAsync(new ViewContact(_contactsDbService,_contactsModel));

        }
    }

	private async void Cancelbtn(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainPage(_contactsDbService));
	}
}