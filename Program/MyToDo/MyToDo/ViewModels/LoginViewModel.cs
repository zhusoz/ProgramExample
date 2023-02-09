using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Dto;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private string account;
        private string password;
        private readonly ILoginService loginService;
        private readonly IEventAggregator aggregator;
        private int selectedIndex;
        private RegisterDto registerUser;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }
        public RegisterDto RegisterUser
        {
            get { return registerUser; }
            set { registerUser = value; RaisePropertyChanged(); }
        }
        public string Title => "ToDo";
        public DelegateCommand<string> ExecuteCommand { get; set; }

        public LoginViewModel(ILoginService loginService, IEventAggregator aggregator)
        {
            ExecuteCommand=new DelegateCommand<string>(Execute);
            this.loginService=loginService;
            this.aggregator=aggregator;
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login": Login(); break;
                case "GoLogin": SelectedIndex=0; break;
                case "GoRegister": SelectedIndex=1; RegisterUser=new RegisterDto(); break;
                case "Register": Register(); break;
            }
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        private async void Register()
        {
            if (string.IsNullOrEmpty(RegisterUser.UserName)||string.IsNullOrEmpty(RegisterUser.Account)||string.IsNullOrEmpty(RegisterUser.Password)||string.IsNullOrEmpty(RegisterUser.NewPassword))
                return;
            if (RegisterUser.Password!=RegisterUser.NewPassword)
                return;
            var registerResult = await loginService.RegisterAsync(new UserDto { Password=RegisterUser.Password, Account=RegisterUser.Account, UserName=RegisterUser.UserName });
            if (registerResult.Success)
            {
                // 注册成功提示消息
                aggregator.SendMessage("注册成功", "Login");
                SelectedIndex=0;
            }
            else
            {
                // 失败提示消息
                aggregator.SendMessage($"注册失败,{registerResult.Message}", "Login");
            }

        }

        /// <summary>
        /// 登录
        /// </summary>
        private async void Login()
        {
            if (string.IsNullOrEmpty(Account)||string.IsNullOrEmpty(Password))
                return;
            var loginResult = await loginService.LoginAsync(new UserDto
            {
                Account = Account,
                Password = Password
            });
            if (loginResult.Success)
            {
                AppSessionExtension.SetLoginUserName(loginResult.Data.UserName);
                aggregator.SendMessage("登录成功", "Login");
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));

            }
            // 失败通知
            aggregator.SendMessage($"登录失败,{loginResult.Message}", "Login");
        }


        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }



        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }



    }
}
