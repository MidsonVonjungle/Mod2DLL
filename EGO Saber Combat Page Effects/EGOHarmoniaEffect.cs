namespace TheOrganizedSaberDLL.EGO_Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_EGOHarmoniaEffect_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "Recover 3 Light and draw 1 Card. [On Use] Use 2 Smoke to increase Power of all dice in this page by 2";

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(2);
            owner.allyCardDetail.DrawCards(1);

            var battleUnitBuf_smoke = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke) as BattleUnitBuf_smoke;
            if (battleUnitBuf_smoke != null && battleUnitBuf_smoke.stack >= 2)
            {
                battleUnitBuf_smoke.UseStack(3);
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = 2
                });
            }
        }
    }
}