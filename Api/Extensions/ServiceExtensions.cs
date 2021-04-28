﻿using Common.EmailHelper.Classes;
using Infrastructure.Data.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure IOptions for Email settings on container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureEmailSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(options => 
            {
                options.From = configuration.GetValue<string>($"{nameof(EmailSettings)}:{nameof(EmailSettings.From)}");
                options.Port = configuration.GetValue<int>($"{nameof(EmailSettings)}:{nameof(EmailSettings.Port)}");
                options.SmtpServer = configuration.GetValue<string>($"{nameof(EmailSettings)}:{nameof(EmailSettings.SmtpServer)}");
                options.Username = configuration.GetValue<string>($"{nameof(EmailSettings)}:{nameof(EmailSettings.Username)}");
                options.Password = configuration.GetValue<string>($"{nameof(EmailSettings)}:{nameof(EmailSettings.Password)}");
            });
        }
        /// <summary>
        /// Configure Api to use Bearer Token Authentication
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtIssuerOptions>(configuration.GetSection(nameof(JwtIssuerOptions)));
            var jwtAppSettings = configuration.GetSection(nameof(JwtIssuerOptions));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtAppSettings[nameof(JwtIssuerOptions.Issuer)],
                    ValidAudience = jwtAppSettings[nameof(JwtIssuerOptions.Audience)],
                    IssuerSigningKey = Common.Extensions.CustomLinqExtensions.GetSymmetricSecurityKey(jwtAppSettings[nameof(JwtIssuerOptions.SecretKey)])
                };
            });

        }
        /// <summary>
        /// Configure Cross site settings
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }
        /// <summary>
        /// Configure Swagger settings
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Shopping Cart Api",
                    Version = "v1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // Tell swagger to include xml comments
                options.IncludeXmlComments(xmlPath);

                // To enable authorization using Swagger(JWT)
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[]{}
                    }
                });
            });

        }
    }
}
