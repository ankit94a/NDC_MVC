using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDCWeb.Models
{
    public class MediaFile
    {
        public MediaFile()
        {
            GuId = Guid.NewGuid();
        }
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GuId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public string FileDescription { get; set; }

        [Column(Order = 2)]
        public int MediaGalleryId { get; set; }
        public MediaGallery MediaGalleries { get; set; }
       
        [NotMapped]
        public int MediaCategoryId { get; set; }
        //public int MediaFileId { get; set; }
    }
}