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
			var appToken = "eyJ0eXAiOiJDU1AiLCAiZW5jIjoiUlNBX09BRVAifQ.dMYX0lYJiVdgpf_EELxP3Z2dnUUSCdRsqKUFE6BFOwgW8aeqAmhXX4PGqIz1E9pscTm8bE_OORgLHeFVQ8SwJlcl4iUy_pGM9gfD8v1sBhWLuT-7dgZfCfAGxxXjOvsiAE-oW8741n-_AVMx9HmPY5-0bebasBAnuvfTUZghXRA.Q9pYG1eESeStPAq3.t1PGIguzLp6qg73XZGBA6rO6EB3slSk1KZYcQ7pgKH8ngs0U-pUTMRKFrOy6Xw.zRCF_1dgqSS1oyXkN4ogDg";

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
