====BATTLESHIPS====

This program is intended to be a command-line game of battleships in C#
It is played with two players against each other.
You try to guess the randomly generated battleship locations in less goes than the other player.

I planned out each major step before starting to program, and a simple overview of what needed to be done.
Eg:
	How to store the gameboard? Use an array and have 2 separate, on for locations and one for visuals.
	How to draw the gameboard? Write each line of the known gameboard using a nested for loop.
	How to check the guessed square's state? 0 = empty | 1 = ship | 2 = guessed/hit | 3 = guessed/miss



====Code====

1 DrawGameBoard()		-	Draws the game board from the array gameBoardKnown using a nested for loop.
2 generateGameBoard()	-	Randomly fills both gameboards with 10 ships after clearing them.
3 resetBoards()			-	Clears both gameboards, used in generateGameBoard() ^^.

4 LetterMap()/			-	Swaps the letter/number passed as an argument into a number or letter respectively. LetterMap() for letters, RevLetterMap() for numbers.
						-	Uses a switch:case setup for each possible letter/number allowed in coordinates. [A-J][1-9].

5 Guess()				-	Takes in the users guess as an argument, and verifies that it is a valid guess, using if statements with various conditions.
6 Check()				-	Checks the state of the guessed square and returns either hit, miss, or already guessed based on the state of that square.

7 Play()				-	The "main" function. 
						-	Calls all of the other functions/processes in order based on each ones return. 
						-	Tracks each player's scores using p1guesses and p2guesses.
						-	Writes out instructions on how to play, and feedback based on the validity of your guess.


====Next Steps====

 - Speeding up the game loop and removing unnessecary lines of text.
 - Streamline guess input: remove the need for a ',' between letter and number coordinates.
 - Possibly add an option to play against the cpu, using randomly generated guesses.
 - ^^ Split this and the two-player mode into two seperate functions that can be run at the end of each turn.
 - Try alternating between players turns instead of letting one player completely finish then the next.

