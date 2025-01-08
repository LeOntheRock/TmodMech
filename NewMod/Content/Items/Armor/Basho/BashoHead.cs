using NewMod.Content.Items.Armor.Basho;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewMod.Content.Items.Armor.Basho
{
    [AutoloadEquip(EquipType.Head)]
    public class BashoHead : ModItem
    {
        public static readonly int BashoBonus = 20;
        
        public static LocalizedText BashoBonusText {  get; private set; }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Replaces NPCs' heads
            // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Hat sits on top of NPCs' hair
            // ArmorIDs.Head.sets.DrawFullHair[Item.headSlot] = true; // Hair is visible, ex: glasses, masks, etc.
            BashoBonusText = this.GetLocalization("SetBonus").WithFormatArgs(BashoBonus);
        }
        public override void SetDefaults()
        {
            Item.width = 32; // Item's width
            Item.height = 32; // Item's height
            Item.value = Item.sellPrice(gold: 100); // Item's value in copper/silver/gold/platnium coins
            Item.rare = ItemRarityID.Gray; // Item's rarity
            Item.defense = 50; // Item's defensive stats
        }

        // IsArmorSet checks if player is wearing a matching set of armor
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BashoBody>() && legs.type == ModContent.ItemType<BashoLegs>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = BashoBonusText.Value;
            player.GetDamage(DamageClass.Melee) += BashoBonus / 100f;
        }
    }
}
