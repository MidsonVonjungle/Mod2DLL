namespace TheOrganizedSaberDLL.EGO_Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_EGOVictor_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            " If Speed is 6 or higher, all dice on this page gain +2 Power; [On Use] Use 4 Smoke to gain 1 Strength and 1 Endurance this scene";

        public override void OnUseCard()
        {
            var battleUnitBuf_smoke = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke) as BattleUnitBuf_smoke;
            if (battleUnitBuf_smoke != null && battleUnitBuf_smoke.stack >= 4)
            {
                battleUnitBuf_smoke.UseStack(4);
                card.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 1, owner);
                card.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Endurance, 1, owner);
            }

            if (card.speedDiceResultValue >= 6)
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = 2
                });
        }
    }
}