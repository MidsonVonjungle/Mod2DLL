namespace TheOrganizedSaberDLL.Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_EvelynPageEffect_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Use 8 Smoke to boost the power of all dice on this page by +8";

        public override void OnUseCard()
        {
            var battleUnitBuf_smoke = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke) as BattleUnitBuf_smoke;
            if (battleUnitBuf_smoke != null && battleUnitBuf_smoke.stack >= 8 && card.card.GetCost() != 0)
            {
                battleUnitBuf_smoke.UseStack(8);
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = 8
                });
            }
        }
    }
}