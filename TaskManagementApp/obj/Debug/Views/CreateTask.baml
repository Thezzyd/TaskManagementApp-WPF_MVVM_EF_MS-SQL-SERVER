   M S B A M L   `   `   `  ����L  HTaskManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TaskManagementApp.ViewsTaskManagementApp.Views  5      S.clr-namespace:TaskManagementApp.CustomControls TaskManagementApp.CustomControls  5      K*clr-namespace:TaskManagementApp.ViewModelsTaskManagementApp.ViewModels  5      (    "TaskManagementApp.Views.CreateTask   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:TaskManagementApp.Views     5      Icustomcontrols.clr-namespace:TaskManagementApp.CustomControls     5      @	viewModel*clr-namespace:TaskManagementApp.ViewModels     5      -   5        $	��330��5
      $	��400��6   
������ .��5      ��   �� CornerRadius$  5��6   $��2q�5         LightBorders#��[  5      ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$ 1,0.7&�63   .��5      �   BgColor1 � Color# [ 6    � Offset$ 0Q�6A   � 5        BgColor2# [ 6   $ 1Q�6A   5      ._�5   
   �  � RowDefinitions 5      �� 5      $��30 �6    �� 5      $��* �6    .+�5       � $��0��6    � ColumnDefinitions 5!      �� 5"      $��* �6'   �� 5#      $��25 �6'   �� 5$      $��5 �6'   .+�5'      �� $��Create New Task��6     	LightText#��[ 5(      $��Center=�5)       �� Padding$ 	10,0,10,0q�5*      7   1TaskManagementApp.CustomControls.CloseModalButton  5,      $��0��63   $��1��6@      SomeCommand 5-   3   �� *CloseCommand+5.      �� 50      $��1��6   $	��300��51      $��	0,20,0,20q�52      .+�53      �  54      �� 55      $
��Auto �6(   �� 56      $
��Auto �6(   �� 57      $��10 �6(   �� 58      $
��Auto �6(   �� 59      $
��Auto �6(   �� 5:      $��10 �6(   �� 5;      $
��Auto �6(   �� 5<      $
��Auto �6(   �� 5=      $��10 �6(   �� 5>      $
��Auto �6(   �� 5?      $��10 �6(   �� 5@      $
��Auto �6(   �� 5A      $��* �6(   .+�5D      �� $��0��6    $��Name *��5E       #��[ 5F       $��Center=�5G       �� 5J      $��1��5K      $��Center=�5L      
������ #��[  5N      #��[ 5O      $��0,0,0,2q�5P      	 �� 
CaretBrush#	 [ 5Q      ��5J      �� *Name+
 �� UpdateSourceTrigger$
 PropertyChanged=�5Q      �� 5T      $��3��6    $��Description��5U       #��[ 5V       $��Center=�5W       �� 5Z      $��4��6a   $��Center=�5[       �� TextWrapping$
 Wrap=�5\       �� AcceptsReturn . $��80��5^      " �� VerticalScrollBarVisibility$
 Auto=�5_      ��5Z      �� *Description+$
 PropertyChanged=�5_      � 5b      $��6��6    5c      �� 5d      $��* �6/   �� 5e      $��10 �6/   �� 5f      $��80 �6/   .+�5h      �� $��0��6$   $��Deadline��5i       #��[ 5j       $��Center=�5k       �� 5m      $��2��6$   $��Priority * ��5n       #��[ 5o       $��Center=�5p       5r      � 5s      $��7��6    5t      �� 5u      $
��Auto �6/   �� 5v      $��* �6/   �� 5w      $��10 �6/   �� 5x      $��10 �6/   �� 5y      $��80 �6/   .+�5|      �� -   5        $��3��5}   "   $��2��62   $��Center=�5~   "   
������ #��[  5�   "   #��[ 5�   "   $��0,0,0,2q�5�   "   #	 [ 5�   "   ��5|   "   �� *
Priority+$
 PropertyChanged=�5�   "   �� 5�      $��0��5�   #   $��Center=�5�   #   $��0,0,5,0q�5�   #    d� 	IsChecked 5�   #   �� *
IsDeadline+ �� Mode$ OneWayToSource=�5�   #   (  "System.Windows.Controls.DatePicker  5�      $��1��6S   {�6%   �� *
IsDeadline+$ TwoWay=�   SelectedDate 5�   %   �� *
Deadline+ � 	Resources 5�      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value��%[   '        �� 5�   "   "  6(   .�5�   &   ��  �@��
IsReadOnly . 6C   5�   #   5�      5�      5�      �� 5�      $��9��5�       $��10(�5�         ErrorMessageColor#��[ 5�        �� TextAlignment$ Center=�5�       $
��Wrap=�5�       ��5�       �� *ErrorMessage+5�       �� 5�      $��9��5�       $��10(�5�         SuccessMessageColor#��[ 5�       $ Center=�5�       $
��Wrap=�5�       ��5�       �� *SuccessMessage+5�       �� 5�      $��11��5�      $��Create Task��5�      $	��100��5�      ��5�      �� *CreateTaskCommand+5�      5�      5�      5�      5�      5�      