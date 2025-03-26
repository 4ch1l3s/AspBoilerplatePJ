using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using internPJ3.Authorization.Roles;
using internPJ3.Authorization.Users;
using internPJ3.MultiTenancy;
//using System.Threading.Tasks;
using internPJ3.Tour;
using internPJ3.Tasks1;
using internPJ3.Person1;
using internPJ3.Product;
using internPJ3.Category;
using System.Threading.Tasks;
using internPJ3.Cart;

namespace internPJ3.EntityFrameworkCore
{
	public class internPJ3DbContext : AbpZeroDbContext<Tenant, Role, User, internPJ3DbContext>
	{
		/* Define a DbSet for each entity of the application */
		public DbSet<Person> People { get; set; }
		public DbSet<TaskTest> Tasks { get; set; }
		public DbSet<CartE> Cart {  get; set; }
		public DbSet<CartItemE> CartItems { get; set; }

		public DbSet<TourE> Tour {  get; set; }

		public DbSet<ProductF> Product { get; set; }

		public DbSet<Categories> CategoryTable { get; set; }
		public internPJ3DbContext(DbContextOptions<internPJ3DbContext> options)
				: base(options)
		{
		}


		//phần code đặt giá trị mặc định cho bảng Categories
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed dữ liệu mặc định vào bảng CategoryTable
			modelBuilder.Entity<Categories>().HasData(
				new Categories
				{
					Id = 1,  // Id cố định
					CategoryName = "Uncategory",
					CategoryDescription = "Default Value"
				}
			);

			// Thiết lập giá trị mặc định cho CategoryId trong ProductF
			modelBuilder.Entity<ProductF>()
					.Property(p => p.CategoryId)
					.HasDefaultValue(1); // Khi không có giá trị thì sẽ là 1

			// Cấu hình khóa ngoại, đặt ON DELETE NO ACTION (để EF không tự xóa)
			modelBuilder.Entity<ProductF>()
					.HasOne(p => p.Categories)
					.WithMany()
					.HasForeignKey(p => p.CategoryId)

					.OnDelete(DeleteBehavior.NoAction); 


		}
	}
}
