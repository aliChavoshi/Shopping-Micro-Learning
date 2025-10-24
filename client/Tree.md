src/
│
├─ app/
│ │
│ ├─ core/                            # 👑 تنظیمات و اجزای مرکزی اپ
│ │ ├─ guards/                        # → AuthGuard, RoleGuard, UnsavedChangesGuard
│ │ ├─ interceptors/                  # → AuthInterceptor, ErrorInterceptor, LoaderInterceptor
│ │ ├─ services/                      # → ApiService, AuthService, LoggerService, ConfigService
│ │ ├─ layouts/                       # → MainLayout, AuthLayout, AdminLayout
│ │ ├─ config/                        # → app.config.ts, environment.config.ts, routes.config.ts
│ │ ├─ tokens/                        # → InjectionTokens برای DI
│ │ ├─ models/                        # → مدل‌های عمومی سطح اپ (AppConfig, ApiResponse, UserSession)
│ │ ├─ utils/                         # → توابع کمکی عمومی مثل date.helper.ts, storage.helper.ts
│ │ ├─ interceptors/
│ │ ├─ animations/                    # → انیمیشن‌های مشترک
│ │ ├─ core.module.ts
│ │ └─ index.ts
│ │
│ ├─ shared/                          # 🧩 اجزای قابل استفاده مجدد بین تمام featureها
│ │ ├─ components/                    # → loading-spinner, modal, pagination, input-field, toast, ...
│ │ │ ├─ loading-spinner/
│ │ │ ├─ modal/
│ │ │ ├─ input-field/
│ │ │ ├─ pagination/
│ │ │ ├─ search-box/
│ │ │ ├─ breadcrumb/
│ │ │ ├─ table/
│ │ │ └─ ...
│ │ ├─ directives/                    # → autofocus, debounceClick, hasPermission
│ │ ├─ pipes/                         # → date-format, price, truncate, translate
│ │ ├─ validators/                    # → custom async/sync validators
│ │ ├─ models/                        # → Entity base models, ApiResponse, PaginationRequest
│ │ ├─ utils/                         # → helpers مثل form.utils.ts, file.utils.ts
│ │ ├─ material/                      # → Angular Material module bundler
│ │ ├─ icons/                         # → icon registry یا icon mapping
│ │ ├─ shared.module.ts
│ │ └─ index.ts
│ │
│ ├─ features/                        # 🚀 هر feature ماژول مستقل
│ │ ├─ home/
│ │ │ ├─ components/
│ │ │ ├─ pages/
│ │ │ ├─ services/
│ │ │ ├─ models/
│ │ │ ├─ state/
│ │ │ ├─ home.routes.ts
│ │ │ └─ home.module.ts
│ │ │
│ │ ├─ products/
│ │ │ ├─ components/                  # → product-card, product-filter, product-list
│ │ │ ├─ services/                    # → products.service.ts, categories.service.ts
│ │ │ ├─ models/                      # → product.model.ts, category.model.ts
│ │ │ ├─ state/                       # → signals or store for products
│ │ │ ├─ resolvers/                   # → pre-fetch product data
│ │ │ ├─ guards/                      # → product-access.guard.ts
│ │ │ ├─ products.routes.ts
│ │ │ └─ products.module.ts
│ │ │
│ │ ├─ cart/
│ │ │ ├─ components/                  # → cart-item, cart-summary
│ │ │ ├─ state/                       # → cart.store.ts or signal-based store
│ │ │ ├─ services/                    # → cart.service.ts
│ │ │ ├─ models/                      # → cart-item.model.ts
│ │ │ ├─ cart.routes.ts
│ │ │ └─ cart.module.ts
│ │ │
│ │ ├─ checkout/
│ │ │ ├─ components/                  # → checkout-form, order-summary, payment-method
│ │ │ ├─ pages/                       # → checkout-page, order-success
│ │ │ ├─ services/
│ │ │ ├─ models/
│ │ │ ├─ resolvers/
│ │ │ ├─ checkout.routes.ts
│ │ │ └─ checkout.module.ts
│ │ │
│ │ ├─ user/
│ │ │ ├─ components/                  # → profile-form, avatar-upload
│ │ │ ├─ pages/                       # → profile-page, account-settings
│ │ │ ├─ services/                    # → user.service.ts, auth.service.ts
│ │ │ ├─ models/                      # → user.model.ts, auth-response.model.ts
│ │ │ ├─ guards/                      # → auth.guard.ts
│ │ │ ├─ user.routes.ts
│ │ │ └─ user.module.ts
│ │ │
│ │ ├─ admin/
│ │ │ ├─ components/
│ │ │ ├─ pages/
│ │ │ ├─ services/
│ │ │ ├─ models/
│ │ │ ├─ state/
│ │ │ ├─ admin.routes.ts
│ │ │ └─ admin.module.ts
│ │
│ ├─ pages/                           # 📄 صفحات عمومی
│ │ ├─ not-found/
│ │ │ ├─ not-found.component.ts
│ │ │ ├─ not-found.component.html
│ │ │ └─ not-found.component.scss
│ │ ├─ login/
│ │ ├─ register/
│ │ ├─ maintenance/                   # → صفحه “در حال به‌روزرسانی”
│ │ └─ error/                         # → صفحه خطای عمومی
│ │
│ ├─ store/                           # 🧠 State سراسری (signal/store)
│ │ ├─ app.store.ts
│ │ ├─ theme.store.ts
│ │ ├─ auth.store.ts
│ │ ├─ notification.store.ts
│ │ ├─ index.ts
│ │ └─ store.module.ts
│ │
│ ├─ animations/                      # → تعریف انیمیشن‌های سراسری Angular
│ ├─ app.routes.ts
│ ├─ app.config.ts
│ ├─ app.component.ts
│ ├─ app.component.html
│ ├─ app.component.scss
│ └─ app.module.ts
│
├─ assets/                            # 🎨 فایل‌های استاتیک (به جای public)
│ ├─ images/
│ ├─ icons/
│ ├─ json/
│ ├─ mock-data/
│ ├─ fonts/
│ └─ i18n/                            # → فایل‌های ترجمه (en.json, fa.json)
│
├─ environments/                      # ⚙️ تنظیمات محیط
│ ├─ environment.ts
│ ├─ environment.prod.ts
│ ├─ environment.staging.ts
│ └─ environment.local.ts
│
├─ styles/                            # 💅 استایل‌های کلی و متغیرها
│ ├─ abstracts/
│ │ ├─ _variables.scss
│ │ ├─ _mixins.scss
│ │ ├─ _functions.scss
│ │ └─ _colors.scss
│ ├─ base/
│ │ ├─ _reset.scss
│ │ ├─ _typography.scss
│ │ ├─ _utilities.scss
│ │ └─ _animations.scss
│ ├─ components/
│ │ ├─ _buttons.scss
│ │ ├─ _cards.scss
│ │ ├─ _inputs.scss
│ │ └─ _tables.scss
│ ├─ themes/
│ │ ├─ _light-theme.scss
│ │ ├─ _dark-theme.scss
│ │ └─ _high-contrast.scss
│ ├─ global.scss
│ └─ index.scss
│
├─ tests/                             # 🧪 تست واحد و یکپارچه
│ ├─ unit/
│ │ ├─ core/
│ │ ├─ shared/
│ │ └─ features/
│ └─ e2e/
│     ├─ specs/
│     ├─ fixtures/
│     └─ support/
│
└─ main.ts                            # نقطه شروع اپ
