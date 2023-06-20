using System.Collections.ObjectModel;
using System.Formats.Tar;

namespace Görsel_Programlama_Ödev2.Views;

public partial class Yapilacaklar : ContentPage
{
    private ObservableCollection<TaskItem> tasks;

    public ObservableCollection<TaskItem> Tasks
    {
        get { return tasks; }
        set
        {
            tasks = value;
            OnPropertyChanged(nameof(Tasks));
        }
    }

    public Yapilacaklar()
    {
        InitializeComponent();
        BindingContext = this;

        Tasks = new ObservableCollection<TaskItem>();
    }

    private void AddTaskButton_Clicked(object sender, EventArgs e)
    {
        string taskName = TaskNameEntry.Text;
        string taskDescription = TaskDescriptionEntry.Text;
        DateTime taskDateTime = TaskDatePicker.Date + TaskTimePicker.Time;

        if (!string.IsNullOrWhiteSpace(taskName))
        {
            TaskItem task = new TaskItem
            {
                Name = taskName,
                Description = taskDescription,
                Deadline = taskDateTime.ToString()
            };

            Tasks.Add(task);

            TaskNameEntry.Text = string.Empty;
            TaskDescriptionEntry.Text = string.Empty;
            TaskDatePicker.Date = DateTime.Now;
            TaskTimePicker.Time = DateTime.Now.TimeOfDay;
        }
    }

    private void TaskListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        TaskItem selectedTask = e.SelectedItem as TaskItem;
        if (selectedTask != null)
        {
            Tasks.Remove(selectedTask);
        }
    }
}

public class TaskItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Deadline { get; set; }
}