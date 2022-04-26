using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WitcherMod.WitcherClasses.Weapons.Signs {
    // Witcher Sign, as opposed to whatever Sign is used for in vanilla
    public abstract class WSign {
        // TODO - delegate input to the player (and probably whether that input is an alt input too)
        // TODO - make some methods have default implementations
        
        protected int LastAltAnimationFrame;
        
        public bool Shootable => ShootProjectile != null;
        
        // ReSharper disable once PossibleNullReferenceException (blame tmodloader if this is null)
        protected int Type => (int) typeof(ModContent)
                .GetMethod(nameof(ModContent.ProjectileType))
                ?.MakeGenericMethod(ShootProjectile)
                .Invoke(null, new object[] {});

        private Type _shootProjectile;
        protected Type ShootProjectile
        {
            get => _shootProjectile;
            set
            {
                if (!value.IsSubclassOf(typeof(ModProjectile)))
                {
                    Exception e = new ArgumentException("Projectile must be a subclass of ModProjectile");
                    Utility.Log(e);
                    throw e;
                }
                _shootProjectile = value;
            }
        }

        protected LegacySoundStyle Sound { get; set; } = SoundID.Item34;
        
        protected int Damage { get; set; }
        protected int Knockback { get; set; }
        protected float ShootSpeed { get; set; }
        
        // TODO - implement stamina cost/cooldown/reuse time

        
        // for future reference
        // item.height = 0;
        //     item.noMelee = true;
        //     item.noUseGraphic = true;
        //     item.useStyle = ItemUseStyleID.HoldingOut;

        public abstract void Shoot(Terraria.Player player, Vector2 position, float speedX, float speedY, 
            int damage, float knockBack);

        // default implementation
        public virtual void AdjustAnimation(Terraria.Player player) {
            if (player.itemAnimation > 0) {
                // speed the animation up (when the hand is going back down)
                player.itemAnimation--;
            }

            LastAltAnimationFrame = player.itemAnimation;
        }
        
        public static Vector2 GetRandRotation(double angle, Vector2 originalPosition, Vector2 position) {
            return (position - originalPosition).RotatedByRandom(angle) + originalPosition;
        }

        public virtual bool CanUseSign(Terraria.Player player) {
            return true;
        }
        
        public void ShootHelper (Terraria.Player player, out float speedX, out float speedY, out int damage, out float knockBack) {
            // TODO - look how crits + rand deviation are done in vanilla
            
            float angle = (Main.MouseWorld - player.Center).ToRotation();

            speedX = (float) (ShootSpeed * Math.Cos(angle));
            speedY = (float) (ShootSpeed * Math.Sin(angle));
            damage = Damage;
            knockBack = Knockback;
        }
        
        // TODO - update with custom key binding
        protected static bool AltUse => Main.LocalPlayer.altFunctionUse == 2;
    }
}