namespace TheOrganizedSaberDLL.EGO_Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_EGOSaberEffect_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "If opponent has 6 or more smoke, all their dice lose 2 power; [On Use] Use 3 smoke reduce the cost of this page by 1 for the rest of the act (up to 3 times)";

        private int SaberLimit;

        public override void OnUseCard()
        {
            var battleUnitBuf_smoke = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke) as BattleUnitBuf_smoke;
            if (battleUnitBuf_smoke != null && battleUnitBuf_smoke.stack >= 3 && SaberLimit < 3 &&
                card.card.GetCost() != 0)
            {
                battleUnitBuf_smoke.UseStack(3);
                card.card.AddBuf(new CostDownSelfBuf());
                SaberLimit += 1;
            }
        }

        public override void OnStartParrying()
        {
            var battleUnitBuf_smoke = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke) as BattleUnitBuf_smoke;
            if (card.target.bufListDetail.GetKewordBufStack(KeywordBuf.Smoke) >= 6)
            {
                var target = card.target;
                if (target == null) return;
                var currentDiceAction = target.currentDiceAction;
                if (currentDiceAction == null) return;
                currentDiceAction.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = -2
                });
            }
        }

        public class CostDownSelfBuf : BattleDiceCardBuf
        {
            public override int GetCost(int oldCost) // Update the new cost
            {
                return oldCost - 1; // Reduce the cost by 1
            }
        }
    }
}