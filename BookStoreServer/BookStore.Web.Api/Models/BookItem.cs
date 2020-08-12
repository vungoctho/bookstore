using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Api.Models
{
    public class BookItem
    {
        public string Id { get; set; }
        public Book VolumeInfo { get; set; }
        
    }
    public class ImageLink
    {
        public string SmallThumbnail { get; set; }
        public string Thumbnail { get; set; }
    }
    public class Book
    {
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public string PublishedDate { get; set; }
        public string Subtitle { get; set; }
        public string Author
        {
            get
            {
                return Authors != null ? string.Join("-", Authors) : "";
            }
        }
        public ImageLink ImageLinks { get; set; }
    }

    public class RootObject
    {
        public int TotalItems { get; set; }
        public List<BookItem> Items { get; set; }
    }
}
