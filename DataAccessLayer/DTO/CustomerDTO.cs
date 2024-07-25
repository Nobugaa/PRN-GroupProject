namespace DataAccessLayer.DTO;

public class CustomerDTO
{
    public int CustomerId { get; set; }
    public string? CustomerFullName { get; set; }
    public string? Telephone { get; set; }
    public string EmailAddress { get; set; } = null!;
    public DateTime? CustomerBirthday { get; set; }
    public byte? CustomerStatus { get; set; }
    public string? Password { get; set; }
    public byte[]? CCCDFaceFront { get; set; }
    public byte[]? CCCDBackSide { get; set; }
}
