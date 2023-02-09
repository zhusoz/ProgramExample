using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Extensions;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyToDo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IEventAggregator aggregator, IDialogHostService dialogHost)
        {
            InitializeComponent();

            aggregator.Register(e =>
            {
                DialogHost.IsOpen = e.IsOpen;
                if (DialogHost.IsOpen)
                    DialogHost.DialogContent=new ProgressView();
            });
            aggregator.RegisterMessage(e =>
            {
                snackbar.MessageQueue?.Enqueue(e.Message);
            });

            this.btnMin.Click+=(o, e) => this.WindowState=WindowState.Minimized;
            this.btnMax.Click+=FixWindow;
            this.btnClose.Click+=async (o, e) =>
            {
                IDialogResult dialogResult = await dialogHost.Question("Tip", "So,will you be like them,abandon me?");
                if (dialogResult.Result!=ButtonResult.OK)
                    return;
                Close();
            };
            this.ColorZone.MouseDown += (o, e) =>
            {
                if (e.LeftButton==MouseButtonState.Pressed)
                {
                    DragMove();
                }
            };
            this.ColorZone.MouseDoubleClick += FixWindow;
            menuListBox.SelectionChanged+=(o, e) => drawerHost.IsLeftDrawerOpen = false;
        }


        private void FixWindow(object o, RoutedEventArgs e)
        {
            if (this.WindowState==WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState= WindowState.Maximized;
            }
        }
    }
}
