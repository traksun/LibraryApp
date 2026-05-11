# 📚 Онлайн Библиотека – ASP.NET Core + Entity Framework Core

ООП проект с релационна база данни. Пълен CRUD за книги, автори и категории.

## Изисквания
- Visual Studio 2022 (или VS Code)
- .NET 8 SDK
- SQL Server LocalDB (идва с Visual Studio)

## Стартиране

### 1. Отвори проекта
```
Отвори LibraryApp.csproj с Visual Studio
```

### 2. Инсталирай пакетите
```
Tools → NuGet Package Manager → Package Manager Console
```
Пакетите се инсталират автоматично при build.

### 3. Създай базата данни (миграция)
```powershell
# В Package Manager Console:
Add-Migration InitialCreate
Update-Database
```

### 4. Стартирай проекта
```
Натисни F5 или зеления бутон ▶
```

Браузърът ще отвори `https://localhost:xxxx`

---

## Структура на проекта

```
LibraryApp/
├── Models/
│   ├── Author.cs          ← Модел за автор
│   ├── Book.cs            ← Модел за книга  
│   ├── Category.cs        ← Модел за категория
│   └── BookCategory.cs    ← Many-to-many (книга ↔ категория)
├── Data/
│   └── LibraryContext.cs  ← DbContext + seed данни
├── Controllers/
│   ├── HomeController.cs
│   ├── BooksController.cs     ← CRUD
│   ├── AuthorsController.cs   ← CRUD
│   └── CategoriesController.cs← CRUD
├── Views/
│   ├── Home/Index.cshtml
│   ├── Books/   (Index, Details, Create, Edit, Delete)
│   ├── Authors/ (Index, Details, Create, Edit, Delete)
│   └── Categories/ (Index, Create, Edit, Delete)
└── wwwroot/css/site.css
```

## Връзки между моделите

```
Author ──< Book >──< BookCategory >── Category
 (1)      (много)    (many-to-many)
```

- **One-to-many**: 1 автор → много книги
- **Many-to-many**: Книга ↔ Категория (чрез BookCategory)

## Seed данни (автоматично)
- 3 автора: Иван Вазов, Алеко Константинов, Христо Ботев
- 4 категории: Роман, Поезия, Разказ, Пътепис
- 4 книги: Под игото, Чичовци, До Чикаго и назад, Стихотворения
