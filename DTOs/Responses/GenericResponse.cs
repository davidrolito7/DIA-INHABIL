namespace ApiDiaInhabil.DTOs.Responses;

public class GenericResponse<T>
{
    public bool Success { get; set; }
    public int Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public object? Error { get; set; }
}