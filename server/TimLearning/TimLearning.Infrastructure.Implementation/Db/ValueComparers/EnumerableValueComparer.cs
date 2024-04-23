using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TimLearning.Infrastructure.Implementation.Db.ValueComparers;

public class EnumerableValueComparer<T> : ValueComparer<IEnumerable<T>>
    where T : IEquatable<T>
{
    public EnumerableValueComparer()
        : base(
            (c1, c2) =>
                (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
            c => c.Aggregate(0, (h, v) => HashCode.Combine(h, v.GetHashCode())),
            c => c.ToList()
        ) { }
}
