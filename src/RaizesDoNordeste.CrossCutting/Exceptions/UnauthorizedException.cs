namespace RaizesDoNordeste.CrossCutting.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException()
        : base("Credenciais inválidas.")
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public UnauthorizedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
