using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dto
{
    public class SummaryDto : INotifyPropertyChanged
    {
        /// <summary>
        /// 汇总
        /// </summary>
		private int totalCount;

        /// <summary>
        /// 已完成数量
        /// </summary>
        private int hasFinishedCount;

        /// <summary>
        /// 完成比率
        /// </summary>
        private string completedRate;

        /// <summary>
        /// 备忘录数量
        /// </summary>
        private int memoryCount;

        /// <summary>
        /// 待办事项列表
        /// </summary>
        private ObservableCollection<ToDoDto> todoList;

        /// <summary>
        /// 备忘录列表
        /// </summary>
        private ObservableCollection<MemoryDto> memoryList;

        public int HasFinishedCount
        {
            get { return hasFinishedCount; }
            set { hasFinishedCount = value; OnPropertyChanged(); }
        }
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; OnPropertyChanged(); }
        }
        public string CompletedRate
        {
            get { return completedRate; }
            set { completedRate = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ToDoDto> TodoList
        {
            get { return todoList; }
            set { todoList = value; OnPropertyChanged(); }
        }
        public ObservableCollection<MemoryDto> MemoryList
        {
            get { return memoryList; }
            set { memoryList = value; OnPropertyChanged(); }
        }
        public int MemoryCount
        {
            get { return memoryCount; }
            set { memoryCount = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
