   M S B A M L   `   `   `  ����C  ?MiniGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MiniGameMiniGame  5   	       MiniGame.LoseWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:MiniGame     5   	     � Title$  
LoseWindow��5   	   $	��700��6   $	��525��6)    � WindowStartupLocation$ CenterScreen=�5	   	   .��5      �    Tetris1#��[  6   .+�5   
   �� -   5         ��Name$ 
Panel_Lose��5   !   $��80q�6   $y�	Collapsed=�65    �� Orientation$ Vertical=�6L   .+�5      �� $
��0 10q�6   $��	YOU LOST!��6&   
�������$��	ExtraBold"�6J   $��Center=�6a   �� 5      $��Total Score: ��6   $��	ExtraBold"�6-   
�������$��Center=�6W   �� 5      -   5         ��Name$ tbScore��5   &   $
�� 0 5q�6   $��Score��67   $��Center=�6D   $
��Bold"�6a   
�������6s   �� 5      $��Total Lines:��6   $��	ExtraBold"�6,   
�������$��Center=�6V   �� 5      -   5        $ tbLines��5   %   $	��0 5q�6   $��Lines��66   
�������$
��Bold"�6V   $��Center=�6h   5      �� 5   
   -   5        $ Panel_Buttons��5      $	��200q�6,   $y�	Collapsed=�69   .+�5      �� -   5         ��Name$ RestartButton��5   $   $
��0 10q�6   $��	TRY AGAIN��6W   $��Center=�6k   $	��100��6�     	btnSparta#��[ 6�   �� 5      -   5        $ 
MenuButton��5   %   $
��0 10q�6   $��
START MENU��6R   $	��100��6g   #��[ 6s   �� 5      -   5        $��
QuitButton��5   #   $
��0 10q�6   $
��QUIT��6N   $��Center=�6]   $	��100��6z   #��[ 6�   5      �� 5   
   -   5        $ Panel_NewHighScore��5   $   $��0 200q�6   $y�	Collapsed=�6@   .+�5      �� -	   5        $��Message��5      $��Center=�6'   $
��Wrap=�6D   
�������$
��Bold"�6k   $	��121��6}   $	��371��6�    �� TextAlignment$ Center=�6�   $��20(�6�   YWNEW HIGHSCORE! You made it into the Tetris highscore list! Please enter a name below...5      �� 5      -
   5         ��Name$ tbxInput��5   $   $
��0 10q�6   $��Center=�66   $	��200��6S   $��2q�6_   �� 5       -   5        $��SubmitButton��5       $��SUBMIT SCORE��6D   $��Center=�6[   $	��100��6x   #��[ 6�   5!      5"      5#      