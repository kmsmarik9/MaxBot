using Microsoft.AspNetCore.Http.Extensions;

namespace KmsDev.MaxBot.Requests
{
    internal static class MaxBotRequestUrl
    {
        public static string WithQuery(string path, Dictionary<string, string?>? queryParams = null)
        {
            var withNotEmptyParams = (queryParams ?? [])
                .Where(p => !string.IsNullOrWhiteSpace(p.Value));

            return $"{path}{(queryParams != null ? new QueryBuilder(withNotEmptyParams) : string.Empty)}";
        }

        public static string? UnixMilliseconds(DateTimeOffset? value)
        {
            return value.HasValue ? value.Value.ToUnixTimeMilliseconds().ToString() : null;
        }

        public static string? Number(long? value)
        {
            return value.HasValue ? value.Value.ToString() : null;
        }

        public static string? Number(int? value)
        {
            return value.HasValue ? value.Value.ToString() : null;
        }

        public static string Join(IEnumerable<string>? values)
        {
            if (values == null)
            {
                return string.Empty;
            }

            return string.Join(',', values);
        }

        public static string Join(IEnumerable<long>? values)
        {
            if (values == null)
            {
                return string.Empty;
            }

            return string.Join(',', values);
        }

        public static string? Flag(bool value)
        {
            return value ? "true" : null;
        }
    }
}
