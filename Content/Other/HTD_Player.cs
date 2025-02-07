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
using Terraria.ModLoader.Config;
using HTDBasic.Content.Projectiles;
using Terraria.GameContent.Biomes;
using Mono.Cecil;
using static System.Net.Mime.MediaTypeNames;
using Terraria.GameContent;


    namespace HTDBasic.Content.Other
    {
        public class HTD_Player : ModPlayer
        {
            public int SubProjectiles_AJ = 0;          // Variable that used as a counter for <ApplePie.Projectile>
            public int attackCooldown_PonyStaff = 0;   // Variable that used as a timer
            public int CooldownTime_PonyStaff = 360;   // [ 1min ] Variable that sets a cooldown for (AltUse) for <PonyStaff.Item>
            public int MaxProjectiles_AJ = 1;          // Amount of <ApplePie.Projectile> would we summond on (AltUse)
        public override void PreUpdate()
        {
            if (attackCooldown_PonyStaff > 0)
            {
                attackCooldown_PonyStaff--;            //Reseting Attack Cooldown for the <PonyStaff.Item>
            }
        }
    }         

    }
    

