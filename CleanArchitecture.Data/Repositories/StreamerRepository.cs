using MyProject.Application.Contracts.Persistence;
using MyProject.Domain;
using MyProject.Infrastructure.Persistence;

namespace MyProject.Infrastructure.Repositories;

public class StreamerRepository : RepositoryBase<Streamer>, IStreamerRepository
{
    public StreamerRepository(StreamerDbContext context) : base(context)
    { }
}
