   M S B A M L   `   `   `  ����B  >RM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:RM.CoreRM.Core  5      !clr-namespace:RM.CoreRM.Core  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ScrollBarPageButton  �� 
TargetType �� Property �� Value �� 
TargetType  ScrollBarThumb  VerticalScrollBar � RowDefinitions  TextColorBrush ]� DecreaseRepeatButton ]� Thumb  MainColorBrush ]� IncreaseRepeatButton �� Triggers	 P� Property
 P� Value  FavsScrollViewer � ColumnDefinitions �� VerticalOffset � Value �� ScrollableHeight � Maximum �� ViewportHeight �� ViewportSize�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9controls,http://metro.mahapps.com/winfx/xaml/controls  5      ;	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5      localclr-namespace:RM.Core  5      $
convertersclr-namespace:RM.Core  5      1system&clr-namespace:System;assembly=mscorlib  5      %
  &	        &	 D    &	 �    7  7   7  7  7   '�� �    7  &	 �    �� 5	      "  ��6(   .�5
   
   ��  �@J�SnapsToDevicePixels . 60   �� 5   
    �@�OverridesDefaultStyle . 62   �� 5   
    � �� .  6&   �� 5   
    � |� .  6&   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� 
������ 6   5      5      5      �� 5      "  n�6#   .�5   
   ��  �@J�SnapsToDevicePixels . 60   �� 5   
    �@�OverridesDefaultStyle . 62   �� 5   
    � �� .  6&   �� 5   
    � |� .  6&   �� 5   
    � �� 5      �� 5      " n�6"   .R�5      �� #��z��6   5       5"      5#      �� 5%      " ��5&      .R�5'   
   �  5(      �� 5)      $��18��6    �� 5*      $��0.00001* �6    �� 5+      $��18��6    .+�5-      �� $��3��6   8��  $	��3,0q�5/      �� 50      $��0��6   $��0��51      ]� 52      $��
PART_Track��6   $��1��53      ��.  55      �� 56      8�� $��ScrollBar.PageUpCommand��57   #    59      n� 5:      8�� $��0q�5;      8�� $��0q�5=       5?      �� 5@      8�� $��ScrollBar.PageDownCommand��5A   #   5C      5E      5F      �� 5I      "  ��6'   .�5J   
   ��  �@J�SnapsToDevicePixels . 60   �� 5K   
    �@�OverridesDefaultStyle . 62    5L   
   P� 5M      	 �@��Orientation$
 Vertical=�6-   . �5N      ��  � ��$ 7��6*   �� 5O       � ��$
 Auto��6+   �� 5P       � ��8   5Q      5R      5T      �� 5W      "  ��6%   .�5X   
   ��  �@�OverridesDefaultStyle . 62   �� 5Y   
    � �� 5Z      �� 5[      " ��6"   .R�5\      �  5]      �� 5^      $
��Auto �6/   �� 5_       5a      �� 5b      �� 5c      $
��Auto �6,   .+�5f      �� $��1��61   �� 5h      $��PART_VerticalScrollBar��6$   # z 5i   $   # z 5j   $   # z 5k   $   $y�Visible=�5l   $   5m      5n      5p      5q      5r      