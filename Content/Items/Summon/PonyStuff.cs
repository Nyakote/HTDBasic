using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using HTDBasic.Content.Buffs;
using HTDBasic.Content.Items.Summon;
using HTDBasic.Content.Projectiles.Minions;

namespace HTDBasic.Content.Items.Summon
{
    public class PonyStuff : ModItem
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
            Item.buffType = ModContent.BuffType<PonyBuff>();

        }

        private int lastSummonedIndex = -1; 

   

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = player.Center;
            position.Y-= 100f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Add the buff to the player
            player.AddBuff(Item.buffType, 2);

            // List of minion projectiles to summon from
            int[] minionProjectiles = new int[]
            {
            ModContent.ProjectileType<PonyMinion_RD>(),
     ModContent.ProjectileType<PonyMinion_RR>(),
     ModContent.ProjectileType<PonyMinion_F>(),
     ModContent.ProjectileType<PonyMinion_PP>(),
     ModContent.ProjectileType<PonyMinion_TW>(),
     ModContent.ProjectileType<PonyMinion_AJ>()
            };
            int randomIndex = Main.rand.Next(minionProjectiles.Length);
            int randomMinionType = minionProjectiles[randomIndex];


            Main.NewText($"Summoning minion: {randomMinionType}", Microsoft.Xna.Framework.Color.Green);


            Vector2 spawnPosition = player.Center + new Vector2(0, -50);


            int projectileID = Projectile.NewProjectile(source, spawnPosition, velocity, randomMinionType, damage, knockback, player.whoAmI);

       
            if (Main.projectile[projectileID] != null)
            {
                Main.NewText($"Summoned projectile with ID: {projectileID}", Microsoft.Xna.Framework.Color.Blue);
            }
            else
            {
                Main.NewText($"Failed to summon the projectile.", Microsoft.Xna.Framework.Color.Red);
            }

            return false; 
        }


    }

}
