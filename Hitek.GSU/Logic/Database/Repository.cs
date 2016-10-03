using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitek.GSU.Logic.Interfaces;
namespace Hitek.GSU.Logic.Database
{
    public partial class Repository: ISavingRepository
    {
        readonly Entities entity;

        public Repository() {
        //    this.entity = new Entities();
        
        }
        public Repository(Entities entity) {
            this.entity = entity;
        
        }

        public int SaveChanges()
        {
            return entity.SaveChanges();
        }
    }
}