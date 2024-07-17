using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using PrinceQ.Models.Entities;
using PrinceQ.Models.ViewModel;
using static PrinceQ.DataAccess.Response.ServiceResponses;

namespace PrinceQ.DataAccess.Interfaces
{
    public interface IAdmin
    {

        //-----DASHBOARD-----//
        //Task<CommonResponse> GetDataYAndM(string year, string month);


        //-----USERS-----//
        Task<GeneralResponse> UserPage();
        Task<GeneralResponse> AllUsers();
        Task<UserRolesResponse> UserDetail(string? id);
        Task<CommonResponse> DeleteUser(string? id);
        Task<CommonResponse> AddUser(UserVM model);
        Task<CommonResponse> UpdateUser(UserVM model);
        Task<UserCategoryResponse> UserCategory(string? userId);
        Task<UserCategoriesResponse> GetAssignCategory(string? userId);
        Task<CommonResponse> AddAssignUserCategories(int[] categoryId, string userId);
        Task<CommonResponse> RemoveAssignUserCategories(int categoryId, string userId);

        //-----MANAGE VIDEO-----//
        Task<videoFilesResponse> AllVideos();
        Task<videoFilesResponse> PlayVideo(string videoName);
        Task<GeneralResponse> DeleteVideo(string videoName);
        Task<GeneralResponse> UploadVideo(IFormFile videoFile);


        //-----Announcement-----//
        Task<DualResponse> AnnouncementDetail(int? id);
        Task<GeneralResponse> AllAnnouncement();
        Task<CommonResponse> AddAnnouncement(Announcement model);
        Task<CommonResponse> UpdateAnnouncement(Announcement model);
        Task<CommonResponse> DeleteAnnouncement(int? id);
        Task<GeneralResponse> GetTotalAnnounce();




    }
}
