using System;
using System.Threading.Tasks;
using Blazor.Ninja.Client.Http;

namespace DevUtils
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var apiUrl = "https://api.blazor.ninja";

			// TODO Paste your app token below
			var appToken = "eyJ0eXAiOiJDU1AiLCAiZW5jIjoiUlNBX09BRVAifQ.ga46Gk0JADOtmaJ3A4rdtYX6D73MdP8aGb5BJMTTyVYSauSj_aiRVL6Q8-9OjvACnzr0OgmcNLM2QXN2nhgg5U3EROQNsTg3PwpJg3320EDy3ePHK_8vCRyEgJNCAUO-74CrJCJu-qVargilZ-UIkkWuwCCjOqy6beOEouFojIE.Q9pYG1eESeStPAq3.t1PGIguzLp6n2ejVMzxD6LPsRBzulSk_fMQcGbpgLn9whJpF8cFLYhTT_be6Xw.iiAwQFMM9Z26uPcc7V02xw";

			var proxyFactory = new HttpProxyFactory(apiUrl, appToken);

			Console.WriteLine("Done");
		}
	}
}
