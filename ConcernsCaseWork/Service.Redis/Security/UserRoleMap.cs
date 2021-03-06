using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Redis.Security
{
	/// <summary>
	/// NOTE: Replace / review this class when AD is integrated.
	/// </summary>
	public static class UserRoleMap
	{
		public const string AdminUserName = "concerns.casework";
		private const string UserE2ECypressUserName = "e2e.cypress.test";
		private static readonly IList<string> ToExcludeUsers = new List<string> { AdminUserName, UserE2ECypressUserName };
		
		private readonly static IDictionary<string, List<RoleEnum>> DefaultUserRoles = new Dictionary<string, List<RoleEnum>>
		{
			{ AdminUserName, new List<RoleEnum> { RoleEnum.User, RoleEnum.Admin, RoleEnum.Leader } },
			{ UserE2ECypressUserName, new List<RoleEnum> { RoleEnum.User, RoleEnum.Admin, RoleEnum.Leader } },
			{ "ben.memmott", new List<RoleEnum> { RoleEnum.User, RoleEnum.Admin, RoleEnum.Leader } },
			{ "richard.machen", new List<RoleEnum> { RoleEnum.User } },
			{ "steve.oconnor", new List<RoleEnum> { RoleEnum.User } },
			{ "elijah.aremu", new List<RoleEnum> { RoleEnum.User, RoleEnum.Admin, RoleEnum.Leader } },
			{ "paulo.lancao", new List<RoleEnum> { RoleEnum.User } },
			{ "richard.joseph", new List<RoleEnum> { RoleEnum.User } },
			{ "william.cook", new List<RoleEnum> { RoleEnum.User } },
			{ "stephanie.maskery", new List<RoleEnum> { RoleEnum.User } },
			{ "paul.simmons", new List<RoleEnum> { RoleEnum.User } },
			{ "james.cheetham", new List<RoleEnum> { RoleEnum.User } },
		};
		
		public static List<RoleEnum> DefaultUserRole()
		{
			return new List<RoleEnum> { RoleEnum.User };
		}
		
		public static IDictionary<string, List<RoleEnum>> InitUserRoles()
		{
			return DefaultUserRoles;
		}

		public static IList<string> GetDefaultUsersExcludedList(IEnumerable<string> users)
		{
			return users.Where(user => !ToExcludeUsers.Contains(user)).ToList();
		}

		public static IList<string> GetDefaultUsersExcludeE2E(IEnumerable<string> users)
		{
			return users.Where(user => !user.Equals(UserE2ECypressUserName, StringComparison.OrdinalIgnoreCase)).ToList();
		}
	}
}