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
	class Point : Powerup
	{
		public Point(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			texture = "Point";
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);

			Entity c = em.Touching(this, EntityManager.EntityClass.player);

			if (c != null && hp > 0)
			{
				hp--;
				em.RemoveEntity(this);
				Global.player.CollectPoint();
			}
		}

		public override void Draw(GameTime gt)
		{
			base.Draw(gt);
			if (Global.drawCollisionBoxes) MonoGame.Primitives2D.DrawCircle(sb, pos, size, 10, Color.Blue, 1);
		}	
	}
}