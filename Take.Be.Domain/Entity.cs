namespace Take.Be.Domain
{
    public abstract class Entity<TKey>
    {
        protected Entity(TKey id = default)
        {
            Id = id;
        }

        public virtual TKey Id { get; }
    }
}
