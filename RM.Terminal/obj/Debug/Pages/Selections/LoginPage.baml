   M S B A M L   `   `   `  ����F  BRM.Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RM.TerminalRM.Terminal  5      B >RM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null2&clr-namespace:RM.Core;assembly=RM.CoreRM.Core 5          RM.Terminal.LoginPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:RM.Terminal     5      7core&clr-namespace:RM.Core;assembly=RM.Core     5      -   5          RM.Core.BasePage`1   Name$
  This��5      $
��1920��5	      .��5      � $	��Top=�6   $
��Left=�6#    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   .+�5   
   ��    EmailTextBox#��[  5      $��0��5      $	��500��5      
 � Tag$ Your login here...��5      $��0,0,0,35q�5      ��5      �� *Email+5      G� 5   
   -   5         G�Name$ UserPassword��5      $��1��5      $	��500��5      $ Your password here...��5      $��0,0,0,35q�5       �� 5"   
   $��3��6     
SmallLabel#��[ 5#      $-��'*incorrect login or password, try again��5$        MainColorBrush#��[ 5%      $
��Left=�5&      $	��Top=�5'      y�5(      �� *IsDataWrong+�� 5*   
   $��3��6   $	��125��5+      $��35��5,      $��Right=�5-      $��Log in  ��5.        ArrowButton#��[ 51      ��5/      �� *LoginCommand+��50      ��  �� ElementName$
 This��51      52      53      