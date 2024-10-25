using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PensionFund.AdminPanel.Contract;
using PensionFund.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionFund.AdminPanel
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController(IUserService userService)
        : Controller
    {
        private readonly IUserService _userService = userService;

        private async Task<bool> UserExists(Guid id)
        {
            var users = await _userService.GetAllUsers();

            return users.Any(user => user.Id == id);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetUsers(
            string sortOrder, string searchString)
        {
            var users = await _userService.GetAllUsers();

            if (!string.IsNullOrEmpty(searchString))
            {
                users = (List<Core.Models.User>)users.Where(set => set.Login.Contains(searchString)
                    || set.Role.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "login_desc":
                    {
                        ViewData["LoginSortParam"] = "login_desc";
                        users = (List<Core.Models.User>)users
                            .OrderByDescending(set => set.Login);
                        break;
                    }
                case "login_asc":
                    {
                        ViewData["LoginSortParam"] = "login_asc";
                        users = (List<Core.Models.User>)users
                            .OrderBy(set => set.Login);
                        break;
                    }
                case "role_desc":
                    {
                        ViewData["RoleSortParam"] = "role_desc";
                        users = (List<Core.Models.User>)users
                            .OrderByDescending(set => set.Role);
                        break;
                    }
                case "role_asc":
                    {
                        ViewData["RoleSortParam"] = "role_asc";
                        users = (List<Core.Models.User>)users
                            .OrderBy(set => set.Role);
                        break;
                    }
                default:
                    {
                        users = (List<Core.Models.User>)users
                            .OrderBy(set => set.Login);
                        break;
                    }
            }

            var response = users.ToList();

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(id)))
            {
                return BadRequest();
            }

            var users = await _userService.GetAllUsers();

            var response = users.Where(user => user.Id == id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser(
            [FromBody] UserRequest request)
        {
            var (user, error) = Core.Models.User.Create(
                Guid.NewGuid(),
                request.Login,
                request.Password,
                request.Role);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var userId = await _userService.CreateUser(user);

            return Ok(userId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id,
            [FromBody] UserRequest request)
        {
            return Ok(await _userService.UpdateUser(id, request.Login, request.Password, request.Role));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveUser(Guid id,
            [FromBody] UserRequest user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.UpdateUser(user.Id, user.Login,
                        user.Password, user.Role);

                    return RedirectToAction(nameof(GetUsers));

                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return Ok(user);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteUser(Guid id)
        {
            return Ok(await _userService.DeleteUser(id));
        }
    }
}
