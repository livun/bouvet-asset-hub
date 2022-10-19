namespace Bouvet.AssetHub.API.ExtensionMethods
{
    public static class MyLinqExtensions
    {
        public static List<T>? NullIfEmpty<T>(this List<T> list) =>
        list.Any() ? list : null;

        // this will be more optimal solution
        //public static <List<T>>? ToListOrNullIfEmpty<T>(this IAsyncEnumerable<T> collection)
        //{
        //    var count = await collection.CountAsync();
        //    if (count == 0)
        //        return await null;
        //    return await (count == 0 ? collection.ToListAsync() : null);

        //}

        // this will be more optimal solution
        public static List<T>? ToListOrNullIfEmpty<T>(this IEnumerable<T> collection) =>
            collection.Any() ? collection.ToList() : null;

    }
}
