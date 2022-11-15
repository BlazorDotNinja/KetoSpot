using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Blazor.Ninja.Client.Http;
using Blazor.Ninja.Common.Data.System;
using Blazor.Ninja.Common.Factories;
using Blazor.Ninja.Common.Meta;

namespace Blazor.Ninja.KetoSpot.WasmApp
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var apiUrl = "https://api.blazor.ninja";

			// TODO Paste your app token below
			var appToken = "eyJ0eXAiOiJDU1AiLCAiZW5jIjoiUlNBX09BRVAifQ.dMYX0lYJiVdgpf_EELxP3Z2dnUUSCdRsqKUFE6BFOwgW8aeqAmhXX4PGqIz1E9pscTm8bE_OORgLHeFVQ8SwJlcl4iUy_pGM9gfD8v1sBhWLuT-7dgZfCfAGxxXjOvsiAE-oW8741n-_AVMx9HmPY5-0bebasBAnuvfTUZghXRA.Q9pYG1eESeStPAq3.t1PGIguzLp6qg73XZGBA6rO6EB3slSk1KZYcQ7pgKH8ngs0U-pUTMRKFrOy6Xw.zRCF_1dgqSS1oyXkN4ogDg";

			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			var proxyFactory = new HttpProxyFactory(apiUrl, appToken);
			builder.Services.AddSingleton<IProxyFactory>(proxyFactory);

			await Task.WhenAll(new List<Task>
			{
				Task.Run(async () => await proxyFactory.GetConfigurationProxy().GetNamespaceAsync(UserNamespace.Label)),
				Task.Run(async () => await proxyFactory.GetConfigurationProxy().GetFeatureAsync<OnboardingFeature>()),
				Task.Run(async () => await proxyFactory.GetConfigurationProxy().GetFeatureAsync<OneTimePasswordFeature>()),
				Task.Run(async () => await proxyFactory.GetConfigurationProxy().GetFeatureAsync<PostboardingFeature>()),
				Task.Run(async () => await proxyFactory.GetConfigurationProxy().GetFeatureAsync<ThemeFeature>()),
				Task.Run(async () => await proxyFactory.GetConfigurationProxy().GetFeatureAsync<TicketFeature>())
			});

			await builder.Build().RunAsync();
		}
	}
}
