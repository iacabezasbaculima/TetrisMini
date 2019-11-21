# TetrisMini

TetrisMini is primarily a classic version of the famous game TETRIS created by Aleksey Pajitnov and it was developed in C# using WPF. Currently, it provides all basic game mechanics including score, level and lines completed.

## Getting Started
Visual Studio 2017 or 2019 is recommended, TetrisMini is officially untested on other development environments.

You can clone the repository to a local destination using git:

`git clone --recursive https://github.com/iacabezasbaculima/TetrisMini.git`

Make sure that you do a `--recursive` clone to fetch all of the submodules.

## The Game
On start, a main menu is displayed with options to 'Play' and 'Quit' the game. Additionally, a TOP 5 Highscores table is visible which shows player name, highscore and lines completed.

While in-game, it is possible to pause the game, return to main menu and/or quit the game.

On game over, the user may be allowed to submit a new highscore, number of lines completed and choose a name, provided that the highscore is greater or equal to the lowest highscore currently in the table or if the table contains less than five scores. 

If the user does not meet the requirements to submit a new highscore, a simple message with the score obtained, level reached and lines completed is shown along with options to return to main menu or quit the game.

## Main Features
- UI displays score, level, lines
- Keyboard-control supported
- Save and view TOP 5 highscores table locally
- Play Tetris theme song

## Gameplay Demo   

[![TetrisMini](http://img.youtube.com/vi/Ju740SLPx7w/0.jpg)](http://www.youtube.com/watch?v=Ju740SLPx7w "TetrisMini Demo")

**GOOD LUCK AND HAPPY PLAYING!**
