using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NDCWeb.Data_Contexts
{
	public class NDCWebContext : DbContext
	{
		public NDCWebContext() : base("NDCWebConString")
		{
		}

		#region DbSets

		#region CMS
		public virtual DbSet<MenuItemMaster> MenuItemMstr { get; set; }
		public virtual DbSet<MenuUrlMaster> MenuUrlMstr { get; set; }
		public virtual DbSet<PageContent> PageContents { get; set; }
		public virtual DbSet<NewsArticle> NewsArticles { get; set; }
		public virtual DbSet<MediaCategoryMaster> MediaCategoryMstr { get; set; }
		public virtual DbSet<MediaGallery> MediaGalleries { get; set; }
		public virtual DbSet<MediaFile> MediaFiles { get; set; }
		public virtual DbSet<MenuRole> MenuRoles { get; set; }
		public virtual DbSet<Visitor> Visitors { get; set; }
		#endregion

		#region Admin
		public virtual DbSet<RankMaster> RankMasters { get; set; }
		public virtual DbSet<Faculty> Faculties { get; set; }
		public virtual DbSet<AppointmentDetail> AppointmentDetails { get; set; }
		public virtual DbSet<StaffDocument> StaffDocuments { get; set; }
		public virtual DbSet<StaffPhoto> StaffPhotos { get; set; }
		public virtual DbSet<StaffMaster> StaffMasters { get; set; }
		public virtual DbSet<CountryMaster> CountryMasters { get; set; }
		public virtual DbSet<StateMaster> StateMasters { get; set; }
		public virtual DbSet<CityMaster> CityMasters { get; set; }
		public virtual DbSet<Community> Communities { get; set; }
		public virtual DbSet<UserActivity> UserActivities { get; set; }
		public virtual DbSet<CircularDetail> CircularDetails { get; set; }
		#endregion

		#region Member

		#region Course
		public virtual DbSet<CourseMember> CourseMembers { get; set; }
		public virtual DbSet<CrsMbrLanguage> CrsMbrLanguages { get; set; }
		public virtual DbSet<CrsMbrQualification> CrsMbrQualifications { get; set; }
		public virtual DbSet<CountryVisit> CountryVisits { get; set; }
		public virtual DbSet<HonourAward> HonourAwards { get; set; }
		public virtual DbSet<CrsMbrBiography> Biographies { get; set; }
		public virtual DbSet<CrsMbrHobby> Hobbies { get; set; }
		public virtual DbSet<CrsMbrSport> Sports { get; set; }
		public virtual DbSet<AsgmtAppointment> AsgmtAppointments { get; set; }
		public virtual DbSet<CourseRegister> CourseRegisters { get; set; }
		public virtual DbSet<ArrivalDetail> ArrivalDetails { get; set; }
		public virtual DbSet<ArrivalMeal> ArrivalMeals { get; set; }
		public virtual DbSet<TallyDetail> TallyDetails { get; set; }
		public virtual DbSet<CrsMbrVehicleSticker> CrsMbrVehicleStickers { get; set; }
		public virtual DbSet<ServiceRation> ServiceRations { get; set; }
		public virtual DbSet<AccountInfo> AccountInfos { get; set; }
		public virtual DbSet<CrsMbrSpouse> CrsMbrSpouses { get; set; }
		public virtual DbSet<SpouseChildren> SpouseChildrens { get; set; }
		public virtual DbSet<SpouseLanguage> SpouseLanguages { get; set; }
		public virtual DbSet<SpouseQualification> SpouseQualifications { get; set; }
		public virtual DbSet<Rakshika> Rakshikas { get; set; }
		public virtual DbSet<CrsMemberPersonal> CrsMemberPersonals { get; set; }
		public virtual DbSet<CrsMbrAppointment> CrsMbrAppointments { get; set; }
		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<SubjectMaster> SubjectMasters { get; set; }
		public virtual DbSet<TopicMaster> TopicMasters { get; set; }
		public virtual DbSet<CrsMbrAddress> CrsMbrAddresses { get; set; }
		public virtual DbSet<VisaDetail> VisaDetails { get; set; }

		public virtual DbSet<AlumniArticle> AlumniArticles { get; set; }
		public virtual DbSet<AlumniArticleMedia> AlumniArticleMedias { get; set; }
		public virtual DbSet<PassportDetail> PassportDetails { get; set; }
		public virtual DbSet<ChildrenPassport> ChildrenPassports { get; set; }
		public virtual DbSet<ArrivalAccompanied> ArrivalAccompanied { get; set; }
		public virtual DbSet<CourseFeedback> CourseFeedbacks { get; set; }
		public virtual DbSet<FeedbackModule> FeedbackModules { get; set; }
		public virtual DbSet<FeedbackSpeaker> FeedbackSpeakers { get; set; }
		public virtual DbSet<EventParticipant> EventParticipants { get; set; }

		#endregion

		#region Mphil
		public virtual DbSet<MPhilMember> MPhilMembers { get; set; }
		public virtual DbSet<MPhilPostGraduate> MPhilPostGraduates { get; set; }
		public virtual DbSet<MPhilDegree> MPhilDegrees { get; set; }
		#endregion

		#region Others
		public virtual DbSet<Leave> Leaves { get; set; }
		public virtual DbSet<ForumBlog> ForumBlogs { get; set; }
		public virtual DbSet<ForumBlogMedia> ForumBlogMedias { get; set; }
		public virtual DbSet<TADAClaims> TADAClaimss { get; set; }
		public virtual DbSet<Infotech> Infotechs { get; set; }
		public virtual DbSet<Accomodation> Accomodations { get; set; }
		public virtual DbSet<AccomodationMedia> AccomodationMedias { get; set; }
		public virtual DbSet<EventMember> EventMembers { get; set; }
		public virtual DbSet<TelecommRequirement> TelecommRequirements { get; set; }
		#endregion

		#region Library
		public virtual DbSet<LibraryMembership> LibraryMembership { get; set; }
		#endregion
		#endregion

		#region Staff
		public virtual DbSet<LockerAllotment> LockerAllotments { get; set; }
		public virtual DbSet<Event> Events { get; set; }
		public virtual DbSet<Speaker> Speakers { get; set; }

		public virtual DbSet<Circular> Circulars { get; set; }
		public virtual DbSet<TrainingActivity> TrainingActivities { get; set; }
		public virtual DbSet<TrainingActivityMedia> TrainingActivityMedias { get; set; }
		#endregion

		#region Public Pages
		public virtual DbSet<SiteFeedback> SiteFeedbacks { get; set; }
		public virtual DbSet<HolidayCalendar> HolidayCalendars { get; set; }
		public virtual DbSet<Suggestion> Suggestions { get; set; }
		public virtual DbSet<OtherRequest> OtherRequests { get; set; }
		#endregion

		#region Alumni
		public virtual DbSet<AlumniMaster> Alumnis { get; set; }
		public virtual DbSet<AlumniFeedback> AlumniFeedbacks { get; set; }
		#endregion

		#endregion
		#region messBill
		public virtual DbSet<MessBill> Messbills { get; set; }
		#endregion
		#region Feedback
		//public virtual DbSet<MemberIAGRole> MemberIAGRoles { get; set; }
		public virtual DbSet<SpeechEvent> SpeechEvents { get; set; }
		#endregion
		#region InStep
		public virtual DbSet<InStepCourse> InStepCourses { get; set; }
		public virtual DbSet<InStepRegistration> InStepRegistrations { get; set; }
		#endregion
		#region Bridge Entities
		public virtual DbSet<UserPwdManger> UserPwdMangers { get; set; }
		#endregion

		public virtual DbSet<AspNetUsers> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			//modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			// configures one-to-many relationship
			modelBuilder.Entity<MediaFile>()
			.HasRequired(r => r.MediaGalleries)
			.WithMany(s => s.iMediaFiles)
			.HasForeignKey(f => f.MediaGalleryId);

			modelBuilder.Entity<ForumBlogMedia>()
		   .HasRequired(r => r.ForumBlogs)
		   .WithMany(s => s.iForumBlogMedias)
		   .HasForeignKey(f => f.ForumBlogId);

			modelBuilder.Entity<CircularMedia>()
		   .HasRequired(r => r.Circulars)
		   .WithMany(s => s.iCircularMedias)
		   .HasForeignKey(f => f.CircularId);

			modelBuilder.Entity<AccomodationMedia>()
		   .HasRequired(r => r.Accomodations)
		   .WithMany(s => s.iAccomodationMedias)
		   .HasForeignKey(f => f.AccomodationId);


			modelBuilder.Entity<ArrivalMeal>()
			.HasRequired(r => r.ArrivalDetails)
			.WithMany(s => s.iArrivalMeals)
			.HasForeignKey(f => f.ArrivalId);

			modelBuilder.Entity<MPhilPostGraduate>()
			.HasRequired(r => r.MPhilMembers)
			.WithMany(s => s.iMPhilPostGraduates)
			.HasForeignKey(f => f.MPhilId);

			//modelBuilder.Entity<SpouseChildren>()
			//.HasRequired(r => r.Spouses)
			//.WithMany(s => s.iSpouseChildrens)
			//.HasForeignKey(f => f.SpouseId);

			modelBuilder.Entity<SpouseLanguage>()
			.HasRequired(c => c.Spouses)
			.WithMany(q => q.iSpouseLanguages)
			.HasForeignKey(f => f.SpouseId);

			modelBuilder.Entity<SpouseQualification>()
			.HasRequired(c => c.Spouses)
			.WithMany(q => q.iSpouseQualifications)
			.HasForeignKey(f => f.SpouseId);

			modelBuilder.Entity<ChildrenPassport>()
			.HasRequired(c => c.Passports)
			.WithMany(q => q.iChildrenPassports)
			.HasForeignKey(f => f.PassportId);

			#region unique Fields
			modelBuilder
			.Entity<CrsMemberPersonal>()
			.Property(t => t.CreatedBy)
			.IsRequired()
			.HasMaxLength(20)
			.HasColumnAnnotation(
			IndexAnnotation.AnnotationName,
				new IndexAnnotation(new IndexAttribute("IX_CreatedByUniqueKey", 1) { IsUnique = true }));

			modelBuilder
			.Entity<CrsMbrAppointment>()
			.Property(t => t.CreatedBy)
			.IsRequired()
			.HasMaxLength(20)
			.HasColumnAnnotation(
			IndexAnnotation.AnnotationName,
				new IndexAnnotation(new IndexAttribute("IX_CreatedByUniqueKey", 1) { IsUnique = true }));

			modelBuilder
			.Entity<CrsMbrSpouse>()
			.Property(t => t.CreatedBy)
			.IsRequired()
			.HasMaxLength(20)
			.HasColumnAnnotation(
			IndexAnnotation.AnnotationName,
				new IndexAnnotation(new IndexAttribute("IX_CreatedByUniqueKey", 1) { IsUnique = true }));

			//modelBuilder.Entity<CrsMemberPersonal>()
			//.HasIndex(u => u.CreatedBy)
			//.IsUnique();

			//modelBuilder.Entity<CrsMbrAppointment>()
			//.HasIndex(u => u.CreatedBy)
			//.IsUnique();

			//modelBuilder.Entity<CrsMbrSpouse>()
			//.HasIndex(u => u.CreatedBy)
			//.IsUnique();

			modelBuilder.Entity<LockerAllotment>()
			.HasIndex(u => u.CourseMemberId)
			.IsUnique();
			#endregion
		}

	}
}