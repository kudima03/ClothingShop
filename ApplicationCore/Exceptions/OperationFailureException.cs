namespace ApplicationCore.Exceptions;

public class OperationFailureException(string message) : Exception(message);