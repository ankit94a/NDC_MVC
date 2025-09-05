using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Infrastructure.Constants
{
    public class CustomEnum
    {
    }
    public enum PositionType
    {
        Top = 1,
        Left = 2,
        Bottom=3
    }
    
    public enum Layout
    {
        Lay1 = 1,
        Lay2 = 2
    }
    public enum PageType
    {
        //Select = 0,
        Static=1,
        Dynamic,
    }
    public enum MenuLevel
    {
        NA = 1,
        Parent,
        Child,
    }
    public enum NewsCategory
    {
        [Display(Name = "What's New")]
        WhatsNew = 1,

        [Display(Name = "Upcoming Events")]
        Upcoming = 2,

        [Display(Name = "Last Months Highlights")]
        LastMonth = 3,

        [Display(Name = "Flash News")]
        Flash = 4,

        [Display(Name = "News")]
        News = 5,

        [Display(Name = "NDC in News")]
        NDC = 6
    }
    public enum NewsDisplayArea
    {
        [Display(Name = "Public")]
        Public = 1,

        [Display(Name = "Member")]
        Member = 2,

        [Display(Name = "Staff")]
        Staff = 3,

        [Display(Name = "Alumni")]
        Alumni = 4,

        [Display(Name = "All")]
        All = 5
    }
    public enum MediaType
    {
        Image = 1,
        Video = 2,
        Document = 3
    }
    public enum CategoryRef
    {
        ImageGallery = 1,
        VideoGallery = 2,
        Others = 3
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
    
}