# Decisions
Append-only, one line each. Older decisions with their full reasoning are in
[docs/PRD.md](PRD.md) ("Key decisions", "Product principles") and in [TODO.md](../TODO.md).

- 2026-07-19 - Clean architecture: Api -> Application <- Infrastructure; Application -> Domain - business logic never touches vendor SDKs (PRD §4)
- 2026-07-19 - Embedded SQLite (WAL) via EF Core, migrations auto-apply on startup - single-user self-hosted, no DB server to run (PRD §4)
- (unverified date) - Provider-neutral AI through `IAiMessageStreamer`, native tool-use, not MCP - a new provider is one adapter; Application and Domain never change (PRD §4)
- (unverified date) - Brand "PersonaOS" fixed everywhere; only the nickname and persona are per-install (PRD §1)
- (unverified date) - Nothing writes without the user's confirmation, except memory with auto-save on - enforced per tool in code, not by the prompt (PRD §2)
- 2026-09-14 - FCM for push, bring-your-own Firebase (TODO.md, Phase 4)
- 2026-10-01 - Documents module removed (PRD §3.6)
- 2026-10-02 - Model stays qwen2.5:3b-instruct - prod is an RTX 3050 6 GB, a 7B doesn't fit with a 16k context (TODO.md)
- 2026-11-03 - Every local model is sent `think: false`; qwen2.5:3b kept over newer small HF models - it scored 99.2% against the others' 84-95% (docs/model-accuracy.md)
- 2026-11-03 - Goals with no period ask for one instead of defaulting (TODO.md, A9)
- 2026-11-07 - Train Qwen3-4B-Instruct-2507 - Apache-2.0, so the weights can ship; Qwen2.5-3B's licence is non-commercial (TODO.md, A8)
- 2026-11-08 - Prod stays on `personaos-qwen2.5:3b`; the 4B is the public-release model on Hugging Face - the 4B hung chat on the 6 GB prod card (TODO.md, A8)
- 2026-11-08 - Recommend 8 GB or more of GPU memory to users, not 6 GB (TODO.md, A14)
- 2026-11-08 - Rename and wordmark spelling wait for a final product name, near the public release (TODO.md)
- 2026-11-11 - Hardware for the 4B: 8 GB GPU and 16 GB RAM; 12 GB for 32k context with voice; 16k context minimum - measured 4.0 GB at 16k plus 2.2 GB for voice (TODO.md, A14)
- 2026-11-11 - Opus 5.5 orchestrates; Haiku/Sonnet/Opus subagents in `.claude/agents`, routed by tier - cheaper bounded work in separate contexts (CLAUDE.md)
- (unverified date) - Angular components keep `ChangeDetectionStrategy.Eager` from the Angular 22 upgrade - several assign fields after an `await`, which OnPush would not redraw; moving to OnPush is its own change (CLAUDE.md)
- (unverified date) - Web unit tests stay on Karma; the optional Vitest migration is not done (CLAUDE.md)
- 2026-11-11 - CLAUDE.md trimmed to rules, commands and orchestration; background moved to docs/architecture.md - keeps the auto-loaded context small (CLAUDE.md)
- 2026-11-11 - Ollama replies capped at 2,048 tokens (`num_predict` = `HistoryPlanner.MaxReplyReserve`) - a looping 4B streamed 6,692 tokens over 161 s; the planner already reserves only that much for a reply (TODO.md, A15)
- 2026-11-11 - External AI clients get an intake-only MCP façade: propose-only tools into a confirm-first Inbox, no read tools, write-only token, LAN/Tailscale only; our own AI stays native tool-use - frontier models research and file goals/tasks while personal data never leaves the box (TODO.md, A16)
- 2026-11-11 - All git history rewritten to the GitHub noreply email and force-pushed (backup mirror kept locally) - GitHub's email-privacy block stays on, so pushes with the private email are declined
- 2026-11-12 - Subagents get least privilege: only `worker` has `Write` (into `output/`), `log-reader` has no `Bash`; tests run in the orchestrator and are saved to `output/logs/` for `log-reader` - keeps secrets and side effects out of cheaper agents
- 2026-11-16 - Product renamed to Aimsby (plan in docs/rename-aimsby.md); data-bound ids (Data Protection purposes, SetApplicationName, compose project/volumes, personaos.db) keep the old name - "PersonaOS" is taken; Aimsby cleared USPTO, TMview, App Store, registries and domains, and renaming those ids would lose stored keys or data
- 2026-11-16 - Rename compatibility: old names stay readable rather than migrated (PERSONAOS_* env as compose fallbacks, JWT accepts both issuers, `ConnectionStrings:PersonaOS`, `personaos-` model names, `personaos.*` localStorage, `personaos:` links); releases also push the `personaos-*` images; Litestream replica path, keystore alias and the training cache volume keep their names - the updater never replaces an install's docker-compose.yml, so old installs pull the old image names forever, and a renamed path would start empty (docs/rename-aimsby.md)
- 2026-11-17 - Drop the old-name compatibility fallbacks (personaos-* images, PERSONAOS_* env, PersonaOS JWT issuer/audience, ConnectionStrings:PersonaOS, old phone package in google-services.json, old repo name in the phone updater, personaos.* web storage); keep the data-bound ids (Data Protection names, personaos.db, compose project and volumes, Litestream path, keystore alias, model cache volume, personaos: links, personaos- model prefix, the name in chat instructions) - no installs besides the owner's prod, which is redeployed fresh; renaming a data-bound id would orphan data

- 2026-11-17 - Rename the data-bound ids too: Data Protection application name and purposes (Aimsby.*), aimsby.db, compose project aimsby (aimsby_* volumes), Litestream path, keystore alias aimsby, training cache volume, and drop the personaos: link scheme and personaos- model prefix - the owner wants no old name in the new version; prod is restored from a backup into the new volume and the AI key, Firebase file and speech key are re-entered
