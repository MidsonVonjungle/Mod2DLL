namespace TheOrganizedSaberDLL.Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_MikealPageEffectSmoke_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] If the user has 4+ Smoke, all dice on this page gain 1 Power";

        public override void OnUseCard()
        {
            var activatedBuf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke);
            if (activatedBuf != null && activatedBuf.stack >= 4)
            {
                var currentDiceAction = owner.currentDiceAction;
                if (currentDiceAction == null) return;
                currentDiceAction.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = 1
                });
            }
        }
    }
}