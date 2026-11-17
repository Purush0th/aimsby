# Rename plan: PersonaOS → Aimsby

_Written 2026-11-16. Owner decision: "Aimsby it is". Phases 1-3 done in code on `rename/aimsby` (2026-11-16). Status lives in TODO.md._

## Why Aimsby, and what was checked
"PersonaOS" is taken. Aimsby (aims + "-by", like a small English place name) was checked on
2026-11-15/16:
- **Clear:** USPTO (0 records, sound-alikes included), TMview (no rows), Apple App Store (US, UK and
  India), GitHub, npm, PyPI and Docker Hub. The .com, .ai, .io, .app and .dev domains were free.
  Instagram @aimsby is a private person, so no conflict.
- **Google Play (partial, 2026-11-16):** a web search limited to play.google.com shows no Aimsby
  (nearest: AIMS, Aimsio, AIMSIS, AimyMe). play.google.com itself doesn't resolve from this machine,
  so the owner confirms with a search in the Play Store app.
- **Not checked from this machine:** India's trademark register (classes 9 and 42; the search has a
  CAPTCHA). The owner checks it before Phase 1.
Full research: the rename-research memory and `docs/decisions.md`.

## Spellings
| Use | Form |
|---|---|
| Brand, UI text, docs, .NET namespaces, projects | `Aimsby` |
| Lowercase ids, images, repos, files, keys | `aimsby` |
| Env vars | `AIMSBY_*` |
| Flutter package | `aimsby_mobile` |

The wordmark (and whether it uses lowercase, like the earlier "personaOS") is the owner's call. The
"Ripples" mark can stay as it is. Brand changes need sign-off.

## Scale
In 961 tracked files, 500 contain the name: about 2,750 hits, and 722 paths include it. Most of the
path hits are the project folders: `src/backend/PersonaOS.{Api,Application,Domain,Infrastructure,Tests}`,
`src/frontend/PersonaOS.{Web,Mobile}` and `PersonaOS.slnx`.

## Never change (data would be lost)
Leave these exactly as they are. Add a comment beside each one saying it is a legacy id, kept on
purpose:
- Data Protection purpose strings `PersonaOS.AnthropicApiKey.v1`, `PersonaOS.FcmServiceAccount.v1` and
  `PersonaOS.SpeechApiKey.v1`. They live in `Infrastructure/Security/DataProtectionSecretProtector.cs`
  and `Application/Common/Interfaces/IPushProviderConfigurator.cs`.
- `SetApplicationName("PersonaOS")` in `Api/Program.cs`. Changing it stops stored keys from decrypting.
- Compose project `name: personaos` (`deploy/docker-compose.yml`) and the updater default
  `COMPOSE_PROJECT_NAME="personaos"`. These names prefix the data volumes (`personaos_api-data`, …),
  so a rename orphans live data.
- The database file `data/personaos.db` (and its litestream path). A rename only buys a cosmetic
  change, and costs a move-on-start migration plus backup and restore paths. Revisit it at the
  public release if it matters.

## Phase 0: owner actions (before any code)
1. Check Google Play and India's trademark register. Register aimsby.com (plus any others wanted),
   the GitHub org/name and the social handles.
2. Rename the repo `Purush0th/personaOS` → `Purush0th/aimsby`. GitHub redirects git and API
   traffic, so old clients keep working.
3. Approve the wordmark and store name.
4. Decide the Android application id (see Phase 3). If it changes, add the new Android app to the
   Firebase project and download a fresh `google-services.json`.
5. Create the Hugging Face repos `purush0th/aimsby-qwen3-4b-instruct-GGUF` and
   `aimsby-qwen2.5-3b-GGUF`. Copy the files and keep the old repos.

## Phase 1: what users see
- **Web:**
  - Title and `apple-mobile-web-app-title` in `index.html`.
  - `name` and `short_name` in `manifest.webmanifest`.
  - Any wordmark text in components.
- **Phone:**
  - App titles and the default nickname in `main.dart` (lines ~249, 361, 450, 798).
  - Notification channel text: `push_service.dart`, `reminder_alarms.dart`.
  - `android:label` in the AndroidManifest, and the iOS `Info.plist` display names.
- **Prompts:**
  - `Ai/Prompts/Fragments/product.prompty` ("… is the fixed product name"), `product.compact.prompty`
    and `tool-rules.prompty` (project URL).
  - Update `SystemPromptBuilderTests` (it asserts the URLs).
  - This is AI-accuracy work: run `scripts/model-check.ps1 -Runs 5` before and after.
- **Docs:**
  - README, `docs/*`, `training/README.md`, `deploy/.env.example`, the updater docstrings.
  - The CLAUDE.md fixed-brand rule becomes `Brand "Aimsby" is fixed everywhere`, and its commands
    and paths are updated.
- **Icons:** labels in `scripts/make-icons.mjs` and `mark.svg`, then regenerate the icons.

## Phase 2: code identifiers (mechanical, one big diff)
- `git mv` the five backend projects, the web and mobile folders, and `PersonaOS.slnx` →
  `Aimsby.*`. Update the project references, the `.slnx`, the Dockerfiles (`ENTRYPOINT ["dotnet",
  "Aimsby.Api.dll"]` and the COPY paths), the workflow paths and the `angular.json` project name.
- Change the namespaces from `PersonaOS.*` to `Aimsby.*`, but skip the Never-change strings above.
  - Embedded prompt resources follow the namespace, so `PromptLibrary.ResourcePrefix` becomes
    `"Aimsby.Prompts."`.
  - Rename the domain type `PersonaOsProject`.
- **EF migrations:** the Designer and snapshot files hold `"PersonaOS.Domain.Entities.X"` strings.
  Replace them in place, then confirm `dotnet ef migrations has-pending-model-changes` is clean.
  Don't regenerate the migrations: there is no schema change.
- **Telemetry:** meter `Aimsby.Chat`, the counter `aimsby.chat.guard.fired`, and the logger
  `Aimsby.Persistence`. Note the rename in the release notes for anyone who scrapes the metrics.
- **Flutter:**
  - Package `personaos_mobile` → `aimsby_mobile` (pubspec, plus the 43 `package:` imports).
  - `lib/api/personaos_api.dart` → `aimsby_api.dart`.
  - The method channels `aimsby/installer` and `aimsby/hands_free` must change in Dart and Kotlin
    together.
  - The sound resource `aimsby_chime`. The notification channel uses it, and Android caches a
    channel's sound, so give the channel a new id.
- Do the edits with Edit or a `newline=''` script. CRLF files were corrupted by text-mode writes
  once.

## Phase 3: names with live users (old names must keep working)
> Superseded 2026-11-17: there are no installs besides the owner's prod, which is redeployed fresh,
> so the env, JWT, connection-string, image, phone-package, repo-name and web-storage fallbacks
> below were removed. The data-bound ids kept below still stand (see docs/decisions.md).

| Item | Plan |
|---|---|
| Env vars `PERSONAOS_VERSION/USER/PASSWORD/DIR/BIND/PORT` | Read `AIMSBY_*` first and fall back to `PERSONAOS_*`; the updater regex (`updater.py` ~104-113) rewrites whichever key the `.env` has |
| JWT issuer/audience `PersonaOS` | Issue `Aimsby`; accept both via `ValidIssuers`/`ValidAudiences` for a few releases so sign-ins survive |
| Web localStorage `personaos.token`, `personaos.theme` | Read the new key, else copy from the old key once, then delete the old one |
| GHCR `personaos-{api,web,whisper,updater}` | `release.yml` pushes both names for a few releases. The compose file switches to `aimsby-*` only after an updater that understands both is out. **First check how prod's updater gets its compose file.** |
| APK asset `personaos-app-v*.apk` | Publish `aimsby-app-v*.apk`; `AppUpdater.isReleaseApk` accepts both |
| Repo constants (`PersonaOsProject.Repository`, `GitHubReleaseFeed`, `update_check.dart`, `model-check.ps1` regex, `release-notes.sh`) | Point them at the new repo after Phase 0 step 2 |
| Local model tags `personaos-qwen3:4b`, `personaos-qwen2.5:3b` | New tags are `aimsby-*`; `LocalModelService`/`ModelProfile` treat an installed old tag as the same model (no re-download) |
| Deep-link scheme `personaos://` | Register `aimsby` and keep `personaos` as a second intent filter; links in existing chats still open |
| Android `applicationId`/`namespace` `com.personaos.personaos_mobile` | **Owner decision.** Changing it makes a new app: the old one gets no in-place update, Firebase needs a new Android app, and `FirebaseConfigParser.AndroidPackageName` must change. Recommendation: change it **now**, before the public release, while the owner's phone is the only install (one reinstall). After the public release it is effectively frozen. |

## Order and partitions
The rename crosses every area, so it runs solo on its own branch (`rename/aimsby` off `dev`, in a
worktree). Each phase is one commit, in this order:
1. **Phase 3 compatibility code first,** while the names are still old. Release this as a normal
   alpha, so prod's updater learns both names.
2. **Phase 2** (moves and namespaces).
3. **Phase 1** (visible text and prompts).
4. **Release:** server and mobile together. The updater swaps the images.
5. **Cleanup,** after 2–3 releases: stop pushing the old image names, and drop the old env and JWT
   fallbacks. The Never-change items stay.

## Verification (each phase)
- `dotnet build Aimsby.slnx` with 0 warnings, and `dotnet test` (saved to `output/logs/`).
- Web: `npx ng test --watch=false --browsers=ChromeHeadless`, plus a look at the title, manifest
  and sign-in survival (old token key).
- Phone: `flutter analyze && flutter test`, and on the emulator, hands-free, the installer channel,
  notification sound and deep links.
- `model-check.ps1 -Runs 5` on the prompt changes.
- **Upgrade test:** install the last release with data, a stored API key and a signed-in session,
  then update to the rename build. Keys must still decrypt, the volumes and DB must be intact,
  sign-in must survive, and the updater must still find the next release.
- A final `git grep -i personaos` should show only the Never-change list, the compatibility
  fallbacks and history (changelog, decisions).
