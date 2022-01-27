namespace TheOrganizedSaberDLL.Saber_Custom_Buf
{
    public class BattleUnitBuf_EGOSmokeBuff_Md5488 : BattleUnitBuf
    {
        public BattleUnitBuf_EGOSmokeBuff_Md5488()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "EGOSmokeBuff"; //Check the buf ID for the description
        protected override string keywordIconId => "EGOSmokeIcon"; //Check the filename for the icon
        public override bool isAssimilation => true;

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }

        public override void OnRoundStart()
        {
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Smoke, 10, _owner);
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            if (IsAttackDice(behavior.Detail))
            {
                var target = behavior.card.target;
                if (target == null) return;
                target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Smoke, 1, _owner);
            }
        }

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            var target = behavior.card.target;
            if (target == null) return;
            target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Smoke, 1, _owner);
        }
    }
}