using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrinceQ.DataAccess.Extensions;
using PrinceQ.DataAccess.Repository;
using PrinceQ.Models.Entities;
using PrinceQ.Models.ViewModel;
using PrinceQ.Utility;
using System.Globalization;

namespace PrinceQueuing.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]

    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<AdminController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        //-----DASHBOARD-----//
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDataByYearAndMonth(string year, string month)
        {
            try
            {
                if (year != null && month == null)
                {
                    // Retrieve monthly data
                    var data = await _unitOfWork.queueNumbers.GetAll(d => d.StatusId == 2 && d.QueueId!.Substring(0, 4) == year);
                    var monthlyData = data
                        .GroupBy(item => item.QueueId!.Substring(4, 2))
                        .Select(g => new
                        {
                            Month = g.Key,
                            CategoryASum = g.Sum(i => i.CategoryId == 1 ? 1 : 0),
                            CategoryBSum = g.Sum(i => i.CategoryId == 2 ? 1 : 0),
                            CategoryCSum = g.Sum(i => i.CategoryId == 3 ? 1 : 0),
                            CategoryDSum = g.Sum(i => i.CategoryId == 4 ? 1 : 0),
                            GenerateDate = g.Select(i => i.QueueId).FirstOrDefault()
                        })
                        .ToList();
                    return Json(new { IsMonth = true, value = monthlyData });
                }
                else if (year != null && month != null)
                {
                    var data = await _unitOfWork.queueNumbers.GetAll(d => d.StatusId == 2 && d.QueueId!.Substring(0, 6) == year + month);
                    var dailyData = data
                        .GroupBy(item => item.QueueId!.Substring(6, 2))
                        .Select(g => new
                        {
                            Day = g.Key,
                            CategoryASum = g.Sum(i => i.CategoryId == 1 ? 1 : 0),
                            CategoryBSum = g.Sum(i => i.CategoryId == 2 ? 1 : 0),
                            CategoryCSum = g.Sum(i => i.CategoryId == 3 ? 1 : 0),
                            CategoryDSum = g.Sum(i => i.CategoryId == 4 ? 1 : 0),
                            GenerateDate = g.Select(i => i.QueueId).FirstOrDefault()
                        })
                        .ToList();

                    return Json(new { IsMonth = false, value = dailyData });
                }
                else
                {
                    return Json(null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDataByYearAndMonth action");
                return Json(new { IsSuccess = false, message = "An error occurred in GetDataByYearAndMonth." });
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> LoadData(string year, string month)
        //{
        //    try
        //    {
        //        if (year != null && month == null)
        //        {
        //            var data = await _unitOfWork.queueNumbers.GetAll(d =>
        //                d.StatusId == 2 &&
        //                d.QueueId!.Substring(0, 4) == year &&
        //                d.ForFilling_start.HasValue &&
        //                d.ForFilling_end.HasValue);

        //            var monthlyData = data
        //                .GroupBy(item => item.CategoryId)
        //                .Select(g => new
        //                {
        //                    CategoryId = g.Key,
        //                    //AverageForFillingTimeInMinutes = (int)(g.Average(i => (i.ForFilling_end!.Value - i.ForFilling_start!.Value).TotalSeconds) / 60),
        //                    AverageForFillingTimeInMinutesAndSeconds = FormatTimeSpan(g.Average(i => (i.ForFilling_end!.Value - i.ForFilling_start!.Value).TotalSeconds))
        //                })
        //                .ToList();

        //            return Json(new { ByMonth = true, value = monthlyData });
        //        }
        //        else
        //        {
        //            return Json(null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in LoadData action");
        //        return Json(new { IsSuccess = false, message = "An error occurred in LoadData." });
        //    }
        //}
        //private string FormatTimeSpan(double totalSeconds)
        //{
        //    var timeSpan = TimeSpan.FromSeconds(totalSeconds);
        //    int minutes = (int)timeSpan.TotalMinutes;
        //    double seconds = timeSpan.Seconds + (timeSpan.Milliseconds / 1000.0);
        //    return $"{(int)minutes}.{(int)seconds:00}";
        //}

        //private string FormatTimeSpan(double totalSeconds)
        //{
        //    var timeSpan = TimeSpan.FromSeconds(totalSeconds);
        //    return $"{(int)timeSpan.TotalMinutes} minutes and {timeSpan.Seconds} seconds";
        //}






        //-----USERS-----//


        public IActionResult Users()
        {
            var roles = _unitOfWork.auth.GetAllRoles();

            var roleList = roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });

            var model = new RegisterVM
            {
                RoleList = roleList
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string? id)
        {
            try
            {
                var userToBeEdit = await _unitOfWork.users.Get(u => u.Id == id);
                //var roles = await _userManager.GetRolesAsync(userToBeEdit!);

                if (userToBeEdit == null) return Json(new { IsSuccess = false, Message = "Error occurred while getting the User" });
                var userRole = await _userManager.GetRolesAsync(userToBeEdit);

                var user = new
                {
                    id = userToBeEdit.Id,
                    userName = userToBeEdit.UserName,
                    email = userToBeEdit.Email,
                    role = userRole,
                    isActive = userToBeEdit.IsActive,
                };

                var roles = await _roleManager.Roles.ToListAsync();

                return Json(new { IsSuccess = true, user, roles, Message = "User get successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUser action");
                return Json(new { IsSuccess = false, message = "An error occurred in GetUser." });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.users.GetAll();
                var usersWithRole = users.Select(async user =>
                {
                    var roles = await _unitOfWork.auth.GetUserRolesAsync(user);
                    return new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.PhoneNumber,
                        user.Created_At,
                        user.IsActiveId,
                        Roles = roles
                    };
                }).Select(t => t.Result).ToList();

                // Sort the users based on their roles
                var sortedUsers = usersWithRole.OrderByDescending(u => u.Roles.Contains("Clerk"))
                                              .ThenByDescending(u => u.Roles.Contains("RegisterPersonnel"))
                                              .ThenByDescending(u => u.Roles.Contains("Admin"))
                                              .ThenBy(u => u.Created_At)
                                              .ToList();

                if (sortedUsers == null) return Json(new { IsSuccess = false, Message = "Retrieved users failed." });
                return Json(new { IsSuccess = true, data = sortedUsers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllUsers action");
                return Json(new { IsSuccess = false, message = "An error occurred in GetAllUsers." });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = new()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        IsActiveId = 1
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        if (!String.IsNullOrEmpty(model.Role))
                        {
                            await _userManager.AddToRoleAsync(user, model.Role);
                        }
                    }
                    await _unitOfWork.SaveAsync();
                    return Json(new { IsSuccess = true, Message = "User added successfully" });
                }
                return Json(new { IsSuccess = false, Message = "Add user failed!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddUsers action");
                return Json(new { IsSuccess = false, Message = "An error occurred in AddUsers." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByIdAsync(model.Id);

                    if (user != null)
                    {
                        user.UserName = model.UserName;
                        user.Email = model.Email;
                        user.IsActiveId = (int)model.IsActiveId!;

                        if (!String.IsNullOrEmpty(model.Password))
                        {
                            var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                            user.PasswordHash = newPasswordHash;
                        }

                        // Remove user from all existing roles
                        var existingRoles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, existingRoles);

                        if (!String.IsNullOrEmpty(model.Role))
                        {
                            await _userManager.AddToRoleAsync(user, model.Role);
                        }

                        await _unitOfWork.SaveAsync();
                        return Json(new { IsSuccess = true, Message = "User updated successfully" });
                    }
                    else
                    {
                        return Json(new { IsSuccess = false, Message = "User not found" });
                    }
                }

                return Json(new { IsSuccess = false, Message = "Update User failed!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateUser action");
                return Json(new { IsSuccess = false, Message = "An error occurred in UpdateUser." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserCategories(string? userId)
        {
            var user = await _unitOfWork.users.Get(u => u.Id == userId);
            var categories = await _unitOfWork.category.GetAll();
            var userCategories = await _unitOfWork.userCategories.GetAll(uc => uc.UserId == userId);

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve
            //};

            //var serializedData = JsonSerializer.Serialize(new { user?.UserName, categories, userCategories }, options);

            //return Content(serializedData, "application/json");

            return Json(new { user, categories, userCategories });
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignCategories(string? userId)
        {
            var user = await _unitOfWork.users.Get(u => u.Id == userId);
            var categories = await _unitOfWork.category.GetAll();
            var userCategories = await _unitOfWork.userCategories.GetAll(uc => uc.UserId == userId);

            return Json(userCategories);
        }
        [HttpPost]
        public async Task<IActionResult> AddAssignUserCategories(int[] categoryId, string userId)
        {
            if (categoryId.Length == 0 || userId == null)
            {
                return Json(new { IsSuccess = false, Message = "User assign failed." });
            }
            var existingUserCategories = await _unitOfWork.userCategories.GetAll(c => c.UserId == userId);

            foreach (int catId in categoryId)
            {
                if (!existingUserCategories.Any(uc => uc.CategoryId == catId))
                {
                    User_Category userCat = new User_Category()
                    {
                        CategoryId = catId,
                        UserId = userId
                    };
                    _unitOfWork.userCategories.Add(userCat);
                }
            }
            await _unitOfWork.SaveAsync();
            return Json(new { IsSuccess = true, Message = "User assign successful." });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAssignUserCategories(int categoryId, string userId)
        {
            if (categoryId == 0 || userId == null)
            {
                return Json(new { IsSuccess = false, Message = "User remove failed." });
            }
            var userCategory = await _unitOfWork.userCategories.Get(uc => uc.CategoryId == categoryId && uc.UserId == userId);

            _unitOfWork.userCategories.Remove(userCategory!);
            await _unitOfWork.SaveAsync();
            return Json(new { IsSuccess = true, Message = "User remove successful." });
        }







        //-----MANAGE VIDEO-----//
        public IActionResult ManageVideo()
        {
            return View();
        }

        public IActionResult AllVideos()
        {
            var videoFiles = Directory.GetFiles("wwwroot/Videos")
             .Select(f => f.Replace("wwwroot", string.Empty))
             .ToArray();

            return Json(videoFiles);
        }



        //-----SERVING RESPORT-----//
        public IActionResult ServingReport()
        {
            return View();
        }





        //-----Clerk RESPORT-----//
        public IActionResult ClerkReport()
        {
            return View();
        }









    }


}
