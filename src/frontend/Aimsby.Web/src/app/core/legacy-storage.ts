/** The prefix every stored key had before the rename to Aimsby. Legacy id, kept on purpose. */
const LEGACY_PREFIX = 'personaos.';
const PREFIX = 'aimsby.';

/**
 * Moves keys saved before the rename ("personaos.token", "personaos.theme", ...) to their new
 * names, once, so an upgrade keeps the user signed in and keeps their choices. A key already
 * saved under the new name wins; the old one is removed either way. Never throws: storage can be
 * blocked, and the app must start regardless.
 */
export function migrateLegacyStorage(storage: Storage = localStorage): void {
  try {
    const legacy: string[] = [];
    for (let i = 0; i < storage.length; i++) {
      const key = storage.key(i);
      if (key?.startsWith(LEGACY_PREFIX)) legacy.push(key);
    }
    for (const key of legacy) {
      const renamed = PREFIX + key.slice(LEGACY_PREFIX.length);
      const value = storage.getItem(key);
      if (value !== null && storage.getItem(renamed) === null) storage.setItem(renamed, value);
      storage.removeItem(key);
    }
  } catch {
    // Storage unavailable: nothing to move.
  }
}
