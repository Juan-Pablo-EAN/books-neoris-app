namespace BooksNeorisApp.Exceptions
{
    public class BusinessException(string message) : Exception(message)
    {
    }

    public class MaximoLibrosPermitidosException : BusinessException
    {
        public MaximoLibrosPermitidosException()
            : base("No es posible registrar el libro, se alcanzó el máximo permitido")
        {
        }
    }

    public class AutorNoEncontradoException : BusinessException
    {
        public AutorNoEncontradoException()
            : base("El autor no está registrado")
        {
        }
    }
}
