using MyToDo.Common;
using MyToDo.Extensions;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MyToDo.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {
        private IRegionNavigationJournal journal;
        private ObservableCollection<Menu> menuList;
        private readonly IRegionManager regionManager;

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<Menu> MenuList
        {
            get { return menuList; }
            set { menuList = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<Menu> NavigateCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand LoginOutCommand { get; private set; }
        public MainViewModel(IRegionManager regionManager, IContainerProvider containerProvider)
        {
            MenuList=new ObservableCollection<Menu>();
            NavigateCommand=new DelegateCommand<Menu>(Navigate);
            this.regionManager=regionManager;
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal!=null && journal.CanGoForward)
                {
                    journal.GoForward();
                }
            });
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();
                }
            });
            LoginOutCommand=new DelegateCommand(() =>
            {
                App.LoginOut(containerProvider);
            });
        }

        private void Navigate(Menu obj)
        {
            if (obj == null)
                return;
            this.regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, callBack =>
            {
                this.journal=callBack.Context.NavigationService.Journal;
            });
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        public void InitMenus()
        {
            MenuList.Clear();
            MenuList.Add(new Menu { Title="首页", Icon="Home", NameSpace="IndexView" });
            MenuList.Add(new Menu { Title="待办事项", Icon="Book", NameSpace="TodoView" });
            MenuList.Add(new Menu { Title="备忘录", Icon="BookEditOutline", NameSpace="MemoryView" });
            MenuList.Add(new Menu { Title="设置", Icon="Cog", NameSpace="SettingsView" });
        }

        /// <summary>
        /// 初始化首页参数
        /// </summary>
        public void Configure()
        {
            UserName=AppSessionExtension.GetLoginUserName();
            InitMenus();
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
        }
    }
}
