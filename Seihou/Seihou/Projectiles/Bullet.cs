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
	class Bullet : Projectile
	{
		public Bullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
		{
            ec = EntityManager.EntityClass.nonSolid;
			size = 10;
			this.speed = speed;
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);
			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			EntityManager.EntityClass target = (owner is Enemy) ? EntityManager.EntityClass.player : EntityManager.EntityClass.enemy ;
			
			Entity c = em.Touching(this,target);

			if (c != null) c.Damage(owner, 1);
		}

		public override void Draw(GameTime gt)
		{	
			ResourceManager.DrawAngledTexture(sb, "Dart1", pos, speed);
		}
	}
}
