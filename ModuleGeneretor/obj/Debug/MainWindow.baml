   M S B A M L   `   `   `  ����V  RRadyn.ModuleGeneretorWinApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ModuleGeneretorModuleGeneretor  5   	   ,    &Radyn.ModuleGeneretorWinApp.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ModuleGeneretor     5   	   -   5          � Title$  تولید ماژول��5   	   $	��700��6W    � WindowStartupLocation$ CenterScreen=�6e   .��5
      � -   5        $��gvmaster��5
       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   .+�5      �� $��تولید ماژول��6   $��3q�6-   .��5      � $��RightToLeft=�6    5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   .+�5      � $��0��6   $��2q�6)   $��Center=�64    � ColumnDefinitions 5      �� 5      $
��Auto �6/   �� 5      $
��Auto �6/    5      �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5       $
��Auto �6,   �� 5!      $
��Auto �6,   �� 5"      $
��Auto �6,   .+�5'      �� $��نوع��6    $��0��6.   $��1��6>   $��Right=�6K   $��2q�6g   $��Center=�6r   �� 5)      -   5        $��drpType��5*   P    �� DisplayMemberPath$ Value��5)   #   $��1��6=    �� 
IsReadOnly . $��2q�6\    �� 
IsEditable .  �� SelectedValuePath$	 Key��6z   $��1��5*      $
��Left=�6(   $��24��6D   $��Center=�6_   $	��400��6z   5,      � 5-      $��1��6   $��2q�6)   $��Center=�64    5.      �� 5/      $
��Auto �6,   �� 50      $
��Auto �6,   .+�52      �  53      �� 54   "   $
��Auto �63   �� 55   "   $
��Auto �63   �� 56   "   $
��Auto �63   �� 57   "   $
��Auto �63   �� 58   "   $
��Auto �63   �� 59   "   $
��Auto �63   .+�5;      �� -   5        $��btngenereratexml��5;   <   $��فایل xml نصب��6%   $��2��6T   $��2q�6d   $��Center=�6�   $��Center=�6�   $	��120��6�   $��25��6�   $��0��6�   �� 5<      -   5        $��btngeneratezip��5<   >   $��تولید فایل zip��6%   $��3��6T   $��2q�6d   $��Center=�6�   $	��Top=�6�   $	��120��6�   $��26��6�   $��0��6�   5>      5B      5D      5E      5F      � 5H   
   $��2��6   .+�5I      �� -   5        $��btnclose��5I   $   $��خروج��6   $��2q�65   $
��Left=�6Y   $��Center=�6u   $	��120��6�   $��25��6�   5J      5K      5L      