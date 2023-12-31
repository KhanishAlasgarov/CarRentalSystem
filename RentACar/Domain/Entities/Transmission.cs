using Core.Persistance.Repositories;

namespace Domain.Entities;

public class Transmission:Entity<Guid>
{
    public string Name { get; set; } = default!;
    public ICollection<Model>? Models { get; set; }

}
