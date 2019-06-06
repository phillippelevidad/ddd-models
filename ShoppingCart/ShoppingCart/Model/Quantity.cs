using CSharpFunctionalExtensions;
using System;

namespace ShoppingCart.Model
{
    public struct Quantity
    {
        private Quantity(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static Result<Quantity> Create(int value)
        {
            if (value < 0)
                return Result.Fail<Quantity>("Quantity cannot be less than zero.");

            return Result.Ok(new Quantity(value));
        }

        public static Quantity Of(int value)
        {
            return Create(value)
                .OnFailure(error => throw new Exception(error))
                .Result.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return Value == ((Quantity)obj).Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();

        public static implicit operator int(Quantity quantity) => quantity.Value;

        public static Quantity operator +(Quantity a, Quantity b) => new Quantity(a.Value + b.Value);

        public static Quantity operator -(Quantity a, Quantity b) => new Quantity(a.Value - b.Value);
    }
}
