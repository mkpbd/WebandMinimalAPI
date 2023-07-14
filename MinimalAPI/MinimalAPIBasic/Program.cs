
using Microsoft.AspNetCore.Mvc;
using MinimalAPIBasic.Data;
using MinimalAPIBasic.Models;

namespace MinimalAPIBasic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Get All Coupon 
            app.MapGet("/api/coupon", () =>
            {
                return Results.Ok(CouponStore.Coupons);
            });

            // Gel Coupn By Id 

            app.MapGet("/api/coupon/{id:int}", (int id) =>
            {
                return Results.Ok(CouponStore.Coupons.Where(x => x.Id == id).FirstOrDefault());
            }).WithName("GetCoupon").Produces(200).Produces(404);

            // Post Coupon  add coupon in Coupon list 

            app.MapPost("/api/coupon/", ([FromBody] Coupon coupon) =>
            {
                if (coupon.Id != 0 || string.IsNullOrEmpty(coupon.Name))
                {
                    return Results.BadRequest("Invalid id or Coupon Name");

                }


                var lastCouponId = CouponStore.Coupons.LastOrDefault();
                coupon.Id = lastCouponId != null ? lastCouponId.Id + 1 : 1;
                CouponStore.Coupons.Add(coupon);

                //return Results.Ok(coupon);

                //return Results.Created($"api/coupon/{coupon.Id}", coupon);

                return Results.CreatedAtRoute("GetCoupon", new { id = coupon.Id }, coupon);


            }).WithName("CreateCoupon").Produces(201).Produces(400);


            app.UseHttpsRedirection();

            app.UseAuthorization();



            app.Run();
        }
    }
}