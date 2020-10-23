// <copyright file="PaparaServiceExtensions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

#if NET45

#elif NET461

#else
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Papara.Extensions
{
    public static class PaparaServiceExtensions
    {
        public static void AddPapara(this IServiceCollection services)
        {
            services.AddTransient<IPaparaClient>(x => new PaparaClient());
        }

        public static void AddPapara(this IServiceCollection services, Action<PaparaOptions> options)
        {
            var paparaOptions = new PaparaOptions();
            options.Invoke(paparaOptions);

            Enum.TryParse(paparaOptions.Env, out PaparaEnv env);

            services.AddTransient<IPaparaClient>(x => new PaparaClient(paparaOptions.ApiKey, env));
        }
    }
}
#endif