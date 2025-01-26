using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Minimap;
using HTDBasic.Content.Projectiles;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System;
namespace HTDBasic.Content.Items.Magic
{
    public class BadBone : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {

            Item.DefaultToStaff(ModContent.ProjectileType<BadSkull>(), 600, 1, 2);
            Item.SetWeaponValues(9999, 8, 1);
            Item.autoReuse = false;
            Item.shootSpeed = 1;
            Item.ArmorPenetration = 9999999;

            Item.useTime = 600;


            // Set damage and knockBack
            Item.UseSound = new SoundStyle($"{nameof(HTDBasic)}/Content/Sounds/BadSkull")
            {
                Volume = 0.9f,
                PitchVariance = 0,
                MaxInstances = 3,
            };


        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 100);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity);

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }

    }
}