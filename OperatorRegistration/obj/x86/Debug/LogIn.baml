   M S B A M L   `   `   `  ����  OperatorRegistration     OperatorRegistration.LogIn   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Login��5   	   $	��220��6   $	��300��6$    � WindowStartupLocation$ CenterScreen=�6G    � 
ResizeMode$ NoResize=�6l    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������ .��5      �� -   5        $��operatorWindow��5   (   $
��Auto��6   $
��Auto��6   
����  �$��1q�6Q    �� CornerRadius$ 
0,20,20,40��6e    J� Opacity$	 0.9Q�6   ��5   
   �� 5      .��5	      � � 5       � Color$ LightYellow��6#    � Offset$	 0.0Q�67   � 5      $ LightGoldenrodYellow��6#   $	 0.5Q�6@   5      5      ._�5   
   � 	 � RowDefinitions	 5      �� 5      $��2* �6    �� 5      $��5* �6    �� 5      $��2* �6    �� 5      $��2* �6    .+�5      �� $��0��6   $ 0,20,0,0��6"      Top#��[  6:   ._�5      � .+�5       �� $��TRAINEE REGISTRATION��6   $��0��6;     lblTitle#��[ 6K   
�������6m   5!      5"      �� 5$      $ 	0,0,20,40��6   $��3��6.    
 Bottom#��[ 6;   5&      �� 5(      $��0��6   .+�5)      �� 5*      �� 5+      $��Login��6   
���� ��  GroupHeaderTemplate#��[ 6:   $��1��6p     GroupBoxStyle#��[ 6}   .��5,      �� .+�5-      �� 
 �� Orientation$
 
Horizontal=�6!   $��Center=�6:   $��Bottom=�6W   $��0,0,0,5q�6r   .+�5.      �� $��User Name :��6    $��90��66   $��Center=�6A   
����  �$
��Bold"�6n   �� 5/      -   5        $��txtCommonID��5/   .   $	��150��6"   6.   50      �� 51      $
 
Horizontal=�6!   $��Center=�6:   $	��Top=�6W   .+�52      �� $��
Password :��6    $��90��65   $��Center=�6@   
����  �$
��Bold"�6m   G� 53      -   5        $��txtCommonPassword��53   2   $	��150��6&   6K   54      55      56      �� 57      $��2��6   $
 
Horizontal=�6&   $��Right=�6?   $	��5,0q�6[   .+�58      �� -   5        $��btnCommonLogin��58   4   $��Login��6   $��60��6)   $��25��6g   
����  �6�   �� 59      -   5        $��btnCommonCancel��59   5   $��Cancel��6   $��60��6*   $��25��6j   
����  �6�   5;      5<      5=      5>      