using System.ComponentModel.DataAnnotations;

namespace TareaPrioridades.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El Nombre es un campo obligatorio.")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El Nombre debe contener solo letras.")]
    public string? Nombre { get; set; }

    [StringLength(10, ErrorMessage = "No Puede Exceder los 10 Caracteres")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "El Numero de teléfono solo puede contener dígitos.")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "El Celular es un campo obligatorio.")]
    [StringLength(10, ErrorMessage = "No Puede Exceder los 10 Caracteres")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "El Celular solo puede contener digitos.")]
    public string? Celular { get; set; }

    [Required(ErrorMessage = "El RNC es un campo obligatorio.")]
    [StringLength(9, ErrorMessage = "No Puede Exceder los 9 Caracteres")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "El RNC solo puede contener digitos.")]
    public string? RNC { get; set; }

    [Required(ErrorMessage = "El correo electrónico es un campo obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "La dirección es un campo obligatorio.")]
    public string? Direccion { get; set; }

}
