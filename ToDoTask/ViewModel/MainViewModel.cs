using System.ComponentModel;
using System.Windows.Input;
using ToDoTask.Models;
using ToDoTask.Services;
using ToDoTask.Views;

namespace ToDoTask.ViewModel;

public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DB _db;

        private List<TaskItem> _items;
        public List<TaskItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        
        private TaskItem _selectedItem;
        public TaskItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private string _currentTitle;
        public string CurrentTitle
        {
            get { return _currentTitle; }
            set
            {
                _currentTitle = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private string _currentDescription;
        public string CurrentDescription
        {
            get { return _currentDescription; }
            set
            {
                _currentDescription = value;
                OnPropertyChanged(nameof(CurrentDescription));
            }
        }
        
        private string _currentPriority;
        public string CurrentPriority
        {
            get { return _currentPriority; }
            set
            {
                _currentPriority = value;
                OnPropertyChanged(nameof(CurrentPriority));
            }
        }
        
        private string _searchTitle;
        public string SearchTitle
        {
            get { return _searchTitle; }
            set
            {
                _searchTitle = value;
                OnPropertyChanged(nameof(SearchTitle));
                Search();
            }
        }
        
        private List<string> _priorities;
        public List<string> Priorities
        {
            get { return _priorities; }
            set
            {
                _priorities = value;
                OnPropertyChanged(nameof(Priorities));
            }
        }

        public MainViewModel()
        {

            _db = new DB();
            _items = _db.TaskItems.ToList();
            _priorities = new List<string>()
            {
                "Low",
                "Medium",
                "High"
            };
            CurrentPriority = "Medium";
            _db.TaskItems.ToList();
            
        }
        
        public ICommand AddItemCommand => new Command(() =>
        {
            AddOrSaveItem();
        });
        
        public ICommand SaveCommand => new Command(() =>
        {
            AddOrSaveItem();
        });
        
        public ICommand SwipeEditItemCommand => new Command<TaskItem>((task) =>
        {
            EditTaskPage _edit = new EditTaskPage(task);
            var navigationPage = new NavigationPage(_edit);
            
            _db.TaskItems.Remove(task);
            _db.SaveChanges();
            Items = _db.TaskItems.ToList();

            Application.Current.MainPage = navigationPage;
        });
        
        public ICommand EditItemCommand => new Command(() =>
        {
            EditItem(SelectedItem);
        });
        
        public ICommand SwipeDeleteItemCommand => new Command<TaskItem>((task) =>
        {
            DeleteItem(task);
        });
        public ICommand DeleteItemCommand => new Command(() =>
        {
            if (SelectedItem != null)
            {
                DeleteItem(SelectedItem);
                SelectedItem = null;
            }
        });
        
        public ICommand BackCommand => new Command(() =>
        {
            GoBackNav();
        });
        
        public ICommand SwipeShowItemCommand => new Command<TaskItem>((taskItem) =>
        {
            ShowTask _edit = new ShowTask(taskItem);
            var navigationPage = new NavigationPage(_edit);
            Application.Current.MainPage = navigationPage;
        });

        public void Search()
        {
            if (string.IsNullOrEmpty(SearchTitle))
            {
                Items = _db.TaskItems.ToList();
            }
            else
            {
                Items = _db.TaskItems.Where(x => x.Title.Contains(SearchTitle)).ToList();
            }
        }

        public void AddOrSaveItem()
        {
            TaskItem newItem = new TaskItem()
            {
                Title = CurrentTitle,
                Description = CurrentDescription,
                Priority = CurrentPriority
            };
            _db.TaskItems.Add(newItem);
            _db.SaveChanges();
            Items = _db.TaskItems.ToList();

            GoBackNav();
        }
        
        public void EditItem(TaskItem taskItem)
        {
            if (taskItem != null)
            {
                EditTaskPage _edit = new EditTaskPage(taskItem);
                var navigationPage = new NavigationPage(_edit);
            
                _db.TaskItems.Remove(SelectedItem);
                _db.SaveChanges();
                Items = _db.TaskItems.ToList();
                SelectedItem = null;
                Application.Current.MainPage = navigationPage;
            }
        }

        public void DeleteItem(TaskItem taskItem)
        {
            _db.TaskItems.Remove(taskItem);
            _db.SaveChanges();
            Items = _db.TaskItems.ToList();
        }

        public void GoBackNav()
        {
            MainPage _mainPage = new MainPage();
            var navigationPage = new NavigationPage(_mainPage);
            Application.Current.MainPage = navigationPage;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }