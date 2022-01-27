using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace TheOrganizedSaberDLL.TheHarmonyPatch
{
    public class TheModInitializer_Md5488 : ModInitializer
    {
        public override void OnInitializeMod() //Look for stuff to load in mods
        {
            ModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            new Harmony("LOR.Therealtest2_MOD").PatchAll();
            SkinUtil.LoadBookSkinsExtra();
            SkinUtil.GetArtWorks(
                new DirectoryInfo(ModParameters.Path +
                                  "/ArtWork")); //This will make use of a new folder in your Assemblies called 'ArtWork'
            SkinUtil.PreLoadBufIcons(); //This will load Buf Icons
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance);
            UnitUtil.ChangePassiveItem();
            LocalizeUtil
                .AddLocalize(); //This will add localization, which you'll need to give a description to your buf
        }
    }
}