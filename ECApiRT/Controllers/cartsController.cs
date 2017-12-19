using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECApiRT.EF;
using ECApiRT.Models;
using Microsoft.AspNetCore.Authorization;

namespace ECApiRT.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/carts")]
    public class cartsController : Controller
    {
        private readonly DataContext _context;

        public cartsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/carts
        [HttpGet]
        [Authorize(Policy = "PrivilegeAdmin")]
        public IEnumerable<cart> Getcart()
        {
            return _context.cart;
        }

        // GET: api/carts/5
        [HttpGet("{id}")]
        [Authorize(Policy = "PrivilegeAdmin")]
        public async Task<IActionResult> Getcart([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await _context.cart.SingleOrDefaultAsync(m => m.cartID == id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // PUT: api/carts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcart([FromRoute] int id, [FromBody] cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cart.cartID)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/carts
        [HttpPost]
        public async Task<IActionResult> Postcart([FromBody] cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.cart.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcart", new { id = cart.cartID }, cart);
        }

        // DELETE: api/carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecart([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await _context.cart.SingleOrDefaultAsync(m => m.cartID == id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.cart.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok(cart);
        }

        private bool cartExists(int id)
        {
            return _context.cart.Any(e => e.cartID == id);
        }


        // GET: api/carts/achats/1
        [HttpGet]
        [Route("achats/{id}")]
        public IEnumerable<Product> FindAchatsProduct(int id)
        {
            var cart = _context.cart.Where(p => p.UserId == id && p.productName == "achat" ).Select(u => u.productID);
            var Products = _context.Product.Where(t => cart.Contains(t.productID)).ToList();
            return Products;
        }

        // GET: api/carts/favorie/1
        [HttpGet]
        [Route("favorie/{id}")]
        public IEnumerable<Product> FindFavorieProduct(int id)
        {
            var cart = _context.cart.Where(p => p.UserId == id && p.productName == "favorie").Select(u => u.productID);
            var Products = _context.Product.Where(t => cart.Contains(t.productID)).ToList();
            return Products;
        }
        // GET: api/carts/returnIDF/1/2
        [HttpDelete]
        [Route("delatea/{id1}/{id2}")]
        public async Task<IActionResult> DelateAchatsId(int id1, int id2)
        {
            var cart = await _context.cart.SingleOrDefaultAsync(p => p.UserId == id1 && p.productID == id2 && p.productName == "achat");
            if (cart == null)
            {
                return NotFound();
            }

            _context.cart.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok(cart);
        }
        // GET: api/carts/returnIDF/1/2
        [HttpDelete]
        [Route("delatef/{id1}/{id2}")]
        public async Task<IActionResult> FindFavorieId(int id1, int id2)
        {
            var cart = await _context.cart.SingleOrDefaultAsync(p => p.UserId == id1 && p.productID == id2 && p.productName == "favorie");
            if (cart == null)
            {
                return NotFound();
            }

            _context.cart.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok(cart);
        }

    }
}