using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Data.Objects;

namespace Mehr.Data
{
    public interface ICrudRepository<TItem>
    {
        IQueryable<TItem> List { get; }

        void Create(TItem item);

        TItem Get(int id);

        TItem Delete(int id);

        void TrackChanges(TItem item);

        TItem LoadOrCreate(int id);

        void SaveChanges();
    }

    public abstract class CrudRepository<TItem, TEntities> : ICrudRepository<TItem>
        where TItem : class, new()
        where TEntities : ObjectContext, new()
    {
        public const string IdentifierPropertyNameDefault = "Id";
        public string IdentifierPropertyName { get; protected set; }

        public CrudRepository()
        { IdentifierPropertyName = IdentifierPropertyNameDefault; }

        public IQueryable<TItem> List { get { return this.ObjectSet; } }

        protected abstract ObjectSet<TItem> ObjectSet { get; }


        public TEntities Entities;
        public CrudRepository(TEntities entities = null)
            : this()
        { this.Entities = entities ?? new TEntities(); }


        public void Create(TItem item)
        { this.ObjectSet.AddObject(item); }

        public TItem Get(int id)
        { return this.ObjectSet.Where(IdentifierPropertyName + " == @0", id).FirstOrDefault(); }

        public TItem Delete(int id)
        {
            var item = Get(id);
            this.ObjectSet.DeleteObject(item);
            return item;
        }

        public void TrackChanges(TItem item)
        {
            var id = GetId(item);
            if (id == default(int))
                this.ObjectSet.AddObject(item);
            else
                this.ObjectSet.Attach(item);
        }

        public TItem LoadOrCreate(int id)
        {
            var item = new TItem();
            SetId(item, id);
            TrackChanges(item);
            return item;
        }

        public void SaveChanges()
        {
            Entities.SaveChanges();
        }

        private int GetId(TItem item)
        {
            var itemType = item.GetType();
            var identifierProperty = itemType.GetProperty(IdentifierPropertyName);
            var id = int.Parse(identifierProperty.GetValue(item, null).ToString());
            return id;
        }

        private void SetId(TItem item, int id)
        {
            var itemType = item.GetType();
            var identifierProperty = itemType.GetProperty(IdentifierPropertyName);
            identifierProperty.SetValue(item, id, null);
        }
    }

    public static class CrudRepository
    {
        public static CrudRepository<TItem, TEntities> CreateDefault<TItem, TEntities>(ObjectSet<TItem> objectSet, TEntities entities = null)
            where TItem : class, new()
            where TEntities : ObjectContext, new()
        {
            return new DefaultCrudRepository<TItem, TEntities>(entities ?? new TEntities(), objectSet);
        }
    }

    public class DefaultCrudRepository<TItem, TEntities> : CrudRepository<TItem, TEntities>
        where TItem : class, new()
        where TEntities : ObjectContext, new()
    {
        public DefaultCrudRepository(TEntities entities, ObjectSet<TItem> objectSet)
            : base(entities)
        {
            this.objectSet = objectSet;
        }

        private ObjectSet<TItem> objectSet;
        protected override ObjectSet<TItem> ObjectSet
        {
            get { return objectSet; }
        }
    }
}
