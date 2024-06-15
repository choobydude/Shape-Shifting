using System;
using System.Collections.Generic;
using System.Linq;

namespace WhackAMole
{
    public static class ListExtensions
    {
        private static Random s_Random = new Random();

        public static void Shuffle<T>(this IList<T> i_List)
        {
            int count = i_List.Count;
            while (count > 1)
            {
                count--;
                int container = s_Random.Next(count + 1);
                T value = i_List[container];
                i_List[container] = i_List[count];
                i_List[count] = value;
            }
        }


        public static List<T> GetRandomObjects<T>(this List<T> i_Objects, Func<T, bool> i_Condition, int i_Count)
        {
            var result = i_Objects.Where(i_Condition).ToList();
            result.Shuffle();
            if (result.Count >= i_Count)
                return result.Take(i_Count).ToList();
            return default;
        }

        public static T GetRandomObject<T>(this List<T> i_Objects, Func<T, bool> i_Condition)
        {
            var result = i_Objects.Where(i_Condition).ToList();
            result.Shuffle();
            if (result.Count > 1)
                return result[0];
            return default;
        }
    }
}

