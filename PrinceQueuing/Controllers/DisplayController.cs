using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DisplayController(IUnitOfWork unitOfWork, IHubContext<QueueHub> hubContext, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //public IActionResult Home()
        //{
        //    var videoFiles = Directory.GetFiles("wwwroot/Videos")
        //   .Select(f => f.Replace("wwwroot", string.Empty))
        //   .ToArray();

        //    ViewBag.VideoFiles = videoFiles;

        //    return View();
        //}

        public IActionResult Home()
        {
            string[] videoFiles = Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "Videos"))
                .Select(f => new FileInfo(f))
                .OrderByDescending(f => f.CreationTime)
                .Select(f => f.FullName.Replace(_webHostEnvironment.WebRootPath, string.Empty))
                .ToArray();

            ViewBag.VideoFiles = videoFiles;

            return View();
        }
        //public IActionResult AllVideos()
        //{
        //    var videoFiles = Directory.GetFiles("wwwroot/Videos")
        //     .Select(f => f.Replace("wwwroot", string.Empty))
        //     .ToArray();

        //    return Json(videoFiles);
        //}

        //GET QUEUE
        public async Task<IActionResult> GetServings()
        {
            try
            {
                var user = await _unitOfWork.users.GetAll();
                var queueSer = await _unitOfWork.servings.GetAll(u => u.Served_At.Date == DateTime.Today);
                var clerkNumber = await _unitOfWork.device.GetAll();
                var queueServe = queueSer.OrderByDescending(q => q.Served_At);

                return Json(new {queues = queueServe, device = clerkNumber});

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }


    }
}
