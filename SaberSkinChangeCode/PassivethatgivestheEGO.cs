using System.Collections.Generic;
using TheOrganizedSaberDLL.Saber_Custom_Buf;
using TheOrganizedSaberDLL.TheHarmonyPatch;

namespace TheOrganizedSaberDLL.SaberSkinChangeCode
{
    public class PassiveAbility_SaberEGOPassiveThing_Md5488 : PassiveAbilityBase
    {
        private static string _packageId = "Therealtest2";
        private MechUtilBase _util;

        public override void OnBattleEnd()
        {
            if (_util.CheckSkinChangeIsActive())
                owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "SaberSkin2" };
        }

        public override void OnRoundEnd()
        {
            _util = new MechUtilBase(new MechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                SkinName = "EGOSaber",
                EgoType = typeof(BattleUnitBuf_EGOSmokeBuff_Md5488),
                EgoCardId = new LorId(ModParameters.PackageId, 11),
                HasEgoAttack = false
            });
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
        }

        public override void OnRoundStartAfter()
        {
            if (_util.EgoCheck()) _util.EgoActive();
        }
    }
}