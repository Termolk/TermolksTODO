using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoTask.Models;
using ToDoTask.ViewModel;

namespace ToDoTask.Views;

public partial class ShowTask : ContentPage
{
    private readonly TaskItem _item;
    public ShowTask(TaskItem item)
    {
        InitializeComponent();
        _item = item;
        BindingContext = new MainViewModel
        {
            CurrentTitle = item.Title,
            CurrentDescription = item.Description,
            CurrentPriority = item.Priority
        };
    }
}