namespace DavidsList.Test.Mocks
{
    using System;
    using DavidsList.Data;
    using Microsoft.EntityFrameworkCore;
    public static class DatabaseMock
    {
        public static DavidsListDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<DavidsListDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
                return new DavidsListDbContext(dbContextOptions);
            }
        }
    }
}


