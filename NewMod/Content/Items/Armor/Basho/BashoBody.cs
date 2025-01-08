using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewMod.Content.Items.Armor.Basho
{
    [AutoloadEquip(EquipType.Body)]
    public class BashoBody : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32; // Item's width
            Item.height = 32; // Item's height
            Item.value = Item.buyPrice(gold: 200); // Item's value in coins (copper,gold,etc...)
            Item.rare = ItemRarityID.Gray; // Item's rarity
            Item.defense = 75; // Item's defensive stats
        }

        public override void UpdateEquip(Player player)
        {
            player.jumpSpeedBoost -= 0.01f; // Item decreases player's jump speed
            player.statLifeMax2 += 40; // Item gives player 40 extra health
            player.endurance = 1f - (0.1f * (1f - player.endurance)); // Item gives player 10% damage reduction
        }
    }
}
