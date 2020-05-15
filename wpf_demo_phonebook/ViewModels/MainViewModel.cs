using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        private ContactModel selectedContact;

        public ContactModel SelectedContact
        {
            get => selectedContact;
            set { 
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ContactModel> contacts = new ObservableCollection<ContactModel>();

        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        private string criteria;

        public string Criteria
        {
            get { return criteria; }
            set { 
                criteria = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchContactCommand { get; set; }
        public RelayCommand GetContactsCommand { get; set; }
        public RelayCommand EditContactCommand { get; set; }
        public RelayCommand RemoveContactCommand { get; set; }

        public MainViewModel()
        {

            SearchContactCommand = new RelayCommand(SearchContact);
            GetContactsCommand = new RelayCommand(GetContacts);
            EditContactCommand = new RelayCommand(EditContact);
            RemoveContactCommand = new RelayCommand(RemoveContact);

            GetContacts(null);

            SelectedContact = Contacts.First<ContactModel>();

        }

        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            int output;
            string searchMethod;
            if (!Int32.TryParse(input, out output))
            {
                searchMethod = "name";
            } else
            {
                searchMethod = "id";
            }

            switch (searchMethod)
            {
                case "id":

                    SelectedContact = PhoneBookBusiness.GetContactByID(output);

                    Contacts.Clear();

                    Contacts.Add(SelectedContact);
                    break;
                case "name":

                    Contacts = PhoneBookBusiness.GetContactsByName(input);

                    if(Contacts.Count > 0)
                    {

                        SelectedContact = Contacts[0];

                    }

                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }
        }

        private void GetContacts(object parameter)
        {
            Contacts = PhoneBookBusiness.GetContacts();
        }

        private void EditContact(object parameter)
        {

            PhoneBookBusiness.EditContact(SelectedContact);

        }

        private void RemoveContact(object parameter)
        {

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                PhoneBookBusiness.RemoveContact(SelectedContact);
                Contacts.Remove(SelectedContact);

            }

        }

    }
}
