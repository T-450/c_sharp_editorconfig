﻿public static class SessionExtensions
{
    public static void SetObject(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T GetObject<T>(this ISession session, string key)
    {
        string value = session.GetString(key);

        return value == null ? default : JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }
}
