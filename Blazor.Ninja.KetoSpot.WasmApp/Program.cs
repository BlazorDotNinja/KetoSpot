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
using Syncfusion.Blazor;

namespace Blazor.Ninja.KetoSpot.WasmApp
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var apiUrl = "https://api.blazor.ninja";

			// TODO Paste your app token below
			var appToken = "eyJ0eXAiOiJDU1AiLCAiZW5jIjoiUlNBX09BRVAifQ.ga46Gk0JADOtmaJ3A4rdtYX6D73MdP8aGb5BJMTTyVYSauSj_aiRVL6Q8-9OjvACnzr0OgmcNLM2QXN2nhgg5U3EROQNsTg3PwpJg3320EDy3ePHK_8vCRyEgJNCAUO-74CrJCJu-qVargilZ-UIkkWuwCCjOqy6beOEouFojIE.Q9pYG1eESeStPAq3.t1PGIguzLp6n2ejVMzxD6LPsRBzulSk_fMQcGbpgLn9whJpF8cFLYhTT_be6Xw.iiAwQFMM9Z26uPcc7V02xw";

			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

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
			
			builder.Services.AddSyncfusionBlazor();

			await builder.Build().RunAsync();
		}
	}
}
