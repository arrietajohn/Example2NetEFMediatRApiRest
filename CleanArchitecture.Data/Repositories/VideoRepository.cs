using MyProject.Application.Contracts.Persistence;
using MyProject.Domain;
using MyProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MyProject.Infrastructure.Repositories;

public class VideoRepository : RepositoryBase<Video>, IVideoRepository
{
    public VideoRepository(StreamerDbContext context) : base(context)
    { 
    }
    public async Task<Video> GetVideoByNombre(string nombreVideo)
    {
        return await _context.Videos!.Where(o => o.Nombre == nombreVideo).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Video>> GetVideoByUsername(string username)
    {
        return await _context.Videos!.Where(v => v.CreatedBy == username).ToListAsync();
    }
}
