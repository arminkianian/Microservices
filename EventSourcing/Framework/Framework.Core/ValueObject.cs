
namespace Framework.Core
{
    public abstract class ValueObject
    {
        private const int HighPrime = 557927;

        protected abstract IEnumerable<object> GetAtomicValues();

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select((x, i) => (x != null ? x.GetHashCode() : 0) + (HighPrime * i))
                .Aggregate((x, y) => x ^ y);
        }

        public ValueObject GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }

        public bool Equals(ValueObject other)
        {
            if (other == null || other.GetType() != GetType())
            {
                return false;
            }

            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((ValueObject)obj);
        }
    }
}