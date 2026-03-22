namespace KPIBackend.Models
{
    /// <summary>
    /// Define una entidad con un identificador único.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Identificador único de la entidad.
        /// </summary>
        Guid Id { get; set; }

    }
}
