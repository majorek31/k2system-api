namespace WebAPI.Common.Exceptions;

class NotFoundException(string message = "NotFound") : Exception(message);