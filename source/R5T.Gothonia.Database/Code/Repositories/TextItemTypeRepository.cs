﻿using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using R5T.Siscia;
using R5T.Venetia;


namespace R5T.Gothonia.Database
{
    public class TextItemTypeRepository<TDbContext> : ProvidedDatabaseRepositoryBase<TDbContext>, ITextItemTypeRepository
        where TDbContext: DbContext, ITextItemDbContext
    {
        public TextItemTypeRepository(DbContextOptions<TDbContext> dbContextOptions, IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextOptions, dbContextProvider)
        {
        }

        public TextItemTypeIdentity New()
        {
            var textItemTypeIdentity = TextItemTypeIdentity.New();

            this.ExecuteInContextSync(dbContext =>
            {
                var entity = new Entities.TextItemType()
                {
                    GUID = textItemTypeIdentity.Value,
                };

                dbContext.Add(entity);

                dbContext.SaveChanges();
            });

            return textItemTypeIdentity;
        }

        public TextItemTypeIdentity New(string name)
        {
            var textItemTypeIdentity = TextItemTypeIdentity.New();

            this.ExecuteInContextSync(dbContext =>
            {
                var entity = new Entities.TextItemType()
                {
                    GUID = textItemTypeIdentity.Value,
                    Name = name,
                };

                dbContext.Add(entity);

                dbContext.SaveChanges();
            });

            return textItemTypeIdentity;
        }

        public void Add(TextItemType textItemType)
        {
            this.ExecuteInContextSync(dbContext =>
            {
                var entity = new Entities.TextItemType()
                {
                    GUID = textItemType.Identity.Value,
                    Name = textItemType.Name,
                };

                dbContext.Add(entity);

                dbContext.SaveChanges();
            });
        }

        public bool Exists(string name)
        {
            var exists = this.ExecuteInContextSync(dbContext =>
            {
                var entity = dbContext.TextItemTypes.Where(x => x.Name == name).SingleOrDefault();

                var output = entity is object;
                return output;
            });

            return exists;
        }

        public bool Exists(TextItemTypeIdentity identity)
        {
            var exists = this.ExecuteInContextSync(dbContext =>
            {
                var entity = dbContext.TextItemTypes.Where(x => x.GUID == identity.Value).SingleOrDefault();

                var output = entity is object;
                return output;
            });

            return exists;
        }

        public TextItemTypeIdentity GetIdentity(string name)
        {
            var identity = this.ExecuteInContextSync(dbContext =>
            {
                var guid = dbContext.TextItemTypes.Where(x => x.Name == name).Select(x => x.GUID).Single();

                var output = TextItemTypeIdentity.From(guid);
                return output;
            });

            return identity;
        }

        public TextItemType Get(string name)
        {
            var textItemType = this.ExecuteInContextSync(dbContext =>
            {
                var entity = dbContext.TextItemTypes.Where(x => x.Name == name).Single();

                var output = new TextItemType()
                {
                    Identity = TextItemTypeIdentity.From(entity.GUID),
                    Name = entity.Name, // Use entity name, not the input name for exactness.
                };
                return output;
            });

            return textItemType;
        }

        public TextItemType Get(TextItemTypeIdentity identity)
        {
            var textItemType = this.ExecuteInContextSync(dbContext =>
            {
                var entity = dbContext.TextItemTypes.Where(x => x.GUID == identity.Value).Single();

                var output = new TextItemType()
                {
                    Identity = TextItemTypeIdentity.From(entity.GUID),
                    Name = entity.Name,
                };
                return output;
            });

            return textItemType;
        }

        public string GetName(TextItemTypeIdentity identity)
        {
            var name = this.ExecuteInContextSync(dbContext =>
            {
                var output = dbContext.TextItemTypes.Where(x => x.GUID == identity.Value).Select(x => x.Name).Single();
                return output;
            });

            return name;
        }

        public void SetName(TextItemTypeIdentity identity, string name)
        {
            this.ExecuteInContextSync(dbContext =>
            {
                var entity = dbContext.TextItemTypes.Where(x => x.GUID == identity.Value).Single();

                entity.Name = name;

                dbContext.SaveChanges();
            });
        }

        public void Delete(TextItemTypeIdentity identity)
        {
            this.ExecuteInContextSync(dbContext =>
            {
                var entity = dbContext.TextItemTypes.Where(x => x.GUID == identity.Value).Single();

                dbContext.TextItemTypes.Remove(entity);

                dbContext.SaveChanges();
            });
        }
    }
}
