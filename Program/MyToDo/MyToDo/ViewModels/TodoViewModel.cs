
using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class TodoViewModel : NavigationViewModel
    {
        private readonly IToDoService toDoService;
        private string search;
        private ToDoDto currentDto;
        private ObservableCollection<ToDoDto> todoList = new ObservableCollection<ToDoDto>();
        private bool isRightDrawerOpened;
        private int selectedIndex;
        private readonly IDialogHostService dialogHost;

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }
        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }


        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        public ToDoDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 待办列表
        /// </summary>
        public ObservableCollection<ToDoDto> TodoList
        {
            get { return todoList; }
            set { todoList = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 右侧菜单栏是否打开
        /// </summary>
        public bool IsRightDrawerOpened
        {
            get { return isRightDrawerOpened; }
            set { isRightDrawerOpened = value; RaisePropertyChanged(); }
        }

        public TodoViewModel(IToDoService toDoService, IContainerProvider provider) : base(provider)
        {
            this.toDoService=toDoService;
            dialogHost=provider.Resolve<IDialogHostService>();
            ExecuteCommand=new DelegateCommand<string>(Execute);
            SelectedCommand=new DelegateCommand<ToDoDto>(Selected);
            DeleteCommand=new DelegateCommand<ToDoDto>(Delete);
        }

        private async void Delete(ToDoDto obj)
        {
            var dialogResult = await dialogHost.Question("温馨提示", $"确定要删除Id:{obj.Id}吗?");
            if (dialogResult.Result!=ButtonResult.OK) return;

            UpdateLoading(true);
            var result = await toDoService.DeleteAsync(obj.Id);
            if (result.Success)
            {
                var model = TodoList.FirstOrDefault(e => e.Id==obj.Id);
                if (model!=null)
                {
                    TodoList.Remove(obj);
                }
            }
            UpdateLoading(false);
        }


        /// <summary>
        /// 选中代办
        /// </summary>
        /// <param name="obj"></param>
        private async void Selected(ToDoDto obj)
        {
            UpdateLoading(true);
            var result = await this.toDoService.GetFirstOrDefaultByIdAsync(obj.Id);
            if (result.Success)
            {
                CurrentDto=result.Data;
                IsRightDrawerOpened=true;
            }
            UpdateLoading(false);

        }

        private void Execute(string method)
        {
            switch (method)
            {
                case "Add": Add(); break;
                case "Query": GetDataListAsync(); break;
                case "Save": Save(); break;
                default: break;
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(CurrentDto.Title)||string.IsNullOrEmpty(CurrentDto.Content))
                return;

            UpdateLoading(true);
            if (CurrentDto.Id>0)
            {
                //编辑操作
                var apiResponse = await toDoService.UpdateAsync(CurrentDto);
                if (apiResponse.Success)
                {
                    var todo = this.TodoList.FirstOrDefault(e => e.Id==CurrentDto.Id);
                    if (todo!=null)
                    {
                        todo.Title= CurrentDto.Title;
                        todo.Content= CurrentDto.Content;
                        todo.Status= CurrentDto.Status;
                    }
                    IsRightDrawerOpened=false;
                }
            }
            else
            {
                //新增操作
                var response = await toDoService.AddAsync(CurrentDto);
                if (response.Success)
                {
                    TodoList.Add(response.Data);
                    IsRightDrawerOpened=false;
                }
            }
            UpdateLoading(false);
        }

        private void Add()
        {
            CurrentDto=new ToDoDto();
            IsRightDrawerOpened= true;
        }


        private async Task GetDataListAsync()
        {
            UpdateLoading(true);
            var response = await this.toDoService.GetAllToDoAsync(new Shared.Utils.ToDoQueryParameter { PageIndex=0, PageSize=100, Keyword=Search, Status=SelectedIndex==0 ? null : SelectedIndex - 1 });
            if (response.Success)
            {
                this.TodoList.Clear();
                foreach (var item in response.Data.Items)
                {
                    this.TodoList.Add(item);
                }
            }
            UpdateLoading(false);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Value"))
            {
                SelectedIndex = navigationContext.Parameters.GetValue<int>("Value");
            }
            else
            {
                SelectedIndex = 0;
            }
            GetDataListAsync();
            base.OnNavigatedTo(navigationContext);

        }
    }
}
