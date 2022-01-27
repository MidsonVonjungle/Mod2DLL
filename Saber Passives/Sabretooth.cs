namespace TheOrganizedSaberDLL.Saber_Passives
{
    public class PassiveAbility_Sabretooth_Md5488 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            owner.allyCardDetail.DrawCards(2);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            var power = 0;
            // Check for a target has the smoke amount
            if (behavior.card.target.bufListDetail.GetKewordBufStack(KeywordBuf.Smoke) >= 6)
            {
                var battleCardResultLog = owner.battleCardResultLog;
                if (battleCardResultLog != null) battleCardResultLog.SetPassiveAbility(this);
                power -= 1;
            }

            if (behavior.card.target.bufListDetail.GetKewordBufStack(KeywordBuf.Smoke) >= 9)
            {
                var battleCardResultLog = owner.battleCardResultLog;
                if (battleCardResultLog != null) battleCardResultLog.SetPassiveAbility(this);
                power -= 1;
            }

            behavior.TargetDice.ApplyDiceStatBonus(new DiceStatBonus
            {
                power = power
            });

            if (behavior.card.owner.bufListDetail.GetKewordBufStack(KeywordBuf.Smoke) >= 4) power = 1;
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                power = power
            });
        }
    }
}