using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using HTDBasic.Content.Buffs;
using HTDBasic.Content.Projectiles.Minions;
using HTDBasic.Content.Projectiles;
using HTDBasic.Content.Other;

namespace HTDBasic.Content.Items.Summon
{
    public class PonyStaff : ModItem
    {   
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item44;

            Item.DamageType = DamageClass.Summon;
            Item.damage = 50;
            Item.knockBack = 5;
            Item.mana = 10;
            Item.noMelee = true;

            Item.value = 1;
            Item.shoot = ModContent.ProjectileType<PonyMinion>();
            Item.buffType = ModContent.BuffType<PonyBuff>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = player.Center;
            position.Y -= 100f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            HTD_Player htdPlayer = player.GetModPlayer<HTD_Player>();

            if (player.altFunctionUse == 2 && htdPlayer.attackCooldown_PonyStaff == 0 && player.statMana >= 200)
            {
                player.AddBuff(116, 120);
                player.statMana -= 200; 
                htdPlayer.attackCooldown_PonyStaff = htdPlayer.CooldownTime_PonyStaff;
                return true;
            }
            else if (player.altFunctionUse == 2 && htdPlayer.attackCooldown_PonyStaff > 0)
            {
                Main.NewText("Attack isn't ready! " + (htdPlayer.attackCooldown_PonyStaff / 60) + " seconds left.", Microsoft.Xna.Framework.Color.Red);
                return false;
            }

            else if (player.altFunctionUse == 2 && player.statMana < 200)
            {
                Main.NewText("This attack requires 200 or more mana points!", Microsoft.Xna.Framework.Color.Red);
                return false;
            }
            else return true;
        }


        private int[] IndexRememberer = new int[3];
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            int[] minionProjectiles = new int[]
            {
                ModContent.ProjectileType<PonyMinion_RD>(),
                ModContent.ProjectileType<PonyMinion_RR>(),
                ModContent.ProjectileType<PonyMinion_F>(),
                ModContent.ProjectileType<PonyMinion_PP>(),
                ModContent.ProjectileType<PonyMinion_TW>(),
                ModContent.ProjectileType<PonyMinion_AJ>()
            };

            int activeMinionCount = 0;
            int randomIndex = 0;

            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.owner == player.whoAmI && minionProjectiles.Contains(proj.type))
                {
                    activeMinionCount++;
                }
            }

            if (activeMinionCount < 3)
            {
                do
                {
                    randomIndex = Main.rand.Next(minionProjectiles.Length);
                } while (IndexRememberer.Contains(randomIndex));
                IndexRememberer[activeMinionCount] = randomIndex;
            }
            else
            {
                do
                {
                    randomIndex = Main.rand.Next(minionProjectiles.Length);
                } while (IndexRememberer.Contains(randomIndex));

                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.active && proj.owner == player.whoAmI && proj.type == minionProjectiles[IndexRememberer[0]])
                    {
                        proj.Kill();
                        break;
                    }
                }

                IndexRememberer[0] = IndexRememberer[1];
                IndexRememberer[1] = IndexRememberer[2];
                IndexRememberer[2] = randomIndex;
            }

            int randomMinionType = minionProjectiles[randomIndex];
            Vector2 spawnPosition = player.Center + new Vector2(0, -50);
            int projectileID = Projectile.NewProjectile(source, spawnPosition, velocity, randomMinionType, damage, knockback, player.whoAmI);

            return false;
        }
    }
}
