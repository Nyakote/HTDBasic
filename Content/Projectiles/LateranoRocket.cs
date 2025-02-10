using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace HTDBasic.Content.Projectiles
{
    public class LateranoRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0.5f;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ai[0] += 1f;

            if (Projectile.ai[0] < 2f)
            {
                Projectile.velocity.Y *= 0.98f;
            }
            else
            {
                Projectile.ai[0] = 2f;

                Projectile.velocity.Y += 0.3f;

                if (Projectile.velocity.Y > 20f)
                {
                    Projectile.velocity.Y = 20f;
                }
            }

            if (Main.rand.NextBool(2))
            {
                Vector2 dustPosition = Projectile.Center - Projectile.velocity * 0.5f;
            
                Dust dust = Dust.NewDustDirect(dustPosition, 1, 1, DustID.Torch,-Projectile.velocity.X * 0.3f, -Projectile.velocity.Y * 0.3f, 100, default, 2f);
                dust.noGravity = true;
                dust.color = new Color(255, 105, 0);
                dust.scale *= 1.5f;
                dust.fadeIn = 0.3f;
            }

            Lighting.AddLight(Projectile.Center, 1f, 0.3f, 0f);

            if (Main.rand.NextBool(2))
            {
                Vector2 dustPosition = Projectile.Center - Projectile.velocity * 0.5f;
                Dust dust = Dust.NewDustDirect(dustPosition, 1, 1, DustID.Torch, -Projectile.velocity.X * 0.3f, -Projectile.velocity.Y * 0.3f, 100, default, 1.5f);
                dust.noGravity = true;
            }
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            //не реализованный массовый урон по противникам

            /*float explosionRadius = 150f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && Vector2.Distance(Projectile.Center, npc.Center) < explosionRadius)
                {
                    npc.StrikeNPC(Projectile.damage, 0, 0, crit: false);
                }
            }*/

            for (int i = 0; i < 20; i++)
            {
                int dustType = DustID.Torch;
                Vector2 position = Projectile.Center + new Vector2(Main.rand.Next(-150, 130), Main.rand.Next(-150, 130)); // Более широкий радиус
                Dust dust = Dust.NewDustDirect(position, 1, 1, dustType, 0f, 0f, 0, default, 2f);
                dust.noGravity = true;
                dust.scale *= 2f;
                dust.fadeIn = 0.5f;
            }

            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Main.rand.Next(-3, 4), Main.rand.Next(-3, 4)), Main.rand.Next(61, 64), 1f);

            for (int i = 0; i < 5; i++)
            {
                Vector2 position = Projectile.Center + new Vector2(Main.rand.NextFloat(-20f, 20f), Main.rand.NextFloat(-20f, 20f));
                Dust dust = Dust.NewDustDirect(position, 1, 1, DustID.Torch, 0f, 0f, 0, default, 3f);
                dust.noGravity = true;
                dust.fadeIn = 0.3f;
                dust.scale *= 2f;
            }
            /*for (int i = 0; i < 20; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.OrangeTorch, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), 150, default, 1.5f);
                Main.dust[dustIndex].velocity *= 2;
                Main.dust[dustIndex].noGravity = true;
            }*/

            Terraria.Audio.SoundEngine.PlaySound(new SoundStyle($"{nameof(HTDBasic)}/Content/Sounds/lateranoweapon_projectile_reach"), Projectile.position);
        }
    }
}
