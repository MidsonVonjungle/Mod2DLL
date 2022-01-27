namespace TheOrganizedSaberDLL.EGO_Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_EGOFaulknerEffect_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[On Use] Use 4 Smoke to gain 2 Protection and 2 Stagger Protection this scene and the next";

        public override void OnUseCard()
        {
            var battleUnitBuf_smoke = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke) as BattleUnitBuf_smoke;
            if (battleUnitBuf_smoke != null && battleUnitBuf_smoke.stack >= 4)
            {
                battleUnitBuf_smoke.UseStack(4);
                card.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Protection, 2, owner);
                card.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.BreakProtection, 2, owner);

                card.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Protection, 1, owner);
                card.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.BreakProtection, 1, owner);
            }
        }
    }
}