using System.Net.Mail;
using Kalmia.Core.Common;
using Kalmia.Core.Entities;
using Kalmia.Core.Interfaces;

namespace Kalmia.Core.Services;

// ------------------------------------------------------------------------------------------------
// Implementation of IUserProfileService to contain UserProfile business logic
// ------------------------------------------------------------------------------------------------
public class UserProfileService : IUserProfileService
{
    private readonly IUserProfileRepository _repo;
    public UserProfileService(IUserProfileRepository repo) => _repo = repo;

    public async Task<Result<List<UserProfileDto>>> GetAllAsync()
    {
        var entities = await _repo.GetAllAsync();
        return Result<List<UserProfileDto>>.Success(entities.Select(a => a.ToDto()).ToList());
    }

    public async Task<Result<UserProfileDto>> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity is null ? Result<UserProfileDto>.NotFound() : Result<UserProfileDto>.Success(entity.ToDto());
    }

    public async Task<Result<UserProfileDto>> AddAsync(UserProfileDto dto)
    {
        var validation = ValidateUserProfile(dto);
        if (!validation.IsSuccess)
            return Result<UserProfileDto>.Failure(validation.Errors);

        var entity = dto.ToEntity();
        var created = await _repo.AddAsync(entity);
        return Result<UserProfileDto>.Success(created.ToDto());
    }

    public async Task<Result<UserProfileDto>> UpdateAsync(int id, UserProfileDto dto)
    {
        var validation = ValidateUserProfile(dto);
        if (!validation.IsSuccess)
            return Result<UserProfileDto>.Failure(validation.Errors);

        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            return Result<UserProfileDto>.NotFound();

        entity.SetFieldsFrom(dto);
        await _repo.UpdateAsync(entity);
        return Result<UserProfileDto>.Success(entity.ToDto());
    }
    
    public async Task<Result<Unit>> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            return Result<Unit>.NotFound();

        await _repo.DeleteAsync(id);
        return Result<Unit>.Success(Unit.Value);
    }

    private static Result<Unit> ValidateUserProfile(UserProfileDto dto)
    {
        var errors = new List<ErrorDetail>();

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add(new ErrorDetail("NAME_REQUIRED", "Name is required.", nameof(dto.Name)));
        else if (dto.Name.Length > 100)
            errors.Add(new ErrorDetail("NAME_TOO_LONG", "Name must be 100 characters or fewer.", nameof(dto.Name)));
        
        if (dto.Email?.Length > 256)
            errors.Add(new ErrorDetail("EMAIL_TOO_LONG", "Email must be 256 characters or fewer.", nameof(dto.Email)));
        else if (!string.IsNullOrWhiteSpace(dto.Email) && !MailAddress.TryCreate(dto.Email, out _))
            errors.Add(new ErrorDetail("EMAIL_INVALID", "Email is not a valid email address.", nameof(dto.Email)));

        if (dto.HeightCm < 0)
            errors.Add(new ErrorDetail("HEIGHT_NEGATIVE", "Height must be greater than or equal to zero.", nameof(dto.HeightCm)));
        
        if (dto.WeightKg < 0)
            errors.Add(new ErrorDetail("WEIGHT_NEGATIVE", "Weight must be greater than or equal to zero.", nameof(dto.WeightKg)));
        
        return errors.Count == 0 ? Result.Success() : Result.Failure(errors);
    }
}