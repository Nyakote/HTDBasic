using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HTDBasic.Content.Projectiles
{

    public class HE308mm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 5;
          
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.6f; // Adjust gravity as needed

            // Rotate the projectile to match its movement direction
            Projectile.rotation = Projectile.velocity.ToRotation();

            // Flip the sprite vertically if moving left
            if (Projectile.spriteDirection == 1 && Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
            }
            else if (Projectile.spriteDirection == -1 && Projectile.velocity.X > 0f)
            {
                Projectile.spriteDirection = 1;
            }
        }

        public override void SetDefaults()
        {
            Projectile.width = 10; // Width of the projectile hitbox
            Projectile.height = 10; // Height of the projectile hitbox

            Projectile.friendly = true; // Whether it can damage enemies
            Projectile.hostile = false; // Whether it can damage the player

            Projectile.penetrate = 1; // Number of enemies the projectile can hit before disappearing
            Projectile.DamageType = DamageClass.Summon; // Use Summon damage
            Projectile.damage = 300;
            Projectile.tileCollide = true; // Whether it can collide with tiles
            Projectile.timeLeft = 300; // Time in frames before the projectile despawns

        }
            public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 posd = target.position;
            SoundEngine.PlaySound(new SoundStyle($"{nameof(HTDBasic)}/Content/Sounds/308HE_Canon"), posd);
            for (int i = 0; i < 4; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TintableDust, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dustIndex].scale = 5f;
                Main.dust[dustIndex].fadeIn = 1.5f;
                Main.dust[dustIndex].noGravity = false;
                Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy((double)Projectile.rotation, default(Vector2)) * 1.1f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dustIndex].scale = 10f;
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2 - 6)).RotatedBy((double)Projectile.rotation, default(Vector2)) * 1.1f;
            }

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 2; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TintableDust, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dustIndex].scale = 5f; // (float)Main.rand.Next(5) * 0.1f;
                Main.dust[dustIndex].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy((double)Projectile.rotation, default(Vector2)) * 1.1f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dustIndex].scale = 10f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2 - 6)).RotatedBy((double)Projectile.rotation, default(Vector2)) * 1.1f;
            }
            return true;
        }
    }
}

