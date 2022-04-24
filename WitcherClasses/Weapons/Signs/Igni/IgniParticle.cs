using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WitcherMod.WitcherClasses.Weapons.Signs.Igni
{
    public class IgniParticle : ModProjectile
    {
        private Vector2? _originalPosition;
        private readonly List<Dust> _dusts = new List<Dust>();

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Igni Projectile");
        }

        public override void SetDefaults() {
            projectile.width = 6; // The width of projectile hitbox
            projectile.height = 6; // The height of projectile hitbox
            projectile.alpha = 255; // This (being set to 255) makes the projectile invisible, only showing the dust.
            // projectile.alpha = 0; // This (being set to 255) makes the projectile invisible, only showing the dust.
            projectile.friendly = true; // Can the projectile deal damage to enemies?
            projectile.hostile = false; // Can the projectile deal damage to the player?
            projectile.penetrate = int.MaxValue;
            // projectile.timeLeft = 5; // A short life time for this projectile to get the flamethrower effect.
            projectile.timeLeft = 19; // A short life time for this projectile to get the flamethrower effect.
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.ranged = true;
            projectile.damage = 30;
            projectile.extraUpdates = 2;
        }

        // so I don't need to have an IgniParticle.png
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Flames;

        public override bool? CanHitNPC(NPC target) => true;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.Burning, 240);
        }

        public override void OnHitPlayer(Terraria.Player target, int damage, bool crit) {
            target.AddBuff(BuffID.Burning, 240, false);
        }

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
            if (projectile.wet)
            {
                projectile.Kill();
            }

            if (_originalPosition == null)
            {
                _originalPosition = projectile.position;
            }

            // make a for loop that runs 2-4 times
            for (int i = 0; i < 3; i++)
            {
                // Using a timer, we scale the earliest spawned dust smaller than the rest.
                float dustScale = 1f;

                if (projectile.ai[0] == 0f)
                {
                    dustScale = 0.25f;
                }
                else if (projectile.ai[0] == 1f)
                {
                    dustScale = 0.5f;
                }
                else if (projectile.ai[0] == 2f)
                {
                    dustScale = 0.75f;
                }

                if (Main.rand.Next(2) == 0)
                {
                    // float falloff = 0.2f;
                    // float falloff = AltFired ? Main.rand.NextFloat(0.25f) + 0.75f : 3.5f;
                    // float falloff = AltFired ? Main.rand.NextFloat(0.25f) + 0.75f : 1.5f;
                    // float falloff = Main.rand.NextFloat(AltFired ? 0.75f : 2.5f);
                    float falloff = Main.rand.NextFloat(AltFired ? 0.0f : 2.5f);
                    // float falloff = 0f;
                    // float falloff = 1;

                    Dust dust;

                    Vector2 dustPos = Sign.GetRandRotation(
                        MathHelper.ToRadians(AltFired ? 0 : 20),
                        _originalPosition.Value, projectile.position
                    );

                    // dustPos = projectile.position;

                    // Vector2 previousPos = dustPos;

                    if (Utility.CollidesWithTile(dustPos))
                    {
                        continue;
                    }


                    if (AltFired)
                    {
                        dust = Dust.NewDustDirect(dustPos,
                            projectile.width, projectile.height, DustID.Fire,
                            projectile.velocity.X * falloff, projectile.velocity.Y * falloff, 100,
                            Color.DarkRed);
                    }
                    else
                    {
                        dust = Dust.NewDustDirect(dustPos,
                            projectile.width, projectile.height, DustID.Fire,
                            projectile.velocity.X * falloff, projectile.velocity.Y * falloff, 100);
                    }

                    // Some dust will be large, the others small and with gravity, to give visual variety.
                    if (Main.rand.NextBool(3))
                    {
                        dust.noGravity = true;
                        // dust.scale *= 3f;
                        dust.scale *= 1.5f;
                        // dust.velocity.X *= 2f;
                        // dust.velocity.Y *= 2f;
                    }

                    // dust.scale = 900;

                    dust.scale *= 1.5f;
                    // dust.velocity *= 1.2f;
                    dust.scale *= dustScale;

                    // add dust to _dusts
                    _dusts.Add(dust);
                }
            }

            CheckForDustCollision();

            projectile.ai[0] += 1f;
        }

        // override the kill method to be like in aardparticle
        public override void Kill(int timeLeft) {
            foreach (Dust d in _dusts)
            {
                KillDust(d);
            }

            base.Kill(timeLeft);
        }

        private void CheckForDustCollision() {
            foreach (Dust dust in _dusts.Where(d => Utility.CollidesWithTile(d.position)))
            {
                KillDust(dust);
            }
        }

        private void KillDust(Dust dust) {
            dust.velocity /= 2f;
            dust.scale /= 1.6f;
        }

        private bool AltFired => Main.player[Main.myPlayer].altFunctionUse == 2;
    }
}