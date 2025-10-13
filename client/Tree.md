src/
│
├─ app/
│ ├─ core/ # 👑 برای بخش‌های سراسری و تنظیمات اپ
│ │ ├─ guards/
│ │ ├─ interceptors/
│ │ ├─ services/
│ │ ├─ layouts/
│ │ ├─ config/
│ │ └─ core.module.ts
│ │
│ ├─ shared/ # 🧩 ماژول‌ها و اجزای قابل استفاده مجدد
│ │ ├─ components/
│ │ │ ├─ loading-spinner/
│ │ │ ├─ modal/
│ │ │ ├─ input-field/
│ │ │ └─ ...
│ │ ├─ directives/
│ │ ├─ pipes/
│ │ ├─ models/
│ │ └─ utils/
│ │
│ ├─ features/ # 🚀 ساختار اصلی برنامه بر پایه ویژگی‌ها
│ │ ├─ home/
│ │ │ ├─ components/
│ │ │ ├─ pages/
│ │ │ ├─ services/
│ │ │ └─ home.routes.ts
│ │ │
│ │ ├─ products/
│ │ │ ├─ components/
│ │ │ ├─ pages/
│ │ │ ├─ services/
│ │ │ ├─ models/
│ │ │ └─ products.routes.ts
│ │ │
│ │ ├─ cart/
│ │ │ ├─ components/
│ │ │ ├─ pages/
│ │ │ ├─ state/ # Signal یا Store (Pinia-like pattern)
│ │ │ └─ cart.routes.ts
│ │ │
│ │ ├─ checkout/
│ │ │ ├─ components/
│ │ │ ├─ pages/
│ │ │ └─ checkout.routes.ts
│ │ │
│ │ ├─ user/
│ │ │ ├─ components/
│ │ │ ├─ pages/
│ │ │ ├─ services/
│ │ │ └─ user.routes.ts
│ │
│ ├─ pages/ # 📄 صفحات عمومی مثل 404، login و ...
│ │ ├─ not-found/
│ │ ├─ login/
│ │ └─ register/
│ │
│ ├─ store/ # 🧠 مدیریت state سراسری (signal/store)
│ │ ├─ app.store.ts
│ │ ├─ theme.store.ts
│ │ └─ ...
│ │
│ ├─ app.routes.ts
│ └─ app.component.ts
│
├─ public/ # 🎨 تصاویر، آیکن‌ها، فایل‌های JSON
│
└─ styles/ # 💅 استایل‌های کلی (global.scss, mixins, variables)
