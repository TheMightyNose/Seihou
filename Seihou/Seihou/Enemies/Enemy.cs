﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Seihou
{
    abstract class Enemy : Entity 
    {
        protected int explosionParticles = 5;

        protected Enemy(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            ec = EntityManager.EntityClass.enemy;
        }

        public override void OnDamaged(Entity by, int damage)
        {
            hp--;

			if (hp <= 0)
			{
				int randomNumber = Global.random.Next(0, 100);

				for (int i = 0; i < explosionParticles; i++)
				{
					em.AddEntity(new Particle(pos, sb, em));
				}

				if (randomNumber > 50)
				{
					if (randomNumber > 80)
						em.AddEntity(new Power(pos, sb, em));
					else
						em.AddEntity(new Point(pos, sb, em));
				}
				
				em.RemoveEntity(this);
			}
        }

        public override void Update(GameTime gt)
		{
			if (pos.Y > Global.screenHeight + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}
	}
}
