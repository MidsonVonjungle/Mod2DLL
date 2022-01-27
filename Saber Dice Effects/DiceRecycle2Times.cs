namespace TheOrganizedSaberDLL.Saber_Dice_Effects
{
    public class DiceCardAbility_DiceRecycle2Times_Md5488 : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Recycle this die 2 times";
        private int _repeatCount;

        public override void OnSucceedAttack()
        {
            if (!owner.IsBreakLifeZero() && _repeatCount < 2)
                // If the user's life isn't 0 and the repeat is not at Max yet
            {
                _repeatCount++; // Increase the repeat counter by +1
                ActivateBonusAttackDice(); // Recycle this die
            }
        }
    }
}