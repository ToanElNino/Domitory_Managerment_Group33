namespace App.Shared.Grpc;

public enum ResultEnum
{
    SUCCESS = 0,
    ERROR = 1,
    UNAUTHENTICATED = 2,
    PERMISSION_DENIED = 3,
    NOT_FOUND = 4,
    INVALID_ARGUMENT = 5,
}