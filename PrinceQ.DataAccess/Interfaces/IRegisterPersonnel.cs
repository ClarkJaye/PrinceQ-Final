using static PrinceQ.DataAccess.Response.ServiceResponses;

namespace PrinceQ.DataAccess.Interfaces
{
    public interface IRegisterPersonnel
    {
        Task<GeneralResponse> HomeVM();
        Task<MultiResponse> GenerateQueue(int categoryId);
        Task<GeneralResponse> GetQueue(string date, int categoryId, int queueNumber);


    }
}
