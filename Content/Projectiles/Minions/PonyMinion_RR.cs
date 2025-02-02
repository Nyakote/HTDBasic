using HTDBasic.Content.Buffs;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
namespace HTDBasic.Content.Projectiles.Minions
{
    public class PonyMinion_RR : ModProjectile
    {
        public override void SetStaticDefaults()
        {

            Main.projPet[Projectile.type] = true; // Makes the projectile a pet
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; // Targeting support
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.minion = true; // Mark it as a minion
            Projectile.DamageType = DamageClass.Summon; // Use summon damage
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1; // Infinite penetration
            Projectile.minionSlots = 1f; // Occupies 1 minion slot
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<PonyBuff>());
                return;
            }

            if (owner.HasBuff(ModContent.BuffType<PonyBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            AIGeneral(owner, out Microsoft.Xna.Framework.Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            AISerchForTarget(owner, out bool foundTarget, out float distanceFromTarget, out Microsoft.Xna.Framework.Vector2 targetCenter);
            AIMovement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);

        }
        private void AIGeneral(Player owner, out Microsoft.Xna.Framework.Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center;
            idlePosition.Y -= 100f;

            float minionPositionOffset = (10 + Projectile.minionPos + 10) * -owner.direction;
            idlePosition.X += minionPositionOffset;
            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 1800f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            float overlapVelocity = 0.04f;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];
                if (
                    i != Projectile.whoAmI &&
                    other.active &&
                    other.owner == Projectile.owner &&
                    Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width
                   )
                {
                    if (Projectile.position.X < other.position.X) { Projectile.velocity.X -= overlapVelocity; }
                    else { Projectile.velocity.X += overlapVelocity; }
                    if (Projectile.velocity.Y < other.velocity.Y) { Projectile.velocity.Y -= overlapVelocity; }
                    else { Projectile.velocity.Y += overlapVelocity; }
                }
            }
        }
        private void AISerchForTarget(Player owner, out bool foundTarget, out float distanceFromTarget, out Microsoft.Xna.Framework.Vector2 targetCenter)
        {
            distanceFromTarget = 700f;
            targetCenter = Projectile.position;
            foundTarget = false;

            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);
                if (between < 1400f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool InRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);

                        bool closeThroughWall = between < 100f;

                        if (((closest && InRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

        }
        private void AIMovement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            float speed = 10f; // Speed of movement
            float inertia = 20f; // How quickly it smooths out the movement
            float orbitDistance = 100f; // Distance from the target while circling

            // Timer for orbiting
            if (!Projectile.localAI[0].Equals(0)) Projectile.localAI[0] += 0.05f;
            else Projectile.localAI[0] = 0.05f;

            if (foundTarget)
            {
                // Calculate circular movement
                float angle = Projectile.localAI[0]; // Increment angle over time
                Vector2 orbitPosition = targetCenter + new Vector2(
                    orbitDistance * (float)Math.Cos(angle),
                    orbitDistance * (float)Math.Sin(angle)
                );

                Vector2 direction = orbitPosition - Projectile.Center;
                direction.Normalize();
                direction *= speed;

                // Smoothly adjust the velocity for orbiting
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
            }
            else
            {
                // Default movement toward idle position if no target
                if (distanceToIdlePosition > 600f)
                {
                    speed = 40f;
                    inertia = 60f;
                }

                if (distanceToIdlePosition > 20f)
                {
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }



        }
    }
}