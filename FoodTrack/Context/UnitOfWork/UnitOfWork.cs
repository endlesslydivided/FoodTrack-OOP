using FakeAtlas.Context.Repository;
using FoodTrack.Context;
using FoodTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAtlas.Context.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private DietManagerContext db = new DietManagerContext();
        private DietManagerDBRepository<User> userRepository;

        public DietManagerDBRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new DietManagerDBRepository<User>(db);
                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

