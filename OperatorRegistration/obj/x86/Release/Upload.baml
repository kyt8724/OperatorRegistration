   M S B A M L   `   `   `  ���� O  KOperatorRegistration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:OperatorRegistration.UtilitiesOperatorRegistration.Utilities  M+clr-namespace:OperatorRegistration.EntitiesOperatorRegistration.Entities  !    OperatorRegistration.Upload   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     >clock,clr-namespace:OperatorRegistration.Utilities     ;loc+clr-namespace:OperatorRegistration.Entities     -     � Title$!  Upload Operator Information��$	��650��$	��750�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency .  � 	Resources 6   0OperatorRegistration.Utilities.DateTimeConverter   DateTimeConverter  fourLettersTextBox  skyColorTxtStyle �� BasedOn �� 
TargetType �� Property	 �� Value  DBIDSBtnStyle  BtnStyle  lbxTextStyle  txbStyle��%7  &	        &	      7  &	 6     7  &	 �     &	 �     7    �� 8   " ��.���  �@��	MaxLength$	 4���� 8   " ��.���  � ��
	 ��  ���  � ��$	 60����  � ��$	 5,0,5,0q��� " ��.���  � ��
	 ��  ��� 8   " ��.���  � ��$	 Center=���  � ��$		 150��.���� -   $��operatorWindow��$
��Auto��$
��Auto��
���**��$��1q�
 �� CornerRadius$
 10,10,10,10������ .��� �  � Color$ #E7EBF7�� � Offset$	 0.0Q�� $ #CEE3FF��$	 0.5Q�._�� ���	#3EEDF5E5 � RowDefinitions �� $��1* ��� $	��11* ��� $��1* � � ColumnDefinitions �� $��1.55* ��� $��2* ��� $
��0.3* ��� $��2* �.+��� $��0��$��0��$��4��$
 	10,10,0,0��  Top#��[ ._��  �� $��4* ��� $��2* �.+��� $��0�� �� Orientation$ 
Horizontal=�.+��� $ ��common\images\db_comit.png��$��	5,10,0,10q��� $ ��UPLOAD TRAINEE INFORMATION��  lblTitle#��[ 
��������� $��1��$ 
Horizontal=�$��Center=�$��Center=�.+�,   &OperatorRegistration.Utilities.UMClock  -     Name$ Clock���� -   $��txbDOMClock��
������ 
�������$��12(�$
��Bold"�����  �� Path$ DateTime� �� 	Converter# [   �� ElementName$ Clock���� $��1��$��0��$��Dates��$	��500��$	��180�� 	 GroupHeaderTemplate#��[	  
 GroupBoxStyle#��[
 .��R� -   $��trvOperatorsByDate��$��0q�$��0q�  �� DataType0   *OperatorRegistration.Entities.TrainingDate �� Padding��%�   (��       *, +)�� "  .R��� $	��140��
����iA�
���/kU�$��1q�$
 2��$��2q�$ 2q�._��� $ 
Horizontal=�.+��� $
��Bold"�
����������� $ DateOfTraining��� $
��Bold"�
�������$��5,0,0,0q����� $ NoOfOperators��� $��1��$��1��$��Trainee List��$	��500��$	��240��#��[	 #��[
 .���� -   $��
lbxTrainee��
���   �$
��Bold"�$��0q� �� View �� P���  �� Width$ 63������ $��Photo��
����  � �� CellTemplate �� .R��� 
��� ���$��1q�._��� $��60��$��50��$
��Fill=����� $ Photo��� $	 153������ $��Operator��
����  � �� .R��� 
��� ���$��1q�._��� $��0q�$	��150��.+��� $	��150��#��[ ���� $ LastName��� $	��150��#��[ ���� $ 	FirstName��� $	��150��#��[ ���� $ OperatorType��� $	��150��#��[ ���� $ InstallationName��� $��1��$��2��$��Center=�$��Center=�$	��140��$��40��.+��� -   $��
btnMoveAll��$��30��$��5q�#��[ .���� $H��B/OperatorRegistration;component/Common/images/arrow_black_last.png�� ^� ToolTip _� $%��Move all records to upload list���� -   $��btnMove��$��30��$��5q�#��[ .���� $H��B/OperatorRegistration;component/Common/images/arrow_black_next.png�� _� $*��$Move selected records to upload list���� -	   $��	btnRemove��$��30��$��5q�#��[ .���� $L��F/OperatorRegistration;component/Common/images/arrow_black_previous.png�� _� $0��*Move back selected records to trainee list���� -
   $��btnRemoveAll��$��30��$��5q�#��[ .���� $I��C/OperatorRegistration;component/Common/images/arrow_black_first.png�� _� $+��%Move back all records to trainee list���� $��1��$��3��$	��500��.+��� $��Uploading List��$	��490��$	��240��#��[	 #��[
 $��0q�.���� -   $��lbxUploading��
���   �$
��Bold"�$��0q�$��0q� �� P��� $ 63������ $��Photo��
����  � �� .R��� 
��� ���$��1q�$ 0q�$��0q�._��� $��60��$��50��$��0q����� $ Photo��� $	 153������ $��Operator��
����  � �� .R��� 
��� ���$��1q�._��� $��0q�$	��150��.+��� $	��150��#��[ ���� $ LastName��� $	��150��#��[ ���� $ 	FirstName��� $	��150��#��[ ���� $ OperatorType��� $	��150��#��[ ���� $ InstallationName��� -    ��Name$ imgNetworkStatus��$��2��$��2��$��30��$��30��$)��#common\images\disconnectnetwork.png���� $��2��$��3��$ 
Horizontal=�$��Right=�$��5q�.+��� -   $��	btnUpload��$��Upload��#��[ {�.  �� -   $��btnClose��$��Close��#��[ ����  �� ImageSource$B </OperatorRegistration;component/Common/images/arrow_last.png��