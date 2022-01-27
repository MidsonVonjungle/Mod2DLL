namespace TheOrganizedSaberDLL.Saber_Dice_Effects
{
    internal class DiceCardAbility_UnclashedGain1Light_Md5488 : DiceCardAbilityBase
    {
        public static string Desc = "[Unclashed] Restore 1 Light";

        public override void BeforeRollDice()
        {
            if (!behavior.IsParrying()) owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
    }
}