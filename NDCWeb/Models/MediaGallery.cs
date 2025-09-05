using NDCWeb.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NDCWeb.Models
{
    public class MediaGallery : BaseEntity
    {
        public MediaGallery()
        {
            iMediaFiles = new List<MediaFile>();
        }
        [Key]
        public int MediaGalleryId { get; set; }
        public MediaType MediaType { get; set; }
        public string Caption { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
        public bool Archive { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ArchiveDate { get; set; }
        public string UserRole { get; set; }
        public int MediaCategoryId { get; set; }
        public virtual MediaCategoryMaster MediaCategoryMasters { get; set; }

        public virtual ICollection<MediaFile> iMediaFiles { get; set; }
    }
}