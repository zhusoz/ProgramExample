
using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Dto;
using Prism.Commands;
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
    public class MemoryViewModel : NavigationViewModel
    {
        private readonly IMemoryService _memoryService;
        private readonly IDialogHostService dialogHost;
        private string search;
        private MemoryDto currentDto;
        private ObservableCollection<MemoryDto> memoryList = new ObservableCollection<MemoryDto>();
        private bool isRightDrawerOpened;
        private int selectedIndex;
        

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<MemoryDto> SelectedCommand { get; private set; }
        public DelegateCommand<MemoryDto> DeleteCommand { get; private set; }


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

        public MemoryDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 待办列表
        /// </summary>
        public ObservableCollection<MemoryDto> MemoryList
        {
            get { return memoryList; }
            set { memoryList = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 右侧菜单栏是否打开
        /// </summary>
        public bool IsRightDrawerOpened
        {
            get { return isRightDrawerOpened; }
            set { isRightDrawerOpened = value; RaisePropertyChanged(); }
        }

        public MemoryViewModel(IMemoryService memoryService, IContainerProvider provider) : base(provider)
        {
            _memoryService = memoryService;
            dialogHost=provider.Resolve<IDialogHostService>();
            ExecuteCommand =new DelegateCommand<string>(Execute);
            SelectedCommand=new DelegateCommand<MemoryDto>(Selected);
            DeleteCommand=new DelegateCommand<MemoryDto>(Delete);
        }

        private async void Delete(MemoryDto obj)
        {
            IDialogResult dialogResult = await dialogHost.Question("温馨提示", $"真的要删除id:{obj.Id}的备忘录吗?");
            if (dialogResult.Result!=ButtonResult.OK)
                return;

            UpdateLoading(true);
            var result = await _memoryService.DeleteAsync(obj.Id);
            if (result.Success)
            {
                var model = MemoryList.FirstOrDefault(e => e.Id==obj.Id);
                if (model!=null)
                {
                    MemoryList.Remove(obj);
                }
            }
            UpdateLoading(false);
        }


        /// <summary>
        /// 选中代办
        /// </summary>
        /// <param name="obj"></param>
        private async void Selected(MemoryDto obj)
        {
            UpdateLoading(true);
            var result = await this._memoryService.GetFirstOrDefaultByIdAsync(obj.Id);
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
                var apiResponse = await _memoryService.UpdateAsync(CurrentDto);
                if (apiResponse.Success)
                {
                    var todo = this.MemoryList.FirstOrDefault(e => e.Id==CurrentDto.Id);
                    if (todo!=null)
                    {
                        todo.Title= CurrentDto.Title;
                        todo.Content= CurrentDto.Content;
                    }
                    IsRightDrawerOpened=false;
                }
            }
            else
            {
                //新增操作
                var response = await _memoryService.AddAsync(CurrentDto);
                if (response.Success)
                {
                    MemoryList.Add(response.Data);
                    IsRightDrawerOpened=false;
                }
            }
            UpdateLoading(false);
        }

        private void Add()
        {
            CurrentDto=new MemoryDto();
            IsRightDrawerOpened= true;
        }



        private async Task GetDataListAsync()
        {
            UpdateLoading(true);
            var response = await _memoryService.GetAllMemoryAsync(new Shared.Utils.MemoryQueryParameter { PageIndex=0, PageSize=100, Keyword=Search});
            if (response.Success)
            {
                this.MemoryList.Clear();
                foreach (var item in response.Data.Items)
                {
                    this.MemoryList.Add(item);
                }
            }
            UpdateLoading(false);
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataListAsync();
        }

    }
}
