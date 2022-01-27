using TheOrganizedSaberDLL.Saber_Custom_Buf;

namespace TheOrganizedSaberDLL.Saber_Combat_Page_Effects
{
    public class DiceCardSelfAbility_GrantEGOEffect_Md5488 : DiceCardSelfAbilityBase
    {
        private static readonly string _packageId = "Therealtest2";

        public static string Desc =
            "Manifest EGO; Exhaust all pages and draw upgraded versions of them; Recover all Light and Stagger Resist";

        public override void OnUseCard()
        {
            var hand = owner.allyCardDetail
                .GetHand(); // This makes a list called 'hand', that contains all pages that were in the hand
            var deck = owner.allyCardDetail
                .GetAllDeck(); // This makes a list called 'deck'. It contains both the hand and deck.

            foreach (var cardx in deck) // This will read all the pages in the hand and deck
                owner.allyCardDetail.ExhaustACardAnywhere(cardx); // Exhaust the cards immediately
            // This will exhaust all pages the user had, both in the deck and hand

            owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 1)); // All EGO Cards
            owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 2));
            owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 4));
            owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 8));
            owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 19));
            owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 20));
            owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 21));
            owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 22));
            owner.allyCardDetail.AddNewCardToDeck(new LorId(_packageId, 23));

            owner.allyCardDetail.DrawCards(6); // Draw 6 pages
        }
    }
}