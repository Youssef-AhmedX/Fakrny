﻿namespace Fakrny.Blazor.Models;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public string? StatusCode { get; set; }
    public string? Error { get; set; }
    public string? Message { get; set; }
    public T? Object { get; set; }
    public List<T>? ObjectsList { get; set; }
}
