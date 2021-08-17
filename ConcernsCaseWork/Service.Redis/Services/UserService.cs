﻿using Service.Redis.Models;
using System.Threading.Tasks;

namespace Service.Redis.Services
{
	public sealed class UserService : IUserService
	{
		// TODO DI Integrate Azure AD
		public UserService()
		{
			
		}

		/// <summary>
		/// Azure AD authorisation
		/// </summary>
		/// <returns></returns>
		public Task<UserClaims> GetUserAsync(UserCredentials userCredentials)
		{
			return Task.FromResult(new UserClaims { Email = "paulo.lancao@education.gov.uk", Id = "User:AD:ID" });
		}
	}
}