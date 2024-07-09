using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PrinceQ.Utility;
using PrinceQ.Models.Entities;
using PrinceQ.Models.ViewModel.RP;
using PrinceQ.DataAccess.Interfaces;
using static PrinceQ.DataAccess.Response.ServiceResponses;

namespace PrinceQueuing.Controllers
{
    [Authorize(Roles = SD.Role_Register)]
    public class RegisterPersonnelController : Controller
    {
        private readonly IRegisterPersonnel _personnel;
        private readonly ILogger<RegisterPersonnelController> _logger;

        public RegisterPersonnelController(IRegisterPersonnel personnel, ILogger<RegisterPersonnelController> logger)
        {
            _personnel = personnel;
            _logger = logger;
        }

        //Dashboard
        public async Task<IActionResult> Home()
        {
            try
            {
                var response = await _personnel.HomeVM();

                if (response.IsSuccess)
                {
                    return View(response.Obj);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Home action");
                return Json(new { IsSuccess = false, Message = "There is an error!" });
            }
        }

        ////TEMPORARY
        //[HttpPost]
        //public async Task<IActionResult> GenerateQueueNumber(int categoryId)
        //{
        //    try
        //    {
        //        var response = await _personnel.GenerateQueue(categoryId);
        //        if (response.IsSuccess)
        //        {
        //            return Json(response);
        //        }
        //        return NotFound(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in Home action");
        //        return Json(new { IsSuccess = false, Message = "There is an error!" });
        //    }
        //}



        // ------------ PRINTT ----------- //

        //FOr Printing the Queue
        public async Task<IActionResult> Print_Form(string date, int categoryId, int queueNumber)
        {
            try
            {
                var response = await _personnel.GetQueue(date, categoryId, queueNumber);

                if (response.IsSuccess)
                {
                    return View(response.Obj);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Print_Form action");
                return Json(new { IsSuccess = false, Message = "There is an error!" });
            }

        }

        //Process
        public async Task<IActionResult> Print_QueueNumber(string date, int categoryId, int queueNumber)
        {
            try
            {
                var response = await _personnel.GetQueue(date, categoryId, queueNumber);

                if (response.IsSuccess)
                {
                    return Json(response);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Print_QueueNumber action");
                return Json(new { IsSuccess = false, Message = "There is an error!" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> GenerateQueueNumber(int categoryId)
        {
            try
            {
                var response = await _personnel.GenerateQueue(categoryId);
                if (response.IsSuccess)
                {
                    return Json(response);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GenerateQueueNumber action");
                return Json(new { IsSuccess = false, Message = "There is an error!" });
            }
        }


    }


}

