using NDCWeb.Core;
using NDCWeb.Data_Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using NDCWeb.Core.IRepositories;
using NDCWeb.Persistence.Repositories;
using Microsoft.AspNet.Identity;
using NDCWeb.Models;

namespace NDCWeb.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NDCWebContext _context;

        #region Interface Instance
        private IMenuItemMasterRepository _MenuItemMstrRepo;
        private IMenuUrlMasterRepository _MenuUrlMstrRepo;
        private IPageContentRepository _PageContentRepo;
        private IMenuRoleRepository _MenuRoleRepo;
        private IMediaCategoryMasterRepository _MediaCategoryMstrRepo;
        private INewsArticleRepository _NewsArticleRepo;
        private IMediaGalleryRepository _MediaGalleryRepo;
        private IMediaFileRepository _MediaFileRepo;
        private IRankMasterRepository _RankMasterRepo;
        private IFacultyRepository _FacultyRepo;
        private IAppointmentDetailRepository _AppointmentDetailRepo;
        private IStaffDocumentRepository _StaffDocumentRepo;
        private IStaffPhotoRepository _StaffPhotoRepo;
        private IStaffMasterRepository _StaffMasterRepo;
        private ICourseMemberRepository _CourseMemberRepo;
        private ICrsMbrLanguageRepository _CrsMbrLanguageRepo;
        private ICrsMbrQualificationRepository _CrsMbrQualificationRepo;
        private ICountryVisitRepository _CountryVisitRepo;
        private IHonourAwardRepository _HonourAwardRepo;
        private ICrsMbrBiographyRepository _CrsMbrBiographyRepo;
        private ICrsMbrHobbyRepository _CrsMbrHobbyRepo;
        private ICrsMbrSportRepository _CrsMbrSportRepo;
        private IAsgmtAppointmentRepository _AsgmtAppointmentRepo;
        private ICourseRegisterRepository _CourseRegisterRepo;
        private IArrivalDetailRepository _ArrivalDetailRepo;
        private IArrivalMealRepository _ArrivalMealRepo;
        private ITallyDetailRepository _TallyDetailRepo;
        private IServiceRationRepository _ServiceRationRepo;
        private IAccountInfoRepository _AccountInfoRepo;
        private ICrsMbrSpouseRepository _CrsMbrSpouseRepo;
        private IRakshikaRepository _RakshikaRepo;
        private IMPhilMemberRepository _MPhilMemberRepo;
        private IMPhilPostGraduateRepository _MPhilPostGraduateRepo;
        private ICrsMemberPersonalRepository _CrsMbrPersonalRepo;
        private ICrsMbrAddressRepository _CrsMbrAddressRepo;
        private ICrsMbrAppointmentRepository _CrsMbrAppointmentRepo;
        private ICourseRepository _CourseRepo;
        private ICountryMasterRepository _CountryMasterRepo;
        private IStateMasterRepository _StateMasterRepo;
        private ICityMasterRepository _CityMasterRepo;
        private IArrivalAccompaniedRepository _ArrivalAccompaniedRepo;
        private IVisaDetailRepository _VisaDetailRepo;
        private IPassportDetailRepository _PassportDetailRepo;
        private ISpouseLanguageRepository _SpouseLanguageRepo;
        private ISpouseChildrenRepository _SpouseChildrenRepo;
        private ISpouseQualificationRepository _SpouseQualificationRepo;
        private IChildrenPassportRepository _ChildrenPassportRepo;
        private ISiteFeedbackRepository _SiteFeedbackRepo;
        private ICourseFeedbackRepository _CourseFeedbackRepo;
        private ILeaveRepository _LeaveRepo;
        private IForumBlogRepository _ForumBlogRepo;
        private IForumBlogMediaRepository _ForumBlogMediaRepo;
        private IAlumniArticleRepository _AlumniArticleRepo;
        private IAlumniArticleMediaRepository _AlumniArticleMediaRepo;
        private ICircularRepository _CircularRepo;
        private ICircularMediaRepository _CircularMediaRepo;
        private ITADAClaimsRepository _TADAClaimsRepo;
        private IAccomodationRepository _AccomodationRepo;
        private ILibraryMembershipRepository _LibraryMembershipRepo;
        private ILockerAllotmentRepository _LockerAllotmentRepo;
       
        private IInfotechRepository _InfotechRepo;
        private IEventRepository _EventRepo; 
        private IFeedbackModuleRepository _FeedbackModuleRepo;
        private IFeedbackSpeakerRepository _FeedbackSpeakerRepo;
        private ISubjectMasterRepository _SubjectMasterRepo;
        private IEventMemberRepository _EventMemberRepo;
		private ITopicMasterRepository _TopicMasterRepo;
        private IEventParticipantRepository _EventParticipantRepo;
        private ITelecommRequirementRepository _TelecommRequirementRepo;
        private IUserActivityRepository _UserActivityRepo;
        private ICrsMbrVehicleStickerRepository _CrsMbrVehicleStickerRepo;
        private ISpeakerRepository _SpeakerRepo;
        private IAlumniRepository _AlumniRepo;
        private ISpeechEventRepository _SpeechEventRepo;
        private IMemberIAGRoleRepository _MemberIAGRoleRepo;
        private IMPhilDegreeRepository _MPhilDegreeRepo;
        private ISocialCalendarRepository _SocialCalendarRepo;
        private ITrainingActivityRepository _TrainingActivityRepo;
        private ITrainingActivityMediaRepository _TrainingActivityMediaRepo;
        private IAlumniFeedbackRepository _AlumniFeedbackRepo;
        private IMessBillRepository _MessBillRepo;
        private ICommunityRepository _CommunityRepo;
        private ICircularDetailRepository _CircularDetailRepo;
        private IHolidayCalendarRepository _HolidayCalendarRepo;
        private ISuggestionRepository _SuggestionRepo;
        private IOthrerRequestRepository _OthreRequestRepo;
        private IVisitorRepository _VisitorRepo;

        private IInStepRegistrationRepository _InStepRegistrationRepo;
        private IInStepCourseRepository _InStepCourseRepo;

        private IUserPwdMangerRepository _UserPwdMangerRepo;
        #endregion
        public UnitOfWork(NDCWebContext context)
        {
            _context = context;
        }
        #region Interface new Object
        public IMenuItemMasterRepository MenuItemMstrRepo => _MenuItemMstrRepo = _MenuItemMstrRepo ?? new MenuItemMasterRepository(_context);
        public IMenuUrlMasterRepository MenuUrlMstrRepo => _MenuUrlMstrRepo = _MenuUrlMstrRepo ?? new MenuUrlMasterRepository(_context);
        public IPageContentRepository PageContentRepo => _PageContentRepo = _PageContentRepo ?? new PageContentRepository(_context);
        public IMenuRoleRepository MenuRoleRepo => _MenuRoleRepo = _MenuRoleRepo ?? new MenuRoleRepository(_context);
        public IMediaCategoryMasterRepository MediaCategoryMstrRepo => _MediaCategoryMstrRepo = _MediaCategoryMstrRepo ?? new MediaCategoryMasterRepository(_context);
        public INewsArticleRepository NewsArticleRepo => _NewsArticleRepo = _NewsArticleRepo ?? new NewsArticleRepository(_context);
        public IMediaGalleryRepository MediaGalleryRepo => _MediaGalleryRepo = _MediaGalleryRepo ?? new MediaGalleryRepository(_context);
        public IMediaFileRepository MediaFileRepo => _MediaFileRepo = _MediaFileRepo ?? new MediaFileRepository(_context);

        public IRankMasterRepository RankMasterRepo => _RankMasterRepo = _RankMasterRepo ?? new RankMasterRepository(_context);
        public IFacultyRepository FacultyRepo => _FacultyRepo = _FacultyRepo ?? new FacultyRepository(_context);
        public IAppointmentDetailRepository AppointmentDetailRepo => _AppointmentDetailRepo = _AppointmentDetailRepo ?? new AppointmentDetailRepository(_context);
        public IStaffDocumentRepository StaffDocumentRepo => _StaffDocumentRepo = _StaffDocumentRepo ?? new StaffDocumentRepository(_context);
        public IStaffPhotoRepository StaffPhotoRepo => _StaffPhotoRepo = _StaffPhotoRepo ?? new StaffPhotoRepository(_context);
        public IStaffMasterRepository StaffMasterRepo => _StaffMasterRepo = _StaffMasterRepo ?? new StaffMasterRepository(_context);
        public ICountryMasterRepository CountryMasterRepo => _CountryMasterRepo = _CountryMasterRepo ?? new CountryMasterRepository(_context);
        public IStateMasterRepository StateMasterRepo => _StateMasterRepo = _StateMasterRepo ?? new StateMasterRepository(_context);
        public ICityMasterRepository CityMasterRepo => _CityMasterRepo = _CityMasterRepo ?? new CityMasterRepository(_context);


        public ICourseMemberRepository CourseMemberRepo => _CourseMemberRepo = _CourseMemberRepo ?? new CourseMemberRepository(_context);
        public ICrsMbrLanguageRepository CrsMbrLanguageRepo => _CrsMbrLanguageRepo = _CrsMbrLanguageRepo ?? new CrsMbrLanguageRepository(_context);
        public ICrsMbrQualificationRepository CrsMbrQualificationRepo => _CrsMbrQualificationRepo = _CrsMbrQualificationRepo ?? new CrsMbrQualificationRepository(_context);
        public ICountryVisitRepository CountryVisitRepo => _CountryVisitRepo = _CountryVisitRepo ?? new CountryVisitRepository(_context);
        public IHonourAwardRepository HonourAwardRepo => _HonourAwardRepo = _HonourAwardRepo ?? new HonourAwardRepository(_context);
        public ICrsMbrBiographyRepository CrsMbrBiographyRepo => _CrsMbrBiographyRepo = _CrsMbrBiographyRepo ?? new CrsMbrBiographyRepository(_context);
        public ICrsMbrHobbyRepository CrsMbrHobbyRepo => _CrsMbrHobbyRepo = _CrsMbrHobbyRepo ?? new CrsMbrHobbyRepository(_context);
        public ICrsMbrSportRepository CrsMbrSportRepo => _CrsMbrSportRepo = _CrsMbrSportRepo ?? new CrsMbrSportRepository(_context);
        public IAsgmtAppointmentRepository AsgmtAppointmentRepo => _AsgmtAppointmentRepo = _AsgmtAppointmentRepo ?? new AsgmtAppointmentRepository(_context);
        public ICourseRegisterRepository CourseRegisterRepo => _CourseRegisterRepo = _CourseRegisterRepo ?? new CourseRegisterRepository(_context);
        public IArrivalDetailRepository ArrivalDetailRepo => _ArrivalDetailRepo = _ArrivalDetailRepo ?? new ArrivalDetailRepository(_context);
        public IArrivalMealRepository ArrivalMealRepo => _ArrivalMealRepo = _ArrivalMealRepo ?? new ArrivalMealRepository(_context);
        public ITallyDetailRepository TallyDetailRepo => _TallyDetailRepo = _TallyDetailRepo ?? new TallyDetailRepository(_context);
        public IServiceRationRepository ServiceRationRepo => _ServiceRationRepo = _ServiceRationRepo ?? new ServiceRationRepository(_context);
        public IAccountInfoRepository AccountInfoRepo => _AccountInfoRepo = _AccountInfoRepo ?? new AccountInfoRepository(_context);
        public ICourseRepository CourseRepo => _CourseRepo = _CourseRepo ?? new CourseRepository(_context);
        public ICrsMbrSpouseRepository CrsMbrSpouseRepo => _CrsMbrSpouseRepo = _CrsMbrSpouseRepo ?? new CrsMbrSpouseRepository(_context);
        public IRakshikaRepository RakshikaRepo => _RakshikaRepo = _RakshikaRepo ?? new RakshikaRepository(_context);
        public ICrsMemberPersonalRepository CrsMbrPersonalRepo => _CrsMbrPersonalRepo = _CrsMbrPersonalRepo ?? new CrsMemberPersonalRepository(_context);
        public ICrsMbrAddressRepository CrsMbrAddressRepo => _CrsMbrAddressRepo = _CrsMbrAddressRepo ?? new CrsMbrAddressRepository(_context);
        public ICrsMbrAppointmentRepository CrsMbrAppointmentRepo => _CrsMbrAppointmentRepo = _CrsMbrAppointmentRepo ?? new CrsMbrAppointmentRepository(_context);
        public IVisaDetailRepository VisaDetailRepo => _VisaDetailRepo = _VisaDetailRepo ?? new VisaDetailRepository(_context);
        public IPassportDetailRepository PassportDetailRepo => _PassportDetailRepo = _PassportDetailRepo ?? new PassportDetailRepository(_context);
        public ISpouseLanguageRepository SpouseLanguageRepo => _SpouseLanguageRepo = _SpouseLanguageRepo ?? new SpouseLanguageRepository(_context);
        public ISpouseChildrenRepository SpouseChildrenRepo => _SpouseChildrenRepo = _SpouseChildrenRepo ?? new SpouseChildrenRepository(_context);
        public ISpouseQualificationRepository SpouseQualificationRepo => _SpouseQualificationRepo = _SpouseQualificationRepo ?? new SpouseQualificationRepository(_context);
        public IChildrenPassportRepository ChildrenPassportRepo => _ChildrenPassportRepo = _ChildrenPassportRepo ?? new ChildrenPassportRepository(_context);
        public ICourseFeedbackRepository CourseFeedbackRepo => _CourseFeedbackRepo = _CourseFeedbackRepo ?? new CourseFeedbackRepository(_context);
        public ICrsMbrVehicleStickerRepository CrsMbrVehicleStickerRepo => _CrsMbrVehicleStickerRepo = _CrsMbrVehicleStickerRepo ?? new CrsMbrVehicleStickerRepository(_context);
        public ISpeakerRepository SpeakerRepo => _SpeakerRepo = _SpeakerRepo ?? new SpeakerRepository(_context);
        public IHolidayCalendarRepository HolidayCalendarRepo => _HolidayCalendarRepo = _HolidayCalendarRepo ?? new HolidayCalendarRepository(_context);
        public IMPhilMemberRepository MPhilMemberRepo => _MPhilMemberRepo = _MPhilMemberRepo ?? new MPhilMemberRepository(_context);
        public IMPhilPostGraduateRepository MPhilPostGraduateRepo => _MPhilPostGraduateRepo = _MPhilPostGraduateRepo ?? new MPhilPostGraduateRepository(_context);
        public IArrivalAccompaniedRepository ArrivalAccompaniedRepo => _ArrivalAccompaniedRepo = _ArrivalAccompaniedRepo ?? new ArrivalAccompaniedRepository(_context);
        public ISiteFeedbackRepository SiteFeedbackRepo => _SiteFeedbackRepo = _SiteFeedbackRepo ?? new SiteFeedbackRepository(_context);
        public IAlumniRepository AlumniRepo => _AlumniRepo = _AlumniRepo ?? new AlumniRepository(_context);

        public ILeaveRepository LeaveRepo => _LeaveRepo = _LeaveRepo ?? new LeaveRepository(_context);
        public IForumBlogRepository ForumBlogRepo => _ForumBlogRepo = _ForumBlogRepo ?? new ForumBlogRepository(_context);
        public IForumBlogMediaRepository ForumBlogMediaRepo => _ForumBlogMediaRepo = _ForumBlogMediaRepo ?? new ForumBlogMediaRepository(_context);

        public IAlumniArticleRepository AlumniArticleRepo => _AlumniArticleRepo = _AlumniArticleRepo ?? new AlumniArticleRepository(_context);
        public IAlumniArticleMediaRepository AlumniArticleMediaRepo => _AlumniArticleMediaRepo = _AlumniArticleMediaRepo ?? new AlumniArticleMediaRepository(_context);
        public ICircularRepository CircularRepo => _CircularRepo = _CircularRepo ?? new CircularRepository(_context);
        public ICircularMediaRepository CircularMediaRepo => _CircularMediaRepo = _CircularMediaRepo ?? new CircularMediaRepository(_context);
        public ITADAClaimsRepository TADAClaimsRepo => _TADAClaimsRepo = _TADAClaimsRepo ?? new TADAClaimsRepository(_context);
        public IAccomodationRepository AccomodationRepo => _AccomodationRepo = _AccomodationRepo ?? new AccomodationRepository(_context);
        public ILibraryMembershipRepository LibraryMembershipRepo => _LibraryMembershipRepo = _LibraryMembershipRepo ?? new LibraryMembershipRepository(_context);

        public ILockerAllotmentRepository LockerAllotmentRepo => _LockerAllotmentRepo = _LockerAllotmentRepo ?? new LockerAllotmentRepository(_context);
        public IMessBillRepository MessBillRepo => _MessBillRepo = _MessBillRepo ?? new MessBillRepository(_context);
        public IInfotechRepository InfotechRepo => _InfotechRepo = _InfotechRepo ?? new InfotechRepository(_context);
        public IEventRepository EventRepo => _EventRepo = _EventRepo ?? new EventRepository(_context);
        public IFeedbackModuleRepository FeedbackModuleRepo => _FeedbackModuleRepo = _FeedbackModuleRepo ?? new FeedbackModuleRepository(_context);
        public IFeedbackSpeakerRepository FeedbackSpeakerRepo => _FeedbackSpeakerRepo = _FeedbackSpeakerRepo ?? new FeedbackSpeakerRepository(_context);
        public ISubjectMasterRepository SubjectMasterRepository => _SubjectMasterRepo = _SubjectMasterRepo ?? new SubjectMasterRepository(_context);
        public IEventMemberRepository EventMemberRepo => _EventMemberRepo = _EventMemberRepo ?? new EventMemberRepository(_context);
        public ITopicMasterRepository TopicMasterRepository => _TopicMasterRepo = _TopicMasterRepo ?? new TopicMasterRepository(_context);
        public IEventParticipantRepository EventParticipantRepository => _EventParticipantRepo = _EventParticipantRepo ?? new EventParticipantRepository(_context);
        public ITelecommRequirementRepository TelecommRequirementRepository => _TelecommRequirementRepo = _TelecommRequirementRepo ?? new TelecommRequirementRepository(_context);
        public IUserActivityRepository UserActivityRepo => _UserActivityRepo = _UserActivityRepo ?? new UserActivityRepository(_context);

        public IMemberIAGRoleRepository MemberIAGRoleRepo => _MemberIAGRoleRepo = _MemberIAGRoleRepo ?? new MemberIAGRoleRepository(_context);
        public ISpeechEventRepository SpeechEventRepo => _SpeechEventRepo = _SpeechEventRepo ?? new SpeechEventRepository(_context);
        public IMPhilDegreeRepository MPhilDegreeRepo => _MPhilDegreeRepo = _MPhilDegreeRepo ?? new MPhilDegreeRepository(_context);
        public ISocialCalendarRepository SocialCalendarRepo => _SocialCalendarRepo = _SocialCalendarRepo ?? new SocialCalendarRepository(_context);

        public ITrainingActivityRepository TrainingActivityRepo => _TrainingActivityRepo = _TrainingActivityRepo ?? new TrainingActivityRepository(_context);
        public ITrainingActivityMediaRepository TrainingActivityMediaRepo => _TrainingActivityMediaRepo = _TrainingActivityMediaRepo ?? new TrainingActivityMediaRepository(_context);

        public ICommunityRepository CommunityRepo => _CommunityRepo = _CommunityRepo ?? new CommunityRepository(_context);
        public ICircularDetailRepository CircularDetailRepo => _CircularDetailRepo = _CircularDetailRepo ?? new CircularDetailRepository(_context);
        public ISuggestionRepository SuggestionRepository => _SuggestionRepo = _SuggestionRepo ?? new SuggestionRepository(_context);
        public IOthrerRequestRepository OthrerRequestRepo => _OthreRequestRepo = _OthreRequestRepo ?? new OtherRequestRepository(_context);
        public IVisitorRepository VisitorRepo => _VisitorRepo = _VisitorRepo ?? new VisitorRepository(_context);


        #region Alumni
        public IAlumniFeedbackRepository AlumniFeedbackRepo => _AlumniFeedbackRepo = _AlumniFeedbackRepo = _AlumniFeedbackRepo ?? new AlumniFeedbackRepository(_context);
        #endregion

        #region IN Step
        public IInStepCourseRepository InStepCourseRepo => _InStepCourseRepo = _InStepCourseRepo ?? new InStepCourseRepository(_context);
        public IInStepRegistrationRepository InStepRegistrationRepo => _InStepRegistrationRepo = _InStepRegistrationRepo ?? new InStepRegistrationRepository(_context);
        #endregion
        public IUserPwdMangerRepository UserPwdMangerRepo => _UserPwdMangerRepo = _UserPwdMangerRepo ?? new UserPwdMangerRepository(_context);
        #endregion
        #region Transaction History
        public int Complete()
        {
            //OnBeforeSaving();
            return _context.SaveChanges();
        }

        public int Complete(Controller controller)
        {
            OnBeforeSaving();
            //string NotifyType = getOperationType();
            int rowAffct = _context.SaveChanges();
            //if (rowAffct > 0) CSPLNotificationExtensions.SetPromptNotification(controller, NotifyType);
            return rowAffct;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Commit()
        {
            OnBeforeSaving();
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            OnBeforeSaving();
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Helper Methods
        private void OnBeforeSaving()
        {
            var entries = _context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is Models.BaseEntity)
                {
                    var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["CreatedAt"] = now;
                            entry.CurrentValues["CreatedBy"] = HttpContext.Current.User.Identity.GetUserId();
                            entry.Property("LastUpdatedAt").IsModified = false;
                            entry.Property("LastUpdatedBy").IsModified = false;
                            break;

                        case EntityState.Modified:
                            entry.CurrentValues["LastUpdatedAt"] = now;
                            entry.CurrentValues["LastUpdatedBy"] = HttpContext.Current.User.Identity.GetUserId();
                            entry.Property("CreatedAt").IsModified = false;
                            entry.Property("CreatedBy").IsModified = false;
                            break;
                    }
                }
            }
        }
        #endregion
    }
}