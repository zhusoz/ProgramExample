   M S B A M L   `   `   `  ???? A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyToDo.ExtensionsMyToDo.Extensions  clr-namespace:MyToDoMyToDo      MyToDo.Views.MainView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     /extclr-namespace:MyToDo.Extensions     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors   localclr-namespace:MyToDo  _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T	 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 $
??1280??$	??768??! 	 Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True ? AllowsTransparency$
 True??   MaterialDesignPaper#???   $??微软雅黑)?$??13(?$??Regular"?  MaterialDesignBody#???  &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?   TextRenderingMode$
 Auto=? ? WindowStartupLocation$ CenterScreen=? ? WindowStyle$
 None=?.??)  #MaterialDesignThemes.Wpf.DialogHost  -     Name$ 
DialogHost??   DialogTheme$ Inherit=?   
Identifier$
 Root??.??)  #MaterialDesignThemes.Wpf.DrawerHost  -   	  Name$	 
drawerHost??
   IsLeftDrawerOpen
 ??  ?? ElementName$ MenuToggleButton?? ?? Path$ 	IsChecked?   LeftDrawerContent ]? $	??220??.+??? $
??0,20q?$	??Top=?.+??? $??50??$??50??$??../Images/tx.jpeg?? J? Clip @?  @? Center$ 25,25&? @? RadiusX$ 25Q? @? RadiusY$ 25Q??? $??Center=????? *
UserName+?? -    ??Name$ menuListBox??  MyListBoxItemStyle#??[ ???? *
MenuList+ ?? Mode$ TwoWay=? ?? UpdateSourceTrigger$ PropertyChanged=?*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged??*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command ?? *NavigateCommand+   CommandParameter ?? $ SelectedItem? ?? RelativeSource ??  ?? AncestorType ListBox ?? Mode$ FindAncestor=????? .R??? $
??10,0q?
??????  ?? Orientation$ 
Horizontal=?.+?'	  !MaterialDesignThemes.Wpf.PackIcon	   	  Kind ?? *Icon+?? $
??10,0q????? *Title+.???  ? RowDefinitions ?? $
??auto ??? .+?'
  !MaterialDesignThemes.Wpf.Snackbar
  -     
 Name$  snackbar??! K? ZIndex$! 1??" 
  MessageQueue" 4  .MaterialDesignThemes.Wpf.MessageQueueExtension  (  "MaterialDesignThemes.Wpf.ColorZone  -   #  Name$# 	ColorZone??$??16q?.  (MaterialDesignThemes.Wpf.ElevationAssist$   	Elevation$	$ Dp4=?%   Mode$% 
PrimaryMid=?.??]? .+??? $ 
Horizontal=?.+?d? -   & d?Name$& MenuToggleButton??4  .System.Windows.Automation.AutomationProperties'   Name$' HamburgerToggleButton??( d? 	IsChecked$( FalseY? ' #MaterialDesignHamburgerToggleButton#??[ ?? $??24,0,0,0q?  MaterialDesignToolButton#??[ ) ? ToolTip$) Previous Item??+  %MaterialDesignThemes.Wpf.RippleAssist*   Feedback* ??  ?? *Self+$ 
Foreground?  BrushRoundConverter+ ?? 	Converter#+ [ ???? *GoBackCommand+??0  *MaterialDesignThemes.Wpf.PackIconExtension  ,   Kind$, 	ArrowLeft=?-   Size- 24????  ?? " ?$ (TextElement.Foreground)??? $??16,0,0,0q?#??[ $) 	Next Item??* ??  ?? *Self+$ 
Foreground?#+ [ ???? *GoForwardCommand+??  $, 
ArrowRight=?- 24????  ?? " ?$ (TextElement.Foreground)??? $??Right=?$ 
Horizontal=?.+?'  !MaterialDesignThemes.Wpf.PopupBox  .   ToggleContent. ?? $??25??$??25??$??../Images/tx.jpeg?? @? $ 	12.5,12.5&?$
 12.5Q?$
 12.5Q?/   PopupContent./ ?? .+??? $??注销?????? *LoginOutCommand+?? -   0 ??Name$0 btnMin??$	??—?? " MaterialDesignFlatAccentButton#??[ ?? -   $0 btnMax??$	??☐??#??[ ?? -	   $0 btnClose??$	??✕??#??[ ?? $
??Left=?$??Center=?$%' Material Design In XAML Toolkit??$??22(?$??	笔记本???? $??1??$   MyToDo.Extensions.PrismManager1   MainViewRegionName! 	 Prism.Regions.RegionManager2   
RegionName#2 Z1 