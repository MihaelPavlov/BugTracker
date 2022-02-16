﻿namespace BugTracker.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Enums;
    using BugTracker.Data.Models;
    using BugTracker.Data.Utilities;
    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Web.ViewModels;
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

        public async Task<OperationResult<ICollection<EmployeeViewModel>>> GetAllEmployeeByProjectId(string projectId)
        {
            var operationResult = new OperationResult<ICollection<EmployeeViewModel>>();
            try
            {
                operationResult.RelatedObject = await this.projectEmployeeRepository.All()
                    .Where(x => x.ProjectId == projectId)
                    .Include("Employee.User")
                    .Select(x => new EmployeeViewModel
                    {
                        UserEmail = x.Employee.User.UserName,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        public async Task<OperationResult<ICollection<WorkItemViewModel>>> GetAllWorkItemsForProjectByProjectId(string projectId)
        {
            var operationResult = new OperationResult<ICollection<WorkItemViewModel>>();
            try
            {
                operationResult.RelatedObject = await this.workItemRepository.All()
                    .Where(x => x.ProjectId == projectId)
                    .Select(x => new WorkItemViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        public async Task<OperationResult> CreateWorkItem(string projectId, string name, string createByEmployeeId, WorkItemType type, WorkItemStatus status = WorkItemStatus.New)
        {
            var operationResult = new OperationResult();

            try
            {
                var newWorkItem = new WorkItem()
                {
                    Name = name,
                    CreateByEmployeeId = createByEmployeeId,
                    ProjectId = projectId,
                    Status = status,
                    Type = type,
                };

                await this.workItemRepository.AddAsync(newWorkItem);
                await this.workItemRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }
    }
}
