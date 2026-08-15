# QULOOBUL MOE'MIENEEN — Prototype (v1.0)

> **This is a visual prototype.** Every screen is built and navigable, but the data behind it is fixed placeholder content — nothing you do here is saved or shared between pages. **For a more interactive prototype, switch to the `dev` branch**, where the same screens run on shared, editable data.

## What this is

QULOOBUL MOE'MIENEEN is a Blazor Server prototype of an NPO operations tool — daily tasks, resource requests, meetings, event planning, and user management, wrapped in a role-based sign-in flow. This `master` branch is the **visual reference build**: it exists to show what the finished application looks and feels like, screen by screen, without the overhead of a working backend.

## What it demonstrates

- **Role-based sign-in** — a name/role picker (no password) that drops the signed-in user straight into the shell, with the navigation menu adjusting to what that role can see.
- **Four staff roles** — Basic Staff, Secondary Staff, Management Staff, and Admin — each surfacing a different slice of the navigation:
  - *Basic Staff*: Home, Daily Tasks, Resource Requests
  - *Secondary Staff*: the above, plus User Management
  - *Management Staff*: everything except User Management
  - *Admin*: every tab
- **The full page set**: Home dashboard, Daily Tasks, Resource Requests, Meetings, Event Planning, User Management, and Notifications.
- **Light/dark mode** and a horizontal/vertical navigation toggle.
- **MudBlazor-driven UI** — cards, dialogs, tables, and menus styled to match the intended production look.

## What it does *not* do

This branch is intentionally placeholder-only:

- No database, no persistence — nothing you create, edit, or complete survives a page refresh.
- Each page holds its **own hardcoded sample list** (tasks, requests, meetings, etc.), so actions on one page don't affect another — completing a task on Home, for instance, won't update the Daily Tasks page.
- Sign-in accepts any name and doesn't check a password — it's a role switcher, not real authentication.

If you want to see the app actually behave like a system — shared data, live notifications, cross-page effects — that's what the `dev` branch prototype is for.

## Tech stack

- **.NET 10** / Blazor Server (Interactive Server render mode)
- **MudBlazor 9.7** for the component library
- Bootstrap for base layout/reset styling

## Running it

```bash
dotnet restore
dotnet run --project INSY7315_Prototype_v1.0
```

Then open the app in your browser (the launch URL is printed in the console) and sign in with any name — pick any role from the dropdown to preview that role's navigation.

## Project structure

```
INSY7315_Prototype_v1.0/
├── Components/
│   ├── Layout/          # MainLayout (sign-in + shell), NavMenu, ReconnectModal
│   └── Pages/            # Home, Tasks, Meetings, Events, Requests, UserManagement, Notifications
├── NewTaskResult.cs
├── Notificationservice.cs
├── Program.cs
└── wwwroot/               # Static assets, logo, Bootstrap
```

---
*IIE Varsity College — INSY7315*
