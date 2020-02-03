using System;

namespace Grasews.Domain.Entities
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime RegistrationDateTime { get; set; }

        protected BaseEntity()
        {
            RegistrationDateTime = DateTime.Now;
        }
    }

    public abstract class BaseEntity
    {
        public DateTime RegistrationDateTime { get; set; }

        protected BaseEntity()
        {
            RegistrationDateTime = DateTime.Now;
        }
    }
}