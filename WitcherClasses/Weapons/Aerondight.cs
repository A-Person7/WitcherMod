using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WitcherMod.WitcherClasses.Weapons {
	public class Aerondight : ModItem {
		// TODO - make more accurate to this effect (maybe):
		// https://witcher.fandom.com/wiki/Aerondight#:~:text=Each%20blow%20generates%20charges%20which%20increase%20sword%20damage%20by%2010%.%20Charges%20are%20lost%20over%20time%20or%20when%20receiving%20damage 
		
		// TODO - implement countdown to reset counter as well, add buff to show counter and/or time
		
		
		private const int DefaultDmg = 190;
		private static readonly int MaxBaseDmg = (int) Math.Pow(2, 16) - 1;
		private int _baseDamage = DefaultDmg;
		private const float DamageMultiplier = 1.1f;
		private int _numHitsConsecutive;

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Each consecutive hit without taking damage increases the damage dealt.");
		}
		
		public override void SetDefaults() {
			item.damage = _baseDamage;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 0;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;

			// item.shoot = ModContent.ProjectileType<AerondightProjectile>();
			// item.shootSpeed = 9f;
			
			item.autoReuse = true;
		}
		
		public override bool Shoot(Terraria.Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			// Vector2 perturbedSpeed = new Vector2(speedX, speedY);
			
			// Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			
			return false;
		}

		// to ensure the tool tip updates are correct for this instance
		public override bool CloneNewInstances => true;

		// silver swords shouldn't be able to damage non monsters
		public override bool CanHitPvp(Terraria.Player player, Terraria.Player target) {
			return false;
		}

		public override bool? CanHitNPC(Terraria.Player player, NPC target) {
			return !target.townNPC;
		}
		
		public override void OnHitNPC(Terraria.Player player, NPC target, int damage, float knockBack, bool crit) {
			if (_numHitsConsecutive < 10) {
				_numHitsConsecutive++;
			} else {
				_baseDamage = Damage();
				_numHitsConsecutive = 0;
			}
		}

		public override void ModifyHitNPC(Terraria.Player player,
			NPC target,
			ref int damage,
			ref float knockBack,
			ref bool crit) {
			crit = _numHitsConsecutive == 10 || crit;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");

			if (tt == null) {
				return;
			}

			tt.text = _baseDamage + " base damage, " + Damage() + " current";
		}
	
		public override void ModifyWeaponDamage(Terraria.Player player, ref float add, ref float mult, ref float flat) {
			item.damage = Damage();
		}

		private int Damage() {
			return (int) Math.Max(0, Math.Min(_baseDamage * Math.Pow(DamageMultiplier, _numHitsConsecutive), MaxBaseDmg));
		}

		public void TakeDamage() {
			Utility.Log("taking damage");
			_numHitsConsecutive = 0;
		}
	}
}