namespace Game {
    public abstract class Character {
        public string Name { get; set; }
        public ushort HP { get; set; }
        public ushort Attack { get; set; }
        public ushort Defense { get; set; }

        public Character(string name, ushort hp, ushort attack, ushort defense)
        {
            Name = name;
            HP = hp;
            Attack = attack;
            Defense = defense;
        }

        public abstract void TakeDamage(ushort damage);

        public abstract ushort GiveDamage(Character character);

        public bool isAlive()
        {
            return HP > 0;
        }
    }

    public class Human : Character {
        public Human(string name, ushort hp, ushort attack, ushort defense) : base(name, hp, attack, defense)
        {
        }

        public override void TakeDamage(ushort damage)
        {
            var dmg = damage - Defense;
            if (dmg < 0) {
                return;
            }
            var ushortDmg = Convert.ToUInt16(dmg);
            if (HP < ushortDmg) {
                HP = 0;
                return;
            }
            HP -= ushortDmg;
        }

        public override ushort GiveDamage(Character character)
        {
            var beforeHp = character.HP;
            character.TakeDamage(Attack);
            return Convert.ToUInt16(beforeHp - character.HP);
        }
    }

    public class Monster : Character {
        public Monster(string name, ushort hp, ushort attack, ushort defense) : base(name, hp, attack, defense)
        {
        }

        public override void TakeDamage(ushort damage)
        {
            var dmg = damage - Defense;
            if (dmg < 0) {
                return;
            }
            var ushortDmg = Convert.ToUInt16(dmg);
            if (HP < ushortDmg) {
                HP = 0;
                return;
            }
            HP -= ushortDmg;
        }

        public override ushort GiveDamage(Character character)
        {
            var beforeHp = character.HP;
            character.TakeDamage(Attack);
            return Convert.ToUInt16(beforeHp - character.HP);
        }
    }
}