using DryIoc;
using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class MsgViewModel : BindableBase, IDialogHostAware
    {
        private string title;
        private string content;

        public string DialogHostName { get; set; } = "Root";
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }


        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); }
        }

        public MsgViewModel()
        {
            CancelCommand=new DelegateCommand(Cancel);
            SaveCommand=new DelegateCommand(Save);
        }

        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters parameters = new DialogParameters();
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, parameters));
            }
        }

        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.Cancel));
            }
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Title"))
                Title=parameters.GetValue<string>("Title");
            if (parameters.ContainsKey("Content"))
                Content=parameters.GetValue<string>("Content");
        }
    }
}
