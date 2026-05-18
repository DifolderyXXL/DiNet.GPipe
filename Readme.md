# DiNet.GPipe

**Local CI/CD automation service triggered by Git repository changes.**

Built on **.NET 10** with **.NET Aspire**, **Clean Architecture**, **CQRS** and an **Event-Driven** design.

## ✅ Implemented

- **Local commit observation** – background workers poll for new commits and trigger actions.
- **Blazor Dashboard** – manage projects, monitor watchers and build statuses.
- **Web API** – Minimal API for integration with the dashboard and external tools.
- **Isolated workspace** - Isolation on local folder level.
- **Builder providers** 
    - **APK** – Gradle-based Android compilation.


## 🚧 In Progress

- **PowerShell pipeline strategies** – execute custom PowerShell scripts as integration/delivery steps after commit detection.
- **Enhanced versioning strategies** – support for additional version number schemes.
- **Transactional watcher reconfiguration** – prevent race conditions when updating branches or intervals.
- **Integration tests** – coverage for API → handler → event bus → worker flows.
- **Isolated workspace** - Isolation on Docker / remote level.


## 📝 License
This project is licensed under the [MIT License](LICENSE).
