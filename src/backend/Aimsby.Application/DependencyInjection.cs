using Microsoft.Extensions.DependencyInjection;
using Aimsby.Application.Ai;
using Aimsby.Application.Ai.Guards;
using Aimsby.Application.Ai.Prompts;
using Aimsby.Application.Ai.Tools;
using Aimsby.Application.Auth;
using Aimsby.Application.Board;
using Aimsby.Application.Board.Tools;
using Aimsby.Application.Configuration;
using Aimsby.Application.Goals;
using Aimsby.Application.Goals.Tools;
using Aimsby.Application.Planner;
using Aimsby.Application.Proactive;
using Aimsby.Application.Planner.Tools;
using Aimsby.Application.Reminders;
using Aimsby.Application.Reminders.Tools;

namespace Aimsby.Application;

public static class DependencyInjection
{
    /// <summary>Registers all Application-layer use cases. Ports are bound in Infrastructure.</summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IInstanceConfigService, InstanceConfigService>();
        services.AddScoped<Aimsby.Application.LocalModels.ILocalModelService, Aimsby.Application.LocalModels.LocalModelService>();
        services.AddSingleton<IPromptLibrary, PromptLibrary>();
        services.AddScoped<ISystemPromptBuilder, SystemPromptBuilder>();
        services.AddScoped<ToolCallPipeline>();
        services.AddScoped<ReplyPipeline>();
        services.AddSingleton<Aimsby.Application.Ai.History.ConversationSummarizer>();
        services.AddSingleton<Aimsby.Application.Ai.Intents.IntentClassifier>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<Aimsby.Application.Updates.IUpdateService, Aimsby.Application.Updates.UpdateService>();
        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<Aimsby.Application.WorkItems.IWorkItemService, Aimsby.Application.WorkItems.WorkItemService>();
        services.AddScoped<IPlannerService, PlannerService>();
        services.AddScoped<IReminderService, ReminderService>();
        services.AddScoped<IReminderAlarmPublisher, ReminderAlarmPublisher>();
        services.AddScoped<Aimsby.Application.Profile.IUserProfileService, Aimsby.Application.Profile.UserProfileService>();
        services.AddScoped<Aimsby.Application.Push.IPushConfigService, Aimsby.Application.Push.PushConfigService>();
        services.AddScoped<Aimsby.Application.Push.IPushTestService, Aimsby.Application.Push.PushTestService>();
        services.AddScoped<IReminderDispatcher, ReminderDispatcher>();
        services.AddScoped<IPlannerAlertDispatcher, PlannerAlertDispatcher>();
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IProactiveBriefComposer, ProactiveBriefComposer>();
        services.AddScoped<IBriefPhraser, BriefPhraser>();
        services.AddScoped<IProactiveService, ProactiveService>();

        services.AddScoped<IChatContext, ChatContext>();
        services.AddScoped<Aimsby.Application.Speech.ISpeechService, Aimsby.Application.Speech.SpeechService>();
        services.AddScoped<Aimsby.Application.Memories.IMemoryService, Aimsby.Application.Memories.MemoryService>();
        services.AddScoped<Aimsby.Application.Notes.INoteService, Aimsby.Application.Notes.NoteService>();

        services.AddScoped<IPersonaToolRegistry, PersonaToolRegistry>();
        services.AddScoped<IPersonaTool, GetGoalsTool>();
        services.AddScoped<IPersonaTool, CreateGoalTool>();
        services.AddScoped<IPersonaTool, CreatePlanTool>();
        services.AddScoped<IPersonaTool, UpdateGoalStatusTool>();
        services.AddScoped<IPersonaTool, MoveGoalTool>();
        services.AddScoped<IPersonaTool, DeleteGoalTool>();
        services.AddScoped<IPersonaTool, GetBoardTool>();
        services.AddScoped<IPersonaTool, GetPlanTool>();
        services.AddScoped<IPersonaTool, GetTaskTool>();
        services.AddScoped<IPersonaTool, CreateSprintTool>();
        services.AddScoped<IPersonaTool, StartSprintTool>();
        services.AddScoped<IPersonaTool, CompleteSprintTool>();
        services.AddScoped<IPersonaTool, AddCommentTool>();
        services.AddScoped<IPersonaTool, GetSprintReportTool>();
        services.AddScoped<IPersonaTool, CreateTaskTool>();
        services.AddScoped<IPersonaTool, UpdateTaskTool>();
        services.AddScoped<IPersonaTool, MoveTaskTool>();
        services.AddScoped<IPersonaTool, DeleteTaskTool>();
        services.AddScoped<IPersonaTool, GetPlannerTool>();
        services.AddScoped<IPersonaTool, AddPlannerItemTool>();
        services.AddScoped<IPersonaTool, UpdatePlannerItemStatusTool>();
        services.AddScoped<IPersonaTool, MovePlannerItemTool>();
        services.AddScoped<IPersonaTool, GetRemindersTool>();
        services.AddScoped<IPersonaTool, CreateReminderTool>();
        services.AddScoped<IPersonaTool, CancelReminderTool>();
        services.AddScoped<IPersonaTool, Aimsby.Application.Profile.RememberAboutUserTool>();
        services.AddScoped<IPersonaTool, Aimsby.Application.Memories.Tools.SearchMemoriesTool>();
        services.AddScoped<IPersonaTool, Aimsby.Application.Memories.Tools.CreateMemoryTool>();
        services.AddScoped<IPersonaTool, Aimsby.Application.Memories.Tools.UpdateMemoryTool>();
        services.AddScoped<IPersonaTool, Aimsby.Application.Memories.Tools.DeleteMemoryTool>();
        services.AddScoped<IPersonaTool, Aimsby.Application.Notes.Tools.GetNotesTool>();
        services.AddScoped<IPersonaTool, Aimsby.Application.Notes.Tools.CreateNoteTool>();
        services.AddScoped<IPersonaTool, Aimsby.Application.Notes.Tools.MoveNoteToBacklogTool>();

        return services;
    }
}
