using static PrinceQ.DataAccess.Response.ServiceResponses;

namespace PrinceQ.DataAccess.Interfaces
{
    public interface IClerk
    {
        Task<GeneralResponse> ServingVM(string userId, string deviceId);
        Task<GeneralResponse> GetAllWaitingQueue(string userId);
        Task<DualResponse> DesignatedClerk(string deviceId, string userId);
        Task<GetResponse> GetServings(string userId, string deviceId);
        Task<GeneralResponse> GetReservedQueues(string userId);
        Task<GeneralResponse> GetFillingUpQueues(string userId);
        Task<GeneralResponse> GetReleasingQueues(string userId);
        Task<GeneralResponse> NextQueueNumber(int Id, string userId);
        Task<GeneralResponse> ReserveQueueNumber(string userId, string deviceId);
        Task<GeneralResponse> CancelQueueNumber(string userId, string deviceId);
        Task<GeneralResponse> ServeQueueFromTable(string generateDate, int categoryId, int qNumber, string userId);
        Task<GeneralResponse> CancelQueueFromTable(string generateDate, int categoryId, int qNumber, string userId);
        Task<GeneralResponse> ToReleaseQueue(string generateDate, int categoryId, int qNumber, string userId);
        Task<CommonResponse> ToUpdateQueue(string generateDate, int categoryId, int qNumber, string userId, int cheque);

    }

}
