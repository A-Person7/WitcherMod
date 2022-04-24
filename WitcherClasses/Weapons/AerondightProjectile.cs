﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using WitcherMod.WitcherClasses.Weapons.Signs.Igni;

namespace WitcherMod.WitcherClasses.Weapons
{
    public class AerondightProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Aerondight");
        }
        
        public override string Texture => "WitcherMod/WitcherClasses/Weapons/Aerondight";
        
        public override void SetDefaults() {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = int.MaxValue;
            projectile.timeLeft = 600;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.light = 0.5f;
            projectile.alpha = 255;
            projectile.scale = 1f;
            projectile.melee = true;
            
        }

        // public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
        //     Vector2 perturbedSpeed = new Vector2(-10, 10).RotatedByRandom(Math.PI * 2);
			     //
        //     // Projectile.NewProjectile(projectile.position.X, projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AerondightProjectile>(), damage, knockback, projectile.owner);
        // }
        
        // autogenerated, I have no idea how this works
        public override void AI() {
            projectile.rotation = (float) Math.Atan2((double) projectile.velocity.Y, (double) projectile.velocity.X) + 1.57f;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            
            if (projectile.timeLeft > 600)
            {
                projectile.timeLeft = 600;
            }
            
            if (projectile.ai[0] > 7f)
            {
                float num296 = 1f;
                if (projectile.ai[0] == 8f)
                {
                    num296 = 0.25f;
                }
                else if (projectile.ai[0] == 9f)
                {
                    num296 = 0.5f;
                }
                else if (projectile.ai[0] == 10f)
                {
                    num296 = 0.75f;
                }
            
                projectile.ai[0] += 1f;
                int num297 = (int) (projectile.position.X / 16f) - 1;
                int num298 = (int) ((projectile.position.X + (float) projectile.width) / 16f) + 2;
                int num299 = (int) (projectile.position.Y / 16f) - 1;
            }
        }
    }
}