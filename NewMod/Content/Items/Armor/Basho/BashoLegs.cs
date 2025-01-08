using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewMod.Content.Items.Armor.Basho
{
    [AutoloadEquip(EquipType.Legs)]
    public class BashoLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32; // Item's width
            Item.height = 32; // Item's height
            Item.value = Item.sellPrice(gold: 100); // Item's value in coins (copper,gold,etc...)
            Item.rare = ItemRarityID.Gray; // Item's rarity
            Item.defense = 65; // Item's defensive stats
        }

        // UpdateEquip gives player an effect upon equiping the item
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f; // Item increases player's movement speed
            player.jumpSpeedBoost -= 0.01f; // Item decreases player's jump speed
        }

    }
}
