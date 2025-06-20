﻿using AutoMapper;
using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Application.Interface;
using MethodologyMain.Persistence.Interfaces;
using MethodologyMain.Infrastructure.Interfaces;

namespace MethodologyMain.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepo;
        private readonly ITagRepository tagRepo;
        private readonly IRedisService redisService;
        private readonly IMapper mapper;
        public UserService(IRedisService redisService, IUserRepository userRepo, ITagRepository tagRepo, IMapper mapper)
        {
            this.redisService = redisService;
            this.userRepo = userRepo;
            this.tagRepo = tagRepo;
            this.mapper = mapper;
        }

        // Создание нового пользователя
        public async Task<GetUserDto> CreateUserAsync(GetUserDto dto, CancellationToken token)
        {
            var user = new UserMainEntity 
            { 
                Id = Guid.NewGuid(),
                UserName = dto.Name,
                Email = dto.Email,
                Education = dto.Bio,
                PhotoUrl = dto.PhotoUrl,
                CreatedAt = DateTime.UtcNow,
                Location = dto.Location,
                Github = dto.Github,
                Website = dto.Website,
                Skills = dto.Skills
            };

            await userRepo.AddAsync(user, token);
            await tagRepo.AddUserTags(user.Id, dto.Tags, token);
            var key = $"user:{user.Id}";
            await redisService.SetStringToCacheAsync(key, dto);
            return mapper.Map<GetUserDto>(user);
        }
        public async Task DeleteUserAsync(Guid userId, CancellationToken token)
        {
            await userRepo.RemoveAsync(userId, token);
            var key = $"user:{userId}";
            await redisService.RemoveStringFromCacheAsync(key);
            // await validation.CheckTeamExistsAsync(teamId, token);
            // await validation.CheckUserIsCaptainOrAdminAsync(teamId, requestingUserId, isAdmin, token);
            // await userRepo.RemoveAsync(teamId, token);
        }
        public async Task<GetUserDto> UpdateUserAsync(GetUserDto dto, CancellationToken token)
        {
            var user = new UserMainEntity
            {
                Id = dto.Id,
                UserName = dto.Name,
                Email = dto.Email,
                Education = dto.Bio,
                PhotoUrl = dto.PhotoUrl,
                CreatedAt = DateTime.UtcNow,
                Location = dto.Location,
                Github = dto.Github,
                Website = dto.Website,
                Skills = dto.Skills
            };
            await tagRepo.UpdateUserTags(user.Id, dto.Tags, token);
            await userRepo.UpdateAsync(user, token);
            var key = $"user:{user.Id}";
            await redisService.RemoveStringFromCacheAsync(key);
            return mapper.Map<GetUserDto>(user);
            // await validation.CheckTeamExistsAsync(teamId, token);
            // await validation.CheckUserIsCaptainOrAdminAsync(teamId, requestingUserId, isAdmin, token);
            // await userRepo.RemoveAsync(teamId, token);
        }

        // Получение информации о пользователе по ID
        public async Task<GetUserDto> GetUserByIdAsync(Guid userId, CancellationToken token)
        {
            // await validation.CheckTeamExistsAsync(teamId, token);
            var key = $"user:{userId}";
            var redisData = await redisService.GetStringFromCacheAsync<GetUserDto>(key);
            if (redisData is not null) return redisData;

            var user = await userRepo.GetUserByIdAsync(userId, token);
            var dto = mapper.Map<GetUserDto>(user);
            await redisService.SetStringToCacheAsync(key, dto);
            return dto;
        }

        // Получение списка пользователей
        public async Task<List<GetUserDto>> GetUsersAllAsync(CancellationToken token)
        {
            var key = $"users";
            var redisData = await redisService.GetStringFromCacheAsync<List<GetUserDto>>(key);
            if (redisData is not null) return redisData;

            var users = await userRepo.GetUsersAllAsync(token);
            var dto = mapper.Map<List<GetUserDto>>(users);
            await redisService.SetStringToCacheAsync(key, dto);
            return dto;
        }
    }
}
