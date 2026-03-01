using DL;
using DL.Entities;
using MediaContext.Application.Contracts.Commands;
using MediatR;

namespace MediaContext.Application.UseCases.Commands
{
    public class UploadFileRequestHandler : IRequestHandler<UploadFileRequest, Guid?>
    {
        private MediaDbContext _db;

        public UploadFileRequestHandler(MediaDbContext db)
        {
            _db = db;
        }

        public async Task<Guid?> Handle(UploadFileRequest request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var mediaFile = new MediaFile(id, request.Content, request.ContentType);
            _db.Add(mediaFile);
            await _db.SaveChangesAsync();

            return id;
        }
    }
}
