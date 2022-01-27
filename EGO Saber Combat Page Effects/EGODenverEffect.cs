namespace TheOrganizedSaberDLL.EGO_Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_EGODenverEffect_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "Recover 2 Light and draw 1 Card. [Start of Clash] Use 2 Smoke to reduce Power of all target's dice by 2";

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(2);
            owner.allyCardDetail.DrawCards(1);
        }

        public override void OnStartParrying()
        {
            var battleUnitBuf_smoke = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke) as BattleUnitBuf_smoke;
            if (battleUnitBuf_smoke != null && battleUnitBuf_smoke.stack >= 2)
            {
                battleUnitBuf_smoke.UseStack(2);
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
    }
}