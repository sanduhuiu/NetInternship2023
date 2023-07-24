using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.DTO.ProjectDTO;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        public Task AddAsync(ProjectAddRequest model);
        public Task<ProjectResponse> GetByIdAsync(string id);
        public Task<ProjectResponse> UpdateAsync(ProjectUpdateRequest model);
    }
}
