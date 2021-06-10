using Blazor.Ninja.Common.Data;

namespace Blazor.Ninja.KickStart.Common
{
	public class MenuItem : IdDataObject
	{
		public string MenuItemCategoryId { get; set; }
		public string Label { get; set; }
		public string Description { get; set; }
		public int KCalories { get; set; }
		public int Carbs { get; set; }
		public double Price { get; set; }
		public string PhotoUrl { get; set; }
	}
}
