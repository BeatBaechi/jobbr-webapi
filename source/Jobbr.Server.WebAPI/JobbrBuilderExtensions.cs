﻿using Jobbr.Server.Builder;
using Jobbr.Server.Common;

namespace Jobbr.Server.WebAPI
{
    public static class JobbrBuilderExtensions
    {
        public static void AddWebApi(this IJobbrBuilder builder)
        {
            builder.Register<IJobbrComponent>(typeof(WebHost));
        }
    }
}