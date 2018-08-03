using System;
using System.Collections.Generic;

namespace GameArchitect.Tasks
{
    public sealed class TaskComparer : IEqualityComparer<ITask>, IEqualityComparer<Lazy<ITask>>
    {
        public bool Equals(ITask x, ITask y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.GetType().Name.Equals(y.GetType().Name, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool Equals(Lazy<ITask> x, Lazy<ITask> y)
        {
            return Equals(x.Value, y.Value);
        }

        public int GetHashCode(ITask obj)
        {
            return obj.GetType().Name.GetHashCode();
        }

        public int GetHashCode(Lazy<ITask> obj)
        {
            return GetHashCode(obj.Value);
        }
    }
}