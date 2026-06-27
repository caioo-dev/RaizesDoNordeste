namespace RaizesDoNordeste.CrossCutting.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NotFoundException(string entidade, object id)
        : base($"{entidade} com id '{id}' não foi encontrado(a).")
    {
    }
}
