namespace BugTracker.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Enums;
    using BugTracker.Data.Models;
    using BugTracker.Data.Utilities;
    using BugTracker.Services.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ProjectOptionsService : IProjectOptionsService
    {
        private readonly IDeletableEntityRepository<WorkItem> workItemRepository;
        private readonly IDeletableEntityRepository<Note> noteRepository;
        private readonly IDeletableEntityRepository<ProjectEmployee> projectEmployeeRepository;

        public ProjectOptionsService(
            IDeletableEntityRepository<WorkItem> workItemRepository,
            IDeletableEntityRepository<Note> noteRepository,
            IDeletableEntityRepository<ProjectEmployee> projectEmployeeRepository)
        {
            this.workItemRepository = workItemRepository;
            this.noteRepository = noteRepository;
            this.projectEmployeeRepository = projectEmployeeRepository;
        }

        /// <inheritdoc />
        public async Task<OperationResult<int>> GetWorkItemsCountByProjectId(string projectId)
        {
            var operationResult = new OperationResult<int>();
            try
            {
                operationResult.RelatedObject = await this.workItemRepository.All().CountAsync(x => x.ProjectId == projectId);
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        /// <inheritdoc />
        public async Task<OperationResult<int>> GetCompletedWorkItemsCountByProjectId(string projectId)
        {
            var operationResult = new OperationResult<int>();
            try
            {
                operationResult.RelatedObject = await this.workItemRepository.All().CountAsync(x => x.ProjectId == projectId && x.Status == WorkItemStatus.Closed);
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        /// <inheritdoc />
        public async Task<OperationResult<int>> GetCreatedNoteItemsCountByProjectId(string projectId)
        {
            var operationResult = new OperationResult<int>();
            try
            {
                operationResult.RelatedObject = await this.noteRepository.All().CountAsync(x => x.ProjectId == projectId);
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        /// <inheritdoc />
        public async Task<OperationResult<int>> GetProjectMembersCountByProjectId(string projectId)
        {
            var operationResult = new OperationResult<int>();
            try
            {
                operationResult.RelatedObject = await this.projectEmployeeRepository.All().CountAsync(x => x.ProjectId == projectId);
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }
    }
}
