namespace TheOrganizedSaberDLL.Saber_Dice_Effects
{
    public class DiceCardAbility_ClashWinGain2StrengthNextScene_Md5488 : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Win] Gain 2 Strength the Next Scene";

        public override void OnWinParrying()
        {
            {
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 2, owner);
            }
        }
    }
}