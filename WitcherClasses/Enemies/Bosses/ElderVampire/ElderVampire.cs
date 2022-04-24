using Terraria.ID;

namespace WitcherMod.WitcherClasses.Enemies.Bosses.ElderVampire
{
    // creates a boss with a unique attack pattern that has moves with the MoveAttributes of the BossMove class
    public class ElderVampire : BossSkeleton
    {
        // private int health;
        // private int damage;
        // private int defense;
        // private int magicDefense;
        // private int magicDamage;
        // private int speed;
        // private int attackRange;
        // private int attackSpeed;
        // private int attackType;
        //
        // private int moveRange;
        // private int moveDamage;
        // private int moveDefense;

        // creates an instance of this class with 100 health, 100 damage, 100 defense, 100 magicDefense, 100 magicDamage, 100 speed, 100 attackRange, 100 attackSpeed, 100 attackType, 100 moveRange, 100 moveDamage, 100 moveDefense
        public ElderVampire() : base()
        {
            // health = 100;
            // damage = 100;
            // defense = 100;
            // magicDefense = 100;
            // magicDamage = 100;
            // speed = 100;
            // attackRange = 100;
            // attackSpeed = 100;
            // attackType = 100;
            // moveRange = 100;
            // moveDamage = 100;
            // moveDefense = 100;
        }
        
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Bananarang;
    }
}