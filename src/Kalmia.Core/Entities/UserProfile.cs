using Kalmia.Core.Common;

namespace Kalmia.Core.Entities;

// ------------------------------------------------------------------------------------------------
// main UserProfile entity class
// ------------------------------------------------------------------------------------------------
public class UserProfile
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public double? HeightCm { get; set; }
    public double? WeightKg { get; set; }
    public Sex Sex { get; set; }

    public double? HeighIn => HeightCm.HasValue ? HeightCm / 2.54 : null;
    public double? WeightLbs => WeightKg.HasValue ? WeightKg * 2.20462 : null;
}

// ------------------------------------------------------------------------------------------------
// UserProfile DTO
// ------------------------------------------------------------------------------------------------
public record UserProfileDto
(
    int? Id,
    string Name,
    string? Email,
    double? HeightCm,
    double? WeightKg,
    Sex Sex
);

// ------------------------------------------------------------------------------------------------
// manual mappings for entity <--> dto
// ------------------------------------------------------------------------------------------------
public static class UserProfileMappingExtensions
{
    public static UserProfileDto ToDto(this UserProfile entity)
    {
        return new UserProfileDto(
            Id: entity.Id,
            Name: entity.Name,
            Email: entity.Email,
            HeightCm: entity.HeightCm,
            WeightKg: entity.WeightKg,
            Sex: entity.Sex
        );
    }

    public static UserProfile ToEntity(this UserProfileDto dto)
    {
        var entity = new UserProfile();
        entity.SetFieldsFrom(dto);
        return entity;
    }

    // create this method instead to be used from the service directly during update and in ToEntity during create
    public static void SetFieldsFrom(this UserProfile entity, UserProfileDto dto)
    {
        entity.Name = dto.Name;
        entity.Email = dto.Email;
        entity.HeightCm = dto.HeightCm;
        entity.WeightKg = dto.WeightKg;
        entity.Sex = dto.Sex;
    }
}