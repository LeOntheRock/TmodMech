using NewMod.Content.Items.Ammo;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewMod.Content.Items.Weapons.Range
{
    public class Ransetsu : ModItem
    {
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties,
            // such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 64; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 0.8f;
            Item.rare = ItemRarityID.Gray; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 16; // The item's use time in ticks (60 ticks == 1 second.) Note: this is a burst rifle
                              // so use time = 1/3 useAnimation
            Item.useAnimation = 48; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
            Item.reuseDelay = 30; //Delay between shots
            Item.consumeAmmoOnLastShotOnly = true; //Use one piece of Ammo per burst

            /* The sound that this item plays when used.
            Item.UseSound = new SoundStyle($"{NewMod}/Assets/Sound_Fxs/Items/Guns/Curtis)
            {
                Volume = 0.9f,
                PitchVariance = 0.2f,
                MaxInstances = 3,
            }*/

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 150; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 15f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 14f; // The speed of the projectile (measured in pixels per frame.) This value equivalent to Handgun
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }
    }
}
