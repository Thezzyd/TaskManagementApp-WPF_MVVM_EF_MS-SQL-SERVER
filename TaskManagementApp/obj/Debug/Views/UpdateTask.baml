   M S B A M L   `   `   `  ����L  HTaskManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TaskManagementApp.ViewsTaskManagementApp.Views  5      S.clr-namespace:TaskManagementApp.CustomControls TaskManagementApp.CustomControls  5      U/clr-namespace:TaskManagementApp.ValueConverters!TaskManagementApp.ValueConverters  5      C&clr-namespace:TaskManagementApp.ModelsTaskManagementApp.Models  5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
      (    "TaskManagementApp.Views.UpdateTask   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:TaskManagementApp.Views     5      Icustomcontrols.clr-namespace:TaskManagementApp.CustomControls     5      KValueConverters/clr-namespace:TaskManagementApp.ValueConverters     5      9Models&clr-namespace:TaskManagementApp.Models     5	      9System&clr-namespace:System;assembly=mscorlib     5
      -   5        $	��370��5      $	��400��6     � 	Resources  5         
TaskStatus S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeName@   :TaskManagementApp.ValueConverters.EnumDescriptionConverter  EnumDescriptionConverter��%�   &	        &	 n     S� 5   
   $ 	GetValues��60   "  6G    5      M� 5      $ Models:TaskStatus��6   5        5   
   63   5      .��5      ��  �� CornerRadius$ 5��6   $��2q�5        LightBorders#��[ 5      ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$ 1,0.7&�63   .��5      �   BgColor1 � Color# [ 6   	 � Offset$	 0Q�6A   � 5         BgColor2# [ 6   $	 1Q�6A   5!      ._�5$   
   � 
 � RowDefinitions
 5%      �� 5&      $��30 �6    �� 5'      $��* �6    .+�5*      � $��0��6    � ColumnDefinitions 5+      �� 5,      $��* �6'   �� 5-      $��25 �6'   �� 5.      $��5 �6'   .+�51      �� $��	Edit Task��6     	LightText#��[ 52      $��Center=�53       �� Padding$ 	10,0,10,0q�54      7   1TaskManagementApp.CustomControls.CloseModalButton  56      $��0��63   $��1��6@      SomeCommand 57   3   �� *CloseCommand+58      �� 5:      $��1��6   $	��300��5;      $��	0,20,0,20q�5<      .+�5=      � 
 5>      �� 5?      $
��Auto �6(   �� 5@      $
��Auto �6(   �� 5A      $��10 �6(   �� 5B      $
��Auto �6(   �� 5C      $
��Auto �6(   �� 5D      $��10 �6(   �� 5E      $
��Auto �6(   �� 5F      $
��Auto �6(   �� 5G      $��10 �6(   �� 5H      $
��Auto �6(   �� 5I      $
��Auto �6(   �� 5J      $��10 �6(   �� 5K      $
��Auto �6(   �� 5L      $��10 �6(   �� 5M      $
��Auto �6(   �� 5N      $��* �6(   .+�5Q      �� $��0��6    $��Name *��5R       #��[ 5S       $��Center=�5T       �� 5W      $��1��5X      $��Center=�5Y      
������ #��[ 5[      #��[ 5\      $��0,0,0,2q�5]       �� 
CaretBrush# [ 5^      ��5W      �� *Name+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5^      �� 5a      $��3��6    $��Description��5b       #��[ 5c       $��Center=�5d       �� 5g      $��4��6n   $��Center=�5h       �� TextWrapping$
 Wrap=�5i       �� AcceptsReturn . $��80��5k      " �� VerticalScrollBarVisibility$
 Auto=�5l      ��5g      �� *Description+$ TwoWay=�$ PropertyChanged=�5l      � 5p      $��6��6    5q      �� 5r      $��* �6/   �� 5s      $��10 �6/   �� 5t      $��80 �6/   .+�5v      �� $��0��6$   $��Deadline��5w   $   #��[ 5x   $   $��Center=�5y   $   �� 5{      $��2��6$   $��
Priority *��5|   $   #��[ 5}   $   $��Center=�5~   $   5�      � 5�      $��7��6    5�      �� 5�      $
��Auto �6/   �� 5�      $��* �6/   �� 5�      $��10 �6/   �� 5�      $��80 �6/   .+�5�      �� -   5        $��3��5�   "   $��Center=�5�   "   #��[ 5�   "   
������ #��[ 5�   "   $��0,0,0,2q�5�   "   # [ 5�   "   ��5�   "   �� *
Priority+$ TwoWay=�$ PropertyChanged=�5�   "   �� 5�      $��0��5�   #   $��Center=�5�   #   $��0,0,5,0q�5�   #    d� 	IsChecked 5�   #   �� *
IsDeadline+$ TwoWay=�$ PropertyChanged=�5�   #   (  "System.Windows.Controls.DatePicker  5�      $��1��6%   {�5�   %   �� *
IsDeadline+$ TwoWay=�$ PropertyChanged=�   SelectedDate 5�   %   �� *
Deadline+$ TwoWay=�  5�      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value��%[   '        �� 5�   "   "  6(   .�5�   &   ��  �@��
IsReadOnly . 6C   5�   #   5�      5�      5�      � 5�      $��9��6    5�      �� 5�      $��* �6/   �� 5�      $��10 �6/   �� 5�      $��80 �6/   .+�5�      �� $��0��6$   $��Status *��5�   $   #��[ 5�   $   $��Center=�5�   $   �� 5�      $��2��6$   $��
IsHidden *��5�   $   #��[ 5�   $   $��Center=�5�   $   5�      � 5�      $��10��6    5�      �� 5�      $��* �6/   �� 5�      $��10 �6/   �� 5�      $��80 �6/   .+�5�      �� -   5        $��2��5�   #   $��Center=�5�   #   $��Center=�5�   #    5�   #   �� *
IsHidden+$ TwoWay=�$ PropertyChanged=�5�   #   �� 5�      $��20��5�   #   ��5�   #   ��  �� Source# [   �� SelectedItem 5�   #   �� *Status+��5�      �� 5�   "   .R�5�   &   �� ��60   ��  �� 	Converter# [ 5�   #   5�      5�      �� 5�      $��12��5�       $��10(�5�         ErrorMessageColor#��[ 5�        �� TextAlignment$ Center=�5�       $
��Wrap=�5�       ��5�       �� *ErrorMessage+5�       �� 5�      $��12��5�       $��10(�5�         SuccessMessageColor#��[ 5�       $ Center=�5�       $
��Wrap=�5�       ��5�       �� *SuccessMessage+5�       ;   5TaskManagementApp.CustomControls.CustomStandardButton  5�      $��14��6:      SomeContent$ Update task��5�   :      SomeCommand 5�   :   �� *UpdateTaskCommand+5�   :   5�      5�      5�      5�      5�      