namespace TheOrganizedSaberDLL.Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_SaberExhaustDraw1_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[On Use] Spend 2 Smoke to decrease the Cost and increase the Power of this Page by 1 for the Act (Max. 2 times)";

        private static readonly string _packageId = "Therealtest2";

        public override void OnUseCard()
        {
            var list = card.card.GetBufList().FindAll(x => x is DiceCardSelfAbility_costDown1self.CostDownSelfBuf);
            var battleUnitBuf_smoke = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke) as BattleUnitBuf_smoke;
            if (battleUnitBuf_smoke != null && battleUnitBuf_smoke.stack >= 2 && card.card.GetCost() != 0)
            {
                battleUnitBuf_smoke.UseStack(2);
                card.card.exhaust = true;
                owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 13));
            }
        }
    }
}