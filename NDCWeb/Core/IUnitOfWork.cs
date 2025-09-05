using NDCWeb.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace NDCWeb.Core
{
    public interface IUnitOfWork : IDisposable
    {
        #region admin
        IMenuItemMasterRepository MenuItemMstrRepo { get; }
        IMenuUrlMasterRepository MenuUrlMstrRepo { get; }
        IPageContentRepository PageContentRepo { get; }
        IMenuRoleRepository MenuRoleRepo { get; }
        IMediaCategoryMasterRepository MediaCategoryMstrRepo { get; }
        INewsArticleRepository NewsArticleRepo { get; }
        IMediaGalleryRepository MediaGalleryRepo { get; }
        IMediaFileRepository MediaFileRepo { get; }
        ICountryMasterRepository CountryMasterRepo { get; }
        IStateMasterRepository StateMasterRepo { get; }
        ICityMasterRepository CityMasterRepo { get; }
        ICommunityRepository CommunityRepo { get; }
        #endregion

        IRankMasterRepository RankMasterRepo { get; }
        IFacultyRepository FacultyRepo { get; }
        IAppointmentDetailRepository AppointmentDetailRepo { get; }
        IStaffDocumentRepository StaffDocumentRepo { get; }
        IStaffPhotoRepository StaffPhotoRepo { get; }
        IStaffMasterRepository StaffMasterRepo { get; }
        ICourseRepository CourseRepo { get; }
        ISiteFeedbackRepository SiteFeedbackRepo { get; }

        #region Member
        ICourseMemberRepository CourseMemberRepo { get; }
        ICrsMbrLanguageRepository CrsMbrLanguageRepo { get; }
        ICrsMbrQualificationRepository CrsMbrQualificationRepo { get; }
        ICountryVisitRepository CountryVisitRepo { get; }
        IHonourAwardRepository HonourAwardRepo { get; }
        ICrsMbrBiographyRepository CrsMbrBiographyRepo { get; }
        ICrsMbrHobbyRepository CrsMbrHobbyRepo { get; }
        ICrsMbrSportRepository CrsMbrSportRepo { get; }
        IAsgmtAppointmentRepository AsgmtAppointmentRepo { get; }
        ICourseRegisterRepository CourseRegisterRepo { get; }
        IArrivalDetailRepository ArrivalDetailRepo { get; }
        IArrivalMealRepository ArrivalMealRepo { get; }
        ITallyDetailRepository TallyDetailRepo { get; }
        IServiceRationRepository ServiceRationRepo { get; }
        IAccountInfoRepository AccountInfoRepo { get; }
        ICrsMbrSpouseRepository CrsMbrSpouseRepo { get; }
        IRakshikaRepository RakshikaRepo { get; }
        ICrsMemberPersonalRepository CrsMbrPersonalRepo { get; }
        ICrsMbrAppointmentRepository CrsMbrAppointmentRepo { get; }
        ICrsMbrAddressRepository CrsMbrAddressRepo { get; }
        IVisaDetailRepository VisaDetailRepo { get; }
        IPassportDetailRepository PassportDetailRepo { get; }
        ISpouseLanguageRepository SpouseLanguageRepo { get; }
        ISpouseChildrenRepository SpouseChildrenRepo { get; }
        ISpouseQualificationRepository SpouseQualificationRepo { get; }
        IChildrenPassportRepository ChildrenPassportRepo { get; }
        ICourseFeedbackRepository CourseFeedbackRepo { get; }
        ICrsMbrVehicleStickerRepository CrsMbrVehicleStickerRepo { get; }

        IMPhilMemberRepository MPhilMemberRepo { get; }
        IMPhilPostGraduateRepository MPhilPostGraduateRepo { get; }
        IArrivalAccompaniedRepository ArrivalAccompaniedRepo { get; }


        IForumBlogRepository ForumBlogRepo { get; }
        IForumBlogMediaRepository ForumBlogMediaRepo { get; }
        ICircularRepository CircularRepo { get; }
        ICircularMediaRepository CircularMediaRepo { get; }
        ITADAClaimsRepository TADAClaimsRepo { get; }
        IInfotechRepository InfotechRepo { get; }
        // ITelecommRequirementRepository TelecommRequirementRepo { get; }
        //ISubjectMasterRepository SubjectMasterRepo { get; }
        //IFeedbackModuleRepository IFeedbackModuleRepo { get; }
        //IFeedbackSpeakerRepository IFeedbackSpeakerRepo { get; }

        IEventMemberRepository EventMemberRepo { get; }
        IMPhilDegreeRepository MPhilDegreeRepo { get; }
        #endregion

        #region Staff
        ILockerAllotmentRepository LockerAllotmentRepo { get; }
        IEventRepository EventRepo { get; }
        #endregion

        IMemberIAGRoleRepository MemberIAGRoleRepo { get; }

        #region Feedback
        ISpeechEventRepository SpeechEventRepo { get; }
        #endregion

        ITrainingActivityRepository TrainingActivityRepo { get; }
        ITrainingActivityMediaRepository TrainingActivityMediaRepo { get; }


        #region Alumni
        IAlumniFeedbackRepository AlumniFeedbackRepo { get; }
        #endregion

        #region IN Step
        IInStepRegistrationRepository InStepRegistrationRepo { get; }
        IInStepCourseRepository InStepCourseRepo { get; }
        #endregion

        int Complete();
        int Complete(Controller controller);
        void Commit();
        Task CommitAsync();
    }
}
