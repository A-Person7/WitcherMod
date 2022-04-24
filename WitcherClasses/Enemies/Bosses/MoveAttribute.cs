namespace WitcherMod.WitcherClasses.Enemies.Bosses
{
    public class MoveAttribute : System.Attribute
    {
        public readonly int[] Phases;

        public MoveAttribute(int[] phases) {
            Phases = phases;
        }
    }
}