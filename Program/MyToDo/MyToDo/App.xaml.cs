using DryIoc;
using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.ViewModels;
using MyToDo.ViewModels.Dialogs;
using MyToDo.Views;
using MyToDo.Views.Dialogs;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        public static void LoginOut(IContainerProvider containerProvider)
        {
            App.Current.MainWindow.Hide();
            AppSessionExtension.RemoveLoginUserName();
            var dialogService = containerProvider.Resolve<IDialogService>();
            dialogService.ShowDialog("LoginView", (callback) =>
            {
                if (callback.Result==ButtonResult.OK)
                {
                    var service = App.Current.MainWindow.DataContext as IConfigureService;
                    if (service!=null)
                        service.Configure();
                    App.Current.MainWindow.Show();
                }
                else
                {
                    App.Current.Shutdown();
                }
            });
        }

        protected override void OnInitialized()
        {
            var dialogService = Container.Resolve<IDialogService>();
            dialogService.ShowDialog("LoginView", (callback) =>
            {
                if (callback.Result==ButtonResult.OK)
                {
                    var service = App.Current.MainWindow.DataContext as IConfigureService;
                    if (service!=null)
                        service.Configure();
                    base.OnInitialized();
                }
                else
                {
                    App.Current.Shutdown();
                }
            });

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 依赖注入...
            var container = containerRegistry.GetContainer();
            container.Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            container.RegisterInstance<string>("http://localhost:12345/", serviceKey: "webUrl");
            container.Register<ILoginService, LoginService>();
            container.Register<IToDoService, ToDoService>();
            container.Register<IMemoryService, MemoryService>();
            container.Register<IDialogHostService, DialogHostService>();

            // 注册弹窗
            //containerRegistry.RegisterDialog<AddToDoView, AddToDoViewModel>();
            //containerRegistry.RegisterDialog<AddMemoryView, AddMemoryViewModel>();
            containerRegistry.RegisterForNavigation<AddToDoView, AddToDoViewModel>();
            containerRegistry.RegisterForNavigation<AddMemoryView, AddMemoryViewModel>();
            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();

            //注册各个模块页面
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<TodoView, TodoViewModel>();
            containerRegistry.RegisterForNavigation<MemoryView, MemoryViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();


            //注册设置模块下的子模块
            containerRegistry.RegisterForNavigation<ThemeView, ThemeViewModel>();
            containerRegistry.RegisterForNavigation<SystemSettingsView, SystemSettingsViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();

        }
    }
}
