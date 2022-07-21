using Game;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Game");
Console.Write("Enter Character Name >>");
var name = Console.ReadLine() ?? "HUMAN";

var userCharacter = new Human(name, 100, 10, 5,
    new List<DamageSkill> { new DamageSkill("Punch", 20, new List<BadStatusState> { }), new DamageSkill("A", 5, new List<BadStatusState>{})});
var monster = new Monster("Ghost", 100, 10, 5, new List<DamageSkill> { new DamageSkill("Kick", 10, new List<BadStatusState> { })});
var turnCount = 0;

while (userCharacter.isAlive() && monster.isAlive()) {
    turnCount++;
    Console.WriteLine($"=====================TURN: {turnCount}======================");
    userCharacter.ProcessPassiveEffect();
    Console.WriteLine();
    Console.Write("Your Turn: Attack[A] / UseSkill[S] /Nothing[N]");
    var turn = Console.ReadKey();
    Console.WriteLine();
    if (turn.KeyChar == 'A') {
        var monsterDamage = userCharacter.GiveDamage(monster);
        Console.WriteLine($"Your Turn: {monster.Name} damaged {monsterDamage}!!");
    }

    if (turn.KeyChar == 'S') {
        var skillName = "";
        while (userCharacter.Skills.Find(x => x.Name == skillName) == null) {
            Console.Write("Use Skill: Enter Skill Name>>");
            skillName = Console.ReadLine();
            Console.WriteLine();
        }
        var monsterDamage =userCharacter.GiveDamageBySkill(monster, skillName);
        Console.WriteLine($"Your Turn: Use Skill {skillName} >> {monster.Name} damaged {monsterDamage}!!");
    }

    if (turn.KeyChar == 'N') {
        Console.WriteLine("Your Turn: You do nothing");
    }

    if (userCharacter.BadStatusStates.Count() > 0) {
        userCharacter.CureBadStatus();
    }

    Console.WriteLine();
    Console.WriteLine($"Interval: {userCharacter.Name}: {userCharacter.HP} / {monster.Name}: {monster.HP}");
    if (!monster.isAlive()) {
        break;
    }
    Console.WriteLine();

    Console.WriteLine("Enemy Turn");
    monster.ProcessPassiveEffect();

    var userDamage = monster.GiveDamage(userCharacter);
    Console.WriteLine($"Enemy Turn: {userCharacter.Name} damaged {userDamage}!!");

    if (monster.BadStatusStates.Count() > 0) {
        monster.CureBadStatus();
    }

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