import random
from time import sleep

player1deck = []
player2deck = []

colors = ["blue", "red", "pink", "green"] 
numbers = ["0","1","2","3","4","5","6","7","8","9"]
specials = ["+2", "+4", "Reverse", "Change Color", "Block"]
deck = ["blue 1", "blue 2", "blue 3", "blue 4", "blue 5", "blue 6", "blue 7", "blue 8", "blue 9",
        "red 1", "red 2", "red 3", "red 4", "red 5", "red 6", "red 7", "red 8", "red 9",
        "green 1", "green 2", "green 3", "green 4", "green 5", "green 6", "green 7", "green 8", "green 9",
        "pink 1", "pink 2", "pink 3", "pink 4", "pink 5", "pink 6", "pink 7", "pink 8", "pink 9",
        "blue +2", "blue +2", "red +2", "red +2", "green +2", "green +2", "pink +2", "pink +2",
        "blue +4", "blue +4", "red +4", "red +4", "green +4", "green +4", "pink +4", "pink +4"
        ] #add specials

def draw(player):
    card = ""
    draw = random.randint(0, (len(deck) - 1))
    card = deck[draw]
    deck.remove(card)

    if player == "1":
        player1deck.append(card)
    elif player == "2":
        player2deck.append(card)
    else:
        return card


def play(player, card, previous):
    global playend
    playend = False
    
    cp = previous.split(" ")[0]
    np = previous.split(" ")[1]
    if card == "draw":
        pass
    else:
        c = card.split(" ")[0]
        n = card.split(" ")[1]

        if n in specials:
            specialCol = True
        else:
            specialCol = False

        if n == np or c == cp or c == "Wild":
            print("Correct card!")
            if player == "1":
                player1deck.remove(card)
            if player == "2":
                player2deck.remove(card)
            if specialCol == True:
                SpecialCol(n, player)
            playend = True
        else:
            print("Invalid card. The last card was: " + previous)
            print("Try again") 

def SpecialCol(card, player):
    if card == "+2":
        if player == "1":
            print("HAHA! Player 2 must now draw 2 cards. Drawing...")
            sleep(2)
            draw("2")
            draw("2")
            show("2")
        else:
            print("HAHA! Player 1 must now draw 2 cards. Drawing...")
            sleep(2)
            draw("1")
            draw("1")
            show("1")
    elif card == "+4":
        if player == "1":
            print("HAHA! Player 2 must now draw 4 cards. Drawing...")
            sleep(2)
            draw("2")
            draw("2")
            draw("2")
            draw("2")
            show("2")
        else:
            print("HAHA! Player 1 must now draw 4 cards. Drawing...")
            sleep(2)
            draw("1")
            draw("1")
            draw("1")
            draw("1")
            show("1")
    elif card == "Reverse":
        pass #Switch Direction
    elif card == "Change Color":
        pass #Change color
    elif card == "Block":
        pass #Block turn

def show(player):
    print("EVERYONE LOOK AWAY EXCEPT FROM THE PLAYER")
    sleep(2)
    for i in range(0,100):
        print("LOOK AWAY")
    if player == "1":
        print(player1deck)
    else:
        print(player2deck)
    for i in range(1,7):
        sleep(1)
        print(0-i)
    for i in range(0,100):
        print(" ")

def check(deck, against):
    deck = str(deck)
    cp = against.split(" ")[0]
    np = against.split(" ")[1]

    if cp in deck or np in deck:
        return True
    else:
        return False

def main():
    global playend
    playend = False

    print("")#Rules, Game name, Stuff

    #setup
    for i in range(1,7):
        draw("1")
        draw("2")
    previous = draw("null")
    print("First card: " + previous)
    
    print("Player 1, your deck:")
    show("1")
    print("Player 2, your deck:")
    show("2")

    run = True
    while run:
        p1 = True
        while p1:
            print("")
            print("----------------Player 1's turn----------------")
            print("Card currently on top is: " + previous)
            print("You can:")
            print("Show cards [show]  - You will return to this screen after.")
            print("Play a card [play] - After doing this, your turn will end.")
            choice = input(">>>")
            if choice == "show":
                show("1")
            elif choice == "play":
                print("----------------Playing(P1)----------------")
                if check(player1deck, previous):
                    print("Play a card, format {color} {number/type} | eg: blue 2")
                    card = input(">>>")
                    if card in player1deck:
                        print("You have this card, playing...")
                        play("1", card, previous)
                        sleep(2)
                        if playend == True:
                            p1 = False
                    else:
                        print("You don't have this card")
                else:
                    print("You can't play any cards. Drawing...")
                    draw("1")
                    sleep(2)
                    show("1")
                    p1 = False

        for i in range(0,100):
            print("")
        previous = card            
        #check p1 isn't on 0 cards
        if len(player1deck) <= 0:
            run = False
            p1win = True
            
        if run:           
            p2 = True
            while p2:
                print("")
                print("----------------Player 2's turn----------------")
                print("Card currently on top is: " + previous)
                print("You can:")
                print("Show cards [show]  - You will return to this screen after.")
                print("Play a card [play] - After doing this, your turn will end.")
                choice = input(">>>")
                if choice == "show":
                    show("2")
                elif choice == "play":
                    print("----------------Playing(P2)----------------")
                    if check(player2deck, previous):
                        print("Play a card, format {color} {number/type} | eg: blue 2")
                        card = input(">>>")
                        if card in player2deck:
                            print("You have this card, playing...")
                            play("2", card, previous)
                            sleep(2)
                            if playend == True:
                                p2 = False
                        else:
                            print("You don't have this card")
                    else:
                        print("You can't play any cards. Drawing...")
                        draw("2")
                        sleep(2)
                        show("2")
                        p2 = False

        for i in range(0,100):
            print("")
        previous = card          
        #check p2 isn't on 0 cards
        if len(player2deck) <= 0:
            run = False
            p2win = True
            
    if p1win == True:
        print("PLAYER 1 WINS")
    elif p2win == True:
        print("PLAYER 2 WINS")
    else:
        print("No cards left.")
        print("DRAW")

main()
