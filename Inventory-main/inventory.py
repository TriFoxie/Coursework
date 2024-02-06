
#itemlist:
#Basic Sword = a
bSword = 0
#Basic Bow = b
bBow = 0
#Basic Staff = c
bStaff = 0
#Health pot = d
hPot = 0
#Mana pot = e
mPot = 0


inventory = ""


def readInv():

    global inventory, bSword, bBow, bStaff, hPot, mPot, invList

    print("Enter filename (no .txt): ")
    sname = input("")
    
    inv = open(sname + ".txt", "r")
    inventory = str(inv.read())
    inv.close()

    a = inventory.count("a")
    bSword = a
    b = inventory.count("b")
    bBow = b
    c = inventory.count("c")
    bStaff = c
    d = inventory.count("d")
    hPot = d
    e = inventory.count("e")
    mPot = e
    

    invList = [0, 0, 0, 0, 0]
    invList.insert(1, bSword)
    invList.insert(2, bBow)
    invList.insert(3, bStaff)
    invList.insert(4, hPot)
    invList.insert(5, mPot)

def showInv():

    global inventory, bSword, bBow, bStaff, hPot, mPot, invList

    if invList[1] > 0:
        print("Basic Sword: " + str(invList[1]))
    if invList[2] > 0:
        print("Basic Bow: " + str(invList[2]))
    if invList[3] > 0:
        print("Basic Staff: " + str(invList[3]))
    if invList[4] > 0:
        print("Health Pot: " + str(invList[4]))
    if invList[5] > 0:
        print("Mana Pot: " + str(invList[5]))

def addInv():
    
    global inventory, bSword, bBow, bStaff, hPot, mPot, invList

    print("Add something: ")
    print("Basic Sword [a]")
    print("Basic Bow [b]")
    print("Basic Staff [c]")
    print("Health Pot [d]")
    print("Mana Pot [e]")
    add = input("")

    if add == "a":
        if bSword < 10:
            bSword = bSword + 1
    elif add == "b":
        if bBow < 10:
            bBow = bBow + 1
    elif add == "c":
        if bStaff < 10:
            bStaff = bStaff + 1
    elif add == "d":
        if hPot < 10:
            hPot = hPot + 1
    elif add == "e":
        if mPot < 10:
            mPot = mPot + 1

    invList = [0, 0, 0, 0, 0]
    invList.insert(1, bSword)
    invList.insert(2, bBow)
    invList.insert(3, bStaff)
    invList.insert(4, hPot)
    invList.insert(5, mPot)

def remove():

    global inventory, bSword, bBow, bStaff, hPot, mPot, invList
    
    print("Remove something: ")
    print("Basic Sword [a]")
    print("Basic Bow [b]")
    print("Basic Staff [c]")
    print("Health Pot [d]")
    print("Mana Pot [e]")
    remove = input("")

    if remove == "a":
        bSword = bSword - 1
        if bSword == 0:
            bSword = bSword + 1
    elif remove == "b":
        bBow = bBow - 1
        if bBow == 0:
            bBow = bBow + 1
    elif remove == "c":
        bStaff = bStaff - 1
        if bStaff == 0:
            bStaff = bStaff + 1
    elif remove == "d":
        hPot = hPot - 1
        if hPot == 0:
            hPot = hPot + 1
    elif remove == "e":
        mPot = mPot - 1
        if mPot == 0:
            mPot = mPot + 1

    invList = [0, 0, 0, 0, 0]
    invList.insert(1, bSword)
    invList.insert(2, bBow)
    invList.insert(3, bStaff)
    invList.insert(4, hPot)
    invList.insert(5, mPot)

def save():
    
    global inventory, bSword, bBow, bStaff, hPot, mPot, invList

    Sname = input("Enter a save name: ")

    a = bSword
    b = bBow
    c = bStaff
    d = hPot
    e = mPot

    print(a)

    file = open(Sname + ".txt", "w")
    while a > 0:
        file.write("a")
        a = a - 1
    print("20%......")
    while b > 0:
        file.write("b")
        b = b - 1
    print("40%.....")
    while c > 0:
        file.write("c")
        c = c - 1
    print("60%.....")
    while d > 0:
        file.write("d")
        d = d - 1
    print("80%.....")
    while e > 0:
        file.write("e")
        e = e - 1
    print("100%....")
    print("Saved to " + Sname + ".txt")

#main
readInv()
    
q = True
while q == True:
    print("-----INVENTORY------")
    showInv()

    print("")
    print("Add item: [1]")
    print("Remove item: [2]")
    print("Reset to file: [r]")
    print("Save and quit: [x]")
    z = input("")

    if z == "1":
        addInv()
    elif z == "2":
        remove()
    elif z.lower() == "r":
        readInv()
    elif z.lower() == "x":
        save()
        q = False
    
