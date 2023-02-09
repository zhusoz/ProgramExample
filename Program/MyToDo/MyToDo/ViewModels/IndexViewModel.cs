
using MaterialDesignColors;
using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Dto;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private SummaryDto summary;
        private ObservableCollection<TaskBar> taskBarList;
        private readonly IDialogHostService dialogHostService;
        private readonly IRegionManager regionManager;
        private readonly IToDoService toDoService;
        private readonly IMemoryService memoryService;
        private readonly IEventAggregator aggregator;
        private string title;

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> EditToDoCommand { get; private set; }
        public DelegateCommand<MemoryDto> EditMemoryCommand { get; private set; }
        public DelegateCommand<ToDoDto> ToDoCompletedCommand { get; private set; }
        public DelegateCommand<TaskBar> NavigateCommand { get; private set; }
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }
        public IndexViewModel(IContainerProvider provider, IDialogHostService dialogHostService) : base(provider)
        {
            
            NavigateCommand=new DelegateCommand<TaskBar>(Navigate);
            EditToDoCommand=new DelegateCommand<ToDoDto>(AddToDo);
            EditMemoryCommand=new DelegateCommand<MemoryDto>(AddMemory);
            ToDoCompletedCommand=new DelegateCommand<ToDoDto>(ToDoCompleted);
            ExecuteCommand=new DelegateCommand<string>(Execute);
            this.dialogHostService=dialogHostService;
            regionManager=provider.Resolve<IRegionManager>();
            toDoService=provider.Resolve<IToDoService>();
            memoryService=provider.Resolve<IMemoryService>();
            aggregator=provider.Resolve<IEventAggregator>();
        }

        private void Navigate(TaskBar obj)
        {
            if (obj==null || obj.Target==null)
                return;
            NavigationParameters parameters = new NavigationParameters();
            if (obj.Title.Equals("已完成"))
                parameters.Add("Value", 2);
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.Target, parameters);
        }


        private async void ToDoCompleted(ToDoDto obj)
        {
            UpdateLoading(true);
            var result = await toDoService.UpdateAsync(obj);
            if (result.Success)
            {
                var todo = Summary.TodoList.FirstOrDefault(e => e.Id==obj.Id);
                if (todo!=null)
                    Summary.TodoList.Remove(todo);
                Summary.HasFinishedCount+=1;
                Summary.CompletedRate=((double)Summary.HasFinishedCount/Summary.TotalCount).ToString("0%");
                Refresh();
            }
            UpdateLoading(false);
            aggregator.SendMessage("已完成.");

        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "AddToDo": AddToDo(null); break;
                case "AddMemory": AddMemory(null); break;
                default: break;
            }
        }

        private async void AddMemory(MemoryDto dto)
        {
            DialogParameters parameters = new DialogParameters();
            if (dto!=null)
                parameters.Add("Value", dto);

            IDialogResult dialogResult = await dialogHostService.ShowDialog("AddMemoryView", parameters);
            if (dialogResult.Result==ButtonResult.OK)
            {
                var model = dialogResult.Parameters.GetValue<MemoryDto>("Value");
                if (model.Id>0)
                {
                    //编辑操作
                    var updateResult = await memoryService.UpdateAsync(model);
                    if (updateResult.Success)
                    {
                        var memory = Summary.MemoryList.FirstOrDefault(e => e.Id==model.Id);
                        if (memory!=null)
                        {
                            memory.Title=updateResult.Data.Title;
                            memory.Content=updateResult.Data.Content;
                        }

                    }
                }
                else
                {
                    //新增操作
                    var addResult = await memoryService.AddAsync(model);
                    if (addResult.Success)
                    {
                        Summary.MemoryCount+=1;
                        Summary.MemoryList.Add(model);
                        Refresh();
                    }
                }
            }
        }

        private async void AddToDo(ToDoDto dto)
        {
            DialogParameters parameters = new DialogParameters();
            if (dto!=null)
                parameters.Add("Value", dto);

            IDialogResult dialogResult = await dialogHostService.ShowDialog("AddToDoView", parameters);
            if (dialogResult.Result==ButtonResult.OK)
            {
                var model = dialogResult.Parameters.GetValue<ToDoDto>("Value");
                if (model.Id>0)
                {
                    //编辑操作
                    var updateResult = await toDoService.UpdateAsync(model);
                    if (updateResult.Success)
                    {
                        var todo = Summary.TodoList.FirstOrDefault(e => e.Id==model.Id);
                        if (todo!=null)
                        {
                            todo.Title=updateResult.Data.Title;
                            todo.Content=updateResult.Data.Content;
                            todo.Status=updateResult.Data.Status;
                            aggregator.SendMessage("已修改待办事项.");
                        }
                        
                    }
                }
                else
                {
                    //新增操作
                    var addResult = await toDoService.AddAsync(model);
                    if (addResult.Success)
                    {
                        Summary.TotalCount+=1;
                        Summary.CompletedRate=((double)Summary.HasFinishedCount/Summary.TotalCount).ToString("0%");
                        Summary.TodoList.Add(model);
                        Refresh();
                        aggregator.SendMessage("已新增待办事项.");
                    }

                }
            }
        }

        public SummaryDto Summary
        {
            get { return summary; }
            set { summary=value; RaisePropertyChanged(); }
        }
        public ObservableCollection<TaskBar> TaskBarList
        {
            get { return taskBarList; }
            set { taskBarList = value; RaisePropertyChanged(); }
        }


        private void InitTaskList()
        {
            TaskBarList=new();
            TaskBarList.Add(new TaskBar { Title="汇总", Color="#0097ff", Icon="ClockFast", Target="TodoView" });
            TaskBarList.Add(new TaskBar { Title="已完成", Color="#10b138", Icon="ClockCheckOutline", Target="TodoView" });
            TaskBarList.Add(new TaskBar { Title="完成比例", Color="#00b3df", Icon="ChartLineVariant", Target="" });
            TaskBarList.Add(new TaskBar { Title="备忘录", Color="#ffa000", Icon="PlaylistStar", Target="MemoryView" });
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void Refresh()
        {
            TaskBarList[0].Val=Summary.TotalCount.ToString();
            TaskBarList[1].Val=Summary.HasFinishedCount.ToString();
            TaskBarList[2].Val=Summary.CompletedRate;
            TaskBarList[3].Val=Summary.MemoryCount.ToString();
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            Title=$"你好,{AppSessionExtension.GetLoginUserName()} {DateTime.Now.GetDateTimeFormats('D')[1]}";
            InitTaskList();
            var summaryResult = await toDoService.SummaryAsync();
            if (summaryResult.Success)
            {
                Summary=summaryResult.Data;
                Refresh();
            }
            base.OnNavigatedTo(navigationContext);
        }

    }
}
