using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;
using Microsoft.EntityFrameworkCore;

namespace MethodologyMain.Persistence.Repository
{
    public class TagRepository : GenericRepository<TagEntity>, ITagRepository
    {
        private readonly ITeamRepository teamRepo;
        private readonly IUserRepository userRepo;
        private readonly IHackathonRepository hackRepo;
        public TagRepository(
            MyDbContext context, 
            ITeamRepository teamRepo, 
            IUserRepository userRepo,
            IHackathonRepository hackRepo) : base(context)
        {
            this.teamRepo = teamRepo;
            this.userRepo = userRepo;
            this.hackRepo = hackRepo;
        }

        public async Task AddTeamTags(Guid teamId, List<string> tagNames, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var team = await teamRepo.GetByIdAsync(teamId, token);
            var existTags = await _context.Tags
                .Where(m => m.TagClassName == "team")
                .Where(m => tagNames.Contains(m.TagName))
                .ToDictionaryAsync(m => m.TagName, t => t, cancellationToken: token);
            var newTagsToAdd = new List<TagEntity>();
            var tagUserRelations = new List<TeamTagEntity>();

            foreach (var tagName in tagNames)
            {
                // Если тег уже существует, используем его
                if (existTags.TryGetValue(tagName, out var tag))
                {
                    // Проверяем, что связь еще не существует
                    if (!team.Tags.Any(t => t.TagId == tag.Id))
                    {
                        team.Tags.Add(new TeamTagEntity
                        {
                            TeamId = teamId,
                            TagId = tag.Id
                        });
                    }
                }
                else
                {
                    // Создаем новый тег
                    var newTag = new TagEntity
                    {
                        TagName = tagName,
                        TagClassName = "team"
                    };
                    newTagsToAdd.Add(newTag);

                    // Добавляем связь с новым тегом
                    // (ID тега будет установлен после сохранения)
                    team.Tags.Add(new TeamTagEntity
                    {
                        TeamId = teamId,
                        Tag = newTag
                    });
                }

            }
            // Добавляем новые теги в контекст
            if (newTagsToAdd.Count != 0)
            {
                await _context.Tags.AddRangeAsync(newTagsToAdd, token);
            }

            // Сохраняем изменения
            await _context.SaveChangesAsync(token);
        }

        public async Task AddUserTags(Guid userId, List<string> tagNames, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var user = await userRepo.GetByIdAsync(userId, token);
            var existTags = await _context.Tags
                .Where(m => m.TagClassName == "user")
                .Where(m => tagNames.Contains(m.TagName))
                .ToDictionaryAsync(m => m.TagName, t => t, cancellationToken: token);
            var newTagsToAdd = new List<TagEntity>();

            foreach (var tagName in tagNames)
            {
                // Если тег уже существует, используем его
                if (existTags.TryGetValue(tagName, out var tag))
                {
                    // Проверяем, что связь еще не существует
                    if (!user.Tags.Any(t => t.TagId == tag.Id))
                    {
                        user.Tags.Add(new UserTagEntity
                        {
                            UserId = userId,
                            TagId = tag.Id
                        });
                    }
                }
                else
                {
                    // Создаем новый тег
                    var newTag = new TagEntity
                    {
                        TagName = tagName,
                        TagClassName = "user"
                    };
                    newTagsToAdd.Add(newTag);

                    // Добавляем связь с новым тегом
                    // (ID тега будет установлен после сохранения)
                    user.Tags.Add(new UserTagEntity
                    {
                        UserId = userId,
                        Tag = newTag
                    });
                }

            }
            // Добавляем новые теги в контекст
            if (newTagsToAdd.Count != 0)
            {
                await _context.Tags.AddRangeAsync(newTagsToAdd, token);
            }

            // Сохраняем изменения
            await _context.SaveChangesAsync(token);
        }

        public async Task AddHackTags(Guid hackId, List<string> tagNames, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var hack = await hackRepo.GetByIdAsync(hackId, token);
            var existTags = await _context.Tags
                .Where(m => m.TagClassName == "hack")
                .Where(m => tagNames.Contains(m.TagName))
                .ToDictionaryAsync(m => m.TagName, t => t, cancellationToken: token);
            var newTagsToAdd = new List<TagEntity>();

            foreach (var tagName in tagNames)
            {
                // Если тег уже существует, используем его
                if (existTags.TryGetValue(tagName, out var tag))
                {
                    // Проверяем, что связь еще не существует
                    if (!hack.Tags.Any(t => t.TagId == tag.Id))
                    {
                        hack.Tags.Add(new HackathonTagEntity
                        {
                            HackathonId = hackId,
                            TagId = tag.Id
                        });
                    }
                }
                else
                {
                    // Создаем новый тег
                    var newTag = new TagEntity
                    {
                        TagName = tagName,
                        TagClassName = "hack"
                    };
                    newTagsToAdd.Add(newTag);

                    // Добавляем связь с новым тегом
                    // (ID тега будет установлен после сохранения)
                    hack.Tags.Add(new HackathonTagEntity
                    {
                        HackathonId = hackId,
                        TagId = tag.Id
                    });
                }

            }
            // Добавляем новые теги в контекст
            if (newTagsToAdd.Count != 0)
            {
                await _context.Tags.AddRangeAsync(newTagsToAdd, token);
            }

            // Сохраняем изменения
            await _context.SaveChangesAsync(token);
        }
    }
}
