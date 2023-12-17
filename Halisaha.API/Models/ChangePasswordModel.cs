using System;
namespace Halisaha.API.Models
{
	public class ChangePasswordModel
	{
		public int id { get; set; }

		public string oldPassword { get; set; }

		public string password { get; set; }

    }
}

