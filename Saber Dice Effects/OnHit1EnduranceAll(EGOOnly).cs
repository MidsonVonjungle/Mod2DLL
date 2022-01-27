namespace TheOrganizedSaberDLL.Saber_Dice_Effects
{
    internal class DiceCardAbility_OnHitGain1EnduranceAllFriend_Md5488 : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Give 1 Endurance to all allies the next scene";

        public override void OnSucceedAttack()
        {
            foreach (var battleUnitModel in BattleObjectManager.instance.GetAliveList(owner.faction))
                battleUnitModel.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, owner);
        }
    }
}