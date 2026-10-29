using DSW_3P_PR01.Models;
using DSW_3P_PR01.Repositories;

namespace DSW_3P_PR01.Services;

public class MisionService : IMisionService
{
    private readonly IMisionRepository _repository;

    public MisionService(IMisionRepository repository)
    {
        _repository = repository;
    }

    private static void Validar(Mision mision)
    {
        if (string.IsNullOrWhiteSpace(mision.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");
        
        if (string.IsNullOrWhiteSpace(mision.Dificultad) || !string.Equals(mision.Dificultad, "Facil") 
        && !string.Equals(mision.Dificultad, "Media") && !string.Equals(mision.Dificultad, "Dificil"))
            throw new ArgumentException("La dificultad debe ser válida.");

        if(mision.RecompensaXP < 0)
            throw new ArgumentException("La duración no es válida");
    }

    public List<Mision> ObtenerTodas()
    {
        return _repository.ObtenerTodas();
    }

    public Mision? ObtenerPorId(int id)
    {
        var mision = _repository.ObtenerPorId(id);

        return mision == null ? null : mision;
    }

    public Mision Agregar(Mision mision)
    {
        Validar(mision);

        return _repository.Agregar(mision);
    }

    public bool Actualizar(int id, Mision mision)
    {
        Validar(mision);

        return _repository.Actualizar(id, mision);
    }

    public bool Eliminar(int id)
    {
        return _repository.Eliminar(id);
    }
}  