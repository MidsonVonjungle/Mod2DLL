using TheOrganizedSaberDLL.Saber_Custom_Buf;

namespace TheOrganizedSaberDLL.Saber_Passives
{
    public class PassiveAbility_OptionalEGOSaber_Md5488 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_EGOSmokeBuff_Md5488
            {
                stack = 0
            });
            owner.bufListDetail.HasAssimilation();
        }
    }
}