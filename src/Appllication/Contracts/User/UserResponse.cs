namespace Application.Contracts.User;

public class UserResponse<T>
{
    public int Id { get; set; }

    public bool Success { get; set; }

    public string Message { get; set; }

    public T Data { get; set; }
}