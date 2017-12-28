
namespace Fbtc.Domain.Entities
{
    public class FileImage
    {
        public string ProjectId { get; set; }
        public string SectionId { get; set; }
        public string FileName { get; set; }
        public string HashName { get; set; }
        public int FileSize { get; set; }
        public string ImagePath { get; set; }
        public string ThumbPath { get; set; }
    }
}
