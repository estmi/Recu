   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Client.ViewClient.View  5   	   3clr-namespace:Client.ViewModelClient.ViewModel  5   	       Client.View.Detall   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Client.View     5   	   -vmclr-namespace:Client.ViewModel     5   	     � Title$  Detall��5	   	   $	��450��6   $	��800��6%    � DataContext 5
         Client.ViewModel.ViewModel  5   
   .��5      ]� .+�5   
   �� $��	Modificar��6   $��Bottom=�6R   ��6%   �� *ModificarRegistreCommand+6R   � 5       6   �� *CurrentRegister+ � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6   $��0��6!   $	��Any��61   $��Center=�6<   $��Center=�6W   �� 5    
   $��0��6   $��1��6   $��Center=�6D   $��Center=�6_   ��6/   �� *Any+6_   �� 5!   
   $��1��6   $��0��6!   $	��Mes��61   $��Center=�6<   $��Center=�6W   �� 5"   
   $��1��6   $��1��6   $��Center=�6D   $��Center=�6_   ��6/   �� *Mes+6_   �� 5#   
   $��2��6   $��0��6!   $��Codi districte��61   $��Center=�6G   $��Center=�6b   �� 5$   
   $��2��6   $��1��6   $��Center=�6N   $��Center=�6i   ��6/   �� *CodiDistricte+6i   �� 5%   
   $��3��6   $��0��6!   $��Nom districte��61   $��Center=�6F   $��Center=�6a   �� 5&   
   $��3��6   $��1��6   $��Center=�6M   $��Center=�6h   ��6/   �� *NomDistricte+6h   �� 5'   
   $��4��6   $��0��6!   $��
Codi barri��61   $��Center=�6C   $��Center=�6^   �� 5(   
   $��4��6   $��1��6   $��Center=�6J   $��Center=�6e   ��6/   �� *	CodiBarri+6e   �� 5)   
   $��5��6   $��0��6!   $��	Nom barri��61   $��Center=�6B   $��Center=�6]   �� 5*   
   $��5��6   $��1��6   $��Center=�6I   $��Center=�6d   ��6/   �� *
NomBarri+6d   �� 5+   
   $��6��6   $��0��6!   $
��Sexe��61   $��Center=�6=   $��Center=�6X   �� 5,   
   $��6��6   $��1��6    ��60   �� *	SexeArray+ �� SelectedValue 6R   �� *Sexe+�� 5-   
   $��7��6   $��0��6!   $��Demanda ocupacio��61   $��Center=�6I   $��Center=�6d   �� 5.   
   $��7��6   $��1��6    ��60   �� *DemandaOcupacioArray+ 6]   �� *DemandaOcupacio+�� 5/   
   $��8��6   $��0��6!   $��Nombre��61   $��Center=�6?   $��Center=�6Z   �� 50   
   $��8��6   $��1��6   $��Center=�6D   $��Center=�6_   ��6/   �� *Num+6_   51      52      53      