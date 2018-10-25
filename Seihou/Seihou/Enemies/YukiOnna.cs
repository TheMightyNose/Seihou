﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	class YukiOnna : Enemy
	{
		private const float fallSpeed = 70.0f;
		private const float bulletSpeed = 300.0f;
		private float fireTimer = 2f;
		private float fireDelay = 0f;
		private const float maxFireDelay = 0.1f;
		private int ammo = 5;
		private int bulletsPerShot = 5;
		private const string bulletTexture = "Snowflake";
		private bool targetSet = false;
		private Vector2 target;
		private const float spread = 20;

		public YukiOnna(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			texture = "YukiOnna";
			size = 10;
			speed.Y = fallSpeed;
			hp = 15;
			if (Settings.difficulty == Settings.Difficulty.easy)
			{
				ammo = 3;
				bulletsPerShot = 3;
			}

			if (Settings.difficulty == Settings.Difficulty.usagi)
			{
				ammo = 7;
			}
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);
			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;
			if (fireDelay <= 0 && ammo > 0 && fireTimer <= 0)
			{
				if (!targetSet)
				{
					target = Global.Normalize(em.GetPlayer().pos - pos) * bulletSpeed;
					targetSet = true;
				}
				float direction = Global.VtoD(target);

				Global.SpreadShot(pos, sb, em, this, bulletSpeed, "bulletTexture", direction, spread, bulletsPerShot);

				fireDelay = maxFireDelay;
				ammo--;
			}

			fireDelay -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
			fireTimer -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
		}
	}
}
