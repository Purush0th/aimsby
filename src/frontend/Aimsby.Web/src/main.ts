import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { migrateLegacyStorage } from './app/core/legacy-storage';

// Before anything reads storage: keys saved before the rename move to their new names.
migrateLegacyStorage();

bootstrapApplication(App, appConfig)
  .catch((err) => console.error(err));
