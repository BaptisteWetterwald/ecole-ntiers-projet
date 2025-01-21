namespace Puissance4.Application.DTOs;

public class ListDto<T>
{
    public List<T> Items { get; set; } = new();
}