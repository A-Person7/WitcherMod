using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using WitcherMod.WitcherClasses.Weapons;
using WitcherMod.WitcherClasses.Weapons.Signs.Aard;
using WitcherMod.WitcherClasses.Weapons.Signs.Igni;

namespace WitcherMod.WitcherClasses.Player {
    public class WitcherPlayer : ModPlayer
    {
        private bool signClicked;
        private bool lastInputDown;
        
        private Item _heldItem;
        private readonly Dictionary<Signs, Item> _signs;
        
        private enum Signs
        {
            Igni,
            Aard,
            Aaxi,
            Quen,
            Yrden
        }
        
        // maps each sign enum to its corresponding Type
        private static readonly Dictionary<Signs, Type> _signTypes = new Dictionary<Signs, Type>
        {
            {Signs.Igni, typeof(Igni)},
            {Signs.Aard, typeof(Aard)},
            // {Signs.Aaxi, typeof(Aaxi)},
            // {Signs.Quen, typeof(Quen)},
            // {Signs.Yrden, typeof(Yrden)}
        };

        public WitcherPlayer() {      
            _signs = new Dictionary<Signs, Item>();
        
            _heldItem = null;
        
            foreach (Signs sign in Enum.GetValues(typeof(Signs))) {
                if (!_signTypes.ContainsKey(sign))
                {
                    Utility.Log("Failed to add " + sign + " due to missing type in _signTypes");
                    return;
                }
                
                Item item = new Item();
                
                int itemType = (int) typeof(ModContent)
                    .GetMethod(nameof(ModContent.ItemType))
                    ?.MakeGenericMethod(_signTypes[sign])
                    .Invoke(null, new Object[] {});
                
                item.SetDefaults(itemType);
                
                _signs.Add(sign, item);
            }
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

        private bool IsSignButtonPressed => Main.mouseXButton2;
        
        public override void PreUpdate() {
            if (IsSignButtonPressed && !lastInputDown)
            {
                signClicked = !signClicked;
                
                if (signClicked)
                {
                    Utility.Log("Sign clicked");
                    OnSign();
                } else if (_heldItem != null)
                {
                    Utility.Log("Sign released");
                    // OffSign();
                }
            }

            lastInputDown = IsSignButtonPressed;
            // Utility.Log(signClicked);
        }

        private void OnSign() {
            if (!player.inventory[player.selectedItem].Equals(_signs[Signs.Igni]))
            {
                Utility.Log("Saving last held item");
                _heldItem = player.inventory[player.selectedItem];
            }
                    
            player.inventory[player.selectedItem] = _signs[Signs.Igni];

            Item i = new Item();
            i.SetDefaults(ModContent.ItemType<Igni>());
            player.inventory[player.selectedItem] = i;
        }

        private void OffSign() {
            Utility.Log("resetting item");
            player.inventory[player.selectedItem] = _heldItem;
            _heldItem = null;
        }
    }
}