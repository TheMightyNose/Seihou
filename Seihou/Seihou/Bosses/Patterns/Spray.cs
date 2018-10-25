﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
    class Spray : Pattern
    {
        float spawnRate;
        float spawnTimer = 0;
        float spread = 20;
        const float bulletSpeed = 300;

        public Spray(Boss daddy, EntityManager em, float spawnRate) : base(2, em, daddy)
        {
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (spawnTimer > spawnRate)
            {
                float Direction = Global.VtoD(Global.player.pos - daddy.pos);

                spawnTimer = 0;
                em.AddEntity(new EnemyBullet(daddy.pos,daddy.sb, em, daddy, new Vector2((float)Math.Cos(Direction - Math.PI / (spread / 2)) * bulletSpeed, (float)Math.Sin(Direction - Math.PI / (spread / 2)) * bulletSpeed), "Snowflake"));
                em.AddEntity(new EnemyBullet(daddy.pos,daddy.sb, em, daddy, new Vector2((float)Math.Cos(Direction + Math.PI / (spread / 2)) * bulletSpeed, (float)Math.Sin(Direction + Math.PI / (spread / 2)) * bulletSpeed), "Snowflake"));

                em.AddEntity(new EnemyBullet(daddy.pos,daddy.sb, em, daddy, Global.Normalize(Global.player.pos - daddy.pos) * bulletSpeed, "Snowflake"));
            }

            base.Update(gt);
        }
    }
}
