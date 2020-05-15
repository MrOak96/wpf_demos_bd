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

        public MainViewModel()
        {

            SearchContactCommand = new RelayCommand(SearchContact);
            GetContactsCommand = new RelayCommand(GetContacts);

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
                    break;
                case "name":
                    SelectedContact = PhoneBookBusiness.GetContactByName(input);
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

    }
}
