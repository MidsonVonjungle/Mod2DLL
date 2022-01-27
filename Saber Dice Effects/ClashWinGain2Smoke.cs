namespace TheOrganizedSaberDLL.Saber_Dice_Effects
{
    public class DiceCardAbility_ClashWinGain2Smoke_Md5488 : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Win] Gain 2 Smoke";

        public override void OnWinParrying()
        {
            {
                var target = card.target;
                if (target == null) return;
                target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Smoke, 2, owner);
            }
        }
    }
}