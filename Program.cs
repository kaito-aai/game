using Game;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Game");
Console.Write("Enter Character Name >>");
var name = Console.ReadLine() ?? "HUMAN";

var userCharacter = new Human(name, 100, 10, 5);
var monster = new Monster("Ghost", 100, 10, 5);
var turnCount = 0;

while (userCharacter.isAlive() && monster.isAlive()) {
    turnCount++;
    Console.WriteLine($"=====================TURN: {turnCount}======================");
    Console.WriteLine();
    Console.Write("Your Turn: Attack[A] / Nothing[N]");
    var turn = Console.ReadKey();
    Console.WriteLine();
    if (turn.KeyChar == 'A') {
        var monsterDamage = userCharacter.GiveDamage(monster);
        Console.WriteLine($"Your Turn: {monster.Name} damaged {monsterDamage}!!");
    }

    if (turn.KeyChar == 'N') {
        Console.WriteLine("Your Turn: You do nothing");
    }

    Console.WriteLine();
    Console.WriteLine($"Interval: {userCharacter.Name}: {userCharacter.HP} / {monster.Name}: {monster.HP}");
    if (!monster.isAlive()) {
        break;
    }
    Console.WriteLine();

    Console.WriteLine("Enemy Turn");

    var userDamage = monster.GiveDamage(userCharacter);
    Console.WriteLine($"Enemy Turn: {userCharacter.Name} damaged {userDamage}!!");

    Console.WriteLine($"Interval: {userCharacter.Name}: {userCharacter.HP} / {monster.Name}: {monster.HP}");
    Console.WriteLine();
}

if (!userCharacter.isAlive()) {
    Console.WriteLine("You Lose");
    Console.ReadKey();
    return;
}

Console.WriteLine("You Win!!");
Console.ReadKey();
return;