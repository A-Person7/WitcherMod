using Terraria.ID;
using Terraria.ModLoader;

namespace WitcherMod.WitcherClasses.Armor {
    [AutoloadEquip(EquipType.Head)]
    public class ProfSunglasses : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Rule of cool.");
        }
        
        public override void SetDefaults() {
            item.width = 14;
            item.height = 10;
            item.value = 10000;
            item.rare = ItemRarityID.LightRed;
            item.vanity = true;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair) {
            drawHair = true;
            drawAltHair = false;
        }
    }
}