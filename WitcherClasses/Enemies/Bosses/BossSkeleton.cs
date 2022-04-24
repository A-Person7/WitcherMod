using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace WitcherMod.WitcherClasses.Enemies.Bosses
{
    public abstract class BossSkeleton : ModNPC
    {
        // if the boss is in one of its moves
        private bool _captured;
        protected MethodInfo currentMove;
        protected MethodInfo[][] movesetByPhase;
        protected readonly int numPhases;
        protected int phase;

        protected BossSkeleton() {
            Init();
        }
        
        protected void Init() {
            phase = 0;

            MethodInfo[] allMethods = GetType().GetMethods();
            
            for (int i = 0; i < numPhases; i++)
            {
                movesetByPhase[i] = allMethods.Where(m=>((MoveAttribute) m.GetCustomAttribute(
                    typeof(MoveAttribute))).Phases.Contains(i)).ToArray();
            }
        }

        protected void SetMove() {
            if (_captured)
            {
                return;
            }

            currentMove = movesetByPhase[phase][Main.rand.Next(movesetByPhase[phase].Length)];
            _captured = true;
        }
    }
}