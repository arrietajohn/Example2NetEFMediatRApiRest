using MyProject.Domain.Common;

namespace MyProject.Domain;

public class VideoActor : BaseDomainModel 
{
    public int VideoId { get; set; }
    public int ActorId { get; set; }

}
