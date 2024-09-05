using System.ComponentModel.DataAnnotations;

namespace StudentBloggAPI.Models.DTOs;

public record UserDTO(int id, string userName, string firstName, string lastName, string email, DateTime created);
	


