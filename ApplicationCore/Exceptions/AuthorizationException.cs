namespace ApplicationCore.Exceptions;

public class AuthorizationException(string message) : Exception(message);