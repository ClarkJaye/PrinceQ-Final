using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PrinceQ.DataAccess.Hubs;
using PrinceQ.DataAccess.Repository;
using System.Management;

namespace PrinceQueuing.Controllers
{
    public class DisplayController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<QueueHub> _hubContext;

        public DisplayController(IUnitOfWork unitOfWork, IHubContext<QueueHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public IActionResult Home()
        {
            var videoFiles = Directory.GetFiles("wwwroot/Videos")
           .Select(f => f.Replace("wwwroot", string.Empty))
           .ToArray();

            ViewBag.VideoFiles = videoFiles;

            return View();
        }

        //GET QUEUE
        public async Task<IActionResult> GetServings()
        {
            try
            {
                var user = await _unitOfWork.users.GetAll();
                var queueSer = await _unitOfWork.servings.GetAll(u => u.Served_At.Date == DateTime.Today);
                var queueServe = queueSer.OrderByDescending(q => q.Served_At);

                return Json(queueServe);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }





    }
}
