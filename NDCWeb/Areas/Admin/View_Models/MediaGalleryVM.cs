using NDCWeb.Infrastructure.Constants;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Admin.View_Models
{
    public class MediaGalleryVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter MediaGalleryId")]
        [Display(Name = "Media Gallery Id")]
        public int MediaGalleryId { get; set; }

        [Required(ErrorMessage = "Select MediaType")]
        [Display(Name = "Media Type")]
        [Range(1, int.MaxValue, ErrorMessage = "Select MediaType")]
        public MediaType MediaType { get; set; }

        [Required(ErrorMessage = "Please Enter Caption")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Caption")]
        public string Caption { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        //[RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Archive")]
        public bool Archive { get; set; }

        [Required(ErrorMessage = "Please Enter PublishDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Please Enter ArchiveDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Archive Date")]
        public DateTime ArchiveDate { get; set; }

        [Required(ErrorMessage = "Select MediaCategory")]
        [Display(Name = "Media Category")]
        public int MediaCategoryId { get; set; }
        public virtual MediaCategoryMaster MediaCategoryMasters { get; set; }
    }
    public class MediaGalleryIndxVM : MediaGalleryVM
    {
        public MediaGalleryIndxVM()
        {
            iMediaFiles = new List<MediaFile>();
        }
        public virtual ICollection<MediaFile> iMediaFiles { get; set; }
    }
    public class MediaGalleryCrtVM : MediaGalleryVM
    {
        public MediaGalleryCrtVM()
        {
            iMediaFiles = new List<MediaFile>();
        }
        public virtual ICollection<MediaFile> iMediaFiles { get; set; }

    }
    public class MediaGalleryUpVM : MediaGalleryVM
    {
        public MediaGalleryUpVM()
        {
            iMediaFiles = new List<MediaFile>();
        }
        public virtual ICollection<MediaFile> iMediaFiles { get; set; }

        [Display(Name = "Want to Overwrite Files ? ")]
        public bool OverWriteFile { get; set; }
    }
}