   M S B A M L   `   `   `  ����   OperatorRegistration     OperatorRegistration.LogIn   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Login��$	��220��$	��300�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � AllowsTransparency . 
������ .���� -   $��operatorWindow��$
��Auto��$
��Auto��
����  �$��1q� �� CornerRadius$ 
0,20,20,40�� J� Opacity$	 0.9Q����� .��� �  � Color$ LightYellow�� � Offset$	 0.0Q�� $ LightGoldenrodYellow��$	 0.5Q�._�� 	 � RowDefinitions	 �� $��2* ��� $��5* ��� $��2* ��� $��2* �.+��� $��0��$ 0,20,0,0��   Top#��[  ._�� .+��� $��TRAINEE REGISTRATION��$��0��  lblTitle#��[ 
��������� $ 	0,0,20,40��$��3�� 
 Bottom#��[ �� $��0��.+��� �� $��Login��
���� ��  GroupHeaderTemplate#��[ $��1��  GroupBoxStyle#��[ .���� .+��� 
 �� Orientation$
 
Horizontal=�$��Center=�$��Bottom=�$��0,0,0,5q�.+��� $��User Name :��$��90��$��Center=�
����  �$
��Bold"��� -   $��txtCommonID��$	��150���� $
 
Horizontal=�$��Center=�$	��Top=�.+��� $��
Password :��$��90��$��Center=�
����  �$
��Bold"�G� -   $��txtCommonPassword��$	��150���� $��2��$
 
Horizontal=�$��Right=�$	��5,0q�.+��� -   $��btnCommonLogin��$��Login��$��60��$��25��
����  ��� -   $��btnCommonCancel��$��Cancel��$��60��$��25��
����  �