﻿using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDo.Common
{
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension containerExtension;

        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension=containerExtension;
        }

        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (parameters == null)
                parameters=new DialogParameters();

            // 从容器中取出当前弹出窗的实例
            var content = containerExtension.Resolve<object>(name);
            
            //验证实例有效性
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");
            
            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogHostService interface");

            viewModel.DialogHostName=dialogHostName;
            DialogOpenedEventHandler eventHandler = (sender, args) =>
            {
                if (viewModel is IDialogHostAware aware)
                {
                    aware.OnDialogOpened(parameters);
                    args.Session.UpdateContent(content);
                }
            };
            return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);

        }
    }
}
