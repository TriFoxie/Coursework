//arrays//
int[,] gameBoard = {
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
};
string[,] gameBoardKnown = new string[10, 10];
char[] rows = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
string[] guesslist = new string[2];
//----//
//Objects//
Random rand = new Random();
//----//
//Variables//
string guesscoord = "";
int guesses = 0;
int p1guesses = 0;
int p2guesses = 0;
//----//

void DrawGameBoard()//Draw the current known board to the console.
{
    Console.WriteLine("|-|0|1|2|3|4|5|6|7|8|9|");
    int i1 = 0;
    int i2 = 0;
    while (i2 < 10)
    {
        Console.Write("|");
        Console.Write(RevLetterMap(i2));
        while (i1 < 10)
        {
            Console.Write("|");
            Console.Write(gameBoardKnown[i2, i1]);
            i1++;
        }
        Console.Write("|");
        Console.Write("\n");
        i1 = 0;
        i2++;
    }
}
void generateGameBoard()//refill a board with a new layout
{
    resetBoards(true, true);

    int ships = 10;

    while (ships > 0)
    {
        int x = rand.Next(1, 10);
        int y = rand.Next(1, 10);
        if (gameBoard[x, y] != 1)
        {
            gameBoard[x, y] = 50;
            ships--;
            updateNeighbours(x, y);
        }
    }
}
void resetBoards(bool known, bool unknown)//reset boards to 0
{
    if (known)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                gameBoardKnown[i, j] = "~";
            }
        }
    }

    if (unknown)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                gameBoard[i, j] = 0;
            }
        }
    }
}
void updateNeighbours(int x, int y)//add numbers around bombs
{
    if (x - 1 >= 0 && y - 1 >= 0) { gameBoard[x - 1, y - 1] += 1; }    //|Top left
    if (x - 1 >= 0 && y + 1 < 10) { gameBoard[x - 1, y + 1] += 1; }    //|Top right
    if (x - 1 >= 0) { gameBoard[x - 1, y] += 1; }                      //|Top
    if (x + 1 < 10 && y - 1 >= 0) { gameBoard[x + 1, y - 1] += 1; }    //|Bottom left
    if (x + 1 < 10 && y + 1 < 10) { gameBoard[x + 1, y + 1] += 1; }    //|Bottom right
    if (x + 1 < 10) { gameBoard[x + 1, y] += 1; }                      //|Bottom
    if (y - 1 >= 0) { gameBoard[x, y - 1] += 1; }                      //|Left
    if (y + 1 < 10) { gameBoard[x, y + 1] += 1; }                      //|Right
}

//Switching letters to numbers and vice versa
int LetterMap(string a)
{
    switch (a)
    {
        case "a": return 0; 
        case "b": return 1; 
        case "c": return 2; 
        case "d": return 3; 
        case "e": return 4;
        case "f": return 5; 
        case "g": return 6; 
        case "h": return 7; 
        case "i": return 8; 
        case "j": return 9; 
        default: return 0; 
    }
}
char RevLetterMap(int a)
{
    switch (a)
    {
        case 0: return 'A';
        case 1: return 'B';
        case 2: return 'C';
        case 3: return 'D';
        case 4: return 'E';
        case 5: return 'F';
        case 6: return 'G';
        case 7: return 'H';
        case 8: return 'I';
        case 9: return 'J';
        default: return 'Z';
    }
}

int Guess()//guess verification and decoding
{
    Console.WriteLine("Enter your guess coordinates in the form:    A,1");
    guesscoord = Console.ReadLine();
    if (guesscoord.Length == 4)
    {
        if (guesscoord[3] == 'f')
        {
            return 6;
        }
    }
    if (guesscoord == "fishes" || guesses >= 90)
    {
        return 5;
    }
    else if (guesscoord == "")
    {
        return 0;
    }else if (guesscoord.Length <= 2)
    {
        return 0;
    }else if (guesscoord[1] != ',')
    {
        return 0;
    }
    guesscoord = guesscoord.ToLower();
    guesslist = guesscoord.Split(',');

    //input verification
    bool verify1 = false;
    bool verify2 = false;
    for (int i = 0; i < 10; i++)
    {
        if (rows[i].ToString() == guesslist[0]) { verify1 = true; } //Checks row is a LETTER
        if (numbers[i].ToString() == guesslist[1]) { verify2 = true; } //Checks column is a NUMBER
    }
    if (!verify1) { return 1; }
    if (!verify2) { return 2; }

    return 3;
}
bool CheckTemp(string x, string y, bool flag = false)//guess implementation
{
    guesses++;
    if (!flag)
    {
        switch (gameBoard[int.Parse(x), int.Parse(y)])
        {
            case > 0 and <= 20:
                gameBoardKnown[int.Parse(x), int.Parse(y)] = gameBoard[int.Parse(x), int.Parse(y)].ToString();
                gameBoard[int.Parse(x), int.Parse(y)] = 40;
                break;
            case 0:
                gameBoardKnown[int.Parse(x), int.Parse(y)] = " ";
                gameBoard[int.Parse(x), int.Parse(y)] = 40;
                break;
            case >= 50:
                gameBoardKnown[int.Parse(x), int.Parse(y)] = "M";
                gameBoard[int.Parse(x), int.Parse(y)] = 40;
                return true;
            case 40:
                Console.WriteLine("You already Guessed that spot!");
                guesses--;
                break;
            default: gameBoardKnown[int.Parse(x), int.Parse(y)] = " "; break;
        }
    }else if (flag)
    {
        switch (gameBoard[int.Parse(x), int.Parse(y)])
        {
            case 40: Console.WriteLine("You already Guessed that spot!"); break;
            default: gameBoardKnown[int.Parse(x), int.Parse(y)] = "F"; break;
        }
        guesses--;
    }
    

    return false;
}

void play(int player)//main function: formatting, guess submission.
{
    bool run = false;
    while (!run)
    {
        Console.Write("\n\nTotal guesses: " + guesses.ToString());
        Console.Write("\n----------------------MineSweeper----------------------");
        Console.Write("\n----------------------PLAYER_0");
        Console.Write(player);
        Console.Write("----------------------\n");
        Console.Write("To place a flag, state your guess with an 'f' after it. E.g: a,0f\n\n");
        DrawGameBoard();
        int ver = Guess();
        Console.Clear();
        switch (ver)
        {
            case 0: Console.WriteLine("\n\n\nActually guess something please. Guess in the Form: A,1    Range: A-J    Range: 0-9"); break;
            case 1: Console.WriteLine("\n\n\nThe first part of your guess must be a LETTER. Form: A,1    Range: A-J"); break;
            case 2: Console.WriteLine("\n\n\nThe second part of your guess must be a NUMBER. Form: A,1    Range: 0-9"); break;
            case 3: Console.WriteLine("\n\n\nGuess accepted."); run = CheckTemp(LetterMap(guesscoord[0].ToString()).ToString(), guesscoord[2].ToString()); break;
            case 5: run = true; break;
            case 6: Console.WriteLine("Placing Flag..."); CheckTemp(LetterMap(guesscoord[0].ToString()).ToString(), guesscoord[2].ToString(), true); break;
            default: Console.WriteLine("\n\n\nYou already guessed that spot."); guesses--; break;
        }
        Console.WriteLine("Press enter");
        Console.ReadLine();
    }
    switch (player)
    {
        case 1: p1guesses = guesses; break;
        case 2: p2guesses = guesses; break;
    }
    Console.WriteLine("YOU DIEDED");
    DrawGameBoard();
    Console.ReadLine();
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            CheckTemp(i.ToString(), j.ToString());
        }
    }
    Console.Clear();
    Console.WriteLine("\nThe board:");
    DrawGameBoard();
    Console.ReadLine();
    guesses = 0;
}

//entrypoint
generateGameBoard();
play(1);

