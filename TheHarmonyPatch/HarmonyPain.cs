using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using Workshop;

namespace TheOrganizedSaberDLL.TheHarmonyPatch
{
    [HarmonyPatch]
    public class HarmoyPatch_Re21341
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BattleUnitView), "ChangeSkin")]
        public static void
            BattleUnitView_ChangeSkin(BattleUnitView __instance,
                string charName) //Allows changing Keypage skins midgame
        {
            if (!ModParameters.SkinNameIds.Exists(x => x.Item1.Contains(charName))) return;
            var skinInfo =
                typeof(BattleUnitView).GetField("_skinInfo", AccessTools.all)?.GetValue(__instance) as
                    BattleUnitView.SkinInfo;
            skinInfo.state = BattleUnitView.SkinState.Changed;
            skinInfo.skinName = charName;
            var currentMotionDetail = __instance.charAppearance.GetCurrentMotionDetail();
            __instance.DestroySkin();
            var gameObject =
                Object.Instantiate(
                    Singleton<AssetBundleManagerRemake>.Instance.LoadCharacterPrefab(charName, "",
                        out var resourceName), __instance.model.view.characterRotationCenter);
            var workshopBookSkinData =
                Singleton<CustomizingBookSkinLoader>.Instance.GetWorkshopBookSkinData(
                    ModParameters.PackageId, charName);
            gameObject.GetComponent<WorkshopSkinDataSetter>().SetData(workshopBookSkinData);
            __instance.charAppearance = gameObject.GetComponent<CharacterAppearance>();
            __instance.charAppearance.Initialize(resourceName);
            __instance.charAppearance.ChangeMotion(currentMotionDetail);
            __instance.charAppearance.ChangeLayer("Character");
            __instance.charAppearance.SetLibrarianOnlySprites(__instance.model.faction);
            __instance.model.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { charName };
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(WorkshopSkinDataSetter), "SetMotionData")]
        public static void WorkshopSkinDataSetter_SetMotionData(WorkshopSkinDataSetter __instance, ActionDetail motion)
        {
            if (__instance.Appearance.GetCharacterMotion(motion) != null) return;
            var item = SkinUtil.CopyCharacterMotion(__instance.Appearance, motion);
            __instance.Appearance._motionList.Add(item);
            if (__instance.Appearance._motionList.Count <= 0) return;
            foreach (var characterMotion in __instance.Appearance._motionList.Where(characterMotion =>
                         !__instance.Appearance.CharacterMotions.ContainsKey(characterMotion.actionDetail)))
            {
                __instance.Appearance.CharacterMotions.Add(characterMotion.actionDetail, characterMotion);
                characterMotion.gameObject.SetActive(false);
            }
        }
    }
}