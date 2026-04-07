namespace KPIBackend.Models
{
    /// <summary>
    /// Define una entidad cuyo nombre debe ser único.
    /// </summary>
    public interface IUniqueName
    {
        /// <summary>
        /// Nombre único para la entidad (ejemplo: rol, facultad, período escolar).
        /// </summary>
        string Nombre { get; set; }
    }

}
