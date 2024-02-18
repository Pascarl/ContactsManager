using ContactsManager.Data;
using ContactsManager.Models;
using static System.Net.WebRequestMethods;

namespace ContactsManager;

public partial class ViewContact : ContentPage
{
	private readonly ContactsDbService _contactsDbService;
	private readonly ContactsModel _contactsModel;
	public ViewContact(ContactsDbService contactsDbService,ContactsModel contactsModel)
	{
		InitializeComponent();
		_contactsDbService = contactsDbService;
		_contactsModel = contactsModel;
		BindingContext = contactsModel;
	}

	private void Editbtn(object sender, EventArgs e)
	{
		Navigation.PushAsync(new EditContact(_contactsDbService,_contactsModel));
	}

	private void Cancelbtn(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage(_contactsDbService));
	}

	private  void Callbtn(object sender, EventArgs e)
	{
		if(PhoneDialer.Default.IsSupported)
		{
		  	PhoneDialer.Default.Open(_contactsModel.Cell);
		}
	}

	private async void Wabtn(object sender, EventArgs e)
	{
		string wa = $"https://wa.me/{_contactsModel.Cell}";
		await Launcher.OpenAsync(wa);
	}

	private async void Emailbtn(object sender, EventArgs e)
	{
		if(Email.Default.IsComposeSupported & _contactsModel.Email != null)
		{
			await Email.Default.ComposeAsync("","",_contactsModel.Email);
		}
		else
		{
			await DisplayAlert("Error!", "Default Not Supported!", "OK");
		}
	}

	private async void Deletebtn(object sender, EventArgs e)
	{
		await _contactsDbService.delete(_contactsModel);
		await Navigation.PushAsync(new MainPage(_contactsDbService));
	}
}