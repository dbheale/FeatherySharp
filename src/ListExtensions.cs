namespace Feathery.Unofficial.Client;

/// <summary>
/// Gets or sets ListExtensions.
/// </summary>
internal static class ListExtensions
{
    public static async Task<IReadOnlyList<T>> AsReadOnly<T>(this Task<List<T>> task) => await task.ConfigureAwait(false);
}