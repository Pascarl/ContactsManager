using ContactsManager.Data;
using ContactsManager.Models;

namespace ContactsManager
{
    public partial class MainPage : ContentPage
    {
        private readonly ContactsDbService _contactsDbService;
        public MainPage(ContactsDbService contactsDbService)
        {
            InitializeComponent();
            _contactsDbService = contactsDbService;
            Task.Run(async () => ContactsList.ItemsSource = await _contactsDbService.GetAll());
        }

        private async void Addbtn(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new AddContact(_contactsDbService));
        }

        private async void ContactsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var con = ContactsList.SelectedItem as ContactsModel;
            string action = await DisplayActionSheet("", "Cancel", null, "Call", "Whatsapp", "Email","View","Delete");
            switch(action)
            {
                case "Call":
                    if (PhoneDialer.Default.IsSupported)
                    {
                        PhoneDialer.Default.Open(con.Cell);
                    }
                    else
                    {
                        return;
                    }
                    break;

                case "Whatsapp":
                    if(con.Cell != null)
                    {
                        string wa = $"https://wa.me/{con.Cell}";
                        await Launcher.OpenAsync(wa);
                    }
                    else
                    {
                        return;
                    }
                    break;

                case "Email":
                    if(con.Email  != null)
                    {
                        await Email.Default.ComposeAsync("", "", con.Email);
                    }
                    else
                    {
                        await DisplayAlert("Error!", "Contact has no email.", "OK");
                    }
                    break;

                case "View":
                    await Navigation.PushAsync(new ViewContact(_contactsDbService, con));
                    break;

                case "Delete":
                    string deloption = await DisplayActionSheet($"Delete {con.Name}?", null, null, "Accept", "Cancel");
                    switch(deloption)
                    {
                        case "Accept":
                            await _contactsDbService.delete(con);
                            ContactsList.ItemsSource = await _contactsDbService.GetAll();
                            break;

                        case "Cancel":
                            await Navigation.PushAsync(new MainPage(_contactsDbService));
                            break;
                    }
                    break;
            }
        }

        private async void Searchbtn(object sender, EventArgs e)
        {
            if(Search.Text == "")
            {
                ContactsList.ItemsSource = await _contactsDbService.GetAll();

            }
            else
            {
               ContactsList.ItemsSource = await _contactsDbService.GetById(Search.Text);
            }
        }

    }

}
