using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Blazor.Ninja.Client.Http;
using Blazor.Ninja.Common.Data.System;
using Blazor.Ninja.Common.Factories;
using Blazor.Ninja.Common.Meta;
using Blazor.Ninja.KetoSpot.Common;

namespace Blazor.Ninja.KetoSpot.ServerApp
{
	public class Program
	{
		public static IProxyFactory ProxyFactory { get; private set; }

		public static async Task Main(string[] args)
		{
			var apiUrl = "https://api.blazor.ninja";

			// TODO Paste your app token below
			var appToken = "eyJ0eXAiOiJDU1AiLCAiZW5jIjoiUlNBX09BRVAifQ.ga46Gk0JADOtmaJ3A4rdtYX6D73MdP8aGb5BJMTTyVYSauSj_aiRVL6Q8-9OjvACnzr0OgmcNLM2QXN2nhgg5U3EROQNsTg3PwpJg3320EDy3ePHK_8vCRyEgJNCAUO-74CrJCJu-qVargilZ-UIkkWuwCCjOqy6beOEouFojIE.Q9pYG1eESeStPAq3.t1PGIguzLp6n2ejVMzxD6LPsRBzulSk_fMQcGbpgLn9whJpF8cFLYhTT_be6Xw.iiAwQFMM9Z26uPcc7V02xw";

			ProxyFactory = new HttpProxyFactory(apiUrl, appToken);

			await Task.WhenAll(new List<Task>
			{
				Task.Run(async () => await ProxyFactory.GetConfigurationProxy().GetNamespaceAsync(UserNamespace.Label)),
				Task.Run(async () => await ProxyFactory.GetConfigurationProxy().GetNamespaceAsync(nameof(MenuItem))),
				Task.Run(async () => await ProxyFactory.GetConfigurationProxy().GetFeatureAsync<OnboardingFeature>()),
				Task.Run(async () => await ProxyFactory.GetConfigurationProxy().GetFeatureAsync<OneTimePasswordFeature>()),
				Task.Run(async () => await ProxyFactory.GetConfigurationProxy().GetFeatureAsync<PostboardingFeature>()),
				Task.Run(async () => await ProxyFactory.GetConfigurationProxy().GetFeatureAsync<ThemeFeature>()),
				Task.Run(async () => await ProxyFactory.GetConfigurationProxy().GetFeatureAsync<TicketFeature>())
			});

			await CreateHostBuilder(args).Build().RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
