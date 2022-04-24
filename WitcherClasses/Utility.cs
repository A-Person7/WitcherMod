using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace WitcherMod.WitcherClasses {
    public static class Utility {
        public static void Log(Object o) {
            Log(o.ToString());
        }
        public static void Log(String message) {
            Main.NewText(message + " " + DateTime.Now, 255, 255, 255, true);
        }

        public static void Log(String message, byte r, byte g, byte b) {
            Main.NewText(message + " " + DateTime.Now, r, g, b, true);
        }
        
        public static bool CollidesWithTile(Vector2 position) {
            return !position.HasNaNs() && Collision.SolidCollision(position, 1, 1);
        }
        
        public static bool CollidesWithTile(Vector2 position, int width, int height) {
            return !position.HasNaNs() && Collision.SolidCollision(position, width, height);
        }
        
        public class DebugException : Exception {
            public DebugException(String message) : base(message) {
                // do nothing
            }
        }
    }
}