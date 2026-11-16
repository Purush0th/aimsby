import { migrateLegacyStorage } from './legacy-storage';

describe('migrateLegacyStorage', () => {
  beforeEach(() => localStorage.clear());
  afterEach(() => localStorage.clear());

  it('moves keys saved before the rename to their new names', () => {
    localStorage.setItem('personaos.token', 'abc');
    localStorage.setItem('personaos.theme', 'dark');

    migrateLegacyStorage();

    expect(localStorage.getItem('aimsby.token')).toBe('abc');
    expect(localStorage.getItem('aimsby.theme')).toBe('dark');
    expect(localStorage.getItem('personaos.token')).toBeNull();
    expect(localStorage.getItem('personaos.theme')).toBeNull();
  });

  it('keeps a value already saved under the new name', () => {
    localStorage.setItem('personaos.theme', 'dark');
    localStorage.setItem('aimsby.theme', 'light');

    migrateLegacyStorage();

    expect(localStorage.getItem('aimsby.theme')).toBe('light');
    expect(localStorage.getItem('personaos.theme')).toBeNull();
  });

  it('leaves other keys alone', () => {
    localStorage.setItem('planner.options', '{}');

    migrateLegacyStorage();

    expect(localStorage.getItem('planner.options')).toBe('{}');
  });
});
