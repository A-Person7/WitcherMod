using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Utils = On.Terraria.Utils;

namespace WitcherMod.WitcherClasses.Weapons.Signs.Aard
{
    public class AardParticle : ModProjectile
    {
        private Vector2? _originalPosition = null;
        private readonly List<Dust> _dusts = new List<Dust>();
        
        public override void SetDefaults() {
            projectile.width = 6; // The width of projectile hitbox
            projectile.height = 6; // The height of projectile hitbox
            projectile.alpha = 255; // This (being set to 255) makes the projectile invisible, only showing the dust.
            projectile.friendly = true; // Can the projectile deal damage to enemies?
            projectile.hostile = false; // Can the projectile deal damage to the player?
            projectile.penetrate = int.MaxValue;
            projectile.timeLeft = 15;
            // projectile.timeLeft = 19; // A short life time for this projectile to get the flamethrower effect.
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.ranged = true;
            // projectile.damage = 20;
            projectile.damage = 5;
            // projectile.extraUpdates = 2;
        }
        
        public override string Texture => "Terraria/Projectile_" + ProjectileID.None;

        public override bool? CanHitNPC(NPC target) => true;

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            // By using ModifyDamageHitbox, we can allow the flames to damage enemies in a larger area than normal without colliding with tiles.
            // Here we adjust the damage hitbox. We adjust the normal 6x6 hitbox and make it 66x66 while moving it left and up to keep it centered.
            int size = 30;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }

        public override void AI() {
            if (_originalPosition == null)
            {
                _originalPosition = projectile.position;
            }

            
            // Using a timer, we scale the earliest spawned dust smaller than the rest.
            float dustScale = 1f;

            if (projectile.ai[0] == 0f)
            {
                dustScale = 0.25f;
            }

            else if (projectile.ai[0] == 1f)
            {
                // dustScale = 0.5f;
                dustScale = 0.35f;
            }
            else if (projectile.ai[0] == 2f)
            {
                // dustScale = 0.75f;
                dustScale = 0.35f;
                // dustScale = 0f;
            }

            // if (Main.rand.Next(3) == 0)
            if (Main.rand.Next(2) == 0)
            {
                // float falloff = 0.2f;
                float falloff = 2.5f;
                // float falloff = 0f;

                Vector2 dustPos = WSign.GetRandRotation(
                    MathHelper.ToRadians(10),
                    _originalPosition.Value, projectile.position
                );
                
                // Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Cloud,
                // projectile.velocity.X * falloff, projectile.velocity.Y * falloff, 30,
                // Color.White);
                
                Dust dust = Dust.NewDustDirect(dustPos, projectile.width, projectile.height, DustID.Cloud,
                    projectile.velocity.X * falloff, projectile.velocity.Y * falloff, 30,
                    Color.White);
                
                // Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Cloud,
                //         projectile.velocity.X * falloff, projectile.velocity.Y * falloff, 50);


                // Some dust will be large, the others small and with gravity, to give visual variety.
                if (Main.rand.NextBool(3))
                {
                    dust.noGravity = true;
                    // dust.scale *= 3f;
                    // dust.scale *= 1.5f;
                    // dust.velocity.X *= 2f;
                    // dust.velocity.Y *= 2f;
                }

                dust.scale *= 0.8f;
                dust.velocity *= 1.2f;
                // dust.scale *= dustScale;
                
                _dusts.Add(dust);
            }

            projectile.ai[0] += 1f;
        }

        public override void Kill(int timeLeft) {
            foreach (Dust dust in _dusts)
            {
                dust.active = false;
            }
            _dusts.Clear();
            
            base.Kill(timeLeft);
        }
    }
}