using Microsoft.AspNetCore.SignalR;
using PrinceQ.DataAccess.Hubs;
using PrinceQ.DataAccess.Interfaces;
using PrinceQ.DataAccess.Repository;
using PrinceQ.Models.Entities;
using PrinceQ.Models.ViewModel.RP;
using static PrinceQ.DataAccess.Response.ServiceResponses;

namespace PrinceQ.DataAccess.Services
{
    public class RegisterPersonnelService : IRegisterPersonnel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<QueueHub> _hubContext;

        public RegisterPersonnelService(IUnitOfWork unitOfWork, IHubContext<QueueHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        //For Home Viewmodel
        public async Task<GeneralResponse> HomeVM()
        {
            var viewModel = new RPVM
            {
                Categories = await _unitOfWork.category.GetAll()
            };
            if (viewModel is null) return new GeneralResponse(false, null, "failed");

            return new GeneralResponse(true, viewModel, "Success");
        }

        //Generate Qeuue Temporary
        public async Task<DualResponse> GenerateQueue(int categoryId)
        {
            if(categoryId == 0) return new DualResponse(false, null, null, "there is no an error!");

            var currentDate = DateTime.Today.ToString("yyyyMMdd");
            var selectedCategory = await _unitOfWork.category.Get(c => c.CategoryId == categoryId);

            var queuesResult = await _unitOfWork.queueNumbers.GetAll(q => q.CategoryId == categoryId && q.QueueId == currentDate);
            var lastQueueNumber = queuesResult.OrderByDescending(q => q.QueueNumber).FirstOrDefault();

            var queueCount = lastQueueNumber != null && lastQueueNumber.QueueId == currentDate ? (int)lastQueueNumber.QueueNumber! + 1 : 1;

            var queueNumber = new Queues
            {
                QueueId = currentDate,
                CategoryId = categoryId,
                StatusId = 1,
                QueueNumber = queueCount,
                //StageId = 1,
            };

            _unitOfWork.queueNumbers.Add(queueNumber);
            await _unitOfWork.SaveAsync();
            await _hubContext.Clients.All.SendAsync("UpdateQueueFromPersonnel");

            return new DualResponse(true, queueNumber, selectedCategory, "Generation successful");
        }

        //Get Queue
        public async Task<GeneralResponse> GetQueue(string date, int categoryId, int queueNumber)
        {
            var queue = await _unitOfWork.queueNumbers.Get(a => a.CategoryId == categoryId && a.QueueNumber == queueNumber && a.QueueId == date);
            if (queue == null) return new GeneralResponse(false, null, "Not found!");

            return new GeneralResponse(true, queue, "Print Successful!");
        }

      
 

     




    }
}
