namespace TheOrganizedSaberDLL.Saber_Dice_Effects
{
    internal class DiceCardAbility_OnHitGain1Endurance_Md5488 : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Gain 1 Endurance next scene";

        public override void OnSucceedAttack()
        {
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, owner);
        }
    }
}