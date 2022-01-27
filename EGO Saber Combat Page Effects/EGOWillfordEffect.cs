namespace TheOrganizedSaberDLL.EGO_Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_EGOWillfordEffect_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[Start of Clash] Use 4 Smoke to make all dice on this page recover 3 Stagger on hit";

        public override void OnUseCard()
        {
            var battleUnitBuf_smoke = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke) as BattleUnitBuf_smoke;
            if (battleUnitBuf_smoke != null && battleUnitBuf_smoke.stack >= 2)
            {
                battleUnitBuf_smoke.UseStack(2);
                card.ApplyDiceAbility(DiceMatch.AllAttackDice, new DiceCardAbility_recoverBreak3atk());
            }
        }
    }
}