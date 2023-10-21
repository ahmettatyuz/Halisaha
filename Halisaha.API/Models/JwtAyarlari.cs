using System;
namespace Halisaha.API.Security
{
	public class JwtAyarlari
	{
		public string? SecretKey { get; set; }

		public string? Audience { get; set; }

		public string? Issuer { get; set; }
	}
}

