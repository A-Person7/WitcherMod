using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace WitcherMod.WitcherClasses.Weapons.Signs.Igni {
    public class Igni : WSign {
        

        // item.useAnimation = 20;
            
        public Igni() {
            ShootProjectile = typeof(IgniParticle);
            Damage = 50;
            Knockback = 2;
            ShootSpeed = 9f;
        }
        
        public override bool CanUseSign(Terraria.Player player) {
            // TODO - implement checking for stamina here (or in the base class and call it from here)
            return !player.wet;
        }
        
        public override void Shoot(Terraria.Player player, Vector2 position, float speedX, float speedY, int damage, float knockBack) {
            int numberProjectiles = 10 + Main.rand.Next(6); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++) {
                int degreeSpread = AltUse ? 9 : 30;

                Vector2 perturbedSpeed =
                    new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(degreeSpread));

                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, Type, damage, knockBack, player.whoAmI);
            }
        }
        
        // // todo - determine if needed
        // public override Vector2? HoldoutOffset() {
        //     // HoldoutOffset has to return a Vector2 because it needs two values (an X and Y value) to move your flamethrower sprite. Think of it as moving a point on a cartesian plane.
        //     return new Vector2(-2, 0); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
        // }

        public override void AdjustAnimation(Terraria.Player player) {
            if (!AltUse) {
                return;
            }

            // TODO - reminder: this checks for right clicking, any future changes of the key to q will need to change this
            // if the alt fire button is held and the item animation is decreasing
            if (Main.mouseRight && Main.LocalPlayer.itemAnimation < LastAltAnimationFrame) {
                player.itemAnimation = player.itemAnimationMax;
            }
            else if (player.itemAnimation > 0) {
                // speed the animation up (when the hand is going back down)
                player.itemAnimation--;
            }

            LastAltAnimationFrame = player.itemAnimation;
        }
    }
}