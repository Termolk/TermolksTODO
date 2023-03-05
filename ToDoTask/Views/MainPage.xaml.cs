using ToDoTask.Views;

namespace ToDoTask;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }
    
    private void BtnClkAddTask(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddTaskPage());
    }
}