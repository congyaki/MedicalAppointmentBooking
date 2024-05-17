using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalAppointmentBooking.WebAPI.ViewModels;
using System.Security.Cryptography;
using System.Text;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.Models.EF;

namespace MedicalAppointmentBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MedicalAppointmentBookingDbContext _context;

        public UsersController(MedicalAppointmentBookingDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> PostUser(FormUserLoginVM _user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'MedicalAppointmentBookingDbContext.Users'  is null.");
          }
            try
            {
                var check = await _context.Users.Where(u => u.UserName == _user.UserName &&
                                                        u.Password == GetMD5(_user.Password))
                                                   .FirstOrDefaultAsync();
                if (check != null && check.Id > 0)
                {
                    return Ok(check);
                }
                return Ok(0);

            } catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("register")]

        public async Task<ActionResult> Post([FromForm] FormUserLoginVM _user)
        {
            var check = _context.Users.Where(u => u.UserName == _user.UserName.ToLower())
                                        .ToList();
            if(check.Count > 0)
            {
                return Ok(-1);
            }

            var user = new User
            {
                UserName = _user.UserName,
                Password = GetMD5(_user.Password),
                Email = _user.Email.ToLower(),
                Address = _user.Address,
                DateOfBirth = _user.DateOfBirth,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                Gender = _user.Gender,
                PhoneNumber = _user.PhoneNumber,
            };

            _context.Users.Add(user); 
            await _context.SaveChangesAsync();
            int _insertID = user.Id;

            if(_insertID > 0)
            {
                return Ok(_insertID);
            }

            return Ok(0);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2string = null;

            for(int i = 0; i < targetData.Length; i++)
            {
                byte2string += targetData[i].ToString("x2");

            }
            return byte2string;
        }
    }
}
