namespace KPIBackend.Models
{
    /// <summary>
    /// Define una entidad que pertenece a un usuario creador.
    /// </summary>
    public interface IOwnedEntity
    {
        /// <summary>
        /// Creador de una entidad.
        /// </summary>
        Guid CreadorId { get; }
    }

}
