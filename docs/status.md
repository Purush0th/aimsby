# Status
_Last updated: 2026-11-17 - old-name fallbacks removed (uncommitted); prod to be redeployed fresh_

Snapshot for starting a session. The full task board, claims and history stay in
[TODO.md](../TODO.md); when the two disagree, TODO.md wins.

## In progress
- Nothing.

## Next
- Old-name fallbacks are removed in the working tree (not committed or released). The next server
  release pushes only `aimsby-*` images, reads only `AIMSBY_*` env and `ConnectionStrings:Aimsby`,
  and signs out any `PersonaOS`-issued token. Owner then redeploys prod fresh: the current
  `deploy/docker-compose.yml` and a `.env` using `AIMSBY_*` keys. Data-bound ids stay (`personaos.db`,
  compose project `personaos` and its volumes, Data Protection names, `personaos:` links, model prefix).
- Rename follow-up (owner): upload
  the new google-services.json (`com.aimsby.app`) in web Settings; install the Aimsby phone app
  (new package, so a reinstall, then remove the old PersonaOS app). Still open: India TM check,
  domain/handles, wordmark sign-off.
- A11. App bug first: the false-claim check does not cover a tool that returned an error
  ("GOAL-10 was created" after a refusal). See `Ai/ActionClaimDetector.cs` and `ClaimCheckGuard`
  in `Ai/Guards/ReplyGuards.cs`; add a failing test, fix, then `model-check.ps1 -Runs 5`. Then the training examples in TODO.md A11.
- Local servers on the OpenAI-compatible adapter (`/v1`) still get a 16,000-token reply cap; a cut
  reply shows no "stopped at the limit" note in any adapter. Neither is filed in TODO.md yet.
- A12. Web search via a self-hosted SearXNG in the compose stack.
- A16. Intake-only MCP facade: Claude researches, files goals/tasks into a confirm-first Inbox;
  no read tools. Design the proposal store first (cross-area). See `output/mcp-facade/`.
- A13. Learn from real use (per-turn quality record; design privacy and opt-in first).
- AI accuracy: the model asks for a task key after being told which tool to call.
- AI accuracy: sprint-level column claims are not checked.
- iOS through the web app first.

## Done (recent)
- 2026-11-17: Aimsby released as `v0.1.0-alpha.30` (server; also ships A15) and
  `mobile-v0.1.0-alpha.32` (phone, `com.aimsby.app`); both built green. This release still
  pushed `personaos-*` images too; later ones will not. All images pull anonymously (public). Old `PersonaOS.*` build folders deleted; dev data,
  `local.properties` and `node_modules` moved into the new folders; rename worktree removed.
- 2026-11-16: rename to Aimsby committed (`0b15c2c`) and pushed; repo renamed to
  `Purush0th/aimsby`. (The old-name fallbacks it added were dropped on 2026-11-17.)
- 2026-11-16: name search done; owner chose **Aimsby** (clear on USPTO, TMview, App Store,
  GitHub/npm/PyPI/Docker Hub, domains). Rename plan in `docs/rename-aimsby.md`; not started.
- 2026-11-12: B4 fixed. The phone dropped the server's error `code`, so adding or moving a task
  into the running sprint failed instead of asking "Scope change". Checked live on web and the
  emulator against a throwaway QA instance; released as `mobile-v0.1.0-alpha.31` (alpha.30 failed in CI: stable Flutter 3.50 needs Gradle 9.3.1, so the workflow now pins Flutter 3.47.3).
- 2026-11-12: `/agent-router` refresh done: agent files, secret-file `permissions.deny` rules in
  `.claude/settings.local.json` (`.env`, `dp-keys/`, key files), CLAUDE.md marker section,
  `output/logs/`; validation passed. CLAUDE.md change uncommitted.
- 2026-11-11: all git history (171 commits, 43 tags, `dev` and `main`) rewritten to the GitHub
  noreply email and force-pushed; release workflows were off during the push, no releases rebuilt.
  Pre-rewrite mirror at `D:\Projects\personaOS-history-backup-20261111.git`. Other clones must be
  re-cloned or hard-reset to origin.
- 2026-11-11: A16 assessed: MCP facade for external AI clients, propose-only tools into a
  confirm-first Inbox, no read tools. Filed in TODO.md; decision logged. Not started.
- 2026-11-11: A15 done. Ollama replies capped at 2,048 tokens (`num_predict`); model-check 225/225
  before and after on the 4B. Pushed to dev, not released. 4B card with the A14 numbers
  published on Hugging Face.
- 2026-11-11: A14 done. README "Hardware for the recommended model": 8 GB GPU, 16 GB RAM, 16k
  context minimum. 4B + voice peaks at 6.1 GB, which explains prod. Report in `output/a14/`.
- 2026-11-08: A8 done (trained Qwen3 4B, 465/465 on every suite, public on Hugging Face);
  A10 done; server and mobile alpha.29 released; phone checks passed on the owner's phone.
- 2026-11-04/07: logo released (alpha.25 / mobile alpha.27), D1 bottom bar; planner/reminder guards.

## Known issues / blockers
- Public release: blocked by the owner until features mature and hallucination is under control.
  Do not suggest cutting it.
- On prod (6 GB card beside Whisper and Kokoro) the 4B left chat loading for minutes. A14: it
  needs 6.1 GB there, more than the card has; runaway replies added to it (capped by A15, released
  in alpha.30). Prod stays on the 3B.
- Rename to Aimsby: India TM and the Play Store search are still the owner's to check.
- Live Anthropic smoke test needs a real API key (kept for the public release).
- Docker Desktop must be running for the speech profile on the dev PC.
- `output/` is gitignored: the A14 benchmark script (`output/a14/bench.mjs`) and the A16
  assessment (`output/mcp-facade/assessment.md`) exist only on this machine.
