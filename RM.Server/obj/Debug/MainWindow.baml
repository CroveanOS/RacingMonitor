   M S B A M L   `   `   `  ����D  @RM.Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:RM.Server.ControlsRM.Server.Controls  5   	   %clr-namespace:RM.Server	RM.Server  5   	       RM.Server.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7
properties clr-namespace:RM.Server.Controls     5   	   )localclr-namespace:RM.Server     5   	   -   5          � Title$  Racing Monitor��5	   	   $	��768��6    $
��1024��6-    � WindowState$ 	Maximized=�6S    � 	Resources 5
      !   RM.Server.Controls.TCPState   TCPState&    RM.Server.Controls.LapCollection  LapCollection#   RM.Server.Controls.CourseList  
CourseList��%W   &	        &	      &	 &       5   
   6     5   
   6#     5   
   6    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��* �6   �� 5      $	��300 �6    � RowDefinitions 5   
   �� 5      $	��130 �6   �� 5      $��* �6   �� 5      $	��200 �6   �� 5      $	��100 �6   .+�5   
   �� $��Racing Monitor��6   $��0��6*   $��0��6:   $��3��6G    �� TextAlignment$ Center=�6[   &   System.Windows.Controls.DataGrid  5!   
   -   5          Name$ LapsGrid��5!         AutoGenerateColumns .  $��1��6�   $��1��6�   $��0, 10q�6�   ��6%   ��  �� Source# [ 	 �� Path$	 LapList�
   Columns
 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 64   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *
Username+ �� Mode$ OneWay=�  5$      $ Kart ID��6%   $	 100 66    6B   �� *KartID+$ OneWay=�  5%      $	 Lap��6%   $	 150 62    6>   �� *Lap+$ OneWay=�  5&      $
 Time��6%   $	 150 63    6?   �� *Time+$ OneWay=�  5'      $ 	Best Time��6%   $	 150 68    6D   �� *
BestTime+$ OneWay=�  5(      $ Absolute Time��6%   $	 150 6<    6H   �� *AbsoluteTime+$ OneWay=�5*        5-   
   -   5        $ 
CourseGrid��5-       .  $��2��6�   $��1��6�   $��2��6�   $��10q�6�   ��6'   �� # [ $	 CList�
 5.        5/      $ Course��6%   $ * 65    6?   �� *Name+$ OneWay=�  50      $ Parts��6%   $	 100 64    6@   �� *Count+$ OneWay=�52      � 55   
   $��2��6   $��3��6   $��	0,0,10,10q�6,   $��Bottom=�6?   $��Right=�6Z   .+�56      �� .+�57      �� -   5         ��Name$ 
TimerLabel��57      $��Timer��6,   $	��200��6<   $��25(�6H   $��43.716��6V   $��Center=�6f   �� 58       �� Orientation$ 
Horizontal=�6   .+�59      �� -   5         ��Name$ TimerStartBtn��59      $��Start��6c   $��0,0,5,0q�6�   {�64   �� *DecoderState.IsConnected+6�   �� 5:      -   5        $ TimerStopBtn��5:      $
��Stop��6b   $��0,0,5,0q�6�   {�63   �� *DecoderState.IsConnected+6�   �� 5;      -   5        $ TimerResetBtn��5;      $��Reset��6c   {�64   �� *DecoderState.IsConnected+6s   5<      5=      5>      � 5A   
   $��0��6   $��3��6   .+�5B      �� $ 
Horizontal=�6   $��	10,0,0,10q�62   5E      5F      �� 5G   
   -   5        $��SettingsBtn��5G      $��Settings��6$   $	��5,0q�67   6D   � 5H   
   $��15q�6   .+�5I      �� $
��Auto=�6   .��5J      �� .+�5K      �� $��Terminal��6    �� 5L      $��IP��6    �� 5M      -	   5         ��Name$ TerminalIpTextBox��5M      �� 5N      $
��Port��6    �� 5O      -
   5        $ TerminalPortTextBox��5O      �� 5Q      $��Decoder��6    $��0,15,0,0q�6/   �� 5R      $��	Port Name��6    �� 5S      -   5         ��Name$ PortNameComboBox��5S      �� 5T      $��	Baud Rate��6    �� 5U      -   5        $ BaudRateComboBox��5U      �� 5V      $��	Data Bits��6    �� 5W      -   5        $ DataBitsComboBox��5W      �� 5X      $��Parity��6    �� 5Y      -   5        $ ParityComboBox��5Y      �� 5Z      $��	Stop Bits��6    �� 5[      -   5        $ StopBitsComboBox��5[      5]      5^      �� 5_      -   5        $ SaveSettingsButton��5_      $
��Save��6R   $��Bottom=�6a   $
��Left=�6|   5`      5a      5b      