   M S B A M L   `   `   `  ???? A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyToDo.ConvertersMyToDo.Converters  +clr-namespace:MyToDo.ViewsMyToDo.Views      MyToDo.Views.MemoryView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     .cvclr-namespace:MyToDo.Converters     @d2http://schemas.microsoft.com/expression/blend/2008     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  &localclr-namespace:MyToDo.Views  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes    ? 	Resources  -   'MyToDo.Converters.IntToVisibleConverter   IntToVisibleConverter??%   &	          .??)  #MaterialDesignThemes.Wpf.DialogHost  .??)  #MaterialDesignThemes.Wpf.DrawerHost     IsRightDrawerOpen ?? *IsRightDrawerOpened+   RightDrawerContent ]? $	??220??$??20q?$??False??.+??? $	??Top=?$??22(?$??添加备忘录???? )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入备忘录概要??$	??Top=????? *CurrentDto.Title+?? $
??0,10q?$ 请输入备忘录内容??$	??Top=? ?? MinLines$ 4?????? *CurrentDto.Content+?? $
??Save??$??添加到备忘录??$	??Top=????? *ExecuteCommand+.???  ? RowDefinitions ?? $
??auto ??? .+??? $??Stretch=? ?? Orientation$ 
Horizontal=?.+??? $??15q?$
??Left=?$??Center=?$ 查找备忘录...??.  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton$
 True?????? *Search+ ?? Mode$ TwoWay=?	 ?? UpdateSourceTrigger$	 PropertyChanged=?.??
 J? InputBindings
 ?? 
 ?? Key$ Enter?? ?? CommandParameter$ Query?? ?? Command ?? *ExecuteCommand+.???? $
??10,5q?$??Right=?$??Center=?$	??Add??$?? + 添加备忘录?????? *ExecuteCommand+?? $??1??$??Center=?$??Center=?y??? *MemoryList.Count+ ?? 	Converter# [  .+??? $	??120??$	??120??$??/Images/nodata.png???? $
??0,10q?$??22(?$B??<尝试添加一些备忘录，以便在此处查看它们...???? $??1??.???? $??Center=????? *
MemoryList+???? .R?? ???? .R??  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect D  >MaterialDesignThemes.Wpf.Transitions.TransitionEffectExtension     Kind$ ExpandIn=?.??? $	??200??$	??180??$	??220??$??10q? ?? $
??auto ??? *	  $Microsoft.Xaml.Behaviors.Interaction 	  Triggers +
  %Microsoft.Xaml.Behaviors.EventTrigger
   
  	EventName$ MouseLeftButtonUp??*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command ?? *DataContext.SelectedCommand+ ?? RelativeSource ??  ?? AncestorType ItemsControl ?? Mode$ FindAncestor=?   CommandParameter ?? .+??? $??2??
????? ? ?? CornerRadius$ 2????  ?? Padding$
 10,5q????? *Title+?? $??1??$
 10,5q????? *	Content+'  !MaterialDesignThemes.Wpf.PopupBox  $??Right=?   PopupContent. ?? $??删除?????? *DataContext.DeleteCommand+ ??  ItemsControl$ FindAncestor=????? ?? $??2??$
}?True??.+?A? 
 ?? Top$ 20?? ?? Right$	 -80??$	??120??$	??120??
??????? J? Opacity$	 0.1Q?A? $ 90??$	 -20??$	??120??$	??120??
???????$	 0.2Q?