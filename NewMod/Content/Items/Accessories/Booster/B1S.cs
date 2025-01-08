using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using NewMod.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;
using System.Threading;

namespace NewMod.Content.Items.Accessories.Booster
{
    [AutoloadEquip(EquipType.Wings)]
    public class B1S : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs
        // This code allows users to toggle loading this content via a config. Another common usage of IsLoadingEnabled would be to use ModLoader.HasMod to check if another mod is enabled or not.
        // Feel free to remove this method in your own Wings if using this as a template, it is superfluous.
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<NewModConfig>().WingsToggle;
        }

        public override void SetStaticDefaults()
        {
            // Fly time: 200 ticks = 3.33 seconds
            // Fly speed: 6
            // Acceleration multiplier: 1.5
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(200, 7f, 2f);
        }

        public override void SetDefaults()
        {
            Item.width = 25;
            Item.height = 25;
            Item.value = Item.buyPrice(gold: 150);
            Item.rare = ItemRarityID.Gray;
            Item.accessory = true;
            Item.mana = 5;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.25f; // Falling glide speed
            ascentWhenRising = 0.1f; // Rising speed
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 1f;
            constantAscend = 0.1f;
        }

        // UpdateAccessory give player effects upon equipping this item
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<QuickBoost>().DashAccessoryEquipped = true;
        }

        internal static object GetLocalization(string v)
        {
            throw new NotImplementedException();
        }
    }

    public class QuickBoost : ModPlayer
    {
        public const int DashRight = 2;
        public const int DashLeft = 3;

        public const int DashCooldown = 25; // 45 frames = 0.75 second cooldown
        public const int DashDuration = 45; // 45 frames = 0.75 second dash duration

        // The initial velocity. 10 velocity = 37.5 tiles/second or 50 mph
        public const float DashVelocity = 8f;

        public int DashDir = -1; // -1 = no dash double tap
        public bool DashAccessoryEquipped;
        public int DashDelay = 0; // Frames remaining till player can dash again
        public int DashTimer = 0; // Frames remaining in the dash

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, B1S.UpdateAccessory will be called and set the flag before PreUpdateMovement
            DashAccessoryEquipped = false;

            // ResetEffects is called not long after player.doubleTapCardinalTimer's values have been set
            // When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
            // If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
            if (Player.statMana >= 5 && Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15 && Player.doubleTapCardinalTimer[DashLeft] == 0)
            {
                DashDir = DashRight;
                Player.CheckMana(5, true);
            }
            else if (Player.statMana >= 5 && Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15 && Player.doubleTapCardinalTimer[DashRight] == 0)
            {
                DashDir = DashLeft;
                Player.CheckMana(5, true);
            }
            else
            {
                DashDir = -1;
            }

            // Add a mana consuming per second function
        }

        private bool CanUseDash()
        {
            return DashAccessoryEquipped
                && Player.dashType == DashID.None // No DashID = No Tabi or EoCShield equipped (give priority to those dashes)
                && !Player.setSolar // player isn't wearing solar armor
                && !Player.mount.Active; // player isn't mounted, since dashes on a mount look weird
        }

        public override void PreUpdateMovement()
        {
            // if the player can use our dash, has double tapped in a direction, and our dash isn't currently on cooldown
            if (CanUseDash() && DashDir != -1 && DashDelay == 0)
            {
                Vector2 newVelocity = Player.velocity;

                switch (DashDir)
                {
                    case DashLeft when Player.velocity.X > -DashVelocity:
                    case DashRight when Player.velocity.X < DashVelocity:
                        {
                            // X-velocity is set here
                            float dashDirection = DashDir == DashRight ? 1 : -1;
                            newVelocity.X = dashDirection * DashVelocity;
                            break;
                        }
                    default:
                        return; // not moving fast enough, so don't start our dash
                }

                // start our dash
                DashDelay = DashCooldown;
                DashTimer = DashDuration;
                Player.velocity = newVelocity;

                // Here you'd be able to set an effect that happens when the dash first activates
                // Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
            }

            if (DashDelay > 0)
                DashDelay--;

            if (DashTimer > 0)
            {
                Player.eocDash = DashTimer;
                Player.armorEffectDrawShadowEOCShield = true;

                // count down frames remaining
                DashTimer--;
            }
        }
    }
}