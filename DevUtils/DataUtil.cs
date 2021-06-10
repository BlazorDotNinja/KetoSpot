using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Blazor.Ninja.Common.Data;
using Blazor.Ninja.Common.Factories;
using Blazor.Ninja.KickStart.Common;

namespace DevUtils
{
	public static class DataUtil
	{
		public static async Task GenerateCategoriesAsync(IProxyFactory factory)
		{
			var categories = new List<MenuItemCategory>
			{
				new() { Label = "Breakfast" },
				new() { Label = "Salads" },
				new() { Label = "Meals" },
				new() { Label = "Desserts" },
				new() { Label = "Bread" },
				new() { Label = "Snacks" }
			};

			var proxy = factory.GetDataProxy<MenuItemCategory>();

			await proxy.CreateAsync(categories);
		}

		public static async Task GenerateSaladsAsync(IProxyFactory factory)
		{
			var categoryProxy = factory.GetDataProxy<MenuItemCategory>();

			var category = await categoryProxy.GetOneAsync(Builders<MenuItemCategory>.Filter.Eq(it => it.Label, "Salads"));

			var itemProxy = factory.GetDataProxy<MenuItem>();

			var item = new MenuItem
			{
				MenuItemCategoryId = category.Id,
				Label = "Sandwiches salad",
				Description = "Who says a great sandwich needs bread? These fun, keto salad sandwiches are proof that lettuce can work just as well. Mix it up with different toppings... So versatile you could eat them for breakfast, lunch, a snack, or dinner!",
				KCalories = 472,
				Carbs = 5,
				Price = 10.99
			};
			item = await itemProxy.CreateAsync(item);
			item.PhotoUrl = await UploadPhotoAsync(factory, item.Id, "salad-sandwiches.jpg");
			await itemProxy.UpdateAsync(item);

			item = new MenuItem

			{
				MenuItemCategoryId = category.Id,
				Label = "Zucchini salad",
				Description = "Zucchini stands in for potatoes in this low-carb makeover of the classic summer fave, potato salad. All of the creamy flavor, and (almost) none of the carbs! Perfect for picnics and barbecues.",
				KCalories = 312,
				Carbs = 4,
				Price = 12.99
			};
			item = await itemProxy.CreateAsync(item);
			item.PhotoUrl = await UploadPhotoAsync(factory, item.Id, "zucchini-salad.jpg");
			await itemProxy.UpdateAsync(item);
			
			item = new MenuItem
			{
				MenuItemCategoryId = category.Id,
				Label = "Spicy Shrimp salad",
				Description = "Get your heat on with this piquant keto salad that will surely awaken your senses and tease those hungry taste buds! Hot shrimp with smooth avocado and crunchy cucumber, combined with a delicious ginger and garlic dressing. In other words: the perfect salad!",
				KCalories = 871,
				Carbs = 9,
				Price = 16.99
			};
			item = await itemProxy.CreateAsync(item);
			item.PhotoUrl = await UploadPhotoAsync(factory, item.Id, "spicy-shrimp-salad.jpg");
			await itemProxy.UpdateAsync(item);

			item = new MenuItem
			{
				MenuItemCategoryId = category.Id,
				Label = "Egg salad",
				Description = "BBQ or potluck, here we come! Zucchini stands in for potatoes in this low-carb and spiced-up makeover of the classic summer fave, potato salad. All of the creamy flavor, and (almost) none of the carbs!",
				KCalories = 531,
				Carbs = 6,
				Price = 12.99
			};
			item = await itemProxy.CreateAsync(item);
			item.PhotoUrl = await UploadPhotoAsync(factory, item.Id, "egg-salad.jpg");
			await itemProxy.UpdateAsync(item);

			item = new MenuItem
			{
				MenuItemCategoryId = category.Id,
				Label = "Roasted tomato salad",
				Description = "Sweet, roasted cherry tomatoes, spiced just right. Sprinkled with oil, chopped scallions, and a splash of vinegar. Mmmm... tastes like summer.",
				KCalories = 114,
				Carbs = 4,
				Price = 10.99
			};
			item = await itemProxy.CreateAsync(item);
			item.PhotoUrl = await UploadPhotoAsync(factory, item.Id, "roasted-tomato-salad.jpg");
			await itemProxy.UpdateAsync(item);
		}

		private static async Task<string> UploadPhotoAsync(
			IProxyFactory factory,
			string id,
			string filename)
		{
			var photoProxy = factory.GetContentProxy("Photo");

			var photo = new Content { Id = id, ContentType = "image/jpeg" };
			photo = await photoProxy.CreateAsync(photo);
			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Resources\\{filename}");
			var data = await File.ReadAllBytesAsync(path);
			await photoProxy.AppendDataAsync(photo.Id, data);

			return photo.Url;
		}
	}
}
