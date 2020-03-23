using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace GameServer.Services
{
    class DBService : Singleton<DBService>
    {
        mmorpg1Entities1 entities;

        public mmorpg1Entities1 Entities
        {
            get { return this.entities; }
        }

        public void Init()
        {
            entities = new mmorpg1Entities1();
        }

        public void Save(bool async = false)
        {

            if (async)
            {
                entities.SaveChangesAsync();
            }
            else
            {
                entities.SaveChanges();
            }

        }
    }
}
