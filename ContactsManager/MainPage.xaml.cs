using ContactsManager.Data;
using ContactsManager.Models;

namespace ContactsManager
{
    public partial class MainPage : ContentPage
    {
        private readonly ContactsDbService _contactsDbService;
        private readonly ContactsModel _contactsModel;
        public MainPage(ContactsDbService contactsDbService)
        {
            InitializeComponent();
            _contactsDbService = contactsDbService;
            Task.Run(async () => ContactsList.ItemsSource = await _contactsDbService.GetAll());
        }

        private void Addbtn(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddContact(_contactsDbService));
        }

        private void ContactsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var con = ContactsList.SelectedItem as ContactsModel;
             Navigation.PushAsync(new ViewContact(_contactsDbService, con));
        }

        private async void Searchbtn(object sender, EventArgs e)
        {
           ContactsList.ItemsSource = await _contactsDbService.GetById(Search.Text);
        }
    }

}
