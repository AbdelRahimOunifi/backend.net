using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECApiRT.Midleware;
using ECApiRT.EF;
using ECApiRT.Models;
using Microsoft.EntityFrameworkCore;

namespace ECApiRT.Controllers
{
    [Produces("application/json")]
    [Route("api/Itoken")]
    public class ItokenController : Controller
    {
        private readonly DataContext _context;

        public ItokenController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Itoken inputModel)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.EMail == inputModel.Username);

            if (user == null)
            {
                var dict1 = new Dictionary<string, string>();
                dict1.Add("status", "Failed");
                dict1.Add("token", "");
                return Ok(dict1);
            }

            if (inputModel.Username != user.EMail || inputModel.Password != user.UserPassword)
            {
                var dict2 = new Dictionary<string, string>();
                dict2.Add("status", "Failed");
                dict2.Add("token", "");
                return Ok(dict2);
            }

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("this is my custom Secret key for authnetication"))
                                .AddSubject("Ounifi Bearer Authentication")
                                .AddIssuer("Ounifi.AbdelRahim.JWT")
                                .AddAudience("Ounifi.AbdelRahim.JWT")
                                .AddClaim("ID", user.UserId.ToString())
                                .AddClaim("Privilege", user.Privilege)
                                .AddClaim("UserName", user.UserName)
                                .AddClaim("UserLName", user.UserLName)
                                .AddExpiry(10)
                                .Build();
            var dict = new Dictionary<string, string>();
            dict.Add("status", "success");
            dict.Add("token", token.Value);
            return Ok(dict);
        }
    }
}
