namespace TheOrganizedSaberDLL.Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_GalePageEffect_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "If this character has 3 or less Smoke, reduce the cost of this page by 1; [On Use] Gain 8 Smoke";

        private bool Cond;

        public override void OnRoundStart_inHand(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (unit.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke).stack > 3)
                Cond = true;
            else
                Cond = false;
        }

        public override int GetCostAdder(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (Cond) return 0;
            return -1;
        }

        public override void OnUseCard()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Smoke, 8);
        }
    }
}