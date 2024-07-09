using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrinceQ.DataAccess.Interfaces;
using PrinceQ.Utility;
using PrinceQueuing.Extensions;
using System.Management;

namespace PrinceQueuing.Controllers
{
    [Authorize(Roles = SD.Role_Clerk)]
    public class ClerkController : Controller
    {
        private readonly IClerk _clerk;
        private readonly ILogger<ClerkController> _logger;

        public ClerkController(IClerk clerk, ILogger<ClerkController> logger)
        {
            _clerk = clerk;
            _logger = logger;
        }

        //Dashboard
        public async Task<IActionResult> Serving()
        {
            try
            {
                var userId = GetCurrentUserId();
                var deviceId = GetDeviceId(); 
                var response = await _clerk.ServingVM(userId, deviceId);

                if (response.IsSuccess)
                {
                    return View(response.Obj);
                }
                return RedirectToAction("Login", "Account");
                //return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NextQueue action");
                return Json(new { IsSuccess = false, message = "An error occurred in NextQueue." });
            }
        }

        //Get the designated Clerk Number
        public async Task<IActionResult> DesignatedDeviceId()
        {
            var deviceId = GetDeviceId();
            var userId = GetCurrentUserId();
            var response = await _clerk.DesignatedClerk(deviceId, userId);

            return Json(response);
        }




        //Get All Categories
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var userId = GetCurrentUserId();
                var response = await _clerk.GetAllWaitingQueue(userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCategories action");
                return Json(new { IsSuccess = false, message = "An error occurred while fetching the categories." });
            }
        }

        //Get Clerk Serving
        public async Task<IActionResult> GetServings()
        {
            try
            {
                var userId = GetCurrentUserId();
                var response = await _clerk.GetServings(userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetServings action");
                return Json(new { IsSuccess = false, message = "An error occurred while fetching the GetServings." });
            }
        }

        //Get All Reserve Queue 
        public async Task<IActionResult> GetReservedQueues()
        {
            try
            {
                // Get the userId of the current user
                var userId = GetCurrentUserId();
                var response = await _clerk.GetReservedQueues(userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetReservedQueues action");
                return Json(new { IsSuccess = false, message = "An error occurred while fetching the GetReservedQueues." });
            }
        }

        //Get All Filling Queue 
        public async Task<IActionResult> GetAllFillingUpQueues()
        {
            try
            {
                // Get the userId of the current user
                var userId = GetCurrentUserId();
                var response = await _clerk.GetFillingUpQueues(userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetReservedQueues action");
                return Json(new { IsSuccess = false, message = "An error occurred while fetching the GetReservedQueues." });
            }
        }
        
        //Get All Releasing Queue 
        public async Task<IActionResult> GetAllReleasingQueues()
        {
            try
            {
                // Get the userId of the current user
                var userId = GetCurrentUserId();
                var response = await _clerk.GetReleasingQueues(userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetReservedQueues action");
                return Json(new { IsSuccess = false, message = "An error occurred while fetching the GetReservedQueues." });
            }
        }


        //NEXT QUEUE
        public async Task<IActionResult> NextQueue(int id)
        {
            try
            {
                //User ID
                var userId = GetCurrentUserId();
                var response = await _clerk.NextQueueNumber(id, userId);
                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NextQueue action");
                return Json(new { IsSuccess = false, message = "An error occurred in NextQueue." });
            }
        }

        //RESERVE QUEUE
        public async Task<IActionResult> ReserveQueue()
        {
            try
            {
                var userId = GetCurrentUserId();
                var response = await _clerk.ReserveQueueNumber(userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ReserveQueue action");
                return Json(new { IsSuccess = false, message = "An error occurred while processing the ReserveQueue." });
            }
        }


        //CANCEL QUEUE
        public async Task<IActionResult> CancelQueue()
        {
            try
            {
                var userId = GetCurrentUserId();
                var response = await _clerk.CancelQueueNumber(userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CancelQueue action");
                return Json(new { IsSuccess = false, message = "An error occurred in CancelQueue." });
            }
        }


        ////// -------------QUEUE RESERVED-------------- //////


        //Serve IN RESERVE QUEUE
        public async Task<JsonResult> ServeQueueFromTable(string generateDate, int categoryId, int qNumber )
        {
            try
            {
                //User ID
                var userId = GetCurrentUserId();
                var response = await _clerk.ServeQueueFromTable(generateDate, categoryId, qNumber, userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ServeInReserve action");
                return Json(new { IsSuccess = false, message = "An error occurred while processing the ServeInReserve." });
            }
        }


        //CANCEL IN RESERVE QUEUE
        public async Task<JsonResult> CancelQueueNumber(string generateDate, int categoryId, int qNumber)
        {
            try
            {
                var userId = GetCurrentUserId();
                var response = await _clerk.CancelQueueFromTable(generateDate, categoryId, qNumber, userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ReserveCancel action");
                return Json(new { IsSuccess = false, message = "An error occurred in ReserveCancel." });
            }
        }



        //QUEUE FROM FILLING TRANSFER TO RELEASING TABLE
        public async Task<JsonResult> FillingToReleaseQueue(string generateDate, int categoryId, int qNumber)
        {
            try
            {
                var userId = GetCurrentUserId();
                var response = await _clerk.ToReleaseQueue(generateDate, categoryId, qNumber, userId);

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ReserveCancel action");
                return Json(new { IsSuccess = false, message = "An error occurred in ReserveCancel." });
            }
        }


        //HELPER
        private string GetCurrentUserId()
        {
            return User.GetUserId();
        }

        private string GetDeviceId()
        {
            ManagementObject os = new ManagementObject("Win32_OperatingSystem=@");
            string deviceId = os["SerialNumber"].ToString()!;

            return deviceId;
        }



    }


}
