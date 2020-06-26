using System.Collections.Generic;

namespace SurveyPortal.Services
{
    public static class ListExtensions
    {
        public static bool HasElements<T>(this IList<T> list)
        {
            return list != null && list.Count > 0;
        }

        public static bool HasExactElements<T>(this IList<T> list, int count)
        {
            return list != null && list.Count == count;
        }
    }
}
