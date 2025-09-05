namespace NDCWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InStepCourseInitialWithAll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationMedia",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        AccomodationId = c.Int(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.GuId)
                .ForeignKey("dbo.Accomodation", t => t.AccomodationId, cascadeDelete: true)
                .Index(t => t.AccomodationId);
            
            CreateTable(
                "dbo.Accomodation",
                c => new
                    {
                        AccomodationId = c.Int(nullable: false, identity: true),
                        MaritalStatus = c.String(),
                        AccomReq = c.String(),
                        ArrangeType = c.String(),
                        AccomodationDate = c.DateTime(),
                        DateOfseniority = c.DateTime(),
                        PriorityFirst = c.String(),
                        PrioritySecond = c.String(),
                        SpecialRequest = c.String(),
                        AnySpecialRequest = c.Boolean(nullable: false),
                        SpecialRequestWithReason = c.String(),
                        SignatureDoc = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.AccomodationId);
            
            CreateTable(
                "dbo.AccountInfo",
                c => new
                    {
                        AccInfoId = c.Int(nullable: false, identity: true),
                        AccountNo = c.String(),
                        AccountType = c.String(),
                        IFSC = c.String(),
                        MICR = c.String(),
                        PassbookPath = c.String(),
                        NameAndAddressOfBanker = c.String(),
                        CDAAcNo = c.String(),
                        BasicPay = c.String(),
                        MSP = c.String(),
                        PayLevel = c.String(),
                        AddressOfPayAc = c.String(),
                        NodalOfficeName = c.String(),
                        NodalOfficeContactNo = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.AccInfoId);
            
            CreateTable(
                "dbo.AlumniArticleMedia",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.GuId)
                .ForeignKey("dbo.AlumniArticle", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.AlumniArticle",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ArticleId);
            
            CreateTable(
                "dbo.AlumniFeedback",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        DepartmentSubject = c.String(),
                        Comment = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
            CreateTable(
                "dbo.AlumniMaster",
                c => new
                    {
                        AluminiId = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                        FirstName = c.String(),
                        CourseSerNo = c.String(),
                        CourseYear = c.String(),
                        NdcEqvCourse = c.String(),
                        YearDone = c.String(),
                        ServiceRetd = c.String(),
                        ServiceId = c.String(),
                        ServiceNo = c.String(),
                        Decoration = c.String(),
                        PermanentAddress = c.String(),
                        NdcCommunicationAddress = c.String(),
                        Email = c.String(),
                        LandlineNo = c.String(),
                        MobileNo = c.String(),
                        FaxNo = c.String(),
                        Spouse = c.String(),
                        AlumniPhoto = c.String(),
                        Verified = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        Appointment = c.String(),
                        Country = c.String(),
                        ServiceRank = c.String(),
                        OtherService = c.String(),
                        IsProminent = c.String(),
                        RegDate = c.DateTime(),
                        VerifyDate = c.DateTime(),
                        Branch = c.String(),
                        OtherInfo = c.String(),
                        UserId = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.AluminiId);
            
            CreateTable(
                "dbo.AppointmentDetail",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        Appointment = c.String(),
                        Organisation = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
            CreateTable(
                "dbo.ArrivalAccompanied",
                c => new
                    {
                        ArrivalMemId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Age = c.String(),
                        Relation = c.String(),
                        Remarks = c.String(),
                        ArrivalId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ArrivalMemId)
                .ForeignKey("dbo.ArrivalDetail", t => t.ArrivalId, cascadeDelete: true)
                .Index(t => t.ArrivalId);
            
            CreateTable(
                "dbo.ArrivalDetail",
                c => new
                    {
                        ArrivalId = c.Int(nullable: false, identity: true),
                        ArrivaAt = c.String(),
                        ArrivalDate = c.DateTime(nullable: false),
                        ArrivalTime = c.Time(nullable: false, precision: 7),
                        ArrivalMode = c.String(),
                        TransportationMode = c.String(),
                        AssistanceRequired = c.String(),
                        MealRequired = c.Boolean(nullable: false),
                        NoofMeals = c.String(),
                        MealFromDate = c.DateTime(),
                        MealToDate = c.DateTime(),
                        MealDietPreference = c.String(),
                        FoodDetachment = c.Boolean(nullable: false),
                        DetachFromDate = c.DateTime(),
                        DetachToDate = c.DateTime(),
                        DetachMealInfo = c.String(),
                        DetachCharges = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ArrivalId);
            
            CreateTable(
                "dbo.ArrivalMeal",
                c => new
                    {
                        MealId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Breakfast = c.Boolean(nullable: false),
                        Lunch = c.Boolean(nullable: false),
                        Dinner = c.Boolean(nullable: false),
                        ArrivalId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.MealId)
                .ForeignKey("dbo.ArrivalDetail", t => t.ArrivalId, cascadeDelete: true)
                .Index(t => t.ArrivalId);
            
            CreateTable(
                "dbo.AsgmtAppointment",
                c => new
                    {
                        AsgmtAppointmentId = c.Int(nullable: false, identity: true),
                        Appointment = c.String(),
                        Organisation = c.String(),
                        Duration = c.String(),
                        Location = c.String(),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.AsgmtAppointmentId);
            
            CreateTable(
                "dbo.CrsMbrBiography",
                c => new
                    {
                        BiographyId = c.Int(nullable: false, identity: true),
                        PenPicture = c.String(),
                        FamilyBackground = c.String(),
                        EarlySchooling = c.String(),
                        AcademicAchievement = c.String(),
                        PersonalValueSystem = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.BiographyId);
            
            CreateTable(
                "dbo.ChildrenPassport",
                c => new
                    {
                        ChildPassportId = c.Int(nullable: false, identity: true),
                        PassportNo = c.String(),
                        PassportIssueDate = c.DateTime(nullable: false),
                        PassportValidUpto = c.DateTime(nullable: false),
                        PassportType = c.String(),
                        PassportImgPath = c.String(),
                        PassportId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ChildPassportId)
                .ForeignKey("dbo.PassportDetail", t => t.PassportId, cascadeDelete: true)
                .Index(t => t.PassportId);
            
            CreateTable(
                "dbo.PassportDetail",
                c => new
                    {
                        PassportId = c.Int(nullable: false, identity: true),
                        HoldingPassport = c.String(),
                        MemberPassportNo = c.String(),
                        MemberPassportIssueDate = c.DateTime(nullable: false),
                        MemberPassportValidUpto = c.DateTime(nullable: false),
                        MemberPassportType = c.String(),
                        SpousePassportNo = c.String(),
                        SpousePassportIssueDate = c.DateTime(nullable: false),
                        SpousePassportValidUpto = c.DateTime(nullable: false),
                        SpousePassportType = c.String(),
                        MemberPassportImgPath = c.String(),
                        SpousePassportImgPath = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.PassportId);
            
            CreateTable(
                "dbo.CircularDetail",
                c => new
                    {
                        CircularDetailId = c.Int(nullable: false, identity: true),
                        DesignationId = c.Int(nullable: false),
                        Show = c.Boolean(nullable: false),
                        CircularId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.CircularDetailId)
                .ForeignKey("dbo.Circular", t => t.CircularId, cascadeDelete: true)
                .ForeignKey("dbo.Community", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.DesignationId)
                .Index(t => t.CircularId);
            
            CreateTable(
                "dbo.Circular",
                c => new
                    {
                        CircularId = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.CircularId);
            
            CreateTable(
                "dbo.CircularMedia",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        CircularId = c.Int(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.GuId)
                .ForeignKey("dbo.Circular", t => t.CircularId, cascadeDelete: true)
                .Index(t => t.CircularId);
            
            CreateTable(
                "dbo.Community",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        Designation = c.String(),
                        DesignationIdDescription = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.CityMaster",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.StateMaster", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.StateMaster",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.CountryMaster", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.CountryMaster",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        SortName = c.String(),
                        CountryName = c.String(),
                        PhoneCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.CountryVisit",
                c => new
                    {
                        CountryVisitId = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        Visit = c.String(),
                        Duration = c.String(),
                        Purpose = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.CountryVisitId);
            
            CreateTable(
                "dbo.CourseFeedback",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        ConductOfStudy = c.String(),
                        SpeakerOnTopicScope = c.String(),
                        CoductOfIAG = c.String(),
                        ConductOfCDsSGES = c.String(),
                        InfoCourseLectureSchedule = c.String(),
                        SuggestionInfraI = c.String(),
                        SuggestionInfraII = c.String(),
                        SuggestionInfraIII = c.String(),
                        OtherComments = c.String(),
                        ToursI = c.String(),
                        TourIDate = c.DateTime(),
                        VirtualToursI = c.String(),
                        VirtualToursIDate = c.DateTime(),
                        VirtualToursII = c.String(),
                        VirtualToursIIDate = c.DateTime(),
                        AdminTourA = c.Boolean(nullable: false),
                        AdminTourB = c.Boolean(nullable: false),
                        AdminTourC = c.String(),
                        AdminSocialFuncA = c.String(),
                        AdminSocialFuncB = c.String(),
                        AdminOfficerMessA = c.String(),
                        AdminOfficerMessB = c.String(),
                        AdminOfficerMessC = c.String(),
                        AccountsA = c.String(),
                        AccountsB = c.String(),
                        Transport = c.String(),
                        AnyOtherSuggestion = c.String(),
                        AdminAccomodationA = c.String(),
                        AdminAccomodationB = c.String(),
                        AdminAccomodationReceptionA = c.String(),
                        AdminAccomodationReceptionB = c.String(),
                        AdminGym = c.String(),
                        AdminBeautyParlour = c.String(),
                        AdminShop = c.String(),
                        CSDFacilities = c.String(),
                        AdminDomesticHelp = c.String(),
                        AdminA = c.String(),
                        AdminB = c.String(),
                        AdminC = c.String(),
                        AdminD = c.String(),
                        AdminE = c.String(),
                        AdminF = c.String(),
                        AdminG = c.String(),
                        UDThesisA = c.Boolean(nullable: false),
                        UDThesisASugg = c.String(),
                        UDThesisB = c.Boolean(nullable: false),
                        UDThesisBSugg = c.String(),
                        UDThesisC = c.Boolean(nullable: false),
                        UDThesisCSugg = c.String(),
                        LibraryA = c.String(),
                        LibraryB = c.String(),
                        LibraryC = c.String(),
                        FeedbackDate = c.DateTime(),
                        FullName = c.String(),
                        MobileNo = c.String(),
                        Email = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
            CreateTable(
                "dbo.CourseMember",
                c => new
                    {
                        CourseMemberId = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                        FirstName = c.String(),
                        NickName = c.String(),
                        FatherName = c.String(),
                        DOJoining = c.DateTime(nullable: false),
                        DOSeniority = c.DateTime(nullable: false),
                        DOBirth = c.DateTime(nullable: false),
                        DOMarriage = c.DateTime(nullable: false),
                        BloodGroup = c.String(),
                        EmailId = c.String(),
                        ApptDesignation = c.String(),
                        ApptOrganisation = c.String(),
                        ApptLocation = c.String(),
                        CurrentAddress = c.String(),
                        CurrentHomeTelephone = c.String(),
                        CurrentFax = c.String(),
                        PermanentAddress = c.String(),
                        PermanentHomeTelephone = c.String(),
                        PermanentFax = c.String(),
                        OffcTelephone = c.String(),
                        StayBySpouse = c.String(),
                        Undertaking = c.Boolean(nullable: false),
                        MemberImgPath = c.String(),
                        JointImgPath = c.String(),
                        Service = c.String(),
                        Branch = c.String(),
                        RankId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.CourseMemberId)
                .ForeignKey("dbo.RankMaster", t => t.RankId, cascadeDelete: true)
                .Index(t => t.RankId);
            
            CreateTable(
                "dbo.RankMaster",
                c => new
                    {
                        RankId = c.Int(nullable: false, identity: true),
                        RankName = c.String(),
                        Seniority = c.Decimal(precision: 18, scale: 2),
                        Service = c.String(),
                        ForParticipant = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.RankId);
            
            CreateTable(
                "dbo.CourseRegister",
                c => new
                    {
                        CourseRegisterId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        DOBirth = c.DateTime(nullable: false),
                        DOCommissioning = c.DateTime(),
                        SeniorityYear = c.String(),
                        EmailId = c.String(),
                        MobileNo = c.String(),
                        WhatsappNo = c.String(),
                        Honour = c.String(),
                        ApptDesignation = c.String(),
                        ApptOrganisation = c.String(),
                        ApptLocation = c.String(),
                        Approved = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                        VerifyDate = c.DateTime(),
                        UserId = c.String(),
                        Branch = c.String(),
                        RankId = c.Int(nullable: false),
                        CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.CourseRegisterId)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .ForeignKey("dbo.RankMaster", t => t.RankId, cascadeDelete: true)
                .Index(t => t.RankId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        UnderRegistration = c.Boolean(nullable: false),
                        TotalStrength = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.CrsMbrAddress",
                c => new
                    {
                        MemberAddressId = c.Int(nullable: false, identity: true),
                        CurrentAddress = c.String(),
                        CurrentTelephone = c.String(),
                        CurrentFax = c.String(),
                        PermanentAddress = c.String(),
                        PermanentTelephone = c.String(),
                        PermanentFax = c.String(),
                        OffcTelephone = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        StateId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.MemberAddressId)
                .ForeignKey("dbo.StateMaster", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.CrsMbrAppointment",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        Designation = c.String(),
                        Organisation = c.String(),
                        Location = c.String(),
                        DOJoining = c.DateTime(nullable: false),
                        DOSeniority = c.DateTime(nullable: false),
                        ServiceNo = c.String(),
                        Service = c.String(),
                        Branch = c.String(),
                        WorkingAsDAMA = c.String(),
                        WorkingAsDAMADetails = c.String(),
                        RankId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.RankMaster", t => t.RankId, cascadeDelete: true)
                .Index(t => t.RankId)
                .Index(t => t.CreatedBy, unique: true, name: "IX_CreatedByUniqueKey");
            
            CreateTable(
                "dbo.CrsMbrLanguage",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Language = c.String(),
                        Read = c.Boolean(nullable: false),
                        Write = c.Boolean(nullable: false),
                        Speak = c.Boolean(nullable: false),
                        Qualification = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.CrsMbrQualification",
                c => new
                    {
                        QualificationId = c.Int(nullable: false, identity: true),
                        Course = c.String(),
                        QualificationType = c.String(),
                        Year = c.String(),
                        Location = c.String(),
                        Country = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.QualificationId);
            
            CreateTable(
                "dbo.CrsMbrSpouse",
                c => new
                    {
                        SpouseId = c.Int(nullable: false, identity: true),
                        SpouseName = c.String(),
                        SpouseBloodGroup = c.String(),
                        SpouseDOBirth = c.DateTime(nullable: false),
                        Occupation = c.String(),
                        ContactNo = c.String(),
                        SpouseStay = c.Boolean(nullable: false),
                        EduHigher = c.String(),
                        EduSubject = c.String(),
                        EduDivision = c.String(),
                        EduUniversity = c.String(),
                        SpousePassportNo = c.String(),
                        SpousePassportName = c.String(),
                        SpousePassportIssueDate = c.DateTime(),
                        SpousePassportValidUpto = c.DateTime(),
                        SpousePassportType = c.String(),
                        SpousePassportCountryIssued = c.String(),
                        SpousePassportImgPath = c.String(),
                        SpousePassportBackImgPath = c.String(),
                        HoldingPassport = c.String(),
                        SpouseVisaNo = c.String(),
                        SpouseVisaIssueDate = c.DateTime(),
                        SpouseVisaValidUpto = c.DateTime(),
                        SpouseVisaPath = c.String(),
                        SpouseFRRONo = c.String(),
                        SpouseFRROIssueDate = c.DateTime(),
                        SpouseFRROValidUpto = c.DateTime(),
                        SpouseFRROPath = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.SpouseId)
                .Index(t => t.CreatedBy, unique: true, name: "IX_CreatedByUniqueKey");
            
            CreateTable(
                "dbo.SpouseLanguage",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Language = c.String(),
                        Read = c.Boolean(nullable: false),
                        Write = c.Boolean(nullable: false),
                        Speak = c.Boolean(nullable: false),
                        Qualification = c.String(),
                        SpouseId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.LanguageId)
                .ForeignKey("dbo.CrsMbrSpouse", t => t.SpouseId, cascadeDelete: true)
                .Index(t => t.SpouseId);
            
            CreateTable(
                "dbo.SpouseQualification",
                c => new
                    {
                        SpouseEduId = c.Int(nullable: false, identity: true),
                        ProfessionalEdu = c.String(),
                        AcademicAchievement = c.String(),
                        Division = c.String(),
                        Institute = c.String(),
                        SpouseId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.SpouseEduId)
                .ForeignKey("dbo.CrsMbrSpouse", t => t.SpouseId, cascadeDelete: true)
                .Index(t => t.SpouseId);
            
            CreateTable(
                "dbo.CrsMbrVehicleSticker",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        BrandModelNo = c.String(),
                        Colour = c.String(),
                        RegistrationNo = c.String(),
                        DrivingLicenseNo = c.String(),
                        RegistrationCertificatePath = c.String(),
                        DrivingLicensePath = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.VehicleId);
            
            CreateTable(
                "dbo.CrsMemberPersonal",
                c => new
                    {
                        CourseMemberId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        Surname = c.String(),
                        FatherName = c.String(),
                        FatherMiddleName = c.String(),
                        FatherSurname = c.String(),
                        MotherName = c.String(),
                        MotherMiddleName = c.String(),
                        MotherSurname = c.String(),
                        NickName = c.String(),
                        MobileNo = c.String(),
                        AlternateMobileNo = c.String(),
                        IndentificationMark = c.String(),
                        EmailId = c.String(),
                        AlternateEmailId = c.String(),
                        DOBirth = c.DateTime(nullable: false),
                        MaritalStatus = c.String(),
                        DOMarriage = c.DateTime(),
                        Gender = c.String(),
                        BloodGroup = c.String(),
                        Height = c.String(),
                        VoterIdNo = c.String(),
                        PANCardNo = c.String(),
                        CommunicationAddress = c.String(),
                        OfficeHouseNo = c.String(),
                        OfficePremisesName = c.String(),
                        OfficeStreet = c.String(),
                        OfficeArea = c.String(),
                        OfficeCity = c.String(),
                        OfficeZipCode = c.String(),
                        BioSketch = c.String(),
                        Undertaking = c.Boolean(nullable: false),
                        StayBySpouse = c.String(),
                        HoldingPassport = c.String(),
                        MemberPassportNo = c.String(),
                        MemberPassportName = c.String(),
                        MemberPassportIssueDate = c.DateTime(),
                        MemberPassportValidUpto = c.DateTime(),
                        MemberPassportType = c.String(),
                        CountryIssued = c.String(),
                        MemberPassportImgPath = c.String(),
                        MemberPassportBackImgPath = c.String(),
                        HoldingPersonalPassportSelf = c.Boolean(nullable: false),
                        MemberPersonalPassportNo = c.String(),
                        MemberPersonalPassportName = c.String(),
                        MemberPersonalPassportIssueDate = c.DateTime(),
                        MemberPersonalPassportValidUpto = c.DateTime(),
                        CountryIssuedPersonalPassport = c.String(),
                        MemberPersonalPassportImgPath = c.String(),
                        MemberPersonalPassportBackImgPath = c.String(),
                        VisaNo = c.String(),
                        VisaIssueDate = c.DateTime(),
                        VisaValidUpto = c.DateTime(),
                        VisaPath = c.String(),
                        SelfFRRONo = c.String(),
                        SelfIssueDate = c.DateTime(),
                        SelfValidUpto = c.DateTime(),
                        SelfFRROPath = c.String(),
                        MemberImgPath = c.String(),
                        JointImgPath = c.String(),
                        OfficeStateId = c.Int(),
                        CitizenshipCountryId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.CourseMemberId)
                .ForeignKey("dbo.CountryMaster", t => t.CitizenshipCountryId, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.StateMaster", t => t.OfficeStateId)
                .Index(t => t.OfficeStateId)
                .Index(t => t.CitizenshipCountryId)
                .Index(t => t.CourseId)
                .Index(t => t.CreatedBy, unique: true, name: "IX_CreatedByUniqueKey");
            
            CreateTable(
                "dbo.EventMember",
                c => new
                    {
                        EventMemberId = c.Int(nullable: false, identity: true),
                        AttendType = c.String(),
                        AttendSelf = c.String(),
                        AttendSpouse = c.String(),
                        DietPrefSelf = c.String(),
                        DietPrefSpouse = c.String(),
                        LiquorPref = c.String(),
                        Remarks = c.String(),
                        EventId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.EventMemberId)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                        EventVenu = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        EventTime = c.DateTime(nullable: false),
                        EventDress = c.String(),
                        Remarks = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.EventParticipant",
                c => new
                    {
                        EventParticipantId = c.Int(nullable: false, identity: true),
                        ParticipateAs = c.String(),
                        AttendSelf = c.Boolean(nullable: false),
                        AttendSpouse = c.Boolean(nullable: false),
                        SecyPermited = c.Boolean(nullable: false),
                        DietaryPrefSelf = c.Boolean(nullable: false),
                        DietaryPrefSpouse = c.Boolean(nullable: false),
                        LiquorPref = c.String(),
                        Remarks = c.String(),
                        EventId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.EventParticipantId)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Faculty",
                c => new
                    {
                        FacultyId = c.Int(nullable: false, identity: true),
                        FacultyName = c.String(),
                        Designation = c.String(),
                        Type = c.String(),
                        StaffType = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.FacultyId);
            
            CreateTable(
                "dbo.FeedbackModule",
                c => new
                    {
                        ModuleFeedbackId = c.Int(nullable: false, identity: true),
                        CoordChairperson = c.String(),
                        TopicForDelete = c.String(),
                        TopicForAdition = c.String(),
                        Suggestions = c.String(),
                        SuggestChanges = c.String(),
                        SuggestionOther = c.String(),
                        CommentsAndRecomedation = c.String(),
                        SubjectId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ModuleFeedbackId)
                .ForeignKey("dbo.SubjectMaster", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.SubjectMaster",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                        Code = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "dbo.FeedbackSpeaker",
                c => new
                    {
                        SpeakerFeedbackId = c.Int(nullable: false, identity: true),
                        QualityTalk = c.String(),
                        RecomendForNextCourse = c.Boolean(nullable: false),
                        Suggetions = c.String(),
                        SpeechEventId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.SpeakerFeedbackId)
                .ForeignKey("dbo.SpeechEvent", t => t.SpeechEventId, cascadeDelete: true)
                .Index(t => t.SpeechEventId);
            
            CreateTable(
                "dbo.SpeechEvent",
                c => new
                    {
                        SpeechEventId = c.Int(nullable: false, identity: true),
                        SpeechDate = c.DateTime(nullable: false),
                        FeedbackStartDate = c.DateTime(nullable: false),
                        FeedbackEndDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SpeakerId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.SpeechEventId)
                .ForeignKey("dbo.Speaker", t => t.SpeakerId, cascadeDelete: true)
                .Index(t => t.SpeakerId);
            
            CreateTable(
                "dbo.Speaker",
                c => new
                    {
                        SpeakerId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        SpeachDate = c.DateTime(),
                        Email = c.String(),
                        AlternateEmail = c.String(),
                        MobileNo = c.String(),
                        Telephone = c.String(),
                        CurrentAddress = c.String(),
                        PhotoPath = c.String(),
                        FilePath = c.String(),
                        TopicId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.SpeakerId)
                .ForeignKey("dbo.TopicMaster", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.TopicMaster",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        TopicName = c.String(),
                        SubjectId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TopicId)
                .ForeignKey("dbo.SubjectMaster", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.ForumBlogMedia",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        ForumBlogId = c.Int(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.GuId)
                .ForeignKey("dbo.ForumBlog", t => t.ForumBlogId, cascadeDelete: true)
                .Index(t => t.ForumBlogId);
            
            CreateTable(
                "dbo.ForumBlog",
                c => new
                    {
                        ForumBlogId = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Description = c.String(),
                        MemberRemark = c.String(),
                        StaffRemark = c.String(),
                        Status = c.String(),
                        StaffId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ForumBlogId)
                .ForeignKey("dbo.StaffMaster", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.StaffMaster",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        EmailId = c.String(),
                        PhoneNo = c.String(),
                        Decoration = c.String(),
                        DOBirth = c.DateTime(nullable: false),
                        DOMarriage = c.DateTime(),
                        DOAppointment = c.DateTime(),
                        PostingOut = c.Boolean(nullable: false),
                        SelfImage = c.String(),
                        FacultyId = c.Int(nullable: false),
                        RankId = c.Int(nullable: false),
                        IsLoginUser = c.Boolean(nullable: false),
                        LoginUserId = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.StaffId)
                .ForeignKey("dbo.Faculty", t => t.FacultyId, cascadeDelete: true)
                .ForeignKey("dbo.RankMaster", t => t.RankId, cascadeDelete: true)
                .Index(t => t.FacultyId)
                .Index(t => t.RankId);
            
            CreateTable(
                "dbo.StaffDocument",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        StaffId = c.Int(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                        Verify = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GuId)
                .ForeignKey("dbo.StaffMaster", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.CrsMbrHobby",
                c => new
                    {
                        HobbyId = c.Int(nullable: false, identity: true),
                        Hobby = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.HobbyId);
            
            CreateTable(
                "dbo.HolidayCalendar",
                c => new
                    {
                        HolidayCalendarId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        HolidayType = c.String(),
                        HolidayDate = c.DateTime(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        ColorCode = c.String(),
                        CountryId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.HolidayCalendarId)
                .ForeignKey("dbo.CountryMaster", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.HonourAward",
                c => new
                    {
                        HonourId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Abbreviation = c.String(),
                        Year = c.String(),
                        Decoration = c.String(),
                        CourseMemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HonourId)
                .ForeignKey("dbo.CourseMember", t => t.CourseMemberId, cascadeDelete: true)
                .Index(t => t.CourseMemberId);
            
            CreateTable(
                "dbo.Infotech",
                c => new
                    {
                        ITId = c.Int(nullable: false, identity: true),
                        LaptopFromDepartment = c.Boolean(nullable: false),
                        DLaptopMake = c.String(),
                        DModelNo = c.String(),
                        DSlNo = c.String(),
                        DAdaptor = c.Boolean(nullable: false),
                        DBag = c.Boolean(nullable: false),
                        LaptopFromCollege = c.Boolean(nullable: false),
                        CLaptopMake = c.String(),
                        CModelNo = c.String(),
                        CSlNo = c.String(),
                        CAdaptor = c.Boolean(nullable: false),
                        CBag = c.Boolean(nullable: false),
                        DeclarationFormDocPath = c.String(),
                        InsuranceReceiptDocPath = c.String(),
                        OutsidePermissionDocPath = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ITId);
            
            CreateTable(
                "dbo.InStepCourse",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        UnderRegistration = c.Boolean(nullable: false),
                        RegistrationStartDate = c.DateTime(),
                        RegistrationEndDate = c.DateTime(),
                        TotalStrength = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.InStepRegistration",
                c => new
                    {
                        InStepRegId = c.Int(nullable: false, identity: true),
                        EmailId = c.String(),
                        MobileNo = c.String(),
                        WhatsappNo = c.String(),
                        PhotoPath = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        FatherName = c.String(),
                        HonourAward = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.String(),
                        ServicesCategory = c.String(),
                        ServiceNo = c.String(),
                        BranchDepartment = c.String(),
                        DateOfCommissioning = c.DateTime(),
                        SeniorityYear = c.String(),
                        AddressLocal = c.String(),
                        AddressPermanent = c.String(),
                        NOKName = c.String(),
                        NOKContact = c.String(),
                        PassportNo = c.String(),
                        PassportName = c.String(),
                        PassportAuthority = c.String(),
                        PassportDocPath = c.String(),
                        AadhaarNo = c.String(),
                        AadhaarDocPath = c.String(),
                        BioData = c.String(),
                        ArrivalTime = c.DateTime(),
                        ArrivalMode = c.String(),
                        DepartureTime = c.DateTime(),
                        DepartureMode = c.String(),
                        ApprovedNominationDocPath = c.String(),
                        Approved = c.Boolean(nullable: false),
                        ApprovedDate = c.DateTime(),
                        AnyOtherRequirement = c.String(),
                        UserId = c.String(),
                        RankId = c.Int(nullable: false),
                        CourseId = c.Int(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.InStepRegId)
                .ForeignKey("dbo.InStepCourse", t => t.CourseId)
                .ForeignKey("dbo.RankMaster", t => t.RankId, cascadeDelete: true)
                .Index(t => t.RankId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Leave",
                c => new
                    {
                        LeaveId = c.Int(nullable: false, identity: true),
                        LeaveCategory = c.String(),
                        LeaveType = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        PrefixDate = c.DateTime(),
                        PrefixToDate = c.DateTime(),
                        SuffixDate = c.DateTime(),
                        SuffixToDate = c.DateTime(),
                        TotalDays = c.Int(nullable: false),
                        ReasonForLeave = c.String(),
                        AddressOnLeave = c.String(),
                        TeleNo = c.String(),
                        RecommendByEmbassy = c.String(),
                        DocPath = c.String(),
                        AQStatus = c.String(),
                        AQStatusDate = c.DateTime(),
                        IAGStatus = c.String(),
                        IAGStatusDate = c.DateTime(),
                        ServiceSDSStatus = c.String(),
                        ServiceSDSStatusDate = c.DateTime(),
                        SecretaryStatus = c.String(),
                        SecretaryStatusDate = c.DateTime(),
                        ComdtStatus = c.String(),
                        ComdtStatusDate = c.DateTime(),
                        GenerateCertificate = c.String(),
                        CountryId = c.Int(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.LeaveId)
                .ForeignKey("dbo.CountryMaster", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.LibraryMembership",
                c => new
                    {
                        LibraryMembershipId = c.Int(nullable: false, identity: true),
                        LockerNo = c.String(),
                        MemberName = c.String(),
                        Designation = c.String(),
                        Address = c.String(),
                        MobileNo = c.String(),
                        EmailId = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.LibraryMembershipId);
            
            CreateTable(
                "dbo.LockerAllotment",
                c => new
                    {
                        LockerAllotmentId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        LockerNo = c.String(),
                        RolesAssign = c.String(),
                        IAG = c.String(),
                        SDSId = c.Int(),
                        CourseMemberId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.LockerAllotmentId)
                .ForeignKey("dbo.CrsMemberPersonal", t => t.CourseMemberId, cascadeDelete: true)
                .ForeignKey("dbo.StaffMaster", t => t.SDSId)
                .Index(t => t.SDSId)
                .Index(t => t.CourseMemberId, unique: true);
            
            CreateTable(
                "dbo.MediaCategoryMaster",
                c => new
                    {
                        MediaCategoryId = c.Int(nullable: false, identity: true),
                        MediaCategoryName = c.String(),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.MediaCategoryId);
            
            CreateTable(
                "dbo.MediaFile",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        MediaGalleryId = c.Int(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                        FileDescription = c.String(),
                    })
                .PrimaryKey(t => t.GuId)
                .ForeignKey("dbo.MediaGallery", t => t.MediaGalleryId, cascadeDelete: true)
                .Index(t => t.MediaGalleryId);
            
            CreateTable(
                "dbo.MediaGallery",
                c => new
                    {
                        MediaGalleryId = c.Int(nullable: false, identity: true),
                        MediaType = c.Int(nullable: false),
                        Caption = c.String(),
                        Description = c.String(unicode: false, storeType: "text"),
                        Archive = c.Boolean(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        ArchiveDate = c.DateTime(nullable: false),
                        UserRole = c.String(),
                        MediaCategoryId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.MediaGalleryId)
                .ForeignKey("dbo.MediaCategoryMaster", t => t.MediaCategoryId, cascadeDelete: true)
                .Index(t => t.MediaCategoryId);
            
            CreateTable(
                "dbo.MenuItemMaster",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        SlugMenu = c.String(),
                        SortOrder = c.Int(nullable: false),
                        MenuName = c.String(),
                        HMenuName = c.String(),
                        PageTitle = c.String(),
                        PageHeading = c.String(),
                        HPageHeading = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        PositionType = c.Int(nullable: false),
                        Layout = c.Int(nullable: false),
                        ExternalLink = c.Boolean(nullable: false),
                        ExternalUrl = c.String(),
                        MenuArea = c.String(),
                        MenuUrlId = c.Int(),
                    })
                .PrimaryKey(t => t.MenuId)
                .ForeignKey("dbo.MenuUrlMaster", t => t.MenuUrlId)
                .Index(t => t.MenuUrlId);
            
            CreateTable(
                "dbo.MenuUrlMaster",
                c => new
                    {
                        MenuUrlId = c.Int(nullable: false, identity: true),
                        UrlPrefix = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        PageType = c.Int(nullable: false),
                        MenuLevel = c.Int(nullable: false),
                        UrlArea = c.String(),
                    })
                .PrimaryKey(t => t.MenuUrlId);
            
            CreateTable(
                "dbo.MenuRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                        Read = c.Boolean(nullable: false),
                        Write = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.MenuId })
                .ForeignKey("dbo.MenuItemMaster", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.MessBill",
                c => new
                    {
                        MessBillId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        MemberStaffId = c.Int(nullable: false),
                        Arrear = c.String(),
                        Extra = c.String(),
                        Messing = c.String(),
                        Tea = c.String(),
                        TableMoney = c.String(),
                        Wine = c.String(),
                        MessSubs = c.String(),
                        Rakshika = c.String(),
                        NDCJournal = c.String(),
                        RB = c.String(),
                        BusFund = c.String(),
                        AlumniDinner = c.String(),
                        PLD = c.String(),
                        Corpusfund = c.String(),
                        BreakupParty = c.String(),
                        CanteenSmartCard = c.String(),
                        Total = c.String(),
                        BillMonth = c.String(),
                        PayStatus = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.MessBillId);
            
            CreateTable(
                "dbo.MPhilDegree",
                c => new
                    {
                        MPhilDegreeId = c.Int(nullable: false, identity: true),
                        YearOfAdmission = c.Int(nullable: false),
                        NameOfApplicant = c.String(),
                        NameOfSupervisor = c.String(),
                        DOB = c.DateTime(nullable: false),
                        POB = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        Community = c.String(),
                        Occupation = c.String(),
                        Address = c.String(),
                        StudyMode = c.String(),
                        NameOfExam = c.String(),
                        RegisterNoMonthYear = c.String(),
                        MonthAndYearOfDegree = c.String(),
                        DegreeCollegeName = c.String(),
                        NoAndDateOfEligibilityCert = c.String(),
                        AffiliateCollege = c.String(),
                        IsRecognisedForMphil = c.String(),
                        ObtainedApproval = c.String(),
                        NoAndDateOfApproval = c.String(),
                        NameDesignationOfSupervisor = c.String(),
                        IsSupervisorRecognisedForCourse = c.String(),
                        SupervisorSignPath = c.String(),
                        HoDSignPath = c.String(),
                        PlaceOfApplication = c.String(),
                        DateOfApplication = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.MPhilDegreeId);
            
            CreateTable(
                "dbo.MPhilMember",
                c => new
                    {
                        MPhilId = c.Int(nullable: false, identity: true),
                        CollegeApplied = c.String(),
                        Subject = c.String(),
                        ApplicantNameEnglish = c.String(),
                        ApplicantNameVernacular = c.String(),
                        EmailId = c.String(),
                        DOBirth = c.DateTime(nullable: false),
                        FatherName = c.String(),
                        MotherName = c.String(),
                        Gender = c.String(),
                        CommunicationAddress = c.String(),
                        CommunicationMob = c.String(),
                        CommunicationPin = c.String(),
                        PermanentAddress = c.String(),
                        PermanentMob = c.String(),
                        PermanentPin = c.String(),
                        Nationality = c.String(),
                        Community = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                        PhysicalHandicap = c.String(),
                        YearsStudies = c.String(),
                        DegreeName = c.String(),
                        SubjectMain = c.String(),
                        UniversityCollegeStudied = c.String(),
                        AnyOtherInformation = c.String(),
                        MemberImgPath = c.String(),
                        MarksStatementDocPath = c.String(),
                        PostGradDegreeDocPath = c.String(),
                        CourseCertificateDocPath = c.String(),
                        TranslatedCopyEngDocPath = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.MPhilId);
            
            CreateTable(
                "dbo.MPhilPostGraduate",
                c => new
                    {
                        PostGraduateId = c.Int(nullable: false, identity: true),
                        RegnNo = c.String(),
                        MonthYearPass = c.String(),
                        PaperTitle = c.String(),
                        AwardedIA = c.String(),
                        AwardedUE = c.String(),
                        MaxIA = c.String(),
                        MaxUE = c.String(),
                        MPhilId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.PostGraduateId)
                .ForeignKey("dbo.MPhilMember", t => t.MPhilId, cascadeDelete: true)
                .Index(t => t.MPhilId);
            
            CreateTable(
                "dbo.NewsArticle",
                c => new
                    {
                        NewsArticleId = c.Int(nullable: false, identity: true),
                        NewsCategory = c.Int(nullable: false),
                        Headline = c.String(),
                        ArticleUrl = c.String(),
                        Description = c.String(storeType: "ntext"),
                        Highlight = c.Boolean(nullable: false),
                        Archive = c.Boolean(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        ArchiveDate = c.DateTime(nullable: false),
                        DisplayArea = c.Int(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.NewsArticleId);
            
            CreateTable(
                "dbo.OtherRequest",
                c => new
                    {
                        OtherRequestId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        MobileNo = c.String(),
                        LockerNo = c.String(),
                        Status = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        Remark = c.String(),
                        RemarkDate = c.DateTime(),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OtherRequestId);
            
            CreateTable(
                "dbo.PageContent",
                c => new
                    {
                        PageContentId = c.Int(nullable: false, identity: true),
                        Content = c.String(storeType: "ntext"),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PageContentId)
                .ForeignKey("dbo.MenuItemMaster", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.Rakshika",
                c => new
                    {
                        RakshikaId = c.Int(nullable: false, identity: true),
                        SpouseName = c.String(),
                        CourseMemberName = c.String(),
                        SpouseNickName = c.String(),
                        DOBirth = c.DateTime(nullable: false),
                        Qualification = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.RakshikaId);
            
            CreateTable(
                "dbo.ServiceRation",
                c => new
                    {
                        RationId = c.Int(nullable: false, identity: true),
                        PersonalNo = c.String(),
                        Eater = c.String(),
                        LRC = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.RationId);
            
            CreateTable(
                "dbo.SiteFeedback",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        DepartmentSubject = c.String(),
                        FullName = c.String(),
                        EmailId = c.String(),
                        Comment = c.String(),
                        Approved = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
            CreateTable(
                "dbo.CrsMbrSport",
                c => new
                    {
                        SportId = c.Int(nullable: false, identity: true),
                        Sport = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.SportId);
            
            CreateTable(
                "dbo.SpouseChildren",
                c => new
                    {
                        ChildId = c.Int(nullable: false, identity: true),
                        ChildName = c.String(),
                        ChildGender = c.String(),
                        ChildDOBirth = c.DateTime(nullable: false),
                        ChildOccupation = c.String(),
                        ChildContactNo = c.String(),
                        ChildStayWithMember = c.String(),
                        ChildPassportNo = c.String(),
                        ChildPassportName = c.String(),
                        ChildPassportIssueDate = c.DateTime(),
                        ChildPassportValidUpto = c.DateTime(),
                        ChildPassportType = c.String(),
                        ChildPassportCountryIssued = c.String(),
                        ChildPassportImgPath = c.String(),
                        ChildPassportBackImgPath = c.String(),
                        HoldingPassport = c.String(),
                        ChildVisaNo = c.String(),
                        ChildVisaIssueDate = c.DateTime(),
                        ChildVisaValidUpto = c.DateTime(),
                        ChildVisaPath = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ChildId);
            
            CreateTable(
                "dbo.StaffPhoto",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                        Verify = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GuId);
            
            CreateTable(
                "dbo.Suggestion",
                c => new
                    {
                        SuggestionId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        SuggestionType = c.String(),
                        Reply = c.String(),
                        Status = c.String(),
                        StaffId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.SuggestionId)
                .ForeignKey("dbo.StaffMaster", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.TADAClaims",
                c => new
                    {
                        TADAId = c.Int(nullable: false, identity: true),
                        CDAACNo = c.String(),
                        BasicPay = c.String(),
                        MSP = c.String(),
                        PayLevel = c.String(),
                        MobileNo = c.String(),
                        BankNameAddress = c.String(),
                        PayACOffcAddress = c.String(),
                        FullName = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TADAId);
            
            CreateTable(
                "dbo.TallyDetail",
                c => new
                    {
                        TallyId = c.Int(nullable: false, identity: true),
                        RankAbbr = c.String(),
                        PassportName = c.String(),
                        TabName = c.String(),
                        NickName = c.String(),
                        CountryService = c.String(),
                        NameORRank = c.String(),
                        ResidentialAddress = c.String(),
                        MobileNo = c.String(),
                        TelephoneNo = c.String(),
                        BrandModelNo = c.String(),
                        Colour = c.String(),
                        RegistrationNo = c.String(),
                        DrivingLicenseNo = c.String(),
                        NoOfVehicle = c.String(),
                        RegistrationCertificatePath = c.String(),
                        DrivingLicensePath = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TallyId);
            
            CreateTable(
                "dbo.TelecommRequirement",
                c => new
                    {
                        TelecommReqId = c.Int(nullable: false, identity: true),
                        HouseNo = c.String(),
                        ReqInternet = c.Boolean(nullable: false),
                        ResidentialComplex = c.String(),
                        TypeOfConnection = c.String(),
                        Comments = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TelecommReqId);
            
            CreateTable(
                "dbo.TrainingActivity",
                c => new
                    {
                        TrainingActivityId = c.Int(nullable: false, identity: true),
                        Module = c.String(),
                        Activity = c.String(),
                        Description = c.String(),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingActivityId);
            
            CreateTable(
                "dbo.TrainingActivityMedia",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        TrainingActivityId = c.Int(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.GuId)
                .ForeignKey("dbo.TrainingActivity", t => t.TrainingActivityId, cascadeDelete: true)
                .Index(t => t.TrainingActivityId);
            
            CreateTable(
                "dbo.UserActivity",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Data = c.String(),
                        UserName = c.String(),
                        IpAddress = c.String(),
                        ActivityDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityId);
            
            CreateTable(
                "dbo.VisaDetail",
                c => new
                    {
                        VisaId = c.Int(nullable: false, identity: true),
                        VisaEntryType = c.String(),
                        VisaNo = c.String(),
                        VisaIssueDate = c.DateTime(nullable: false),
                        VisaValidUpto = c.DateTime(nullable: false),
                        SelfFRRONo = c.String(),
                        SelfIssueDate = c.DateTime(nullable: false),
                        SelfValidUpto = c.DateTime(nullable: false),
                        SpouseFRRONo = c.String(),
                        SpouseIssueDate = c.DateTime(nullable: false),
                        SpouseValidUpto = c.DateTime(nullable: false),
                        VisaPath = c.String(),
                        SelfFRROPath = c.String(),
                        SpouseFRROPath = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.VisaId);
            
            CreateTable(
                "dbo.Visitor",
                c => new
                    {
                        VisitorId = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(nullable: false),
                        Slug = c.String(),
                        IpAddress = c.String(),
                        VisitDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.VisitorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingActivityMedia", "TrainingActivityId", "dbo.TrainingActivity");
            DropForeignKey("dbo.Suggestion", "StaffId", "dbo.StaffMaster");
            DropForeignKey("dbo.PageContent", "MenuId", "dbo.MenuItemMaster");
            DropForeignKey("dbo.MPhilPostGraduate", "MPhilId", "dbo.MPhilMember");
            DropForeignKey("dbo.MenuRoles", "MenuId", "dbo.MenuItemMaster");
            DropForeignKey("dbo.MenuItemMaster", "MenuUrlId", "dbo.MenuUrlMaster");
            DropForeignKey("dbo.MediaFile", "MediaGalleryId", "dbo.MediaGallery");
            DropForeignKey("dbo.MediaGallery", "MediaCategoryId", "dbo.MediaCategoryMaster");
            DropForeignKey("dbo.LockerAllotment", "SDSId", "dbo.StaffMaster");
            DropForeignKey("dbo.LockerAllotment", "CourseMemberId", "dbo.CrsMemberPersonal");
            DropForeignKey("dbo.Leave", "CountryId", "dbo.CountryMaster");
            DropForeignKey("dbo.InStepRegistration", "RankId", "dbo.RankMaster");
            DropForeignKey("dbo.InStepRegistration", "CourseId", "dbo.InStepCourse");
            DropForeignKey("dbo.HonourAward", "CourseMemberId", "dbo.CourseMember");
            DropForeignKey("dbo.HolidayCalendar", "CountryId", "dbo.CountryMaster");
            DropForeignKey("dbo.ForumBlogMedia", "ForumBlogId", "dbo.ForumBlog");
            DropForeignKey("dbo.ForumBlog", "StaffId", "dbo.StaffMaster");
            DropForeignKey("dbo.StaffMaster", "RankId", "dbo.RankMaster");
            DropForeignKey("dbo.StaffDocument", "StaffId", "dbo.StaffMaster");
            DropForeignKey("dbo.StaffMaster", "FacultyId", "dbo.Faculty");
            DropForeignKey("dbo.FeedbackSpeaker", "SpeechEventId", "dbo.SpeechEvent");
            DropForeignKey("dbo.SpeechEvent", "SpeakerId", "dbo.Speaker");
            DropForeignKey("dbo.Speaker", "TopicId", "dbo.TopicMaster");
            DropForeignKey("dbo.TopicMaster", "SubjectId", "dbo.SubjectMaster");
            DropForeignKey("dbo.FeedbackModule", "SubjectId", "dbo.SubjectMaster");
            DropForeignKey("dbo.EventParticipant", "EventId", "dbo.Event");
            DropForeignKey("dbo.EventMember", "EventId", "dbo.Event");
            DropForeignKey("dbo.CrsMemberPersonal", "OfficeStateId", "dbo.StateMaster");
            DropForeignKey("dbo.CrsMemberPersonal", "CourseId", "dbo.Course");
            DropForeignKey("dbo.CrsMemberPersonal", "CitizenshipCountryId", "dbo.CountryMaster");
            DropForeignKey("dbo.SpouseQualification", "SpouseId", "dbo.CrsMbrSpouse");
            DropForeignKey("dbo.SpouseLanguage", "SpouseId", "dbo.CrsMbrSpouse");
            DropForeignKey("dbo.CrsMbrAppointment", "RankId", "dbo.RankMaster");
            DropForeignKey("dbo.CrsMbrAddress", "StateId", "dbo.StateMaster");
            DropForeignKey("dbo.CourseRegister", "RankId", "dbo.RankMaster");
            DropForeignKey("dbo.CourseRegister", "CourseId", "dbo.Course");
            DropForeignKey("dbo.CourseMember", "RankId", "dbo.RankMaster");
            DropForeignKey("dbo.CityMaster", "StateId", "dbo.StateMaster");
            DropForeignKey("dbo.StateMaster", "CountryId", "dbo.CountryMaster");
            DropForeignKey("dbo.CircularDetail", "DesignationId", "dbo.Community");
            DropForeignKey("dbo.CircularMedia", "CircularId", "dbo.Circular");
            DropForeignKey("dbo.CircularDetail", "CircularId", "dbo.Circular");
            DropForeignKey("dbo.ChildrenPassport", "PassportId", "dbo.PassportDetail");
            DropForeignKey("dbo.ArrivalMeal", "ArrivalId", "dbo.ArrivalDetail");
            DropForeignKey("dbo.ArrivalAccompanied", "ArrivalId", "dbo.ArrivalDetail");
            DropForeignKey("dbo.AlumniArticleMedia", "ArticleId", "dbo.AlumniArticle");
            DropForeignKey("dbo.AccomodationMedia", "AccomodationId", "dbo.Accomodation");
            DropIndex("dbo.TrainingActivityMedia", new[] { "TrainingActivityId" });
            DropIndex("dbo.Suggestion", new[] { "StaffId" });
            DropIndex("dbo.PageContent", new[] { "MenuId" });
            DropIndex("dbo.MPhilPostGraduate", new[] { "MPhilId" });
            DropIndex("dbo.MenuRoles", new[] { "MenuId" });
            DropIndex("dbo.MenuItemMaster", new[] { "MenuUrlId" });
            DropIndex("dbo.MediaGallery", new[] { "MediaCategoryId" });
            DropIndex("dbo.MediaFile", new[] { "MediaGalleryId" });
            DropIndex("dbo.LockerAllotment", new[] { "CourseMemberId" });
            DropIndex("dbo.LockerAllotment", new[] { "SDSId" });
            DropIndex("dbo.Leave", new[] { "CountryId" });
            DropIndex("dbo.InStepRegistration", new[] { "CourseId" });
            DropIndex("dbo.InStepRegistration", new[] { "RankId" });
            DropIndex("dbo.HonourAward", new[] { "CourseMemberId" });
            DropIndex("dbo.HolidayCalendar", new[] { "CountryId" });
            DropIndex("dbo.StaffDocument", new[] { "StaffId" });
            DropIndex("dbo.StaffMaster", new[] { "RankId" });
            DropIndex("dbo.StaffMaster", new[] { "FacultyId" });
            DropIndex("dbo.ForumBlog", new[] { "StaffId" });
            DropIndex("dbo.ForumBlogMedia", new[] { "ForumBlogId" });
            DropIndex("dbo.TopicMaster", new[] { "SubjectId" });
            DropIndex("dbo.Speaker", new[] { "TopicId" });
            DropIndex("dbo.SpeechEvent", new[] { "SpeakerId" });
            DropIndex("dbo.FeedbackSpeaker", new[] { "SpeechEventId" });
            DropIndex("dbo.FeedbackModule", new[] { "SubjectId" });
            DropIndex("dbo.EventParticipant", new[] { "EventId" });
            DropIndex("dbo.EventMember", new[] { "EventId" });
            DropIndex("dbo.CrsMemberPersonal", "IX_CreatedByUniqueKey");
            DropIndex("dbo.CrsMemberPersonal", new[] { "CourseId" });
            DropIndex("dbo.CrsMemberPersonal", new[] { "CitizenshipCountryId" });
            DropIndex("dbo.CrsMemberPersonal", new[] { "OfficeStateId" });
            DropIndex("dbo.SpouseQualification", new[] { "SpouseId" });
            DropIndex("dbo.SpouseLanguage", new[] { "SpouseId" });
            DropIndex("dbo.CrsMbrSpouse", "IX_CreatedByUniqueKey");
            DropIndex("dbo.CrsMbrAppointment", "IX_CreatedByUniqueKey");
            DropIndex("dbo.CrsMbrAppointment", new[] { "RankId" });
            DropIndex("dbo.CrsMbrAddress", new[] { "StateId" });
            DropIndex("dbo.CourseRegister", new[] { "CourseId" });
            DropIndex("dbo.CourseRegister", new[] { "RankId" });
            DropIndex("dbo.CourseMember", new[] { "RankId" });
            DropIndex("dbo.StateMaster", new[] { "CountryId" });
            DropIndex("dbo.CityMaster", new[] { "StateId" });
            DropIndex("dbo.CircularMedia", new[] { "CircularId" });
            DropIndex("dbo.CircularDetail", new[] { "CircularId" });
            DropIndex("dbo.CircularDetail", new[] { "DesignationId" });
            DropIndex("dbo.ChildrenPassport", new[] { "PassportId" });
            DropIndex("dbo.ArrivalMeal", new[] { "ArrivalId" });
            DropIndex("dbo.ArrivalAccompanied", new[] { "ArrivalId" });
            DropIndex("dbo.AlumniArticleMedia", new[] { "ArticleId" });
            DropIndex("dbo.AccomodationMedia", new[] { "AccomodationId" });
            DropTable("dbo.Visitor");
            DropTable("dbo.VisaDetail");
            DropTable("dbo.UserActivity");
            DropTable("dbo.TrainingActivityMedia");
            DropTable("dbo.TrainingActivity");
            DropTable("dbo.TelecommRequirement");
            DropTable("dbo.TallyDetail");
            DropTable("dbo.TADAClaims");
            DropTable("dbo.Suggestion");
            DropTable("dbo.StaffPhoto");
            DropTable("dbo.SpouseChildren");
            DropTable("dbo.CrsMbrSport");
            DropTable("dbo.SiteFeedback");
            DropTable("dbo.ServiceRation");
            DropTable("dbo.Rakshika");
            DropTable("dbo.PageContent");
            DropTable("dbo.OtherRequest");
            DropTable("dbo.NewsArticle");
            DropTable("dbo.MPhilPostGraduate");
            DropTable("dbo.MPhilMember");
            DropTable("dbo.MPhilDegree");
            DropTable("dbo.MessBill");
            DropTable("dbo.MenuRoles");
            DropTable("dbo.MenuUrlMaster");
            DropTable("dbo.MenuItemMaster");
            DropTable("dbo.MediaGallery");
            DropTable("dbo.MediaFile");
            DropTable("dbo.MediaCategoryMaster");
            DropTable("dbo.LockerAllotment");
            DropTable("dbo.LibraryMembership");
            DropTable("dbo.Leave");
            DropTable("dbo.InStepRegistration");
            DropTable("dbo.InStepCourse");
            DropTable("dbo.Infotech");
            DropTable("dbo.HonourAward");
            DropTable("dbo.HolidayCalendar");
            DropTable("dbo.CrsMbrHobby");
            DropTable("dbo.StaffDocument");
            DropTable("dbo.StaffMaster");
            DropTable("dbo.ForumBlog");
            DropTable("dbo.ForumBlogMedia");
            DropTable("dbo.TopicMaster");
            DropTable("dbo.Speaker");
            DropTable("dbo.SpeechEvent");
            DropTable("dbo.FeedbackSpeaker");
            DropTable("dbo.SubjectMaster");
            DropTable("dbo.FeedbackModule");
            DropTable("dbo.Faculty");
            DropTable("dbo.EventParticipant");
            DropTable("dbo.Event");
            DropTable("dbo.EventMember");
            DropTable("dbo.CrsMemberPersonal");
            DropTable("dbo.CrsMbrVehicleSticker");
            DropTable("dbo.SpouseQualification");
            DropTable("dbo.SpouseLanguage");
            DropTable("dbo.CrsMbrSpouse");
            DropTable("dbo.CrsMbrQualification");
            DropTable("dbo.CrsMbrLanguage");
            DropTable("dbo.CrsMbrAppointment");
            DropTable("dbo.CrsMbrAddress");
            DropTable("dbo.Course");
            DropTable("dbo.CourseRegister");
            DropTable("dbo.RankMaster");
            DropTable("dbo.CourseMember");
            DropTable("dbo.CourseFeedback");
            DropTable("dbo.CountryVisit");
            DropTable("dbo.CountryMaster");
            DropTable("dbo.StateMaster");
            DropTable("dbo.CityMaster");
            DropTable("dbo.Community");
            DropTable("dbo.CircularMedia");
            DropTable("dbo.Circular");
            DropTable("dbo.CircularDetail");
            DropTable("dbo.PassportDetail");
            DropTable("dbo.ChildrenPassport");
            DropTable("dbo.CrsMbrBiography");
            DropTable("dbo.AsgmtAppointment");
            DropTable("dbo.ArrivalMeal");
            DropTable("dbo.ArrivalDetail");
            DropTable("dbo.ArrivalAccompanied");
            DropTable("dbo.AppointmentDetail");
            DropTable("dbo.AlumniMaster");
            DropTable("dbo.AlumniFeedback");
            DropTable("dbo.AlumniArticle");
            DropTable("dbo.AlumniArticleMedia");
            DropTable("dbo.AccountInfo");
            DropTable("dbo.Accomodation");
            DropTable("dbo.AccomodationMedia");
        }
    }
}
