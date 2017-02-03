using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MVCRazorApp.Models
{
	public class Product
	{



		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Name { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		[DisplayName("Image")]
		public byte[] ProductImg { get; set; }




	}

	public partial class ProductDBContext : DbContext
	{
		public ProductDBContext() : base(nameOrConnectionString: "MyDatabaseContext") { }

		public DbSet<Product> Products { get; set; }

	}
}
