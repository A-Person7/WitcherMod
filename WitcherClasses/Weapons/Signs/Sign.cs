using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WitcherMod.WitcherClasses.Weapons.Signs {
    public abstract class Sign : ModItem {
        // TODO - maybe make custom damage class
        
        // TODO - make class that takes all of this class' subclasses and sets them
        
        // call this by invoking base.SetDefaults in this method signature for classes that inherit this
        public override void SetDefaults() {
            item.magic = true;
            // TODO - maybe replace this to draw the symbol
            item.width = 0;
            item.height = 0;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }
        
        // set texture to terraria/none
        public override string Texture => "Terraria/Projectile_" + ProjectileID.None;


        public override bool? CanHitNPC(Terraria.Player player, NPC target) {
            return true;
        }
        
        public static Vector2 GetRandRotation(double angle, Vector2 originalPosition, Vector2 position) {
            return (position - originalPosition).RotatedByRandom(angle) + originalPosition;
        }
    }
}