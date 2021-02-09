using System;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Wrappers;

namespace Nytte
{
    public static class Extensions
    {
        public static INytteBuilder AddNytte(this IServiceCollection services)
        {
            services.AddSingleton<IJson, Json>();
            return new NytteBuilder(services);
        }

        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            var att = (T) Attribute.GetCustomAttribute(type, typeof(T));
            return att;
        }
    }
}
