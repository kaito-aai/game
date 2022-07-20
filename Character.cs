namespace Game {
    public enum BadStatus {
        Poison,
    }

    public class BadStatusState {
        public BadStatus badStatus { get; }
        public int turnUntilCure { get; private set; }

        public BadStatusState(BadStatus badStatus, int turnUntilCure)
        {
            this.badStatus = badStatus;
            this.turnUntilCure = turnUntilCure;
        }

        public void Cure()
        {
            if (this.turnUntilCure == 0)
            {
                return;
            }
            this.turnUntilCure--;
        }
    }

    public class Damage {
        public ushort DamagePoint { get; }

        public Damage(ushort damagePoint) {
            this.DamagePoint = damagePoint;
        }
    }

    public class DamageSkill : Damage {
        public string Name { get; }
        public ushort DamagePoint { get; }
        public List<BadStatusState> BadStatusStates { get; private set; } = new List<BadStatusState>();
        public DamageSkill(string name, ushort damagePoint, List<BadStatusState>? badStatusStates) : base(damagePoint) {
            this.Name = name;
            if (badStatusStates == null) {
                return;
            }
            this.BadStatusStates = badStatusStates;
        }
    }

    public abstract class Character {
        public string Name { get; set; }
        public ushort HP { get; set; }
        public ushort Attack { get; set; }
        public ushort Defense { get; set; }
        public List<DamageSkill> Skills { get; }
        public List<BadStatusState> BadStatusStates { get; set; } = new List<BadStatusState>();

        public Character(string name, ushort hp, ushort attack, ushort defense, List<DamageSkill> skills)
        {
            Name = name;
            HP = hp;
            Attack = attack;
            Defense = defense;
            Skills = skills;
        }

        public abstract void TakeDamage(Damage damage);
        public abstract void TakeDamageBySkill(DamageSkill damage);

        public abstract ushort GiveDamage(Character character);
        public abstract ushort GiveDamageBySkill(Character character, string skillName);

        public abstract void ProcessPassiveEffect();

        public bool isAlive()
        {
            return HP > 0;
        }

        public void CureBadStatus() {
            if (this.BadStatusStates.Count() == 0) {
                return;
            }

            this.BadStatusStates.ForEach(badStatus => {
                badStatus.Cure();
            });
        }
    }

    public class Human : Character {
        public Human(string name, ushort hp, ushort attack, ushort defense, List<DamageSkill> skills) : base(name, hp, attack, defense, skills)
        {
        }

        public override void TakeDamage(Damage damage)
        {
            var dmg = damage.DamagePoint - Defense;
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

        public override void TakeDamageBySkill(DamageSkill damage)
        {
            this.TakeDamage(damage);
            foreach (var bad in damage.BadStatusStates) {
                if (this.BadStatusStates.Find(x => x.badStatus == bad.badStatus) != null) {
                    continue;
                }
                BadStatusStates.Add(bad);
            }
        }

        public override ushort GiveDamage(Character character)
        {
            var beforeHp = character.HP;
            var damage = new Damage(Attack);
            character.TakeDamage(damage);
            return Convert.ToUInt16(beforeHp - character.HP);
        }

        public override ushort GiveDamageBySkill(Character character, string skillName)
        {
            var beforeHp = character.HP;
            var damage = Skills.Find(x => x.Name == skillName);
            if (damage == null) {
                throw new Exception("skill not found");
            }

            character.TakeDamageBySkill(damage);
            return Convert.ToUInt16(beforeHp - character.HP);
        }

        public override void ProcessPassiveEffect()
        {
            foreach (var badStatus in this.BadStatusStates) {
                if (badStatus.badStatus == BadStatus.Poison) {
                    var poisonDamage = 10;
                    if (HP < poisonDamage) {
                        HP = 0;
                        return;
                    }
                    HP -= 10;
                }
            }
        }
    }

    public class Monster : Character {
        public Monster(string name, ushort hp, ushort attack, ushort defense, List<DamageSkill> skills) : base(name, hp, attack, defense, skills)
        {
        }

        public override void TakeDamage(Damage damage)
        {
            var dmg = damage.DamagePoint - Defense;
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

        public override void TakeDamageBySkill(DamageSkill damage)
        {
            this.TakeDamage(damage);
            foreach (var bad in damage.BadStatusStates) {
                if (this.BadStatusStates.Find(x => x.badStatus == bad.badStatus) != null) {
                    continue;
                }
                BadStatusStates.Add(bad);
            }
        }

        public override ushort GiveDamage(Character character)
        {
            var beforeHp = character.HP;
            var damage = new Damage(Attack);
            character.TakeDamage(damage);
            return Convert.ToUInt16(beforeHp - character.HP);
        }

        public override ushort GiveDamageBySkill(Character character, string skillName)
        {
            var beforeHp = character.HP;
            var damage = Skills.Find(x => x.Name == skillName);
            if (damage == null) {
                throw new Exception("skill not found");
            }

            character.TakeDamageBySkill(damage);
            return Convert.ToUInt16(beforeHp - character.HP);
        }

        public override void ProcessPassiveEffect()
        {
            foreach (var badStatus in this.BadStatusStates) {
                if (badStatus.badStatus == BadStatus.Poison) {
                    var poisonDamage = 10;
                    if (HP < poisonDamage) {
                        HP = 0;
                        return;
                    }
                    HP -= 10;
                }
            }
        }
    }
}