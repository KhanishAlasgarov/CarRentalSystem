using Core.Persistance.Repositories;

namespace Domain.Entities;

public class Model : Entity<Guid>
{
    public Guid BrandId { get; set; }
    public Guid FuelId { get; set; }
    public Guid TransmissionId { get; set; }
    public string Name { get; set; } = default!;
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; } = default!;

    public virtual Brand? Brand { get; set; }
    public virtual Fuel? Fuel { get; set; }
    public virtual Transmission? Transmission { get; set; }
    public virtual ICollection<Car>? Cars { get; set; }

    public Model()
    {
        
    }

    public Model(Guid brandId, Guid fuelId, Guid transmissionId, string name, decimal dailyPrice, string? imageUrl)
    {
        BrandId = brandId;
        FuelId = fuelId;
        TransmissionId = transmissionId;
        Name = name;
        DailyPrice = dailyPrice;
        ImageUrl = imageUrl;
    }
}
