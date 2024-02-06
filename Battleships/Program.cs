int[,] gameBoard = {
    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
};
string[,] gameBoardKnown = {
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
    {"~", "~", "~", "~", "~", "~", "~", "~", "~", "~"},
};
char[] rows = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
string[] guesslist = new string[2];

string guesscoord = "";
int guesses = 0;

void DrawGameBoard()
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

    return 0;
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

int Guess()
{
    Console.WriteLine("Enter your guess coordinates in the form:    A,1");
    guesscoord = Console.ReadLine();
    if (guesscoord == "")
    {
        return 0;
    }else if (guesscoord.Length < 3)
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

bool Check(string x, string y)
{
    guesses++;
    switch (gameBoard[LetterMap(x), int.Parse(y)])
    {
        case 0:
            Console.WriteLine("MISS!");
            gameBoard[LetterMap(x), int.Parse(y)] = 2;
            gameBoardKnown[LetterMap(x), int.Parse(y)] = "0";
            break;
        case 1:
            Console.WriteLine("HIT!");
            gameBoard[LetterMap(x), int.Parse(y)] = 2;
            gameBoardKnown[LetterMap(x), int.Parse(y)] = "X";
            break;
        case 2 or 3:
            Console.WriteLine("You already guessed that spot. Try another.");
            guesses--;
            break;
    }

    return false;
}

bool run = true;

while (run)
{
    Console.Write("\n\nTotal guesses: " + guesses.ToString());
    Console.Write("\n----------------------BattleShips----------------------");
    Console.Write("\n-------------------------------------------------------\n");
    DrawGameBoard();
    int ver = Guess();
    switch (ver)
    {
        case 0: Console.WriteLine("\n\n\nActually guess something please. Guess in the Form: A,1    Range: A-J    Range: 0-9"); break;
        case 1: Console.WriteLine("\n\n\nThe first part of your guess must be a LETTER. Form: A,1    Range: A-J"); break;
        case 2: Console.WriteLine("\n\n\nThe second part of your guess must be a NUMBER. Form: A,1    Range: 0-9"); break;
        case 3: Console.WriteLine("\n\n\nGuess accepted."); Check(guesscoord[0].ToString(), guesscoord[2].ToString()); break;
        default: Console.WriteLine("\n\n\nYou already guessed that spot."); break;
    }
}