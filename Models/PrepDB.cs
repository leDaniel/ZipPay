using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZipPay.Data;

namespace ZipPay.Models
{
    public static class PrepDB
    {
       public static void PrepPopulation(IApplicationBuilder app)
       {
           using (var serviceScope = app.ApplicationServices.CreateScope() )
           {
               SeedData( serviceScope.ServiceProvider.GetService<ZipPayContext>() );
           }
       }

       public static void SeedData(ZipPayContext  context) 
       {
           // Apply migrations
           context.Database.Migrate();
       }

    }
}