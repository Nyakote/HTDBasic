using HTDBasic.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HTDBasic.Content.Items.Range
{
    public class LateranoGun : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 39;
            Item.height = 13;
            Item.scale = 2f;
            Item.rare = ItemRarityID.Green;

            Item.useTime = 50; 
            Item.useAnimation = 50; 
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.autoReuse = true;

            Item.UseSound = new SoundStyle($"{nameof(HTDBasic)}/Content/Sounds/lateranoweapon_projectile_start")
            {
                Volume = 0.8f,
                PitchVariance = 0.2f,
                MaxInstances = 3
            };
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 100; 
            Item.knockBack = 5f; 
            Item.noMelee = true;

            Item.shoot = ModContent.ProjectileType<LateranoRocket>(); 
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Rocket;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
/*        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ExampleItem>()
                .AddTile<Tiles.Furniture.ExampleWorkbench>()
                .Register();
        }*/

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12f, -2f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
/*            // Every projectile shot from this gun has a 1/3 chance of being an ExampleInstancedProjectile
            if (Main.rand.NextBool(3))
            {
                type = ModContent.ProjectileType<ExampleInstancedProjectile>();
            }*/
        }

    }
}
