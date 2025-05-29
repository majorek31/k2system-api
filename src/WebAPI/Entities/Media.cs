using WebAPI.Common;

namespace WebAPI.Entities;

public class Media : BaseEntity
{
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public long FileSize { get; set; }
    public string Path { get; set; }
    
    public int UploaderId { get; set; }
    public User Uploader { get; set; }
}