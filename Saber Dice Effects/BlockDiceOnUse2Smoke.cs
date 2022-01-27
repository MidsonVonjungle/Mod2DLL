namespace TheOrganizedSaberDLL.Saber_Dice_Effects
{
    public class DiceCardAbility_BlockDiceOnUse2Smoke_Md5488 : DiceCardAbilityBase
    {
        public static string Desc = "[On Play] Gain 2 Smoke";

        public override void AfterAction()
        {
            {
                var target = card.target;
                if (target == null) return;
                target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Smoke, 2, owner);
            }
        }
    }
}