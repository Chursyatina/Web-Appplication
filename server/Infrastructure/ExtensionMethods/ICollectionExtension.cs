namespace Infrastructure.ExtensionMethods
{
    using System.Collections.Generic;

    public static class ICollectionExtension
    {
        public static void AddRange<TItem>(this ICollection<TItem> items, IEnumerable<TItem> additionalItems)
        {
            foreach (TItem item in additionalItems)
            {
                items.Add(item);
            }
        }
    }
}
