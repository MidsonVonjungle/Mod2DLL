namespace TheOrganizedSaberDLL.Saber_Passives
{
    public class PassiveAbility_SmokeRush_Md5488 : PassiveAbilityBase
    {
        public override void OnCreated()
        {
            name = Singleton<PassiveDescXmlList>.Instance.GetName(10008);
            desc = Singleton<PassiveDescXmlList>.Instance.GetDesc(10008);
        }

        public override int SpeedDiceNumAdder()
        {
            if (owner.bufListDetail.GetKewordBufStack(KeywordBuf.Smoke) >= 4) return 1;
            return 0;
        }

        public override void OnRollSpeedDice()
        {
            var SpeedDice = owner.speedDiceCount;
            if (SpeedDice <= 0) return;

            for (var i = 0; i < SpeedDice; i++)
                if (!owner.speedDiceResult[i].breaked)
                    if (owner.bufListDetail.GetKewordBufStack(KeywordBuf.Smoke) >= 9)
                    {
                        // owner.GetSpeed(i);
                        owner.speedDiceResult[i].value = 999;
                        i = SpeedDice;
                    }
        }

        public override void OnWaveStart()
        {
            var PuffyBrume = owner.passiveDetail.AddPassive(new PassiveAbility_240026());
            PuffyBrume.Hide();
        }
    }
}