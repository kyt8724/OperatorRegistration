   M S B A M L   `   `   `  ����   OperatorRegistration'    !OperatorRegistration.SearchOffice   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  SearchOffice��$	��500��$	��800�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � 	Resources    
SOBtnStyle  BtnStyle �� BasedOn �� 
TargetType �� Triggers P� Property P� Value	 �� Property
 �� Value �� ScaleX �� ScaleY �� CenterX �� CenterY��%  &	        7  �� 8   " �� P�  �@J�IsMouseOver . . ��� 	 � z�
 �� $	 1.2Q�$	 1.2Q�$ 25Q�$ 25Q��� 	 � ��

 � ���.��� 	 � ��

 ������ 	 � ��$
 60���� 	 � ��$
 5,0,5,0q��� 	 �@J�RenderTransformOrigin$
 0.5, 0&�.���  � RowDefinitions �� $��2* ��� $��6* ��� $��6* ��� $��6* ��� $��2* � � ColumnDefinitions �� $��2* ��� $��4* �.+��� $��0��$��0��$��4��  Top#��[ �� $��common\images\office.png��$
��Left=�$��5q��� $��Search Office��$��0��$��1��  lblTitle#��[ 
�������$��80,0,0,0q��� $��1��$��4��$��0��  GroupBoxStyle#��[ 
������$��Search Result��  GroupHeaderTemplate#��[ .��� -   $��gdSearchResult�� �� $��2* ��� $	��13* � .+�� $��0�� �� ��  �� �� �� .+��� $��0��$��0��$��3�� �� Orientation$ 
Horizontal=�$
��Left=�$��Center=�$��5,0,0,0q�.+�	� -   $��rdbSOShowAll��$��Show All�� 	� 	GroupName$ gpnOfficeList��	� -   $��rdbSOSearchByUnit��$��By Unit��$ gpnOfficeList��$��20,0,0,0q�	� -   $��rdbSOSearchByName��$��	By Office��$ gpnOfficeList��$��20,0,0,0q��� $��1��$��0��$��3��$ 
Horizontal=�$
��Left=�$��5,0,0,0q�.+��� -   $��txtSONameSearch��$	��195��  skyColorTxtStyle#��[  �� 	MaxLength$ 30���� -   $��btnSONameSearch��#��[  $��25��$��5,0,0,0q�����  �� ImageSource$B </OperatorRegistration;component/Common/images/searchicon.png��� $��1��$��5q� �� $	��12* ��� $��1* �.+��� -   $��lbxOfficeName��$��0��  skyColorLbxStyle#��[ �� -   $��spSOLabelSelector��$��1��$��Center=�.+��� -	   $��lblSONumber��$��#��  Selector#��[ $��Lucida Console)��� -
   $��lblSOA��$��A��#��[ $��Lucida Console)��� -   $��lblSOB��$��B��#��[ $��Lucida Console)��� -   $��lblSOC��$��C��#��[ $��Lucida Console)��� -   $��lblSOD��$��D��#��[ $��Lucida Console)��� -   $��lblSOE��$��E��#��[ $��Lucida Console)��� -   $��lblSOF��$��F��#��[ $��Lucida Console)��� -   $��lblSOG��$��G��#��[ $��Lucida Console)��� -   $��lblSOH��$��H��#��[ $��Lucida Console)��� -   $��lblSOI��$��I��#��[ $��Lucida Console)��� -   $��lblSOJ��$��J��#��[ $��Lucida Console)��� -   $��lblSOK��$��K��#��[ $��Lucida Console)��� -   $��lblSOL��$��L��#��[ $��Lucida Console)��� -   $��lblSOM��$��M��#��[ $��Lucida Console)��� -   $��lblSON��$��N��#��[ $��Lucida Console)��� -   $��lblSOO��$��O��#��[ $��Lucida Console)��� -   $��lblSOP��$��P��#��[ $��Lucida Console)��� -   $��lblSOQ��$��Q��#��[ $��Lucida Console)��� -   $��lblSOR��$��R��#��[ $��Lucida Console)��� -   $��lblSOS��$��S��#��[ $��Lucida Console)��� -   $��lblSOT��$��T��#��[ $��Lucida Console)��� -   $��lblSOU��$��U��#��[ $��Lucida Console)��� -   $��lblSOV��$��V��#��[ $��Lucida Console)��� -    $��lblSOW��$��W��#��[ $��Lucida Console)��� -!   $��lblSOX��$��X��#��[ $��Lucida Console)��� -"   $��lblSOY��$��Y��#��[ $��Lucida Console)��� -#   $��lblSOZ��$��Z��#��[ $��Lucida Console)��� $��1��$��1��#��[ 
����  �$��Installation Information��#��[ .��� -$   $��gdInstallationInformation�� �� �� ��  �� $��4* ��� $��8* ��� $��4* ��� $��4* �.+��� $��Installation��$
��Bold"�$��0��$��0���� $
��Code��$
��Bold"�$��0��$��2��$��Right=��� $
��Area��$
��Bold"�$��1��$��0���� $
��City��$
��Bold"�$��2��$��0���� -%   $��cbxInstallationName��$��0��$��1�� 	 skyColorCbxStyle#��[	 �� -&   $��txbInstallationCode��$��0��$��3�� 
 skyColorTxbStyle#��[
 $��30�� �� TextAlignment$ Center=�$
��Left=��� -'   $��txbArea��$��1��$��1��#��[
 �� -(   $��txbCity��$��2��$��1��#��[
 �� $��2��$��1��#��[ 
����� �$��Unit Information��#��[ .��� -)   $��gdUnitInformation�� �� �� ��  �� $��4* ��� $��8* ��� $��4* ��� $��4* �.+��� $��	Unit Name��$
��Bold"�$��0��$��0���� $��
Unit Phone��$
��Bold"�$��1��$��0���� $	��UIC��$
��Bold"�$��2��$��0���� $��APO/AP��$
��Bold"�$��2��$��2��$��Right=��� -*   $��txbUnitName��$��0��$��1��#��[
 �� $��0��$��3��$ 
Horizontal=�$��Right=��� -+   $��txbUIC��$��2��$��1��#��[
 �� -,   $��spnUnitPhone��$ 
Horizontal=�$��Center=�$��1��$��1��.+��� --   $��txbUnitPhonePrefix��$��35��#��[
 �� $��-���� -.   $��txbUnitPhoneNumber��$��35��#��[
 �� -/   $��spnUnitApoAp��$ 
Horizontal=�$��Center=�$��2��$��3��.+��� -0   $��
txbUnitAPO��$��40��#��[
 �� $��/���� -1   $��	txbUnitAP��$��35��#��[
 �� $��3��$��1��#��[ 
��� d �$��Office Information��#��[ .��� -2   $��gdOfficeInformation�� �� �� ��  �� $��4* ��� $��8* ��� $��4* ��� $��4* �.+��� $��Office Name��$
��Bold"�$��0��$��0���� $��Office Code��$
��Bold"�$��0��$��2��$��Right=��� $��Building Name��$
��Bold"�$��1��$��0���� $��Building No��$
��Bold"�$��1��$��2��$��Right=��� $��Room No��$
��Bold"�$��2��$��0���� -3   $��txtOfficeName��$��0��$��1��#��[
 �� -4   $��txbOfficeCode��$��0��$��3��#��[
 �� -5   $��txtBuildingName��$��1��$��1��#��[
 �� -6   $��txtBuildingNo��$��1��$��3��$
��Left=�#��[
 $��50���� -7   $��	txtRoomNo��$��2��$��1��$
��Left=�#��[
 $��30���� $��2��$��3��$ 
Horizontal=�$��Right=�.+��� -8   $��btnClearOfficeInformation��$��Clear��#��[  $��40���� $��4��$��1��$ 
Horizontal=�$��Right=�$��Center=�$��0,0,5,0q�.+��� -9   $��btnSOSelect��$��Select��#��[  �� -:   $��
btnSOClose��$��Close��#��[  