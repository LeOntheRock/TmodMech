using Microsoft.Build.Evaluation;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewMod.Content.Items.Ammo
{
    public class MechBullet : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 12; // The damage of ammo (not combined with weapon)
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; // Item will be consumed when used
            Item.knockBack = 1.5f;
            Item.value = 30; // Item's value in copper coins
            Item.rare = ItemRarityID.White;
            Item.shoot = ModContent.ProjectileType<Projectiles.MechBullet>(); // The projectile that weapons fire when using this item as ammunition.
            Item.shootSpeed = 4.5f; // The speed of the projectile. This value equivalent to Silver Bullet 
            Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
        }
    }
}