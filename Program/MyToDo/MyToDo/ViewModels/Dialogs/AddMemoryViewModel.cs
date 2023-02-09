using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Shared.Dto;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddMemoryViewModel : BindableBase, IDialogHostAware
    {
        public string DialogHostName { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        private MemoryDto currentDto;
        public MemoryDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }

        public AddMemoryViewModel()
        {
            CancelCommand=new DelegateCommand(Cancel);
            SaveCommand=new DelegateCommand(Save);
        }

        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters parameters = new DialogParameters();
                parameters.Add("Value", CurrentDto);
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
            var model = parameters.GetValue<MemoryDto>("Value");
            if (model == null)
                CurrentDto=new MemoryDto();
            else
                CurrentDto=model;
        }
    }
}
