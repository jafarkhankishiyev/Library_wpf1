   M S B A M L   `   `   `  ����F  BLibrary_wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Library_wpf.ViewsLibrary_wpf.Views  5      '    !Library_wpf.Views.AddEditBookView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Library_wpf.Views     5      .��5	      � 
�������  � ColumnDefinitions  5
   
   �� 5      6!   �� 5      $	��600 �6   6-   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $��1��6   $��2��6!   ��61   �� *SelectedGenresText+�� 5   
   $��1��6   $��0��6!   ��61   �� *SelectedAuthorsText+� 5   
   $��1��6   $��1��6   $��1��6,   $��1��6=   $��Center=�6Q   $	��590��6n     5      �� 5      6%   �� 5      $	��100 �6#   �� 5      6%   �� 5      $��30 �6#   �� 5      $��30 �6#    5!      �� 5"      6"   �� 5#      6"   �� 5$      6"   �� 5%      6"   �� 5&      6"   �� 5'      6"   .+�5)      �� -   5         ��Name$ AuthorBookComboBox��5)   \   $��0��6   $��3��6'   $��Right=�64   $	��170��6P    �� DisplayMemberPath$
 Name��6�   ��6x   �� *AuthorListSource+ �� SelectedItem 6�   �� *SelectedBookAuthor+6�   �� 5*      -   5        $ GenreBookComboBox��5*   p   $��1��6   $��2��6'   $��1��6;   $	��170��6H   $��Right=�6T   $
 Name��6�   ��6�   �� *GenreListSource+ 6�   �� *SelectedBookGenre+6�   �� 5+      $��1��6   $��3��6"   $��+��62   $��30��6>   $
��Left=�6I   ��6d   �� *AddAnotherGenreCommand+{�6�   �� *AddAnotherGenreButtonEnabled+�� 5,      $��1��6   $��4��6"   $��-��62   $��30��6>   $��Center=�6I   ��6f   �� *DeleteAnotherGenreCommand+{�6�   �� *!DeleteAnotherGenreButtonEnabled+�� 5-      $��3��6   $��1��6"   $
��Left=�62   $��+��6M   $��33��6Y   $��25��6d   $��Center=�6�   ��6p   �� *AddAnotherAuthorCommand+{�6�   �� *AddAnotherAuthorButtonEnabled+�� 5.      $��3��6   $��1��6"   $��33��62   $��25��6=   $��Center=�6I   $��Center=�6f   $��-��6�   ��6�   �� *DeleteAnotherAuthorCommand+{�6�   �� *" DeleteAnotherAuthorButtonEnabled+�� 5/      -   5         ��Name$ Button1��5/   X   $��4��6   $��2��6"   $��1��63   $��1��6D   $
��Save��6j   $��13(�6y   $��85��6�   $��30��6�   $��Center=�6�   $��Center=�6�   ��6�   �� *SaveCommand+{�6�   �� *SaveButtonEnabled+��50      �� 51       �� Opacity$	 0.5Q�6"    �� Color$
 Blue��60   53      �� 55      -   5         ��Name$ nameTextBox��55   3   $��0��6   $��1��6&   $
��Auto��6H   $��Center=�6V   $��Right=�6q   $	��170��6�   ��6�   �� *
NameText+�� 56      -   5        $ yearTextBox��56   3   $��2��6   $��3��6&   $
��Auto��6H   $��Center=�6V   $��Right=�6q   $	��170��6�   ��6�   �� *
YearText+�� 58      -   5        	 ��Name$	 genreTextBlock��58   5   $��1��6   $��2��6%   $��Center=�6N   $
��Left=�6i   Genre:6�   �� 59      -   5        $	 authorTextBlock��59   5   $��3��6   $��0��6%   $��Center=�6N   $
��Left=�6i   	Author:6�   �� 5:      -   5        $	 nameTextBlock��5:   5   $��0��6   $��1��6(   $��Center=�6L   $
��Left=�6g   Title:6�   �� 5;      -	   5        $	 yearTextBlock��5;   5   $��3��6   $��2��6%   $��Center=�6L   $
��Left=�6g   Year:6�   �� 5<      -
   5        $	 BookTextBlock��5<   5   $��0��6   $��1��6%   $	��Top=�6L   $��Center=�6d   $
��Book��6�   $
��Bold"�6�   
�������6�   �� 5>      -   5        $	 nameWarningTextBlock��5>   5   $��0��6   $��0��6%   $��Bottom=�6S   
���  ��$��12(�6   ��6�   �� *NameWarningText+�� 5?      -   5        $	 authorWarningTextBlock��5?   5   $��2��6   $��0��6%   $��Bottom=�6U   
���  ��$��12(�6�   ��6�   �� *AuthorWarningText+�� 5@      -   5        $	 genreWarningTextBlock��5@   5   $��0��6   $��2��6%   $��Bottom=�6T   
���  ��$��12(�6�   ��6�   �� *GenreWarningText+�� 5A      -   5        $	 yearWarningTextBlock��5A   I   $��2��6   $��2��6%   $��2��65   $��Bottom=�6g   
���  ��$��12(�6�   ��6�   �� *YearWarningText+5C      5D      5E      