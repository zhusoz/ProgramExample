using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MyToDo.ViewModels
{
    public class ThemeViewModel : BindableBase
    {
        private readonly PaletteHelper _paletteHelper = new();
        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;
        public DelegateCommand<object> ChangeHueCommand { get; private set; }

        public ThemeViewModel()
        {
            ChangeHueCommand=new DelegateCommand<object>(ChangeHue);
            //ITheme theme = _paletteHelper.GetTheme();
            //SelectedColor= theme.PrimaryMid.Color;

        }

        //private Color? _selectedColor;
        //public Color? SelectedColor
        //{
        //    get => _selectedColor;
        //    set
        //    {
        //        if (_selectedColor != value)
        //        {
        //            _selectedColor = value;
        //            RaisePropertyChanged();
        //        }
        //    }
        //}
        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }

        private void ChangeHue(object? obj)
        {
            var hue = (Color)obj!;
            ITheme theme = _paletteHelper.GetTheme();
            theme.PrimaryLight = new ColorPair(hue.Lighten());
            theme.PrimaryMid = new ColorPair(hue);
            theme.PrimaryDark = new ColorPair(hue.Darken());
            _paletteHelper.SetTheme(theme);

        }
    }
}
