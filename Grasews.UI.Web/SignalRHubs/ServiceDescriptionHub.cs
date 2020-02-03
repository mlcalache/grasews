using Microsoft.AspNet.SignalR;

namespace Grasews.Web.SignalRHubs
{
    public class ServiceDescriptionHub : Hub
    {
        public void NotifyServiceDescriptionUpdate(int idServiceDescription)
        {
            Clients.Others.broadcastNotificationServiceDescriptionUpdate(idServiceDescription, "The service description has just been updated.");
        }

        public void NotifyTaskCreated(int idServiceDescription)
        {
            Clients.Others.broadcastNotificationTaskCreated(idServiceDescription, "A new task has just been created.");
        }

        public void NotifyTaskCommentCreated(int idServiceDescription)
        {
            Clients.Others.broadcastNotificationTaskCommentCreated(idServiceDescription, "A new comment in a task has just been created.");
        }

        public void NotifyIssueCreated(int idServiceDescription)
        {
            Clients.Others.broadcastNotificationIssueCreated(idServiceDescription, "A new issue has just been created.");
        }

        public void NotifyIssueAnswerCreated(int idServiceDescription)
        {
            Clients.Others.broadcastNotificationIssueAnswerCreated(idServiceDescription, "A new answer in an issue has just been created.");
        }
    }
}