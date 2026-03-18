namespace KPIBackend.Models
{
    public interface IOwnedEntity
    {
        Guid CreadorId { get; }
    }

}
