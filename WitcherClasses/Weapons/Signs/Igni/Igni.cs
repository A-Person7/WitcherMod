using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WitcherMod.WitcherClasses.Weapons.Signs.Igni {
    public class Igni : Sign {
        private int _lastAltAnimationFrame;

        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Try this on the guide");
        }

        public override void SetDefaults() {
            base.SetDefaults();

            item.damage = 50;
            // TODO - test the useTime
            item.useTime = 2;
            item.useAnimation = 20;
            item.knockBack = 2;
            item.value = 10000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item34;
            item.shoot = ModContent.ProjectileType<IgniParticle>();
            item.shootSpeed = 9f;
            item.autoReuse = false;
            
            item.noUseGraphic = true;
        }

        public override bool AltFunctionUse(Terraria.Player player) => true;
        
        public override bool CanUseItem(Terraria.Player player) {
            // TODO - implement checking for stamina here
            return !player.wet;
        }
        
        public override bool Shoot(Terraria.Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            AdjustAnimation();
            
            int numberProjectiles = 10 + Main.rand.Next(6); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++) {
                int degreeSpread = AltUse ? 9 : 30;

                Vector2 perturbedSpeed =
                    new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(degreeSpread));

                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
           }
            
            
            return false; // return false because we don't want tmodloader to shoot projectile
        }
        
        // // todo - determine if needed
        // public override Vector2? HoldoutOffset() {
        //     // HoldoutOffset has to return a Vector2 because it needs two values (an X and Y value) to move your flamethrower sprite. Think of it as moving a point on a cartesian plane.
        //     return new Vector2(-2, 0); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
        // }

        private void AdjustAnimation() {
            if (!AltUse) {
                return;
            }

            // TODO - reminder: this checks for right clicking, any future changes of the key to q will need to change this
            // if the alt fire button is held and the item animation is decreasing
            if (Main.mouseRight && Main.LocalPlayer.itemAnimation < _lastAltAnimationFrame) {
                Main.LocalPlayer.itemAnimation = Main.LocalPlayer.itemAnimationMax;
            }
            else if (Main.LocalPlayer.itemAnimation > 0) {
                // speed the animation up (when the hand is going back down)
                Main.LocalPlayer.itemAnimation--;
            }

            _lastAltAnimationFrame = Main.LocalPlayer.itemAnimation;
        }

        private bool AltUse => Main.LocalPlayer.altFunctionUse == 2;
    }
}