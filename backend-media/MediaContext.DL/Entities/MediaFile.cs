using DL.Enums;

namespace DL.Entities;

public class MediaFile
{
    public Guid Id { get; set; }
    public byte[] Content { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public FileStatus Status { get; private set; }
    public DateTime UploadTime { get; private set; }

    private MediaFile()
    {
    }

    private MediaFile(Guid id, byte[] cotnext, string contentType, FileStatus status, DateTime uploadTime)
    {
        Id = id;
        Content = cotnext;
        ContentType = contentType;
        Status = status;
        UploadTime = uploadTime;
    }

    public static MediaFile Create(Guid id, byte[] content, string contentType)
    {
        return new MediaFile(
            id, content, contentType,
            FileStatus.Pending, DateTime.UtcNow);
    }

    public void MarkAsInUse()
    {
        Status = FileStatus.InUse;
    }

    public void MarkAsUnused()
    {
        Status = FileStatus.Unused;
    }
}