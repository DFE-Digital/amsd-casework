﻿using ConcernsCaseWork.Services.Cases;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Serilog;
using Service.Redis.Services;
using Service.TRAMS.Cases;
using StackExchange.Redis;
using System;
using System.Configuration;
using System.Net.Mime;

namespace ConcernsCaseWork.Extensions
{
	public static class StartupExtension
	{
		private static readonly IRedisMultiplexer RedisMultiplexer = new RedisMultiplexer();
		public static IRedisMultiplexer Implementation { private get; set; } = RedisMultiplexer;
		
		public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
		{
			try
			{
				var vCapConfiguration = JObject.Parse(configuration["VCAP_SERVICES"]) ?? throw new ConfigurationErrorsException("AddRedis::VCAP_SERVICES missing");
				var redisCredentials = vCapConfiguration["redis"]?[0]?["credentials"] ?? throw new ConfigurationErrorsException("AddRedis::Credentials missing");
				var password = (string)redisCredentials["password"] ?? throw new ConfigurationErrorsException("AddRedis::Credentials::password missing");
				var host = (string)redisCredentials["host"] ?? throw new ConfigurationErrorsException("AddRedis::Credentials::host missing");
				var port = (string)redisCredentials["port"] ?? throw new ConfigurationErrorsException("AddRedis::Credentials::port missing");
				var tls = (bool)redisCredentials["tls_enabled"];

				Log.Information($"Starting Redis Server Host - {host}");
				Log.Information($"Starting Redis Server Port - {port}");
				Log.Information($"Starting Redis Server TLS - {tls}");

				var redisConfigurationOptions = new ConfigurationOptions { Password = password, EndPoints = { $"{host}:{port}" }, Ssl = tls };
				var redisConnection = Implementation.Connect(redisConfigurationOptions);

				services.AddStackExchangeRedisCache(
					options =>
					{
						options.ConfigurationOptions = redisConfigurationOptions;
						options.InstanceName = $"Redis-{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";
					});
				services.AddDataProtection().PersistKeysToStackExchangeRedis(redisConnection, "DataProtectionKeys");
			}
			catch (Exception ex)
			{
				var errorMessage = $"AddRedis::Exception::{ex.Message}";
				Log.Error(errorMessage);
				throw new ConfigurationErrorsException(errorMessage);
			}
		}

		/// <summary>
		/// HttpFactory for Trams API
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <exception cref="ConfigurationErrorsException"></exception>
		public static void AddTramsApi(this IServiceCollection services, IConfiguration configuration)
		{
			var tramsApiEndpoint = configuration["trams:api_endpoint"];
			var tramsApiKey = configuration["trams:api_key"];
			if (string.IsNullOrEmpty(tramsApiEndpoint) || string.IsNullOrEmpty(tramsApiKey)) 
				throw new ConfigurationErrorsException("AddTramsApi::missing configuration");
			
			Log.Information($"Starting Trams API Endpoint - {tramsApiEndpoint}");
			
			services.AddHttpClient("TramsClient", client =>
			{
				client.BaseAddress = new Uri(tramsApiEndpoint);
				client.DefaultRequestHeaders.Add("ApiKey", tramsApiKey);
				client.DefaultRequestHeaders.Add("ContentType", MediaTypeNames.Application.Json);
			});
		}

		public static void AddInternalServices(this IServiceCollection services)
		{
			// Cases service model and external TRAMS.
			services.AddSingleton<ICaseModelService, CaseModelService>();
			services.AddSingleton<ICaseService, CaseService>();
			
			// Redis service model
			services.AddTransient<ICacheProvider, CacheProvider>();
			services.AddTransient<IActiveDirectoryService, ActiveDirectoryService>();
			services.AddTransient<ICachedUserService, CachedUserService>();
		}
	}
}