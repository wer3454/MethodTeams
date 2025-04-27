using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;
using Microsoft.EntityFrameworkCore;

namespace MethodologyMain.Persistence.Repository
{
    public class TagRepository : GenericRepository<TagEntity>, ITagRepository
    {
        private readonly ITeamRepository teamRepo;
        public TagRepository(MyDbContext context, ITeamRepository teamRepo) : base(context)
        {
            this.teamRepo = teamRepo;
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
                    tagUserRelations.Add(new TeamTagEntity
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
    }
}
