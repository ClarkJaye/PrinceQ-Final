namespace PrinceQ.DataAccess.Response
{
    public class ServiceResponses
    {
        //public record class GeneralResponse(bool IsSuccess, string Message);
        public record class GeneralResponse(bool IsSuccess, object? Obj, string Message);
        public record class DualResponse(bool IsSuccess, object? device, object? user, string Message);
        public record class GetResponse(bool IsSuccess, int? CategoryId, int? QueueNumber, string Message);
        public record class MultiResponse(bool IsSuccess, object? Queue, object? Category, string Message);
    }
}
