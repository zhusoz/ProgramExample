using MyToDo.Common;
using MyToDo.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyToDo.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public SettingsViewModel(IRegionManager regionManager)
        {
            CreateTestData();
            this.regionManager=regionManager;
            NavigateCommand=new DelegateCommand<SettingDto>(Navigate);
            
        }
        

        private void Navigate(SettingDto obj)
        {
            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.Target);
        }

        private ObservableCollection<SettingDto> settingsList;
        private readonly IRegionManager regionManager;
        public ICommand NavigateCommand { get; private set; }
        

        public ObservableCollection<SettingDto> SettingsList
        {
            get { return settingsList; }
            set { settingsList = value; RaisePropertyChanged(); }
        }


        private void CreateTestData()
        {
            ObservableCollection<SettingDto> list = new ObservableCollection<SettingDto>();
            list.Add(new SettingDto { Icon="Palette", Target="ThemeView", Title="个性化" });
            list.Add(new SettingDto { Icon="Cog", Target="SystemSettingsView", Title = "系统设置" });
            list.Add(new SettingDto { Icon="InformationOutline", Target="AboutView", Title="关于更多" });
            SettingsList= list;
        }
    }
}
