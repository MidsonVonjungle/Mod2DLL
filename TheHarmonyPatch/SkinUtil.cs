using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using Workshop;
using Object = UnityEngine.Object;

namespace TheOrganizedSaberDLL.TheHarmonyPatch
{
    public static class SkinUtil
    {
        public static void GetArtWorks(DirectoryInfo dir) //Get images and artwork from the ArtWorkFolder
        {
            if (dir.GetDirectories().Length != 0)
            {
                var directories = dir.GetDirectories();
                foreach (var t in directories) GetArtWorks(t);
            }

            foreach (var fileInfo in dir.GetFiles())
            {
                var texture2D = new Texture2D(2, 2);
                texture2D.LoadImage(File.ReadAllBytes(fileInfo.FullName));
                var value = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height),
                    new Vector2(0f, 0f));
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                ModParameters.ArtWorks[fileNameWithoutExtension] = value;
            }
        }

        public static void PreLoadBufIcons() //Add Custom Buff Icons
        {
            foreach (var baseGameIcon in Resources.LoadAll<Sprite>("Sprites/BufIconSheet/")
                         .Where(x => !BattleUnitBuf._bufIconDictionary.ContainsKey(x.name)))
                BattleUnitBuf._bufIconDictionary.Add(baseGameIcon.name, baseGameIcon);
            foreach (var artWork in ModParameters.ArtWorks.Where(x =>
                         !x.Key.Contains("Glow") && !x.Key.Contains("Default") &&
                         !BattleUnitBuf._bufIconDictionary.ContainsKey(x.Key)))
                BattleUnitBuf._bufIconDictionary.Add(artWork.Key, artWork.Value);
        }

        public static void LoadBookSkinsExtra() //Add custom sprites
        {
            try
            {
                var dictionary =
                    typeof(CustomizingBookSkinLoader).GetField("_bookSkinData", AccessTools.all)
                            ?.GetValue(Singleton<CustomizingBookSkinLoader>.Instance) as
                        Dictionary<string, List<WorkshopSkinData>>;
                foreach (var item in ModParameters.SkinParameters)
                {
                    var workshopSkinData =
                        dictionary?[ModParameters.PackageId].Find(x => x.dataName.Contains(item.Name));
                    var clothCustomizeData = workshopSkinData.dic[ActionDetail.Default];
                    foreach (var skinData in item.SkinParameters.Where(x =>
                                 !workshopSkinData.dic.ContainsKey(x.Motion)))
                    {
                        var value = new ClothCustomizeData
                        {
                            spritePath = clothCustomizeData.spritePath.Replace("Default.png", skinData.FileName),
                            frontSpritePath = clothCustomizeData.spritePath.Replace("Default.png", skinData.FileName),
                            hasFrontSprite = clothCustomizeData.hasFrontSprite,
                            pivotPos = new Vector2((skinData.PivotPosX + 512f) / 1024f,
                                (skinData.PivotPosY + 512f) / 1024f),
                            headPos = new Vector2(skinData.PivotHeadX / 100f, skinData.PivotHeadY / 100f),
                            headRotation = skinData.HeadRotation,
                            direction = CharacterMotion.MotionDirection.FrontView,
                            headEnabled = clothCustomizeData.headEnabled,
                            hasFrontSpriteFile = clothCustomizeData.hasFrontSpriteFile,
                            hasSpriteFile = clothCustomizeData.hasSpriteFile
                        };
                        workshopSkinData.dic.Add(skinData.Motion, value);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static CharacterMotion
            CopyCharacterMotion(CharacterAppearance apprearance,
                ActionDetail detail) //Copy the files in the sprite folder for the new ingame sprites
        {
            var characterMotion = Object.Instantiate(apprearance._motionList[0]);
            characterMotion.transform.parent = apprearance._motionList[0].transform.parent;
            characterMotion.transform.name = apprearance._motionList[0].transform.name;
            characterMotion.actionDetail = detail;
            characterMotion.motionSpriteSet.Clear();
            characterMotion.motionSpriteSet.Add(new SpriteSet(
                characterMotion.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>(),
                CharacterAppearanceType.Body));
            characterMotion.motionSpriteSet.Add(new SpriteSet(
                characterMotion.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>(),
                CharacterAppearanceType.Head));
            characterMotion.motionSpriteSet.Add(new SpriteSet(
                characterMotion.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>(),
                CharacterAppearanceType.Body));
            characterMotion.transform.localScale = new Vector3(1, 1, 1);
            return characterMotion;
        }
    }
}