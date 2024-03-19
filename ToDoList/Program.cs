namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            /*builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(6);
            });*/

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

           /* app.UseSession();*/

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Use 1");
            //    next.Invoke();
            //    await context.Response.WriteAsync("Terminate Use 1");

            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Use 2");
            //    next.Invoke();
            //    await context.Response.WriteAsync("Terminate Use 2");

            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Run 1");
            //});

            app.Run();
        }
    }
}
