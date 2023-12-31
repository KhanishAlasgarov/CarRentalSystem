using Core.Persistance.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Car : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public Guid ModelId { get; set; }
    public Model? Model { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string? Plate { get; set; }
    public short MinFindexScore { get; set; }

    public CarState CarState { get; set; }
}
