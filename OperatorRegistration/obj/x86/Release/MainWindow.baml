   M S B A M L   `   `   `  ���� O  KOperatorRegistration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:OperatorRegistration.EntitiesOperatorRegistration.Entities  O,clr-namespace:OperatorRegistration.UtilitiesOperatorRegistration.Utilities  %    OperatorRegistration.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ;loc+clr-namespace:OperatorRegistration.Entities     >clock,clr-namespace:OperatorRegistration.Utilities     -     � Title$  DBIDS Operator Manager��$	��650��$
��1000�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency . ���	#30F5D794 � 	Resources 6   0OperatorRegistration.Utilities.DateTimeConverter   DateTimeConverter  fourLettersTextBox  skyColorTxtStyle �� BasedOn �� 
TargetType �� Property	 �� Value  DBIDSBtnStyle  BtnStyle  lbxTextStyle  txbStyle��%7  &	        &	      7  &	 6     7  &	 �     &	 �     7    �� 8   " ��.���  �@��	MaxLength$	 4���� 8   " ��.���  � ��
	 ��  ���  � ��$	 60����  � ��$	 5,0,5,0q��� " ��.���  � ��
	 ��  ��� 8   " ��.���  � ��$	 Center=���  � ��$		 140��.���� -   $��operatorWindow��$
��Auto��$
��Auto��
���**��$��1q�
 �� CornerRadius$
 10,10,10,10�����#FEF8F0._�� ���	#3EEDF5E5 � RowDefinitions �� $��2* ��� $	��24* ��� $��6* ��� $��2* � � ColumnDefinitions �� $��5* ��� $��5* ��� $
��3.5* �.+��� $��0��$��0��$
 	10,10,0,0��  Top#��[ $��3��._��  �� $��4* ��� $��2* �.+��� $��0�� �� Orientation$ 
Horizontal=�.+��� $$��Common\images\registration.png���� $��TRAINEE REGISTRATION��  lblTitle#��[ 
��������� $ 
Horizontal=�$��1��$��Center=�$��Center=�.+�,   &OperatorRegistration.Utilities.UMClock  -     Name$ Clock���� -   $��txbDOMClock��
������ 
�������$��12(�$
��Bold"�����  �� Path$ DateTime� �� 	Converter# [   �� ElementName$ Clock���� $��Personal Information��
���**��$��1��$��2�� 	 GroupHeaderTemplate#��[	  
 GroupBoxStyle#��[
 $��5,5,5,0q�$��5q�����  �� ImageSource$K E/OperatorRegistration;component/Common/images/DSC00441-black10%25.png��.���  �� $	��10* ��� $��5* �.+�� $��0�� �� �� �� �� �� �� �� �� ��  �� $��5* ��� $
��2.7* ��� $��5* ��� $
��1.5* ��� $��5* �.+��� $��0��$��0��$��8��$	��0,5q�.+��� ���	#FFF8E2E2$��2q�$	��140��$	��185��$
 5��._��� -   $��imgDBIDSOperator��$	��135��$	��180��$
��Fill=��� $ 
Horizontal=�$��Center=�$��3,3,3,3q�
����+��$��30��.+��� -   $��btnCapturePersonPhoto��$��Capture��#��[ $��50��
��� d �{�. ��.  �� -   $��btnPastePersonPhoto��$��Paste��#��[ $��50��
��� d �{�. ��.  �� -   $��btnClearPersonPhoto��$��Clear��#��[ $��50��
��� d �{�. ��.  �� -	   $��cbxTypeOfPID��$��0��$��1��  CbxStyle#��[ $��80��$
��Left=�
������ �� -
   $��cbxTypeOfClass��$��0��$��4��#��[ $��80��$��Right=�
������ .@��� $��Initial��$��80�� �� 
IsSelected . �� $��Renewal��$��80���� $��	Last Name��$��1��$��1��  
LabelStyle#��[ �� $��First��$��1��$��3��#��[ �� $��Middle��$��2��$��1��#��[ �� $	��DOB��$��2��$��3��#��[ �� $	��Job��$��3��$��1��#��[ �� $��Office��$��4��$��1��#��[ �� $��Mobile��$��4��$��3��#��[ �� $��Email��$��5��$��1��#��[ �� $��Location��$��6��$��1��#��[ �� $��Service��$��7��$��1��#��[ �� $
��Rank��$��7��$��3��#��[ �� $��Nationality��$��8��$��1��#��[ �� $��Gender��$��8��$��3��#��[ �� -   $��spnKID��$��0��$��2��$ 
Horizontal=�$y�Visible=�$��5 0 0 0q�.+��� -   $��txtPrefixKID��#��[ $��50�� �� 	MaxLength$ 6���� -   $��lblKID��$��-���� -   $��txtSuffixKID��#��[ $��55��$ 7���� -   $��spnSSN��$��0��$��2��$��2��$ 
Horizontal=�$y�	Collapsed=�$��5 0 0 0q�.+��� -   $��txtPrefixSSN��#��[ $��30��$ 3���� -   $��lblSSN1��$��-���� -   $��	txtMidSSN��#��[ $��25��$ 2���� -   $��lblSSN2��$��-���� -   $��txtSuffixSSN��#��[ $��40���� -   $��lblPersonStatus��$��8��$��0��$��16(�$
��Bold"�$��Center=�$��Center=��� -   $��txtPassport��$��0��$��2��$��2��#��[ $y�	Collapsed=�$	��120��$ 13��$
��Left=�$��5 0 0 0q��� -   $��txtLastName��$��1��$��2��#��[ $ 30�� �� CharacterCasing$ Upper=��� -   $��txtFirstName��$��1��$��4��#��[ $ 30��$ Upper=��� -   $��txtMiddleName��$��2��$��2��#��[ $ 30��$ Upper=���.  (  "System.Windows.Controls.DatePicker  -   $��dpkDateOfBirth��$��2��$��4�����	#FED1E6FF$��25���� -   $��txtJobTitle��$��3��$��2��$��3��#��[ $ 50��$	��405��$ Upper=��� -   $��spnOfficePhone��$ 
Horizontal=�$��Center=�$��4��$��2��.+��� -   $��cbxOfficePhoneArea��$��50��  skyColorCbxStyle#��[ ��.  $��5 0 0 0q��� $��-���� -   $��txtOfficePhonePrefix��$��35��#��[ �� $��-���� -   $��txtOfficePhoneNumber��$��35��#��[ �� -    $��spnMobilePhone��$ 
Horizontal=�$��Center=�$��4��$��4��.+��� -!   $��cbxMobilePhoneArea��$��50��#��[ ��.  $��5 0 0 0q��� $��-���� -"   $��txtMobilePhonePrefix��$��35��#��[ �� $��-���� -#   $��txtMobilePhoneNumber��$��35��#��[ �� $ 
Horizontal=�$��5��$��2��$��3��$��Center=�$��5 0 0 0q�.+��� -$   $��txtEmail��#��[ $	��220��$ 50���� $��@���� -%   $��cbxEmailServer��$	��160��#��[ ��.  �� $ 
Horizontal=�$��6��$��2��$��3��$��Center=�$��5 0 0 0q�.+��� -&   $��cbxInstallation��$	��160��$��2q�#��[ ��.  �� -'   $��txbCity��  skyColorTxbStyle#��[ $	��160��$��2q��� -(   $��txbArea��#��[ $��75��$��2q��� -)   $��
cbxService��$��7��$��2��#��[  �� IsTextSearchEnabled . ��.  �� -*   $��cbxRank��$��7��$��4��#��[ ��.  �� -+   $��cbxNationality��$��8��$��2��#��[ ��.  �� -,   $��	cbxGender��$��8��$��4��#��[ ��.  � $��1�� �� �� �� ��  �� $��5* ��� $
��2.7* ��� $��5* ��� $
��1.5* ��� $��5* �.+��� $��Operator Type��$��0��$��1��#��[ �� $��Office��$��0��$��3��#��[ �� $��Remarks��$��1��$��1��#��[ �� --   $��cbxOperatorType��$��0��$��2��#��[ ��.  �� -.   $��	cbxOffice��$��0��$��4��#��[ ��.  �� -/   $��txtPRRemarks��$��1��$��2��$��3��$��2��#��[  �� AcceptsReturn . $ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=�$	 500��$
��Auto�� �� TextWrapping$
 Wrap=�$��50����.  �� $��	Agreement��
���**��$��2��$��0��$��2��#��[	 #��[
 .���  �� $��9* ��� $��1* �.+��� $��0��$I��CI agree to save my personal information to use for DBIDS Operation!��$
��10,3q�$
��Wrap=�
���   � � ForceCursor .  �� -0   $��cbxPolicyAgreement��$��14(�$
��Bold"�$��1��$��Agree��$��Center=��� $��Trainee List��
����  �$��1��$��2��$��2��#��[	 #��[
 .���  �� $��1* ��� $	��13* ��� $��1* �.+��� $ 
Horizontal=�$��0��.+��� $��Date : ��
���   �$
��Bold"�$��15 0 0 0q�$��Center=��� -1   $��cbxTrainedDate��$	��100��#��[ ��.  �� -2   $��btnRefreshList��$��Refresh��#��[ $��50��
��� d ���.  &   System.Windows.Controls.DataGrid  -3   $��dgdFindPersonList��$��1�����	#FFD1E6FF   AutoGenerateColumns .     
IsReadOnly . $��5q���.    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Last Name��    Width5  /System.Windows.Controls.DataGridLengthConverter$	  10* 1  +System.Windows.Controls.DataGridBoundColumn!   Binding! �� *
LastName+  $ 
First Name��$	  10* ! �� *	FirstName+  $	 Loc��$  4* ! �� *Installation+"   RowDetailsTemplate" �� .R��� $��2q�
�������# �� Padding$# 2q�._��� $ 
Horizontal=�.+��� $��2q�.+��� 
��� ���$��1q�._��� $��
imgTrainee��$��60��$��45��$
��Fill=����� $ Photo��� .+��� $ 
Horizontal=�.+��� $��12(�$��OFF : ��$��Center=��� $��16(�
���p�$��Center=����� *OfficePhone+�� $ 
Horizontal=�.+��� $��12(�$��MOB : ��$��Center=��� $��16(�
���p�$��Center=����� *MobilePhone+�� $ 
Horizontal=�.+��� $��12(�$��JOB : ��$��Center=��� $��16(�
���p�$��Center=����� *
JobTitle+�� $ 
Horizontal=�$��2��.+��� $��
Trainee : ��
���   �$
��Bold"�$��	130 0 5 0q�$��Center=��� -4   $��txbNoOfTrainees��$��Center=�$
��Bold"��� $��3��$��1��$��Right=�$��35��$ 
Horizontal=�$��Center=�
��� ���.+��� -5   $��btnDBIDSSave��$
��Save��#��[ �� -6   $��btnDBIDSRemove��$��Delete��#��[ 
���  ���� -7   $��btnDBIDSClear��$��Clear��#��[ �� $��3��$��2��$��Right=�$ 
Horizontal=�
����� �.+��� -8   $��btnDBIDSUpload��$��Upload��#��[ $
��10,0q��� -9   $��btnDBIDSClose��$��Close��#��[ $��0,0,10,0q�
���  ��