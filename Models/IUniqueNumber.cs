namespace KPIBackend.Models
{
    /// <summary>
    /// Define una entidad identificada por un número único y nombre único.
    /// </summary>
    public interface IUniqueNumber
    {
        /// <summary>
        /// Número único para la entidad (únicamente aplica al grupo de indicadores).
        /// </summary>
        public int NumeroGrupo { get; }

        /// <summary>
        /// Nombre único para la entidad (únicamente aplica al grupo de indicadores).
        /// </summary>
        public string DescripcionGrupo { get; }
    }
}
