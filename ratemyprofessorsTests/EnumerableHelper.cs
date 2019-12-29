using System;
using System.Collections.Generic;
using System.Text;

namespace ratemyprofessorsTests
{
    public static class EnumerableHelper
    {
        public static int Count(this IEnumerable<object> data)
        {
            var enumerator = data.GetEnumerator();

            int count = 0;

            while (enumerator.MoveNext())
                count++;

            return count;
        }
    }
}
