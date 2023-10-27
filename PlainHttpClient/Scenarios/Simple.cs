﻿using Microsoft.Extensions.DependencyInjection;

namespace PlainHttpClient.Scenarios
{
    internal class Simple
    {
        public static async Task Execute(IServiceProvider provider)
        {
            var client = provider.GetRequiredService<IServiceClient>();

            var response = await client.GetHello();

            Console.WriteLine(response);
        }
    }
}
