   M S B A M L   `   `   `  ????A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MyToDo.Views.DialogsMyToDo.Views.Dialogs  5      &     MyToDo.Views.Dialogs.AddToDoView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:MyToDo.Views.Dialogs     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      $	??450??5	      $	??280??5
      .??5      ?   ? RowDefinitions  5   
   ?? 5      $
??auto ?6   ?? 5      ?? 5      $
??auto ?6   .+?5   
   ??  ?? Padding$ 20,10q?5      $??20(?5      $??添加到待办??5      ]? 5   
   $??1??5      $
??20,0q?5      $??False??5      .+?5      ?? $	??Top=?6    ?? Orientation$ 
Horizontal=?6.   .+?5      ?? $??Center=?6   $??状态:??67   ?? 5      $
??10,0q?5       ?? SelectedIndex 5       ?? *CurrentDto.Status+.@?5!      ?? $??待办??6#   ?? 5"      $??已办??6#   5#      5$      ?? 5%      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入标题??5&      $	??Top=?5'      ??5(      ?? *CurrentDto.Title+?? 5)      $	??100??5*      $ 请输入内容??5+       ?? AcceptsReturn$
 True??5,      $	??Top=?5-       ?? TextWrapping$
 Wrap=?5/      ??5.      ?? *CurrentDto.Content+5/      50      ?? 51   
   $??2??52      $??10q?53      $??Right=?54      $??Center=?55      $ 
Horizontal=?56      .+?57      ?? $??0,0,10,0q?58      $??取消??5:          MaterialDesignOutlinedButton#??[  5;      ??59      ?? *CancelCommand+5;      ?? 5<      $??确定??65   ??6   ?? *SaveCommand+65   5=      5>      5?      