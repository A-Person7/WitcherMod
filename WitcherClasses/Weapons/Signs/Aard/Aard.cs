using Microsoft.Xna.Framework;
using Terraria;

namespace WitcherMod.WitcherClasses.Weapons.Signs.Aard
{
    // When push comes to shove...
    public class Aard : WSign
    {
        public Aard() {
            Damage = 150;
            Knockback = 25;
            ShootProjectile = typeof(AardParticle);
            ShootSpeed = 9f;
        }
        
        public override void Shoot(Terraria.Player player, Vector2 position, float speedX, float speedY, int damage, float knockBack) {
            int numberProjectiles = 10 + Main.rand.Next(6); 
            for (int i = 0; i < numberProjectiles; i++) {
                const int degreeSpread = 30;

                Vector2 perturbedSpeed =
                    new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(degreeSpread));

                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, Type, damage, knockBack, player.whoAmI);
            }
        }

        // // todo - determine if needed
        // public override Vector2? HoldoutOffset() {
        //     // HoldoutOffset has to return a Vector2 because it needs two values (an X and Y value) to move your flamethrower sprite. Think of it as moving a point on a cartesian plane.
        //     return new Vector2(0, -2); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
        // }
    }
}