using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halisaha.API.Security;
using Halisaha.Business.Abstract;
using Halisaha.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Halisaha.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class OwnerContoller : Controller
    {
        private IOwnerService _ownerService;
        private JwtAyarlari _jwtAyarlari;
        public OwnerContoller(IOwnerService ownerService, IOptions<JwtAyarlari> jwtAyarlari)
        {
            _ownerService = ownerService;
            _jwtAyarlari = jwtAyarlari.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]Owner owner)
        {
            if (ModelState.IsValid)
            {
                var result = await _ownerService.GetOwnerByPhone(owner.Phone!);
                if (result != null)
                {

                }
            }
        }

    }
}

