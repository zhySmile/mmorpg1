using System.Collections;
using System.Collections.Generic;
using Entities;
using SkillBridge.Message;
using UnityEngine;

namespace Managers
{
    interface IEntityNotify
    {
        void OnEntityResoved();
        void OnEntityChanged(Entity entity);
        void OnEntityEvent(EntityEvent dataEvent);
    }

    class EntityManager : Common.Singleton<EntityManager>
    {
        Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
        Dictionary<int, IEntityNotify> notifiers = new Dictionary<int, IEntityNotify>();

        public void RegisterEntityChangeNotify(int entityId, IEntityNotify notify)
        {
            this.notifiers[entityId] = notify;
        }

        public void AddEntity(Entity entity)
        {
            entities[entity.entityId] = entity;
        }

        public void RemoveEntity(NEntity entity)
        {
            this.entities.Remove(entity.Id);
            if (notifiers.ContainsKey(entity.Id))
            {
                notifiers[entity.Id].OnEntityResoved();
                notifiers.Remove(entity.Id);
            }
        }

        public void OnEntitySync(NEntitySync data)
        {
            Entity entity = null;
            entities.TryGetValue(data.Id, out entity);
            if (entity!=null)
            {
                if (data.Entity!=null)
                {
                    entity.EntityData = data.Entity;
                }

                if (notifiers.ContainsKey(data.Id))
                {
                    notifiers[entity.entityId].OnEntityChanged(entity);
                    notifiers[entity.entityId].OnEntityEvent(data.Event);
                }
            }
        }
    }
}