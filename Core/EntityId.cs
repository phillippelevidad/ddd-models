using System;

namespace Core
{
    public sealed class EntityId
    {
        public EntityId() : this(Guid.NewGuid())
        {
        }

        public EntityId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Cannot be an empty guid.", nameof(value));

            Value = value;
        }

        public Guid Value { get; }

        public override bool Equals(object obj)
        {
            return obj is EntityId other && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(EntityId left, EntityId right) => left.Value == right.Value;

        public static bool operator !=(EntityId left, EntityId right) => left.Value != right.Value;

        public static implicit operator Guid(EntityId id) => id.Value;

        public static explicit operator EntityId(Guid value) => new EntityId(value);
    }
}
