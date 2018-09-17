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
    class EntityManager
    {

        private readonly List<Entity>  nonSolidEntities = new List<Entity>();
        private readonly List<Entity>  entities = new List<Entity>();
		private readonly Queue<Entity> pollAddEntities = new Queue<Entity>();
		private readonly Queue<Entity> pollRemoveEntities = new Queue<Entity>();

        public EntityManager() { }
    
        //TODO: make it find nonSolidEntities too
        public Entity FindById(int id)
        {
            foreach(Entity e in entities)
            {
                if (e.id == id)
                {
                    return e;
                }
            }
            return null;
        }

        public Entity Touching(Entity ent)
        {
            foreach (Entity e in entities)
            {
                if (e.id == ent.id) continue;
                if (Collision.Circle(ent,e))
                {
                    return e;
                }
            }
            return null;
        }

        public int GetEntityCount() => entities.Count;

		public void RemoveEntity(Entity ent) => pollRemoveEntities.Enqueue(ent);

        public void AddEntity(Entity ent) => pollAddEntities.Enqueue(ent);

        public void Update(GameTime gt)
        {
            foreach (Entity e in entities) e.Update(gt);
            foreach (Entity e in nonSolidEntities) e.Update(gt);

			while(pollAddEntities.Count > 0)
			{
                Entity add = pollAddEntities.Dequeue();
                
                if (add.collision)
                {
                    entities.Add(add);
                }
                else
                {
                    nonSolidEntities.Add(add);
                }
            }

            while (pollRemoveEntities.Count > 0)
            {
                Entity rem = pollRemoveEntities.Dequeue();

                if (rem.collision)
                {
                    entities.Remove(rem);
                }
                else
                {
                    nonSolidEntities.Remove(rem);
                }
            }
        }

        public void Draw(GameTime gt)
        {
            foreach (Entity e in entities) e.Draw(gt);
            foreach (Entity e in nonSolidEntities) e.Draw(gt);
        }

        public void ClearEntities()
        {
            entities.Clear();
            nonSolidEntities.Clear();
        }
    }
}
