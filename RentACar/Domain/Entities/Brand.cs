using Core.Persistance.Repositories;

namespace Domain.Entities;

public class Brand : Entity<Guid>
{
    public string? Name { get; set; }
    public ICollection<Model>? Models { get; set; }

    public Brand()
    {

    }
    public Brand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
