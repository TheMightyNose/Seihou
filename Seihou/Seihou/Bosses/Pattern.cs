﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
    abstract class Pattern
    {
        public bool finsihed;
        private float timer;
        private readonly float duration;
        protected Boss daddy;
        protected EntityManager em;

        public void Reset()
        {
            timer = duration;
            finsihed = false;
        }

        public Pattern(float duration,EntityManager em, Boss daddy)
        {
            this.daddy = daddy;
            this.em = em;
            this.duration = duration;
            timer = duration;
            this.em = em;
        }

        //Always call this base update at top of override call
        public virtual void Update(GameTime gt)
        {
            timer -= (float)gt.ElapsedGameTime.TotalSeconds;
            if (timer < 0) { finsihed = true; return; }
        }
    }
}
