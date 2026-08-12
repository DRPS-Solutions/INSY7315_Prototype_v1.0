// NotificationService.cs
//
// Lightweight in-memory notification store for the prototype. No
// namespace is declared on purpose so this drops into any project
// without needing a matching @using — everything here lives in the
// global namespace, same as the rest of this prototype's code-behind
// classes.
//
// Register in Program.cs:
//     builder.Services.AddScoped<NotificationService>();
//
// Scoped (not Singleton) is intentional: in Blazor Server each
// circuit is one signed-in "session" in MainLayout's simple
// role-only login, so Scoped gives every logged-in user their own
// notification feed instead of one shared global list.

using System;
using System.Collections.Generic;
using System.Linq;

namespace INSY7315_Prototype_v1._0
{

    public enum NotificationType
    {
        ResourceRequest,
        EventAssignment
    }

    public class NotificationItem
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public NotificationType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;

        // Optional name of the event/request this notification refers to -
        // shown as a small chip and used to decide which page "View" opens.
        public string? LinkedName { get; set; }
    }

    public class NotificationService
    {
        // Components (NavMenu's bell badge, the Notifications page) subscribe
        // to this and call StateHasChanged so the UI reacts immediately to
        // Add/MarkAsRead/etc. without any polling.
        public event Action? OnChange;

        private readonly List<NotificationItem> _notifications = new();

        public IReadOnlyList<NotificationItem> Notifications =>
            _notifications.OrderByDescending(n => n.Timestamp).ToList();

        public int UnreadCount => _notifications.Count(n => !n.IsRead);

        public NotificationService()
        {
            SeedDemoData();
        }

        public void Add(NotificationType type, string title, string message, string? linkedName = null)
        {
            _notifications.Add(new NotificationItem
            {
                Type = type,
                Title = title,
                Message = message,
                LinkedName = linkedName
            });
            NotifyStateChanged();
        }

        public void MarkAsRead(NotificationItem item)
        {
            if (item.IsRead) return;
            item.IsRead = true;
            NotifyStateChanged();
        }

        public void MarkAllAsRead()
        {
            if (_notifications.All(n => n.IsRead)) return;
            foreach (var n in _notifications) n.IsRead = true;
            NotifyStateChanged();
        }

        public void Remove(NotificationItem item)
        {
            _notifications.Remove(item);
            NotifyStateChanged();
        }

        public void ClearAll()
        {
            if (_notifications.Count == 0) return;
            _notifications.Clear();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        // A few seeded notifications so the bell/page aren't empty on
        // first load - mirrors the placeholder seed data already used in
        // Requests.razor / Events.razor.
        private void SeedDemoData()
        {
            _notifications.Add(new NotificationItem
            {
                Type = NotificationType.EventAssignment,
                Title = "Added to Annual Donor Gala",
                Message = "You've been added to the staff list for \"Annual Donor Gala\".",
                LinkedName = "Annual Donor Gala",
                Timestamp = DateTime.Now.AddHours(-3)
            });
            _notifications.Add(new NotificationItem
            {
                Type = NotificationType.ResourceRequest,
                Title = "New resource request",
                Message = "Aisha Patel requested a projector for \"Annual Donor Gala\".",
                LinkedName = "Annual Donor Gala",
                Timestamp = DateTime.Now.AddDays(-2)
            });
            _notifications.Add(new NotificationItem
            {
                Type = NotificationType.ResourceRequest,
                Title = "Request approved",
                Message = "Naledi Dube approved your \"Printing budget\" request.",
                LinkedName = "Community Workshop",
                Timestamp = DateTime.Now.AddDays(-4),
                IsRead = true
            });
        }
    }
}