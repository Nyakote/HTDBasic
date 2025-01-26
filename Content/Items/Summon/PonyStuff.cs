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
using HTDBasic.Content.Projectiles;

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
            Item.damage = 5;
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
            position.Y-= 100f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, Main.myPlayer);
            return false;
        }
    } 
    
}
