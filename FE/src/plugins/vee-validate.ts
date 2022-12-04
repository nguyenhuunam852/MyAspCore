import { configure } from 'vee-validate';
import { localize, setLocale } from '@vee-validate/i18n';
import en from '@vee-validate/i18n/dist/locale/en.json';
import vi from '@vee-validate/i18n/dist/locale/vi.json';

configure({
  generateMessage: localize({ en, vi }),
});

setLocale('en')