# QULOOBUL MOE'MIENEEN — Prototype (v1.0-dev)

**High-fidelity, fully interactive prototype.** There's still no real database behind it, but every screen shares one in-memory data layer, so actions on one page genuinely affect the others — this branch is the one to explore if you want to see the system actually *work*, not just look right.

## What this is

QULOOBUL MOE'MIENEEN is a Blazor Server prototype of an NPO operations tool — daily tasks, resource requests, meetings, event planning, user management, and live notifications, wrapped in a role-based sign-in flow. This `dev` branch takes the visual design shown on `master` and wires it up to a shared, in-memory data service, so the prototype behaves like a real multi-user application for demo purposes.

## What makes it interactive

- **Real sign-in against seeded accounts** — usernames and passwords are checked against a seeded user list (`AppDataService`), with one-click demo-account chips on the login screen so you can try each role without hunting for credentials.
- **One shared data layer, not per-page placeholders** — `AppDataService` is a Singleton, so every signed-in session sees and edits the same pool of tasks, events, requests, and meetings. Concretely:
  - Completing a task on **Home** updates it on **Daily Tasks**, and vice versa.
  - Creating an **Event** makes it selectable from the Requests page's "link to an event" dropdown and from the New Task dialog.
  - Approving or denying a **Resource Request** is reflected wherever that request appears.
- **Live notifications** — the bell icon in the nav bar tracks unread counts per signed-in user via `NotificationService` (a Singleton, routed by user/role) and updates instantly when something elsewhere in the app triggers a notification, no refresh needed.
- **Four staff roles**, each with a distinct navigation footprint:
  - *General Staff*: Home only
  - *Facility Manager*: General Staff's tabs, plus Daily Tasks and Resource Requests (view only — can't approve/deny)
  - *Secondary Admin*: everything except User Management
  - *Admin*: everything, including approve/deny on Resource Requests
- **Light/dark mode** and a horizontal/vertical navigation toggle, same as `master`.
- Additional interaction surfaces not present on `master`: a **Task Details dialog**, a full **Models** layer (`AppModels.cs`) backing every entity, and role-aware gating logic that lives centrally in `MainLayout` rather than being re-derived per page.

## What it still doesn't do

- No real database — everything lives in memory for the lifetime of the running app and resets the moment it restarts.
- No network/API layer — this is a single-process demo, not a client-server split.

## Tech stack

- **.NET 10** / Blazor Server (Interactive Server render mode)
- **MudBlazor 9.7** for the component library
- Bootstrap for base layout/reset styling
- `AppDataService` (Singleton) — shared in-memory store for users, tasks, events, requests, and meetings
- `NotificationService` (Singleton) — role/user-targeted live notification routing

## Running it

```bash
dotnet restore
dotnet run --project INSY7315_Prototype_v1.0
```

Open the app in your browser (the launch URL is printed in the console). On the sign-in screen, click any of the demo-account chips to auto-fill credentials for that role, then hit **Log In** — or explore Requests, Tasks, or Events across two roles side by side to see the shared data layer in action.

## Project structure

```
INSY7315_Prototype_v1.0/
├── AppDataService.cs      # Shared in-memory data layer (users, tasks, events, requests, meetings)
├── Models/
│   └── AppModels.cs       # Entity models backing AppDataService
├── Components/
│   ├── Layout/             # MainLayout (real login + shell), NavMenu, ReconnectModal
│   └── Pages/               # Home, Tasks, Meetings, Events, Requests, UserManagement,
│                             #   Notifications, plus dialogs (CreateTask, TaskDetails,
│                             #   CompletedBy, DenyRequest)
├── NewTaskResult.cs
├── Notificationservice.cs
├── Program.cs
└── wwwroot/                 # Static assets, logo, Bootstrap
```

## Relation to `master`

`master` is the visual-only prototype — same screens, hardcoded placeholder data per page, no persistence or cross-page effects. This `dev` branch is where those same screens were made to actually interact with each other through a shared data layer. If you only need to see what the app looks like, `master` is lighter to read through; if you want to see it behave like a system, stay here.

## Youtube Demonstration Video Link

https://youtu.be/iajl1yTe4pc

---
*Emeris — INSY7315*
