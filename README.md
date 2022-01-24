# ASP.NET Core BugTracker

# Trello Plan
https://trello.com/b/TYidZAk1/bugtracker

# Project Description

### Registration
When you register you need to select **register as a user** or **register as a CEO of a company**.
If you choose: 

**User** - you will have to choose which company you will work for. We have a field with all registered companies in our platform from which you will have to choose your company. After completing the registration you will need to wait for your approval from the company. We do this to protect against fake users
(Imagine registering as a user for a company and the company has not hired you at all. This allows the CEO of the company to approve those users only he knows about.)

**Register as a CEO of a company** - you get the administration role of CEO. You will have all permission over your company and projects.

### Roles that we have!
1. Build Administrators or CEO(chief executive officer) - Role for the person that registers the company. 
2. Manager - Role for the person who is assigned to be the head of the project. That role can be assigned only by the CEO of the company.
3. Contributors - people who are allowed to contribute fully to the project code.
4. Readers - people who are allowed only to view project information, work items, and others but not modify them.
5. Can create your own roles for a particular project and decide what permissions to give them. Can be created only by the CEO.
Organization level permissions (Options): 
General:
Service:
Notes:
Tasks:

### Organization level permissions:

**General:**
1. Create a new project - Only the CEO can create a new project.
Every project can have one Manager of the project.
2. Add members to the project- Can be done only by the CEO or Manager
3. Create a new team for the project - Only the CEO and Manager can create a new team for the project.  Can be Core Team, Bootcamp Interns.
Every team will have separated tasks, its own channels for communication, and its own notes.
4. Change project settings - only the CEO and Manager can change the project settings.
5. Delete project  - Only the CEO that registers the company has that permission.
6. Delete team - Only the CEO and Manager have that permission.
7. Edit - Can add users and groups and edit organization-level permissions for users and groups. Can be done by the Manager of the project.
8. View Control - users assigned to а specific team will have a reader role over other team's view. Except on the communication part. Task comments, discussion comments, and conversation.
Only the CEO and Manager can edit information in all teams.

**Service:**
1. Trigger events - can be created by everyone in the specific project including the CEO and Manager. Can be used about meetings, messages that everybody must-see, and others. Modal with the message stay till the user closed it.

**Project Overview:**
1. Every project has a readme.md and can be edited only by the CEO and Manager. 

**Notes:**
1. Create note - can be created by everyone except the reader.
Created Note will be automatically removed after one week. 
CEO and Manager have administration permission to see all notes!

**Tasks(work items):** 
1. Create new tasks - can be created by everyone who is assigned to this particular project except the reader.
CEO and Manager have permission to create a task and decide to assign it to a specific team.
Users in particular teams can only create tasks for their teams.
2. Task types - Task, Bug, Feature, if you don't select type, the default will be New.
3. Task Assign - can be assigned to the user who needs to complete it.
Who can assign a task - everyone except the reader.
4. Task status - default new, can be changed by everyone except by the reader.
5. Task planning, effort(hours) - can be changed only by the CEO, Manager, or the creator of the task.
