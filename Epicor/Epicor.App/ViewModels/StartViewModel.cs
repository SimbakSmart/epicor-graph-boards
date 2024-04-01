
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Epicor.App.UC;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MenuItem = Epicor.App.Helpers.MenuItem;



namespace Epicor.App.ViewModels
{
    public partial class StartViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isOpen;

        private ObservableCollection<MenuItem> _menuOptions;
        private MenuItem _selectedItem;
        private UserControl _selectedUserControl;

        private string _titleTextBlock;

        public ObservableCollection<MenuItem> MenuOptions
        {
            get { return _menuOptions; }
            set { SetProperty(ref _menuOptions, value); }
        }

        public MenuItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                SelectedUserControl = (UserControl)Activator.CreateInstance(_selectedItem.UserControlType);
                TitleTextBlock = _selectedItem.Title;
                IsOpen = false;
            }
        }

        public string TitleTextBlock
        {
            get { return _titleTextBlock; }
            set { SetProperty(ref _titleTextBlock, value); }
        }


        public UserControl SelectedUserControl
        {
            get { return _selectedUserControl; }
            set { SetProperty(ref _selectedUserControl, value); }
        }

        public StartViewModel()
        {

            TitleTextBlock= "ANÁLISIS DE REPORTES";
            MenuOptions = new ObservableCollection<MenuItem>()
            {
               new MenuItem("QUEUES","ViewDashboard","ANÁLISIS DE REPORTES EN QUEUES",typeof(QueuesControl)),
               new MenuItem("USUARIOS","AccountGroup","ANÁLISIS DE REPORTES EN USUARIOS",typeof(UsersControl)),
            };
            SelectedItem = MenuOptions[0];

            IsOpen = false;
        }



        [RelayCommand]
        private void ToggleMenu()
        {
            IsOpen = IsOpen ? true : false;
        }

       


    }
}
