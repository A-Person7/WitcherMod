using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WitcherMod.WitcherClasses.Weapons.Signs.Aard
{
    public class Aard : Sign
    {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("When push comes to shove...");
        }

        public override void SetDefaults() {
            base.SetDefaults();
            
            item.damage = 150;
            // TODO - test the useTime
            item.useTime = 2;
            item.useAnimation = 20;
            item.knockBack = 25;
            item.value = 10000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item34;
            item.shoot = ModContent.ProjectileType<AardParticle>();
            item.shootSpeed = 9f;
            item.autoReuse = false;
        }
        
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Bananarang;
        
        public override bool Shoot(Terraria.Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {

            int numberProjectiles = 10 + Main.rand.Next(6); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++) {
                int degreeSpread = 30;

                Vector2 perturbedSpeed =
                    new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(degreeSpread));

                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            
            
            return false; // return false because we don't want tmodloader to shoot projectile
        }
        
        // // todo - determine if needed
        // public override Vector2? HoldoutOffset() {
        //     // HoldoutOffset has to return a Vector2 because it needs two values (an X and Y value) to move your flamethrower sprite. Think of it as moving a point on a cartesian plane.
        //     return new Vector2(0, -2); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
        // }
    }
}