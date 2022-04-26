using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using WitcherMod.WitcherClasses.Weapons;
using WitcherMod.WitcherClasses.Weapons.Signs;
using WitcherMod.WitcherClasses.Weapons.Signs.Aard;
using WitcherMod.WitcherClasses.Weapons.Signs.Igni;

namespace WitcherMod.WitcherClasses.Player {
    public class WitcherPlayer : ModPlayer
    {
        private bool _signClicked;
        private bool _lastInputDown;

        private enum Signs
        {
            Igni,
            Aard,
            Aaxi,
            Quen,
            Yrden
        }
        
        // maps each sign enum to its corresponding Type
        private readonly Dictionary<Signs, WSign> _signs = new Dictionary<Signs, WSign>
        {
            {Signs.Igni, new Igni()},
            {Signs.Aard, new Aard()},
            // {Signs.Aaxi, new Aaxi()},
            // {Signs.Quen, new Quen()},
            // {Signs.Yrden, new Yrden()}
        };
        
        private Signs _currentSign;

        private WSign CurrentSign => _signs[_currentSign];
        
        public WitcherPlayer() {      
            _currentSign = Signs.Igni;
        }

        // TODO - you know the thing
        // private int cooldownFrames;
        
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit) {
            foreach (Item i in player.inventory) {
                if (i.modItem is Aerondight aerondight) {
                    aerondight.TakeDamage();
                }
            }
        }

        private static bool IsSignButtonPressed => Main.mouseXButton2;
        
        public override void PreUpdate() {
            if (IsSignButtonPressed)
            {
                OnSign(CurrentSign);
            }
        }

        private void OnSign(WSign s) {
            if (!s.CanUseSign(player))
            {
                return;
            }

            s.AdjustAnimation(player);

            if (s.Shootable)
            {
                s.ShootHelper(player, out float speedX, out float speedY, out int damage, out float knockBack);
                s.Shoot(player, player.Center, speedX, speedY, damage, knockBack);
            }
            // TODO - call all the Sign hooks here appropriately
        }
    }
}